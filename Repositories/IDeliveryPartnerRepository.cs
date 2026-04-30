using TeaTimeDelivery.DTOs;
using TeaTimeDelivery.Models;

namespace TeaTimeDelivery.Repositories
{
    public interface IDeliveryPartnerRepository
    {
        Task <DeliveryPartner> CreateDeliveryPartner(DeliveryPartner partner);
        Task<DeliveryPartner> GetDeliveryPartnerById(int id);
        Task<IEnumerable<DeliveryPartner>> GetAllDeliveryPartners();
        Task<DeliveryPartner> UpdateDeliveryPartner(UpdateDeliveryPartnerDto dto, string photoName);
        Task<bool> DeleteDeliveryPartner(int id);
        Task<IEnumerable<DeliveryPartner>> GetByVehicleType(string vehicleType);
    }
}
