using Core.Contracts;
using Core.Enums;
using Core.Models;
using Core.ViewModels;
using System;
using System.Threading.Tasks;

namespace Core.Managers
{
    public sealed class ApplicationUserManager
    {
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly IMunicipalityRepository _municipalityRepository;
        private readonly IUserRoleRepository _userRoleRepository;


        public ApplicationUserManager(IApplicationUserRepository applicationUserRepository,
            IMunicipalityRepository municipalityRepository, IUserRoleRepository userRoleRepository)
        {
            _applicationUserRepository = applicationUserRepository;
            _municipalityRepository = municipalityRepository;
            _userRoleRepository = userRoleRepository;
        }

        public async Task<IOperationResult<bool>> Create(ApplicationUserCreateViewModel user)
        {
            try
            {
                await CreateUser(user);
                return BasicOperationResult<bool>.Ok();
            }
            catch (Exception ex)
            {
                return BasicOperationResult<bool>.Fail("Ha ocurrido un error al crear el usuario", ex.ToString());
            }
        }

        private async Task<UserAddress> BuildUserAddress(ApplicationUserCreateViewModel user)
        {
            Municipality municipality =
                await _municipalityRepository.FindAsync(municipality => municipality.MunicipalityId == user.MunicipalityId);

            return new UserAddress
            {
                Address = user.Address,
                Sector = user.Sector,
                Municipality = municipality
            };
        }

        private UserPhone BuildUserPhone(ApplicationUserCreateViewModel user)
        {
            return new UserPhone { PhoneNumber = user.PhoneNumber };
        }

        private async Task CreateUser(ApplicationUserCreateViewModel user)
        {
            UserAddress address = await BuildUserAddress(user);
            UserPhone phone = BuildUserPhone(user);
            UserRole role = await _userRoleRepository.FindAsync(x => x.UserRoleId == (int)RoleTypes.Client);

            ApplicationUser userResult = new ApplicationUser
            {
                UserId = user.UserId,
                Name = user.Name,
                Lastname = user.Lastname,
                Email = user.Email,
                IdentificationNumber = user.IdentificationNumber,
                Address = address,
                Phone = phone,
                Role = role
            };

            _applicationUserRepository.Create(userResult);
            await _applicationUserRepository.SaveAsync();
        }
    }
}
