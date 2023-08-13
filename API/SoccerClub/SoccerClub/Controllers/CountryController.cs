using Microsoft.AspNetCore.Mvc;
using SoccerClub.Services;
using SoccerClub.ViewModel;

namespace SoccerClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _iCountryService;

        public CountryController(
            ICountryService iCountryService)
        {
            _iCountryService = iCountryService;
        }
        [HttpGet("GetList")]
        public IActionResult GetList([FromQuery] CountrySearchVM search)
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

            msg = _iCountryService.GetList(search, out object result);
            if (msg.Length > 0) return BadRequest(msg);

            return Ok(result);
        }
        [HttpPost("Insert")]
        public IActionResult Insert([FromBody] CountryInsertVM insert)
        {
            string msg = string.Empty;
            if (insert == null)
            {
                ModelState.AddModelError("NotFoundInsert", "Insert is null!");
                return Ok(ModelState);
            }
            if (ModelState.IsValid)
            {
                msg = _iCountryService.Insert(insert);
                if (msg.Length > 0) return BadRequest(msg);
            }
            return Ok("ok");
        }
        [HttpPut("Update")]
        public IActionResult Update([FromBody] CountryUpdateVM update)
        {
            string msg = string.Empty;
            if (update == null)
            {
                ModelState.AddModelError("NotFoundUpdate", "Update is null!");
                return Ok(ModelState);
            }
            if (ModelState.IsValid)
            {
                msg = _iCountryService.Update(update);
                if (msg.Length > 0) return BadRequest(msg);
            }
            return Ok("ok");
        }

        [HttpPatch("Delete")]
        public IActionResult Delete([FromQuery] int CountryID)
        {
            string msg = string.Empty;
            if (CountryID == null)
            {
                ModelState.AddModelError("NotFoundCountryID", "CountryID is empty!");
                return Ok(ModelState);
            }
            msg = _iCountryService.Delete(CountryID);
            if (msg.Length > 0) return BadRequest(msg);

            return Ok("ok");
        }

        [HttpGet("GetDetail")]
        public IActionResult GetDetail([FromQuery] int CountryID)
        {
            string msg = string.Empty;
            if (CountryID == null)
            {
                ModelState.AddModelError("NotFoundCountryID", "CountryID is empty!");
                return Ok(ModelState);
            }
            msg = _iCountryService.GetDetail(CountryID, out object result);
            if (msg.Length > 0) return BadRequest(msg);

            return Ok(result);
        }
    }
}
