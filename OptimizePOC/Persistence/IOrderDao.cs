using OptimizePOC.Models;

namespace OptimizePOC.Persistence
{
    /// <summary>
    /// The Order DAO interface.
    /// </summary>
    public interface IOrderDao : IDao<Order, int>, ISupportsSaveDao<Order, int>, ISupportsDeleteDao<Order>
    {
    }
}