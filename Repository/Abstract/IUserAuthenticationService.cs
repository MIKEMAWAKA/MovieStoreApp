using System;
using MovieStoreApp.Models.DTO;

namespace MovieStoreApp.Repository.Abstract
{
	public interface IUserAuthenticationService
	{

        Task<Status> LoginAsync(Login model);
        Task LogoutAsync();
        Task<Status> RegisterAsync(Registration model);
        //Task<Status> ChangePasswordAsync(ChangePasswordModel model, string username);
    }
}

