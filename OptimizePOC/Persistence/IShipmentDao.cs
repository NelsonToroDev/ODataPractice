using OptimizePOC.Models;

namespace OptimizePOC.Persistence
{
    /// <summary>
    /// The Order DAO interface.
    /// </summary>
    public interface IShipmentDao : IDao<Shipment, int>, ISupportsSaveDao<Shipment, int>, ISupportsDeleteDao<Shipment>
    {
    }
}