using System.Data.Services.Common;

namespace OptimizePOC.Models
{
    [DataServiceKey("Id")]
    public class Order
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    [DataServiceKey("Id")]
    public class Shipment
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}