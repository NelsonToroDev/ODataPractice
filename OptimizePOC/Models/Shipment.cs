using System.Data.Services.Common;
using NHibernate.Mapping.Attributes;

namespace OptimizePOC.Models
{
    [DataServiceKey("Id")]
    [Class]
    public class Shipment
    {
        [Id(Name = "Id")]
        [Generator(1, Class = "Identity")]
        public int Id { get; set; }

        [Property]
        public string Name { get; set; }
    }
}