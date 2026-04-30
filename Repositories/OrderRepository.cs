using Dapper;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using TeaTimeDelivery.Data;
using TeaTimeDelivery.DTOs;

namespace TeaTimeDelivery.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DapperContext _context;
        public OrderRepository(DapperContext context)
        {
            _context = context;
        }



        public async Task<int> CreateOrder(CreateOrderDto dto)
        {
            using var connection = _context.CreateConnection();

            var table = new DataTable();
            table.Columns.Add("MenuId", typeof(int));
            table.Columns.Add("Quantity", typeof(int));

            foreach (var item in dto.Items)
            {
                table.Rows.Add(item.MenuId, item.Quantity);
            }

            var parameters = new DynamicParameters();

            parameters.Add("@InvoiceNo", dto.InvoiceNo);
            parameters.Add("@CustomerName", dto.CustomerName);
            parameters.Add("@CustomerPhone", dto.CustomerPhone);
            parameters.Add("@DeliveryAddress", dto.DeliveryAddress);
            parameters.Add("@Location", dto.Location);

            parameters.Add("@DeliveryCharge", dto.DeliveryCharge);
            parameters.Add("@GST", dto.GST);
            parameters.Add("@Discount", dto.Discount);

            parameters.Add("@RestaurantId", dto.RestaurantId);
            parameters.Add("@VehicleType", dto.VehicleType);

            parameters.Add("@Items", table.AsTableValuedParameter("OrderItemType"));

            var orderId = await connection.ExecuteScalarAsync<int>(
                "sp_CreateOrder",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return orderId;
        }

        public async Task<IEnumerable<OrderResponseDto>> GetAllOrders()
        {
            using var connection = _context.CreateConnection();
            using var multi = await connection.QueryMultipleAsync(
                "sp_GetAllOrders",
                commandType: CommandType.StoredProcedure);

            var orders = (await multi.ReadAsync<OrderResponseDto>()).ToList();
            var items= (await multi.ReadAsync<OrderItemDto>()).ToList();
            foreach (var order in orders)
            {
                order.Items =items.Where(i => i.OrderId ==order.Id).ToList();
            }
            return orders;
        }
        public async Task<OrderResponseDto> GetOrderById(int orderId)
        {
            using var connection = _context.CreateConnection();

            using var multi =await connection.QueryMultipleAsync(
                "sp_GetOrderById",
                new { OrderId = orderId },
                commandType: CommandType.StoredProcedure);
            var order = await multi.ReadFirstOrDefaultAsync<OrderResponseDto>();
            var items = (await multi.ReadAsync<OrderItemDto>()).ToList();

            if(order != null)
            {
                order.Items = items;
            }
            return order;
        }
        public async Task<IEnumerable<OrderResponseDto>> GetOrdersByRestaurant(int restaurantId)
        {
            using var connection = _context.CreateConnection();
            using var multi = await connection.QueryMultipleAsync(
                "sp_GetOrdersByRestaurant",
                 new { RestaurantId = restaurantId },
                 commandType: CommandType.StoredProcedure

                );
            var orders = (await multi.ReadAsync<OrderResponseDto>()).ToList();
            var items = (await multi.ReadAsync<OrderItemDto>()).ToList();

            foreach (var order in orders)
            {
                order.Items = items.Where(i => i.OrderId == order.Id).ToList();
            }

            return orders;

        }
        public async Task<bool> DeleteOrder(int orderId)
        {
            using var connection = _context.CreateConnection();

            var result = await connection.ExecuteAsync(
                "sp_DeleteOrder",
                new { OrderId = orderId },
                commandType: CommandType.StoredProcedure
            );

            return result > 0;
        }
        
        public async Task<int> OrderAction(OrderActionDto dto)
        {
            using var connection = _context.CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@OrderId", dto.OrderId);
            parameters.Add("@DeliveryPartnerId", dto.DeliveryPartnerId);
            parameters.Add("@ActionType", dto.ActionType.ToUpper());
            if (!string.IsNullOrWhiteSpace(dto.OTP))
                parameters.Add("@OTP", dto.OTP);


            var result = await connection.ExecuteScalarAsync<int>(
                "sp_OrderAction",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }


        public async Task<int> CancelOrderByUser(int orderId, int userId)
        {
            using var connection = _context.CreateConnection();

            return await connection.ExecuteScalarAsync<int>(
                "sp_CancelOrderByUser",
                new { OrderId = orderId, UserId = userId },
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task<IEnumerable<OrderStatusHistoryDto>> GetOrderStatusHistory(int orderId)
        {
            using var connection = _context.CreateConnection();

            var query = @"
        SELECT 
            osh.Id,
            osh.OrderId,
            osh.DeliveryPartnerId,
            osh.StatusId,
            os.Name AS StatusName,
            osh.CreatedAt
        FROM OrderStatusHistory osh
        INNER JOIN OrderStatuses os 
            ON osh.StatusId = os.Id
        WHERE osh.OrderId = @OrderId
        ORDER BY osh.CreatedAt";

            return await connection.QueryAsync<OrderStatusHistoryDto>(
                query,
                new { OrderId = orderId }
            );
        }
        public async Task<OrderDashboardDto> GetOrderDashboard(int deliveryPartnerId, string vehicleType)
        {
            using var connection = _context.CreateConnection();

            using var multi = await connection.QueryMultipleAsync(
                "sp_GetOrderDashboard",
                new
                {
                    DeliveryPartnerId = deliveryPartnerId,
                    VehicleType = vehicleType
                },
                commandType: CommandType.StoredProcedure
            );

            var counts = await multi.ReadFirstOrDefaultAsync<OrderCountsDto>();
            var pendingOrders = (await multi.ReadAsync<PendingOrderDto>()).ToList();

            return new OrderDashboardDto
            {
                Counts = counts ?? new OrderCountsDto(),
                PendingOrders = pendingOrders
            };
        }
        public async Task<OrderDetailsDto> GetOrderDetails(int orderId)
        {
            using var connection = _context.CreateConnection();

            var result = await connection.QueryFirstOrDefaultAsync<OrderDetailsDto>(
                "sp_GetOrderDetailsById",
                new { OrderId = orderId },
                commandType: CommandType.StoredProcedure
            );

            return result;
        }
        public async Task<OrderSummaryDto> GetOrderSummary(int orderId)
        {
            using var connection = _context.CreateConnection();

            using var multi = await connection.QueryMultipleAsync(
                "sp_GetOrderSummary",
                new { OrderId = orderId },
                commandType: CommandType.StoredProcedure
            );

            var order = await multi.ReadFirstOrDefaultAsync<OrderSummaryDto>();

            if (order != null)
            {
                var items = (await multi.ReadAsync<OrderItemSummaryDto>()).ToList();
                order.Items = items;
            }

            return order;
        }
        public async Task<SalesDashboardResponseDto> GetSalesDashboard(SalesDashboardRequestDto dto)
        {
            using var connection = _context.CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@FromDate",  dto.FromDate?.ToDateTime(TimeOnly.MinValue));
            parameters.Add("@ToDate", dto.ToDate?.ToDateTime(TimeOnly.MaxValue));

            using var multi = await connection.QueryMultipleAsync(
                "sp_GetSalesDashboard",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            var summary = await multi.ReadFirstOrDefaultAsync<SalesDashboardResponseDto>();

            var orders = (await multi.ReadAsync<CompletedOrderDto>()).ToList();

            if (summary == null)
            {
                summary = new SalesDashboardResponseDto
                {
                    TotalCash = 0,
                    TotalCard = 0,
                    TotalSales = 0,
                    CompletedOrders = new List<CompletedOrderDto>()
                };
            }
            else
            {
                summary.CompletedOrders = orders;
            }

            return summary;
        }
        public async Task<List<CustomerSummaryDto>> GetCustomerSummary(CustomerSummaryRequestDto dto)
        {
            using var connection = _context.CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@FromDate", dto.FromDate?.ToDateTime(TimeOnly.MinValue));
            parameters.Add("@ToDate", dto.ToDate?.ToDateTime(TimeOnly.MaxValue));

            var result = await connection.QueryAsync<CustomerSummaryDto>(
                "sp_GetCustomerSummary",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result.ToList();
        }
        public async Task<ShopDashboardResponseDto> GetShopDashboard(ShopDashboardRequestDto dto)
        {
            using var connection = _context.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@Date", dto.Date);

            using var multi = await connection.QueryMultipleAsync(
                "sp_ShopDashboard",
                parameters,
                commandType: CommandType.StoredProcedure);
            var summary = await multi.ReadFirstOrDefaultAsync<ShopOrderSummaryDto>();
            var recentOrders = (await multi.ReadAsync<ShopRecentOrderDto>()).ToList();
            return new ShopDashboardResponseDto
            {
                Summary = summary ?? new ShopOrderSummaryDto(),
                RecentOrders = recentOrders
            };

        }
        public async Task<ShopCashflowResponsetDto> GetShopCashflow(ShopCashflowRequestDto dto)
        {
            using var connection = _context.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@FromDate", dto.FromDate);
            parameters.Add("@ToDate", dto.ToDate);

            using var multi = await connection.QueryMultipleAsync(
                "sp_ShopCashFlow",
                parameters,
                commandType: CommandType.StoredProcedure);

            var summary = await multi.ReadFirstOrDefaultAsync<ShopCashSummaryDto>();
            var partners = (await multi.ReadAsync<ShopPartnerCashDto>()).ToList();

            return new ShopCashflowResponsetDto
            {
                Summary = summary,
                Partners = partners
            };

        }
        public async Task<AdminDashboardResponseDto> GetAdminDashboard(AdminDashboardRequestDto dto)
        {
            using var connection = _context.CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@Date", dto.Date);
            parameters.Add("@RestaurantName", dto.RestaurantName);

            using var multi = await connection.QueryMultipleAsync(
                "sp_AdminDashboard",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            var summary = await multi.ReadFirstOrDefaultAsync<AdminDashboardSummaryDto>();
            var recentOrders = (await multi.ReadAsync<AdminDashboardRecentOrderDto>()).ToList();

            return new AdminDashboardResponseDto
            {
                Summary = summary ?? new AdminDashboardSummaryDto(),
                RecentOrders = recentOrders
            };
        }
        public async Task<List<AdminOrdersListResponseDto>> GetAdminOrdersList(AdminOrdersListRequestDto dto)
        {
            using var connection = _context.CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@Date", dto.Date);
            parameters.Add("@RestaurantName", dto.RestaurantName);

            var result = await connection.QueryAsync<AdminOrdersListResponseDto>(
                "sp_AdminOrdersList",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result.ToList();
        }
        public async Task<AdminCashFlowResponseDto> GetAdminCashFlow(AdminCashFlowRequestDto dto)
        {
            using var connection = _context.CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@Date", dto.Date);

            using var multi = await connection.QueryMultipleAsync(
                "sp_AdminCashFlow",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            var summary = await multi.ReadFirstOrDefaultAsync<AdminCashFlowSummaryDto>();
            var restaurants = (await multi.ReadAsync<RestaurantCashFlowDto>()).ToList();

            return new AdminCashFlowResponseDto
            {
                Summary = summary ?? new AdminCashFlowSummaryDto(),
                Restaurants = restaurants
            };
        }
    }
}
