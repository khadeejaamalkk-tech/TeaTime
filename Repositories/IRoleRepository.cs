using TeaTimeDelivery.Models;
namespace TeaTimeDelivery.Repositories
{
    public interface IRoleRepository
    {
        Task CreateRole(Role role);
        Task<IEnumerable<Role>> GetAllRoles();
    }
}
