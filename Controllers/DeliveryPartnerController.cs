using Microsoft.AspNetCore.Mvc;
using TeaTimeDelivery.Services;
using TeaTimeDelivery.DTOs;


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
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.GetAllDeliveryPartners();
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _service.GetDeliveryPartnerById(id);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDeliveryPartnerDto dto)
        {
            var response = await _service.CreateDeliveryPartner(dto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut("update-deliverypartner")]
        public async Task<IActionResult> Update([FromForm] UpdateDeliveryPartnerDto dto)
        {
            var response = await _service.UpdateDeliveryPartner(dto);
            
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _service.DeleteDeliveryPartner(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
