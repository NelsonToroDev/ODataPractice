using System.Data.Services.Common;

namespace OptimizePOC.Models
{
    [DataServiceKey("Name")]
    public class OrderModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    [DataServiceKey("Name")]
    public class Ints
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}