using Microsoft.AspNetCore.Mvc;
using SoccerClub.Services;
using SoccerClub.ViewModel;

namespace SoccerClub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatchController : ControllerBase
    {
        private readonly IMatchService _iMatchService;

        public MatchController(
            IMatchService iMatchService)
        {
            _iMatchService = iMatchService;
        }
        [HttpGet("GetList")]
        public IActionResult GetList([FromQuery] MatchSearchVM search)
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

            msg = _iMatchService.GetList(search, out object result);
            if (msg.Length > 0) return BadRequest(msg);

            return Ok(result);
        }
        [HttpPost("Insert")]
        public IActionResult Insert([FromBody] MatchInsertVM insert)
        {
            string msg = string.Empty;
            if (insert == null)
            {
                ModelState.AddModelError("NotFoundInsert", "Insert is null!");
                return Ok(ModelState);
            }
            if (ModelState.IsValid)
            {
                msg = _iMatchService.Insert(insert);
                if (msg.Length > 0) return BadRequest(msg);
            }
            return Ok("ok");
        }
        [HttpPut("Update")]
        public IActionResult Update([FromBody] MatchUpdateVM update)
        {
            string msg = string.Empty;
            if (update == null)
            {
                ModelState.AddModelError("NotFoundUpdate", "Update is null!");
                return Ok(ModelState);
            }
            if (ModelState.IsValid)
            {
                msg = _iMatchService.Update(update);
                if (msg.Length > 0) return BadRequest(msg);
            }
            return Ok("ok");
        }

        [HttpPatch("Delete")]
        public IActionResult Delete([FromQuery] Guid MatchID)
        {
            string msg = string.Empty;
            if (MatchID == Guid.Empty)
            {
                ModelState.AddModelError("NotFoundMatchID", "MatchID is empty!");
                return Ok(ModelState);
            }
            msg = _iMatchService.Delete(MatchID);
            if (msg.Length > 0) return BadRequest(msg);

            return Ok("ok");
        }

        [HttpGet("GetDetail")]
        public IActionResult GetDetail([FromQuery] Guid MatchID)
        {
            string msg = string.Empty;
            if (MatchID == Guid.Empty)
            {
                ModelState.AddModelError("NotFoundMatchID", "MatchID is empty!");
                return Ok(ModelState);
            }
            msg = _iMatchService.GetDetail(MatchID, out object result);
            if (msg.Length > 0) return BadRequest(msg);

            return Ok(result);
        }

    }
}
