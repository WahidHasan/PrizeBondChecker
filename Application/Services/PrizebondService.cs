using Application.Models.CheckBond;
using Application.Models.DownloadTemplate;
using Application.Models.Draw;
using Application.Models.PrizebondView;
using Application.Shared.Helpers;
using Application.Shared.Models;
using AutoMapper;
using Domain.Entity.Draws;
using Domain.Enums;
using Domain.Prizebond;
using Infrastructure.Repository.Base;
using IronOcr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using MongoDB.Driver;
using OfficeOpenXml;
using PrizeBondChecker.Domain;
using PrizeBondChecker.Domain.Constants;
using PrizeBondChecker.Domain.Prizebond;
using System.ComponentModel;
using System.Net;

namespace PrizeBondChecker.Services
{
    public class PrizebondService : IPrizebondService
    {
        private readonly IRepository<Prizebond> _prizebondRepository;
        private readonly IRepository<UserPrizebonds> _userPrizebondsRepository;
        private readonly IRepository<Users> _usersRepository;
        private readonly IRepository<Draws> _drawsRepository;
        private readonly IMapper _mapper;

        public PrizebondService(IRepository<Prizebond> prizebondRepository, IRepository<Users> usersRepository, IRepository<UserPrizebonds> userPrizebondsRepository, IMapper mapper, IRepository<Draws> drawsRepository)
        {
            _prizebondRepository = prizebondRepository;
            _usersRepository = usersRepository;
            _userPrizebondsRepository = userPrizebondsRepository;
            _mapper = mapper;
            _drawsRepository = drawsRepository;
        }

        public async Task<PrizebondCreateModel> AddBondToList(PrizebondCreateModel prizebond)
        {
            if (prizebond?.UserId != null)
            {
                var userEntity = await _usersRepository.FindByIdAsync(prizebond.UserId);
                if (userEntity == null)
                    throw new Exception(ApplicationMessages.UserNotFound);

                //var mappedData = _mapper.Map<List<Prizebond>>(prizebond.Prizebonds);
                //await _prizebondRepository.InsertManyAsync(mappedData);

                var userPrizebonds = prizebond.Prizebonds.Select(bond => new UserPrizebonds
                {
                    UserId = prizebond.UserId,
                    BondId = bond.BondId!,
                    Serial = bond.Serial!,
                    BondIdInBengali = bond?.BondIdInBengali,
                    Notes = bond?.Notes,
                }).ToList();

                await _userPrizebondsRepository.InsertManyAsync(userPrizebonds);
                return prizebond;

            }
            return default;

        }

        public async Task<List<Prizebond>> GetAllAsync()
        {
            return await _prizebondRepository.GetAllAsync();
        }

        public async Task<CommonApiResponses> GetByUserIdAsync(PrizebondRequestQuery request)
        {
            var searchText = request.SearchText != null ? request.SearchText.ToLower().Trim() : string.Empty;
            var userPrizebondList = await _userPrizebondsRepository.FilterByAsync(x => x.UserId == request.UserId && x.BondId.Contains(searchText));

            var pagedData = userPrizebondList.Skip((request.PageNo - 1) * request.PageSize).Take(request.PageSize).ToList();
            var pagedMappedData = _mapper.Map<List<PrizebondListViewModel>>(pagedData);
            var data = new Pager<PrizebondListViewModel>() { Items = pagedMappedData, Count = userPrizebondList.Count, PageNo = request.PageNo, PageSize = request.PageSize };

            return new CommonApiResponses(true, (int)HttpStatusCode.OK, ApplicationMessages.HttpStatusCodeDescriptionOk, ApplicationMessages.DataRetriveSuccessfull, data);
        }

