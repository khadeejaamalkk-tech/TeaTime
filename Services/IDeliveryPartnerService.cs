using TeaTimeDelivery.DTOs;
using TeaTimeDelivery.Helpers;
using TeaTimeDelivery.Models;

namespace TeaTimeDelivery.Services
{
    public interface IDeliveryPartnerService
    {
        Task<ApiResponse<IEnumerable<DeliveryPartnerDto>>> GetAllDeliveryPartners();
        Task<ApiResponse<DeliveryPartnerDto>> GetDeliveryPartnerById(int id);
        Task<ApiResponse<string>> CreateDeliveryPartner(CreateDeliveryPartnerDto dto);
        Task<ApiResponse<object>> UpdateDeliveryPartner(UpdateDeliveryPartnerDto dto);
        Task<ApiResponse<string>> DeleteDeliveryPartner(int id);
    }
}
