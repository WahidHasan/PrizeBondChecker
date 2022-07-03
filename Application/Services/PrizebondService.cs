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

                var mappedData = _mapper.Map<List<Prizebond>>(prizebond.Prizebonds);
                await _prizebondRepository.InsertManyAsync(mappedData);

                var userPrizebonds = new List<UserPrizebonds>();
                foreach (var bond in mappedData)
                {
                    userPrizebonds.Add(new UserPrizebonds
                    {
                        PrizebondId = bond.Id,
                        UserId = prizebond.UserId
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

        public async Task<CommonApiResponses> Delete(PrizebondDeleteModel prizebond)
        {
            var userEntity = await _usersRepository.FindByIdAsync(prizebond.UserId);
            if (userEntity == null)
                throw new Exception(ApplicationMessages.UserNotFound);

            var selectedPrizebonds = new List<Prizebond>();
            foreach (var pId in prizebond.BondIds)
            {
                var bond = await _prizebondRepository.FindOneAsync(x => x.bondId == pId);
                var userPrizebondEntity = await _userPrizebondsRepository.FindOneAsync(x => x.PrizebondId == bond.Id);
                await _userPrizebondsRepository.DeleteOneAsync(userPrizebondEntity);
                selectedPrizebonds.Add(bond);
            }
            await _prizebondRepository.DeleteManyAsync(selectedPrizebonds);
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
