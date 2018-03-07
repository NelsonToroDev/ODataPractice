using System.Data.Services;
using System.Data.Services.Common;


namespace OptimizePOC
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "OrdersService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select OrdersService.svc or OrdersService.svc.cs at the Solution Explorer and start debugging.
    public class OrdersService : DataService<OrdersDataModel>
    {
        public static void InitializeService(DataServiceConfiguration config)
        {
            config.SetEntitySetAccessRule("*", EntitySetRights.AllRead);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
        }
    }
}