        public async Task<CommonApiResponses> Delete(PrizebondDeleteModel prizebond)
        {
            var userEntity = await _usersRepository.FindByIdAsync(prizebond.UserId);
            if (userEntity == null)
                throw new Exception(ApplicationMessages.UserNotFound);

            var selectedPrizebonds = new List<Prizebond>();
            foreach (var pId in prizebond.BondIds)
            {
                //var bond = await _prizebondRepository.FindOneAsync(x => x.bondId == pId);
                var userPrizebondEntity = await _userPrizebondsRepository.FindOneAsync(x => x.Id == pId);
                if (userPrizebondEntity != null)
                    await _userPrizebondsRepository.DeleteOneAsync(userPrizebondEntity);
                //selectedPrizebonds.Add(bond);
            }
            //await _prizebondRepository.DeleteManyAsync(selectedPrizebonds);
            return new CommonApiResponses()
            {
                IsSuccess = true,
                StatusCode = (int)HttpStatusCode.Accepted,
                StatusDetails = ApplicationMessages.HttpStatusCodeDescriptionAccepted,
                Message = ApplicationMessages.DataDeletedSuccessfull,
                Data = null
            };
        }

        public async Task<MemoryStream> DownloadPrizebondTemplate()
        {
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (var xlPackage = new ExcelPackage(stream))
            {
                #region For Upload Template
                var pbworksheet = xlPackage.Workbook.Worksheets.Add("Prizebonds");
                pbworksheet.Columns.Style.Numberformat.Format = "@";
                int row = 1, column = 1;

                var properties = TypeDescriptor.GetProperties(typeof(DownloadTemplateViewModel));
                foreach (PropertyDescriptor property in properties)
                {
                    pbworksheet.Cells[row, column].Value = property.DisplayName;
                    pbworksheet.Cells[row, column].Style.Font.Bold = true;

                    pbworksheet.Column(column).Width = 19;
                    column++;
                }

                xlPackage.Workbook.Properties.Title = "Prizebonds";

                #endregion

                xlPackage.Save();
                stream.Position = 0;
            }

            return stream;
        }

        public async Task<MemoryStream> DownloadDrawExcelFile(DownloadDrawExcelCommand request)
        {

            var data = await GenerateTextFromImage(request.Image);
            var outputList = data.Split(',').ToList();


            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (var xlPackage = new ExcelPackage(stream))
            {
                #region For Download Template
                var pbworksheet = xlPackage.Workbook.Worksheets.Add("DrawInfo");
                pbworksheet.Columns.Style.Numberformat.Format = "@";
                int row = 1, column = 1;

                var properties = TypeDescriptor.GetProperties(typeof(PrizeTypeViewModel));
                foreach (PropertyDescriptor property in properties)
                {
                    pbworksheet.Cells[row, column].Value = property.DisplayName;
                    pbworksheet.Cells[row, column].Style.Font.Bold = true;

                    pbworksheet.Column(column).Width = 19;
                    column++;
                }

                row = 2;
                var fifthPrize = 5;
                foreach (var bondId in outputList)
                {
                    pbworksheet.Cells[row, fifthPrize].Value = bondId;
                    row++;
                }

                xlPackage.Workbook.Properties.Title = "Prizebonds";

                #endregion

                xlPackage.Save();
                stream.Position = 0;
            }

            return stream;
        }

        public async Task<CommonApiResponses> AddNewDraw(AddNewDrawCommand request)
        {
            var month = request.DrawDate.Month;
            var year = request.DrawDate.Year;
            var existData = await _drawsRepository.FindOneAsync(x => x.DrawNumber == request.DrawNumber || (x.Month == month && x.Year == year));
            if (existData?.DrawNumber == request.DrawNumber)
                throw new Exception(ApplicationMessages.DrawNumberExist);
            else if(existData != null)
                throw new Exception(ApplicationMessages.DrawMonthYearExist);


            var selectedBondsList = new List<SelectedBond>();
            var stream = request.File.OpenReadStream();
            var memoryStream = new MemoryStream();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets.First();
                var rowCount = worksheet.Dimension.Rows;
                var columnCount = worksheet.Dimension.Columns;
                var totalIterCount = 0;
                for (var col = 1; col <= 5; col++)
                {
                    for (var row = 2; row <= rowCount; row++)
                    {
                        var value = CommonHelper.ToTrimmedString(worksheet.Cells[row, col].Value);
                        if (string.IsNullOrWhiteSpace(value))
                        {
                            break;
                        }
                        var bondObj = GenerateSelectedBondObject(value, col);
                        selectedBondsList.Add(bondObj);
                        totalIterCount++;
                        if ((col <= 2 && row == 2) || (col == 3 || col == 4) && row <= 3)
                        {
                            if (row == 3 || col <= 2)
                                break;
                        }
                    }
                }
            }
            var data = new Draws()
            {
                DrawDate = request.DrawDate,
                Month = month,
                Year= year,
                DrawNumber= request.DrawNumber,
                SelectedBonds= selectedBondsList,
            };
            await _drawsRepository.InsertOneAsync(data);

