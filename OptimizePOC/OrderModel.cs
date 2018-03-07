using System.Data.Services.Common;

namespace OptimizePOC
{
    [DataServiceKey("Name")]
    public class OrderModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}