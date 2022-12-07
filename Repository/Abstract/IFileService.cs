﻿using System;
namespace MovieStoreApp.Repository.Abstract
{
	public interface IFileService
	{

		public Tuple<int, string> SaveImage(IFormFile imageFile);


		public bool DeleteImage(string imageFileName);
	}
}

