using Microsoft.AspNetCore.Mvc;
using SoccerClub.Services;
using SoccerClub.ViewModel;

namespace SoccerClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _iCategoryService;

        public CategoryController(
            ICategoryService iCategoryService)
        {
            _iCategoryService = iCategoryService;
        }
        [HttpGet("GetList")]
        public IActionResult GetList([FromQuery] CategorySearchVM search)
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

            msg = _iCategoryService.GetList(search, out object result);
            if (msg.Length > 0) return BadRequest(msg);

            return Ok(result);
        }
        [HttpPost("Insert")]
        public IActionResult Insert([FromBody] CategoryInsertVM insert)
        {
            string msg = string.Empty;
            if (insert == null)
            {
                ModelState.AddModelError("NotFoundInsert", "Insert is null!");
                return Ok(ModelState);
            }
            if (ModelState.IsValid)
            {
                msg = _iCategoryService.Insert(insert);
                if (msg.Length > 0) return BadRequest(msg);
            }
            return Ok("ok");
        }
        [HttpPut("Update")]
        public IActionResult Update([FromBody] CategoryUpdateVM update)
        {
            string msg = string.Empty;
            if (update == null)
            {
                ModelState.AddModelError("NotFoundUpdate", "Update is null!");
                return Ok(ModelState);
            }
            if (ModelState.IsValid)
            {
                msg = _iCategoryService.Update(update);
                if (msg.Length > 0) return BadRequest(msg);
            }
            return Ok("ok");
        }

        [HttpPatch("Delete")]
        public IActionResult Delete([FromQuery] Guid CategoryID)
        {
            string msg = string.Empty;
            if (CategoryID == Guid.Empty)
            {
                ModelState.AddModelError("NotFoundCategoryID", "CategoryID is empty!");
                return Ok(ModelState);
            }
            msg = _iCategoryService.Delete(CategoryID);
            if (msg.Length > 0) return BadRequest(msg);

            return Ok("ok");
        }

        [HttpGet("GetDetail")]
        public IActionResult GetDetail([FromQuery] Guid CategoryID)
        {
            string msg = string.Empty;
            if (CategoryID == Guid.Empty)
            {
                ModelState.AddModelError("NotFoundCategoryID", "CategoryID is empty!");
                return Ok(ModelState);
            }
            msg = _iCategoryService.GetDetail(CategoryID, out object result);
            if (msg.Length > 0) return BadRequest(msg);

            return Ok(result);
        }
    }
}
