using Microsoft.AspNetCore.Mvc;
using SoccerClub.Services;
using SoccerClub.ViewModel;

namespace SoccerClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubController : ControllerBase
    {
        private readonly IClubService _iClubService;

        public ClubController(
            IClubService iClubService)
        {
            _iClubService = iClubService;
        }
        [HttpGet("GetList")]
        public IActionResult GetList([FromQuery] ClubSearchVM search)
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

            msg = _iClubService.GetList(search, out object result);
            if (msg.Length > 0) return BadRequest(msg);

            return Ok(result);
        }
        [HttpPost("Insert")]
        public IActionResult Insert([FromBody] ClubInsertVM insert)
        {
            string msg = string.Empty;
            if (insert == null)
            {
                ModelState.AddModelError("NotFoundInsert", "Insert is null!");
                return Ok(ModelState);
            }
            if (ModelState.IsValid)
            {
                msg = _iClubService.Insert(insert);
                if (msg.Length > 0) return BadRequest(msg);
            }
            return Ok("ok");
        }
        [HttpPut("Update")]
        public IActionResult Update([FromBody] ClubUpdateVM update)
        {
            string msg = string.Empty;
            if (update == null)
            {
                ModelState.AddModelError("NotFoundUpdate", "Update is null!");
                return Ok(ModelState);
            }
            if (ModelState.IsValid)
            {
                msg = _iClubService.Update(update);
                if (msg.Length > 0) return BadRequest(msg);
            }
            return Ok("ok");
        }

        [HttpPatch("Delete")]
        public IActionResult Delete([FromQuery] Guid ClubID)
        {
            string msg = string.Empty;
            if (ClubID == Guid.Empty)
            {
                ModelState.AddModelError("NotFoundClubID", "ClubID is empty!");
                return Ok(ModelState);
            }
            msg = _iClubService.Delete(ClubID);
            if (msg.Length > 0) return BadRequest(msg);

            return Ok("ok");
        }

        [HttpGet("GetDetail")]
        public IActionResult GetDetail([FromQuery] Guid ClubID)
        {
            string msg = string.Empty;
            if (ClubID == Guid.Empty)
            {
                ModelState.AddModelError("NotFoundClubID", "ClubID is empty!");
                return Ok(ModelState);
            }
            msg = _iClubService.GetDetail(ClubID, out object result);
            if (msg.Length > 0) return BadRequest(msg);

            return Ok(result);
        }
    }
}
