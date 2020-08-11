using Core.Contracts;
using Core.Enums;
using Core.Models;
using Core.ViewModels;
using System;
using System.Collections.Generic;
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

        public async Task<IOperationResult<List<ApplicationUserViewModel>>> GetUsers()
        {
            try
            {
                List<ApplicationUserViewModel> users = await BuildUsersList();

                return BasicOperationResult<List<ApplicationUserViewModel>>.Ok(users);
            }
            catch (Exception ex)
            {
                return BasicOperationResult<List<ApplicationUserViewModel>>.Fail("Ocurrió un error obteniendo la lista de usuarios.", ex.ToString());
            }
        }

        public async Task<IOperationResult<ApplicationUserViewModel>> GetUser(string userId)
        {
            try
            {
                ApplicationUserViewModel users = await BuildUser(userId);

                return BasicOperationResult<ApplicationUserViewModel>.Ok(users);
            }
            catch (Exception ex)
            {
                return BasicOperationResult<ApplicationUserViewModel>.Fail("Ocurrió un error obteniendoel usuario.", ex.ToString());
            }
        }

        private async Task<ApplicationUserViewModel> BuildUser(string userId)
        {
            ApplicationUser dbUser = await _applicationUserRepository.FindAsync(user => user.UserId == userId,
                user => user.Phone,
                user => user.Role, x => x.Address,
                user => user.Address.Municipality);

            return new ApplicationUserViewModel
            {
                UserId = dbUser.UserId,
                Name = dbUser.Name,
                Lastname = dbUser.Lastname,
                IdentificationNumber = dbUser.IdentificationNumber,
                Email = dbUser.Email,
                PhoneNumber = dbUser.Phone.PhoneNumber,
                Address = dbUser.Address.Address,
                Sector = dbUser.Address.Sector,
                Municipality = dbUser.Address.Municipality,
            };
        }

        private async Task<List<ApplicationUserViewModel>> BuildUsersList()
        {
            List<ApplicationUserViewModel> buildedUsersList = new List<ApplicationUserViewModel>();
            IEnumerable<ApplicationUser> dbUsers = await _applicationUserRepository.FindAllAsync(user => !string.IsNullOrEmpty(user.UserId),
                user => user.Phone,
                user => user.Role, x => x.Address,
                user => user.Address.Municipality);

            foreach (ApplicationUser user in dbUsers)
            {
                ApplicationUserViewModel userToAdd = BuildModelIntoViewModel(user);
                buildedUsersList.Add(userToAdd);
            }

            return buildedUsersList;
        }

        private ApplicationUserViewModel BuildModelIntoViewModel(ApplicationUser user)
        {
            ApplicationUserViewModel viewModel = new ApplicationUserViewModel
            {
                UserId = user.UserId,
                Name = user.Name,
                Lastname = user.Lastname,
                IdentificationNumber = user.IdentificationNumber,
                PhoneNumber = user.Phone.PhoneNumber,
                Email = user.Email,
                Address = user.Address.Address,
                Sector = user.Address.Sector,
                Municipality = user.Address.Municipality,
            };

            return viewModel;
        }

        public async Task<IOperationResult<string>> UpdateUser(ApplicationUserCreateViewModel user)
        {
            try
            {
                UserAddress address = await BuildUserAddress(user);
                UserPhone phone = BuildUserPhone(user);
                UserRole role = await _userRoleRepository.FindAsync(x => x.UserRoleId == (int)RoleTypes.Client);

                ApplicationUser userFound = await _applicationUserRepository.FindAsync(dbUser => dbUser.UserId == user.UserId);

                userFound.UserId = user.UserId;
                userFound.Name = user.Name;
                userFound.Lastname = user.Lastname;
                userFound.Email = user.Email;
                userFound.Address = address;
                userFound.Phone = phone;
                userFound.Role = role;

                await _applicationUserRepository.SaveAsync();

                return BasicOperationResult<string>.Ok("Usuario actualizado correctamente.");
            }
            catch (Exception ex)
            {
                return BasicOperationResult<string>.Fail("Ocurrió un error actualizando al usuario.", ex.ToString());
            }
        }

        public async Task<IOperationResult<string>> DeleteUser(string userId)
        {
            try
            {
                ApplicationUser userToDelete = await _applicationUserRepository.FindAsync(user => user.UserId == userId);

                _applicationUserRepository.Remove(userToDelete);

                await _applicationUserRepository.SaveAsync();

                return BasicOperationResult<string>.Ok("Usuario eliminado correctamente.");
            }
            catch (Exception ex)
            {
                return BasicOperationResult<string>.Fail("Ocurrió un error eliminando el usuario.", ex.ToString());
            }
        }

    }
}
