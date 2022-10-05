using Application.Models.PrizebondView;
using Application.Shared.Models;
using AutoMapper;
using Domain.Prizebond;
using Infrastructure.Repository.Base;
using PrizeBondChecker.Domain;
using PrizeBondChecker.Domain.Constants;
using PrizeBondChecker.Domain.Prizebond;
using System.Net;

namespace PrizeBondChecker.Services
{
    public class PrizebondService : IPrizebondService
    {
        private readonly IRepository<Prizebond> _prizebondRepository;
        private readonly IRepository<UserPrizebonds> _userPrizebondsRepository;
        private readonly IRepository<Users> _usersRepository;
        private readonly IMapper _mapper;

        public PrizebondService(IRepository<Prizebond> prizebondRepository, IRepository<Users> usersRepository, IRepository<UserPrizebonds> userPrizebondsRepository, IMapper mapper)
        {
            _prizebondRepository = prizebondRepository;
            _usersRepository = usersRepository;
            _userPrizebondsRepository = userPrizebondsRepository;
            _mapper = mapper;
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

                var userPrizebonds = new List<UserPrizebonds>();
                foreach (var bond in prizebond.Prizebonds)
                {
                    userPrizebonds.Add(new UserPrizebonds
                    {
                        //PrizebondId = bond.Id,
                        UserId = prizebond.UserId,
                        BondId = bond.BondId,
                        Serial = bond.Serial,
                        BondIdInBengali = bond?.BondIdInBengali
                    });

                }

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
            var userPrizebondList = await _userPrizebondsRepository.FilterByAsync(x=> x.UserId == request.UserId && x.BondId.Contains(searchText));

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
    }
}
