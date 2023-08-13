using Microsoft.AspNetCore.Mvc;
using SoccerClub.Services;
using SoccerClub.ViewModel;

namespace SoccerClub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _iArticleService;

        public ArticleController(
            IArticleService iArticleService)
        {
            _iArticleService = iArticleService;
        }
        [HttpGet("GetList")]
        public IActionResult GetList([FromQuery] ArticleSearchVM search)
        {
            string msg = string.Empty;
            if (search == null)
            {
                ModelState.AddModelError("NotFoundSearch", "Search is null!");
                return Ok(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(msg);
            }

            msg = _iArticleService.GetList(search, out object result);
            if (msg.Length > 0) return BadRequest(msg);

            return Ok(result);
        }
        [HttpPost("Insert")]
        public IActionResult Insert([FromBody] ArticleInsertVM insert)
        {
            string msg = string.Empty;
            if (insert == null)
            {
                ModelState.AddModelError("NotFoundInsert", "Insert is null!");
                return Ok(ModelState);
            }
            if (ModelState.IsValid)
            {
                msg = _iArticleService.Insert(insert);
                if (msg.Length > 0) return BadRequest(msg);
            }
            return Ok("ok");
        }
        [HttpPut("Update")]
        public IActionResult Update([FromBody] ArticleUpdateVM update)
        {
            string msg = string.Empty;
            if (update == null)
            {
                ModelState.AddModelError("NotFoundUpdate", "Update is null!");
                return Ok(ModelState);
            }
            if (ModelState.IsValid)
            {
                msg = _iArticleService.Update(update);
                if (msg.Length > 0) return BadRequest(msg);
            }
            return Ok("ok");
        }

        [HttpPatch("Delete")]
        public IActionResult Delete([FromQuery] Guid ArticleID)
        {
            string msg = string.Empty;
            if (ArticleID == Guid.Empty)
            {
                ModelState.AddModelError("NotFoundArticleID", "ArticleID is empty!");
                return Ok(ModelState);
            }
            msg = _iArticleService.Delete(ArticleID);
            if (msg.Length > 0) return BadRequest(msg);
            return Ok("ok");
        }

        [HttpGet("GetDetail")]
        public IActionResult GetDetail([FromQuery] Guid ArticleID)
        {
            string msg = string.Empty;
            if (ArticleID == Guid.Empty)
            {
                ModelState.AddModelError("NotFoundArticleID", "ArticleID is empty!");
                return Ok(ModelState);
            }
            msg = _iArticleService.GetDetail(ArticleID, out object result);
            if (msg.Length > 0) return BadRequest(msg);

            return Ok(result);
        }

    }
}
