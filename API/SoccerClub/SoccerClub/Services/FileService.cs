using BSS;

namespace SoccerClub.Services
{
    public interface IFileService
    {
        public string UploadFileSeaWeed(IFormFile file, out string url, int Expritime = 0, int Count = 1);
        public Task<string?> UpLoadFile(IFormFile file);
    }
    public class FileService : IFileService
    {
        public async Task<string?> UpLoadFile(IFormFile file)
        {
            var allowedExtensionsImage = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            var fileExtensionImage = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensionsImage.Contains(fileExtensionImage))
            {
                throw new ArgumentException("Can not upload this file");
            }

            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("No file was uploaded");
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine("Uploads/", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return fileName;
        }
        public string UploadFileSeaWeed(IFormFile file, out string url, int Expritime = 0, int Count = 1)
        {
            url = null;

            string msg = ""; /* ValidateFile(file, 10);*/
            if (msg.Length > 0) return msg;

            // lấy url 
            msg = CreateSeaweedUrl(out Seaweed sea, "SOCCRCLUB", Expritime, Count);
            if (msg.Length > 0) return msg;

            // upload file  
            msg = UploadFileSeaWeed(file, $"{sea.url}/{sea.fid}");
            if (msg.Length > 0) return msg;

            url = sea.publicUrl + "/" + sea.fid;
            if (!url.StartsWith("http")) url = "http://" + url;
            return "";
        }
        public string CreateSeaweedUrl(out Seaweed sea, string Collection = "", int Expritime = 0, int Count = 1)
        {
            sea = null;
            string SeaweedUrl = Constant.Image;
            string msg = Ultility.NewSeaweedAssign(SeaweedUrl + "/dir/assign", out sea, Collection, Count, Expritime);
            if (msg.Length > 0) return msg;
            return "";
        }
        public string UploadFileSeaWeed(IFormFile file, string SeaweedUrl)
        {
            string fileName = DateTime.Now.ToString("yyyyMMdd") + "_" + file.FileName;
            string msg = Ultility.UploadFileSeaweed(file.OpenReadStream(), fileName, SeaweedUrl, out dynamic _);
            if (msg.Length > 0) return msg;

            return "";
        }
    }
}
