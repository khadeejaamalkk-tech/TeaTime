using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeaTimeDelivery.DTOs;
using TeaTimeDelivery.Services;


namespace TeaTimeDelivery.Controllers
{
    [ApiController]
    [Route("api/[controller]")]


    public class DeliveryPartnerController: ControllerBase
    {
        private readonly IDeliveryPartnerService _service;
        public DeliveryPartnerController(IDeliveryPartnerService service)
        {
            _service = service;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.GetAllDeliveryPartners();
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _service.GetDeliveryPartnerById(id);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateDeliveryPartnerDto dto)
        {
            var response = await _service.CreateDeliveryPartner(dto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut("update-deliverypartner")]
        [Authorize]
        public async Task<IActionResult> Update([FromForm] UpdateDeliveryPartnerDto dto)
        {
            var response = await _service.UpdateDeliveryPartner(dto);
            
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _service.DeleteDeliveryPartner(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
