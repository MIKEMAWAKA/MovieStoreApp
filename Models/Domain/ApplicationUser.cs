using System;
using Microsoft.AspNetCore.Identity;

namespace MovieStoreApp.Models.Domain
{
	public class ApplicationUser : IdentityUser
	{

		public string Name { get; set; }


	}
}

