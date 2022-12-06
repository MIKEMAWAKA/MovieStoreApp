using System;
using System.ComponentModel.DataAnnotations;

namespace MovieStoreApp.Models.DTO
{
	public class Login
	{
        //[Required]
        //public string Name { get; set; }

        //[Required]
        //[EmailAddress]
        //public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
       
    }
}