            return new CommonApiResponses(true, (int)HttpStatusCode.OK, ApplicationMessages.HttpStatusCodeDescriptionOk, ApplicationMessages.DataAddedSuccessfull, data);
        }

        private SelectedBond GenerateSelectedBondObject(string cellValue, int column)
        {
            var col = column.ToString();
            var bond = new SelectedBond()
            {
                BondId = cellValue,
                PrizeCategory = (PrizeCategory)Enum.Parse(typeof(PrizeCategory), col),
            };
            return bond;
        }

        private async Task<string> GenerateTextFromImage(IFormFile image)
        {
            var Ocr = new IronTesseract();
            Ocr.Language = OcrLanguage.Bengali;
            OcrResult ocrResult;
            var bytes = await GetBytes(image);
            using (var ocrInput = new OcrInput(bytes))
            {
                ocrResult = Ocr.Read(ocrInput);
                Console.WriteLine(ocrResult.Text);
            }
            var tempString = ocrResult.Text;

            tempString = tempString.Replace("\r\n", "").Replace(" ", "");

            tempString = string.Join(",", Enumerable.Range(0, (int)Math.Ceiling((double)tempString.Length / 7))
            .Select(i => tempString.Substring(i * 7, Math.Min(7, tempString.Length - i * 7)))
            .ToArray());
            return tempString;
        }

        public async Task<byte[]> GetBytes(IFormFile formFile)
        {
            await using var memoryStream = new MemoryStream();
            await formFile.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }

        public async Task<CommonApiResponses> CheckUserBondsWithDraw(CheckUserBondsListCommand request)
        {
            var drawInfo = await _drawsRepository.FindOneAsync(x => x.DrawNumber == request.DrawNumber);
            if (drawInfo == null)
                throw new Exception(ApplicationMessages.DrawNumberNotFound);
            var drawBondIds = drawInfo.SelectedBonds.Select(bn => bn.BondId);
            var userBondIds = _userPrizebondsRepository.Collection.AsQueryable()
                .Where(x => x.UserId == request.UserId && drawBondIds.Contains(x.BondIdInBengali))
                .Select(y => new MatchedBondViewModel
                {
                    BondId = y.BondId,
                    BondIdInBengali = y.BondIdInBengali,
                    Serial = y.Serial,
                    Notes = y.Notes
                }).ToList();

            foreach(var bond in userBondIds)
            {
                bond.PrizeCategory = drawInfo.SelectedBonds.Find(p => p.BondId == bond.BondIdInBengali).PrizeCategory!;
            }
                        
            return new CommonApiResponses(true, (int)HttpStatusCode.OK, ApplicationMessages.HttpStatusCodeDescriptionOk, ApplicationMessages.DataAddedSuccessfull, userBondIds);
        }
    }
}


//  Need to replace above foreach by below chunk of code

//var result = await _userPrizebondsRepository.Collection.Aggregate()
//    .Lookup(
//        foreignCollection: _drawsRepository.Collection,
//        localField: u => u.BondIdInBengali,
//        foreignField: d => d.SelectedBonds.Select(b => b.BondId),
//        @as: (MatchedBondViewModel u) => u.SelectedBonds
//    )
//    .Unwind(u => u.SelectedBonds)
//    .Project(u => new MatchedBondViewModel
//    {
//        BondId = u.BondId,
//        BondIdInBengali = u.BondIdInBengali,
//        Serial = u.Serial,
//        Notes = u.Notes,
//        PrizeCategory = u.SelectedBonds.PrizeCategory
//    })
//    .ToListAsync();