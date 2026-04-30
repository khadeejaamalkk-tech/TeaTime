using TeaTimeDelivery.DTOs;
using TeaTimeDelivery.Helpers;
using TeaTimeDelivery.Repositories;

namespace TeaTimeDelivery.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IDeliveryPartnerRepository _deliveryRepo;
        private readonly INotificationRepository _notificationRepo;

        public OrderService(
            IOrderRepository repository,
            IDeliveryPartnerRepository deliveryRepo,
            INotificationRepository notificationRepo
              )
        {
            _repository = repository;
            _deliveryRepo = deliveryRepo;
            _notificationRepo = notificationRepo;

        }

        public async Task<ApiResponse<int>> CreateOrder(CreateOrderDto dto)
        {
            try
            {
                if (dto == null || dto.Items == null || !dto.Items.Any())
                    return ApiResponse<int>.BadRequest("Order must contain items");


                var orderId = await _repository.CreateOrder(dto);


                var vehicleType = dto.VehicleType?.Trim().ToLower();

                var partners = await _deliveryRepo.GetByVehicleType(vehicleType);

                if (partners != null && partners.Any())
                {
                    string message = $"New Order #{orderId} available for {dto.VehicleType}";

                    foreach (var partner in partners)
                    {
                        await _notificationRepo.CreateNotification(partner.Id, message);
                    }
                }

                return ApiResponse<int>.Created(orderId, "Order created successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<int>.Error(ex.Message);
            }
        }
        public async Task<ApiResponse<IEnumerable<OrderResponseDto>>> GetAllOrders()

        {
            try
            {
                var orders = await _repository.GetAllOrders();
                return ApiResponse<IEnumerable<OrderResponseDto>>.Success(orders);
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<OrderResponseDto>>.Error(ex.Message);

            }
        }
        public async Task<ApiResponse<OrderResponseDto>> GetOrderById(int orderId)
        {
            try
            {
                var order = await _repository.GetOrderById(orderId);
                if (order == null)
                    return ApiResponse<OrderResponseDto>.NotFound("order not found");
                return ApiResponse<OrderResponseDto>.Success(order);
            }
            catch (Exception ex)
            {

                return ApiResponse<OrderResponseDto>.Error(ex.Message);

            }

        }
        public async Task<ApiResponse<IEnumerable<OrderResponseDto>>> GetOrdersByRestaurant(int restaurantId)
        {
            try
            {
                var orders = await _repository.GetOrdersByRestaurant(restaurantId);
                return ApiResponse<IEnumerable<OrderResponseDto>>.Success(orders);


            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<OrderResponseDto>>.Error(ex.Message);


            }

        }
        public async Task<ApiResponse<string>> DeleteOrder(int orderId)

        {
            try
            {
                var result = await _repository.DeleteOrder(orderId);
                if (!result)
                    return ApiResponse<string>.NotFound("Order not found");
                return ApiResponse<string>.SuccessNoData("Order deleted successfully");

            }
            catch (Exception ex)
            {
                {
                    return ApiResponse<string>.Error(ex.Message);
                }
            }
        }
        

        public async Task<ApiResponse<string>> OrderAction(OrderActionDto dto)
        {
            try
            {
                if (dto == null)
                    return ApiResponse<string>.BadRequest("Request cannot be null");
                if (dto.OrderId <= 0)
                    return ApiResponse<string>.BadRequest("Invalid OrderId");

                if (dto.DeliveryPartnerId <= 0)
                    return ApiResponse<string>.BadRequest("Invalid DeliveryPartnerId");

                if (string.IsNullOrWhiteSpace(dto.ActionType))
                    return ApiResponse<string>.BadRequest("ActionType is required");
                var action = dto.ActionType.ToUpper();

                if (action != "ACCEPT" && action != "CANCEL" && action != "PICKED" && action != "DELIVERED" && action != "COMPLETED")
                    return ApiResponse<string>.BadRequest("Invalid action type");
                var result = await _repository.OrderAction(dto);
                return result switch
                {
                    1 => ApiResponse<string>.Success("Action completed successfully"),
                    0 => ApiResponse<string>.BadRequest("Action failed due to invalid state"),
                    -1 => ApiResponse<string>.BadRequest("Cancel not allowed for this order"),
                    -2 => ApiResponse<string>.BadRequest("Invalid action type"),
                    -3 => ApiResponse<string>.BadRequest("Pick not allowed. Order must be accepted by you"),

                    -4 => ApiResponse<string>.BadRequest("Delivery not allowed. Order must be picked first"),
                    _ => ApiResponse<string>.BadRequest("Unexpected error occurred")

                };
            }
            catch (Exception ex)
            {
                return ApiResponse<string>.BadRequest($"Error: {ex.Message}");

            }
        }


        public async Task<ApiResponse<string>> CancelOrderByUser(int orderId, int userId)
        {
            try
            {
                var result = await _repository.CancelOrderByUser(orderId, userId);

                return result switch
                {
                    1 => ApiResponse<string>.Success("Order cancelled successfully"),
                    0 => ApiResponse<string>.BadRequest("Cancellation failed"),
                    -1 => ApiResponse<string>.BadRequest("You cannot cancel this order at this stage"),
                    _ => ApiResponse<string>.Error("Unexpected error")
                };
            }
            catch (Exception ex)
            {
                return ApiResponse<string>.Error(ex.Message);
            }
        }
        public async Task<ApiResponse<IEnumerable<OrderStatusHistoryDto>>> GetOrderHistory(int orderId)
        {
            var history = await _repository.GetOrderStatusHistory(orderId);

            if (!history.Any())
                return ApiResponse<IEnumerable<OrderStatusHistoryDto>>.NotFound("No history found");

            return ApiResponse<IEnumerable<OrderStatusHistoryDto>>.Success(history);
        }
        public async Task<ApiResponse<OrderDashboardDto>> GetDashboard(int deliveryPartnerId)
        {
            try
            {
                if (deliveryPartnerId <= 0)
                    return ApiResponse<OrderDashboardDto>.BadRequest("Invalid delivery partner ID");

                var partner = await _deliveryRepo.GetDeliveryPartnerById(deliveryPartnerId);

                if (partner == null)
                    return ApiResponse<OrderDashboardDto>.NotFound("Delivery partner not found");

                var dashboard = await _repository.GetOrderDashboard(
                    deliveryPartnerId,
                    partner.VehicleType
                );

                return ApiResponse<OrderDashboardDto>.Success(dashboard);
            }
            catch (Exception ex)
            {

                return ApiResponse<OrderDashboardDto>.Error("Something went wrong while fetching dashboard");
            }
        }
        public async Task<ApiResponse<OrderDetailsDto>> GetOrderDetails(int orderId)
        {
            try
            {
                if (orderId <= 0)
                    return ApiResponse<OrderDetailsDto>.BadRequest("Invalid order ID");

                var order = await _repository.GetOrderDetails(orderId);

                if (order == null)
                    return ApiResponse<OrderDetailsDto>.NotFound("Order not found");

                return ApiResponse<OrderDetailsDto>.Success(order);
            }
            catch (Exception ex)
            {

                return ApiResponse<OrderDetailsDto>.Error("Something went wrong while fetching order details");
            }
        }
        public async Task<ApiResponse<OrderSummaryDto>> GetOrderSummary(int orderId)
        {
            try
            {
                var data = await _repository.GetOrderSummary(orderId);

                if (data == null)
                    return ApiResponse<OrderSummaryDto>.NotFound("Order not found");

                return ApiResponse<OrderSummaryDto>.Success(data);
            }
            catch (Exception ex)
            {
                return ApiResponse<OrderSummaryDto>.BadRequest(ex.Message);
            }
        }
        public async Task<ApiResponse<SalesDashboardResponseDto>> GetSalesDashboard(SalesDashboardRequestDto dto)
        {
            try
            {
                if (dto.FromDate.HasValue && dto.ToDate.HasValue && dto.FromDate > dto.ToDate)
                {
                    return ApiResponse<SalesDashboardResponseDto>
                        .BadRequest("FromDate cannot be greater than ToDate");
                }

                var result = await _repository.GetSalesDashboard(dto);

                if (result == null || result.CompletedOrders == null || !result.CompletedOrders.Any())
                {
                    return ApiResponse<SalesDashboardResponseDto>
                        .SuccessNoData("No sales data found for given date range");
                }

                return ApiResponse<SalesDashboardResponseDto>
                    .Success(result, "Sales dashboard fetched successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<SalesDashboardResponseDto>
                    .Error("Something went wrong while fetching dashboard");
            }
        }
        public async Task<ApiResponse<List<CustomerSummaryDto>>> GetCustomerSummary(CustomerSummaryRequestDto dto)
        {
            try
            {
                var result = await _repository.GetCustomerSummary(dto);

                if (result == null || !result.Any())
                {
                    return ApiResponse<List<CustomerSummaryDto>>
                        .SuccessNoData("No customer data found");
                }

                return ApiResponse<List<CustomerSummaryDto>>
                    .Success(result, "Customer summary fetched successfully");
            }
            catch (Exception)
            {
                return ApiResponse<List<CustomerSummaryDto>>
                    .Error("Error while fetching customer summary");
            }
        }
        public async Task<ApiResponse<ShopDashboardResponseDto>> GetShopDashboard(ShopDashboardRequestDto dto)
        {
            try
            {
                var data = await _repository.GetShopDashboard(dto);
                return ApiResponse<ShopDashboardResponseDto>.Success(data, "Dashbard fetched successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<ShopDashboardResponseDto>.BadRequest("Database error occurred");


                return ApiResponse<ShopDashboardResponseDto>.NotFound("Something went wrong");
            }
        }
        public async Task<ApiResponse<ShopCashflowResponsetDto>> GetShopCashFlow(ShopCashflowRequestDto dto)
        {
            try
            {
                var result = await _repository.GetShopCashflow(dto);

                if (result == null)
                    return ApiResponse<ShopCashflowResponsetDto>.BadRequest("No data found");
                return ApiResponse<ShopCashflowResponsetDto>.Success(result, "Cash flow fetched");
            }
            catch (Exception ex)
            {
                return ApiResponse<ShopCashflowResponsetDto>.NotFound($"Error: {ex.Message}");
            }

        }
        public async Task<ApiResponse<AdminDashboardResponseDto>> GetAdminDashboard(AdminDashboardRequestDto dto)
        {
            try
            {
                var result = await _repository.GetAdminDashboard(dto);

                return ApiResponse<AdminDashboardResponseDto>.Success(
                    result,
                    "Dashboard fetched successfully"
                );
            }
            catch (Exception ex)
            {
                return ApiResponse<AdminDashboardResponseDto>.NotFound(
                    "Failed to fetch dashboard"
                    
                );
            }
        }
        public async Task<ApiResponse<List<AdminOrdersListResponseDto>>> GetAdminOrdersList(AdminOrdersListRequestDto dto)
        {
            try
            {
                var result = await _repository.GetAdminOrdersList(dto);

                if (result == null || !result.Any())
                {
                    return ApiResponse<List<AdminOrdersListResponseDto>>
                        .BadRequest("No records found");
                }

                return ApiResponse<List<AdminOrdersListResponseDto>>
                    .Success(result, "Orders fetched successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<List<AdminOrdersListResponseDto>>
                    .NotFound("An error occurred: " + ex.Message);
            }
        }
        public async Task<ApiResponse<AdminCashFlowResponseDto>> GetAdminCashFlow(AdminCashFlowRequestDto dto)
        {
            try
            {
                var result = await _repository.GetAdminCashFlow(dto);

                if (result == null)
                {
                    return ApiResponse<AdminCashFlowResponseDto>.NotFound("No data found");
                }

                return ApiResponse<AdminCashFlowResponseDto>.Success(
                    result,
                    "Cash flow fetched successfully"
                );
            }
            catch (Exception ex)
            {
                return ApiResponse<AdminCashFlowResponseDto>.BadRequest(
                    "Something went wrong: " + ex.Message
                );
            }
        }
    }
}
