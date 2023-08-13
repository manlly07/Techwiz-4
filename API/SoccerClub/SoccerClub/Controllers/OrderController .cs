using Microsoft.AspNetCore.Mvc;
using SoccerClub.Services;
using SoccerClub.ViewModel;

namespace SoccerClub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _iOrderService;

        public OrderController(
            IOrderService iOrderService)
        {
            _iOrderService = iOrderService;
        }
        [HttpGet("GetList")]
        public IActionResult GetList([FromQuery] OrderSearchVM search)
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

            msg = _iOrderService.GetList(search, out object result);
            if (msg.Length > 0) return BadRequest(msg);

            return Ok(result);
        }
        [HttpPost("Insert")]
        public IActionResult Insert([FromBody] OrderInsertVM insert)
        {
            string msg = string.Empty;
            if (insert == null)
            {
                ModelState.AddModelError("NotFoundInsert", "Insert is null!");
                return Ok(ModelState);
            }
            if (ModelState.IsValid)
            {
                msg = _iOrderService.Insert(insert);
                if (msg.Length > 0) return BadRequest(msg);
            }
            return Ok("ok");
        }
        [HttpPut("Update")]
        public IActionResult Update([FromBody] OrderUpdateVM update)
        {
            string msg = string.Empty;
            if (update == null)
            {
                ModelState.AddModelError("NotFoundUpdate", "Update is null!");
                return Ok(ModelState);
            }
            if (ModelState.IsValid)
            {
                msg = _iOrderService.Update(update);
                if (msg.Length > 0) return BadRequest(msg);
            }
            return Ok("ok");
        }

        [HttpPatch("Delete")]
        public IActionResult Delete([FromQuery] int OrderID)
        {
            string msg = _iOrderService.Delete(OrderID);
            if (msg.Length > 0) return BadRequest(msg);

            return Ok("ok");
        }

        [HttpGet("GetDetail")]
        public IActionResult GetDetail([FromQuery] int OrderID)
        {
            string msg = _iOrderService.GetDetail(OrderID, out object result);
            if (msg.Length > 0) return BadRequest(msg);

            return Ok(result);
        }

    }
}
