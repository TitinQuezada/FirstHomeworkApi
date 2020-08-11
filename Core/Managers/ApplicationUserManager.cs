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

        public IOperationResult<List<ApplicationUserCreateViewModel>> GetUsers()
        {
            try
            {
                List<ApplicationUserCreateViewModel> users = BuildUsersList();

                return BasicOperationResult<List<ApplicationUserCreateViewModel>>.Ok(users);
            }
            catch (Exception ex)
            {
                return BasicOperationResult<List<ApplicationUserCreateViewModel>>.Fail("Ocurrió un error obteniendo la lista de usuarios.", ex.ToString());
            }
        }

        private List<ApplicationUserCreateViewModel> BuildUsersList()
        {
            List<ApplicationUserCreateViewModel> buildedUsersList = new List<ApplicationUserCreateViewModel>();
            IEnumerable<ApplicationUser> dbUsers = _applicationUserRepository.GetUsers();

            foreach (ApplicationUser user in dbUsers)
            {
                ApplicationUserCreateViewModel userToAdd = BuildModelIntoViewModel(user);
                buildedUsersList.Add(userToAdd);
            }

            return buildedUsersList;
        }

        private ApplicationUserCreateViewModel BuildModelIntoViewModel(ApplicationUser user)
        {
            ApplicationUserCreateViewModel viewModel = new ApplicationUserCreateViewModel();
            viewModel.UserId = user.UserId;
            viewModel.Name = user.Name;
            viewModel.Lastname = user.Lastname;
            viewModel.IdentificationNumber = user.IdentificationNumber;
            viewModel.PhoneNumber = user.Phone.PhoneNumber;
            viewModel.Email = user.Email;
            viewModel.Address = user.Address.Address;
            viewModel.Sector = user.Address.Sector;
            //viewModel.Municipality = $"{user.Address.Municipality.MunicipalityId} {user.Address.Municipality.Description}";

            return viewModel;
        }

        public async Task<IOperationResult<string>> UpdateUser(ApplicationUserCreateViewModel user)
        {
            try
            {
                UserAddress address = await BuildUserAddress(user);
                UserPhone phone = BuildUserPhone(user);
                UserRole role = await _userRoleRepository.FindAsync(x => x.UserRoleId == (int)RoleTypes.Client);

                ApplicationUser userFound = await _applicationUserRepository.FindAsync(dbUser => dbUser.Email == user.Email);

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
