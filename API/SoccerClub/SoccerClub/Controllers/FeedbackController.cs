using Microsoft.AspNetCore.Mvc;
using SoccerClub.Services;
using SoccerClub.ViewModel;

namespace SoccerClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _iFeedbackService;

        public FeedbackController(
            IFeedbackService iFeedbackService)
        {
            _iFeedbackService = iFeedbackService;
        }
        [HttpGet("GetList")]
        public IActionResult GetList([FromQuery] FeedbackSearchVM search)
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

            msg = _iFeedbackService.GetList(search, out object result);
            if (msg.Length > 0) return BadRequest(msg);

            return Ok(result);
        }
        [HttpPost("Insert")]
        public IActionResult Insert([FromBody] FeedbackInsertVM insert)
        {
            string msg = string.Empty;
            if (insert == null)
            {
                ModelState.AddModelError("NotFoundInsert", "Insert is null!");
                return Ok(ModelState);
            }
            if (ModelState.IsValid)
            {
                msg = _iFeedbackService.Insert(insert);
                if (msg.Length > 0) return BadRequest(msg);
            }
            return Ok("ok");
        }
        //[HttpPut("Update")]
        //public IActionResult Update([FromBody] FeedbackUpdateVM update)
        //{
        //    string msg = string.Empty;
        //    if (update == null)
        //    {
        //        ModelState.AddModelError("NotFoundUpdate", "Update is null!");
        //        return Ok(ModelState);
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        msg = _iFeedbackService.Update(update);
        //        if (msg.Length > 0) return BadRequest(msg);
        //    }
        //    return Ok("ok");
        //}

        [HttpPatch("Delete")]
        public IActionResult Delete([FromQuery] int FeedbackID)
        {
            string msg = string.Empty;
            if (FeedbackID == null)
            {
                ModelState.AddModelError("NotFoundFeedbackID", "FeedbackID is empty!");
                return Ok(ModelState);
            }
            msg = _iFeedbackService.Delete(FeedbackID);
            if (msg.Length > 0) return BadRequest(msg);

            return Ok("ok");
        }

        [HttpGet("GetDetail")]
        public IActionResult GetDetail([FromQuery] int FeedbackID)
        {
            string msg = string.Empty;
            if (FeedbackID == null)
            {
                ModelState.AddModelError("NotFoundFeedbackID", "FeedbackID is empty!");
                return Ok(ModelState);
            }
            msg = _iFeedbackService.GetDetail(FeedbackID, out object result);
            if (msg.Length > 0) return BadRequest(msg);

            return Ok(result);
        }
    }
}
