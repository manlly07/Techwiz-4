using BSS;
using Microsoft.AspNetCore.Mvc;
using SoccerClub.Services;
using SoccerClub.ViewModel;

namespace SoccerClub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IFileService _iFileService;

        public FileController(IFileService iFileService)
        {
            _iFileService = iFileService;
        }

        //public Result UploadFile([FromForm] FileUploadModel file)
        //{
        //    string msg = _iFileService.UploadFileSeaWeed(file.File, out string url);
        //    if (msg.Length > 0) return Log.ProcessError(msg).ToResultError();
        //    return url.ToResultOk();
        //}
        [HttpPost("UploadFile")]
        public async Task<string> UploadFile([FromForm] FileUploadModel file)
        {
            string path = await _iFileService.UpLoadFile(file.File);
            if (path.Length < 0)
            {
                throw new Exception("This file can not upload");
            }
            return path;
        }

    }

}
