using Microsoft.AspNetCore.Mvc;
using SoccerClub.Services;
using SoccerClub.ViewModel;

namespace SoccerClub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _iPlayerService;

        public PlayerController(
            IPlayerService iPlayerService)
        {
            _iPlayerService = iPlayerService;
        }
        [HttpGet("GetList")]
        public IActionResult GetList([FromQuery] PlayerSearchVM search)
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

            msg = _iPlayerService.GetList(search, out object result);
            if (msg.Length > 0) return BadRequest(msg);

            return Ok(result);
        }
        [HttpPost("Insert")]
        public IActionResult Insert([FromBody] PlayerInsertVM insert)
        {
            string msg = string.Empty;
            if (insert == null)
            {
                ModelState.AddModelError("NotFoundInsert", "Insert is null!");
                return Ok(ModelState);
            }
            if (ModelState.IsValid)
            {
                msg = _iPlayerService.Insert(insert);
                if (msg.Length > 0) return BadRequest(msg);
            }
            return Ok("ok");
        }
        [HttpPut("Update")]
        public IActionResult Update([FromBody] PlayerUpdateVM update)
        {
            string msg = string.Empty;
            if (update == null)
            {
                ModelState.AddModelError("NotFoundUpdate", "Update is null!");
                return Ok(ModelState);
            }
            if (ModelState.IsValid)
            {
                msg = _iPlayerService.Update(update);
                if (msg.Length > 0) return BadRequest(msg);
            }
            return Ok("ok");
        }

        [HttpPatch("Delete")]
        public IActionResult Delete([FromQuery] Guid PlayerID)
        {
            string msg = string.Empty;
            if (PlayerID == Guid.Empty)
            {
                ModelState.AddModelError("NotFoundPlayerID", "PlayerID is empty!");
                return Ok(ModelState);
            }
            msg = _iPlayerService.Delete(PlayerID);
            if (msg.Length > 0) return BadRequest(msg);

            return Ok("ok");
        }

        [HttpGet("GetDetail")]
        public IActionResult GetDetail([FromQuery] Guid PlayerID)
        {
            string msg = string.Empty;
            if (PlayerID == Guid.Empty)
            {
                ModelState.AddModelError("NotFoundPlayerID", "PlayerID is empty!");
                return Ok(ModelState);
            }
            msg = _iPlayerService.GetDetail(PlayerID, out object result);
            if (msg.Length > 0) return BadRequest(msg);

            return Ok(result);
        }

    }
}
