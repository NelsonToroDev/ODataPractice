using System.Diagnostics;
using System.Linq;

namespace OptimizePOC
{
    public class OrdersDataModel
    {
        public OrdersDataModel()
        {
            var processProjection = from p in Process.GetProcesses()
                                    select new OrderModel()
                                    {
                                        Id = p.Id,
                                        Name = p.ProcessName
                                    };
            Orders = processProjection.AsQueryable();
        }

        public IQueryable<OrderModel> Orders { get; set; }
    }
}