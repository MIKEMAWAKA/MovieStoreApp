using System;
using MovieStoreApp.Repository.Abstract;

namespace MovieStoreApp.Repository.Implementation
{
	public class FileService : IFileService
    {
        private readonly IWebHostEnvironment environment;

        public FileService(IWebHostEnvironment environment)
		{
            this.environment = environment;
        }

        public bool DeleteImage(string imageFileName)
        {
            throw new NotImplementedException();
        }

        public Tuple<int, string> SaveImage(IFormFile imageFile)
        {
            try
            {
                var wwPath = this.environment.WebRootPath;

                var path = Path.Combine(wwPath, "Uploads");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                //chek the allowed extentions
                var ext = Path.GetExtension(imageFile.FileName);
                var allowedExtensions = new string[] { ".jpg", ".png", ".jpeg" };

                if (!allowedExtensions.Contains(ext))
                {
                    string msg = string.Format("Only {0} extensions are allowed", string.Join(" ", allowedExtensions));

                    return new Tuple<int, string>(0, msg);


                }

                string uniqueString = Guid.NewGuid().ToString();

                var newFileName = uniqueString + ext;
                var fileWidthPath = Path.Combine(path, newFileName);
                var stream = new FileStream(fileWidthPath, FileMode.Create);
                imageFile.CopyTo(stream);
                stream.Close();

                return new Tuple<int, string>(1, newFileName);

               
            }
            catch (Exception ex)
            {
                return new Tuple<int, string>(0, "Error has occured");

            }
        }
    }
}

