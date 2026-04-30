using TeaTimeDelivery.DTOs;
using TeaTimeDelivery.Helpers;
using TeaTimeDelivery.Models;
using TeaTimeDelivery.Repositories;

namespace TeaTimeDelivery.Services
{
    public class DeliveryPartnerService : IDeliveryPartnerService
    {
        private readonly IDeliveryPartnerRepository _repository;

        public DeliveryPartnerService(IDeliveryPartnerRepository repository)
        {
            _repository = repository;
        }

        
        public async Task<ApiResponse<IEnumerable<DeliveryPartnerDto>>> GetAllDeliveryPartners()
        {
            try
            {
                var partners = await _repository.GetAllDeliveryPartners();

                var result = partners.Select(p => new DeliveryPartnerDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Phone = p.Phone,
                    VehicleType = p.VehicleType,
                    Location = p.Location,
                });

                return ApiResponse<IEnumerable<DeliveryPartnerDto>>.Success(result);
            }
            catch
            {
                return ApiResponse<IEnumerable<DeliveryPartnerDto>>.Error("Error while fetching delivery partners");
            }
        }

        
        public async Task<ApiResponse<DeliveryPartnerDto>> GetDeliveryPartnerById(int id)
        {
            try
            {
                var partner = await _repository.GetDeliveryPartnerById(id);

                if (partner == null)
                    return ApiResponse<DeliveryPartnerDto>.NotFound("Delivery Partner not found");

                var dto = new DeliveryPartnerDto
                {
                    Id = partner.Id,
                    Name = partner.Name,
                    Phone = partner.Phone,
                    VehicleType = partner.VehicleType,
                    Location = partner.Location,
                };

                return ApiResponse<DeliveryPartnerDto>.Success(dto);
            }
            catch
            {
                return ApiResponse<DeliveryPartnerDto>.Error("Error while fetching delivery partner");
            }
        }

        
        public async Task<ApiResponse<string>> CreateDeliveryPartner(CreateDeliveryPartnerDto dto)
        {
            try
            {
                var model = new DeliveryPartner
                {
                    Name = dto.Name,
                    Phone = dto.Phone,
                    VehicleType = dto.VehicleType,
                    Location = dto.Location,
                    IsActive = true
                };

                await _repository.CreateDeliveryPartner(model);

                return ApiResponse<string>.Created("Created", "Delivery Partner created successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<string>.BadRequest(ex.Message);
            }
        }
        public async Task<ApiResponse<object>> UpdateDeliveryPartner(UpdateDeliveryPartnerDto dto)
        {
            try
            {
                string fileName = null;

                if (dto.Photo != null && dto.Photo.Length > 0)
                {
                    var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.Photo.FileName);

                    var filePath = Path.Combine(folderPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await dto.Photo.CopyToAsync(stream);
                    }
                }

                await _repository.UpdateDeliveryPartner(dto, fileName);

                string photoUrl = fileName != null
                    ? $"https://localhost:7199/uploads/{fileName}"
                    : null;

                var responseData = new
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Phone = dto.Phone,
                    VehicleType = dto.VehicleType,
                    Location = dto.Location,
                    PhotoUrl = photoUrl,
                    IsActive = dto.IsActive
                };

                return ApiResponse<object>.Success(responseData, "Updated successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<object>.Error(ex.Message);
            }
        }




        public async Task<ApiResponse<string>> DeleteDeliveryPartner(int id)
        {
            try
            {
                await _repository.DeleteDeliveryPartner(id);

                return ApiResponse<string>.SuccessNoData("Delivery Partner deleted successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<string>.BadRequest(ex.Message);
            }
        }
    }
}