using System.Collections.Generic;
using System.Reflection;
using Common.Logging;
using Spring.Stereotype;
using Spring.Transaction;
using Spring.Transaction.Interceptor;
using OptimizePOC.Models;

namespace OptimizePOC.Persistence
{
    /// <summary>
    /// The Order DAO.
    /// </summary>
    [Repository]
    public class OrderDao : HibernateDao, IOrderDao
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Finds by primary id.
        /// </summary>
        /// <param name="id">
        /// The id to find the Order.
        /// </param>
        /// <returns>
        /// The Order found.
        /// </returns>
        [Transaction(TransactionPropagation.Supports, ReadOnly = true)]
        public Order FindByPrimaryId(int id)
        {
            return this.CurrentSession.Get<Order>(id);
        }

        /// <summary>
        /// Finds all.
        /// </summary>
        /// <returns>
        /// The Orders found.
        /// </returns>
        [Transaction(TransactionPropagation.Supports, ReadOnly = true)]
        public IList<Order> FindAll()
        {
            return this.FindAll<Order>();
        }

        /// <summary>
        /// Creates the entity.
        /// </summary>
        /// <param name="Order">
        /// The new Order.
        /// </param>
        /// <returns>
        /// The created id.
        /// </returns>
        [Transaction(TransactionPropagation.Required)]
        public int Create(Order Order)
        {
            return (int)this.CurrentSession.Save(Order);
        }

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <param name="modifiedOrder">
        /// The entity.
        /// </param>
        [Transaction(TransactionPropagation.Required)]
        public void Update(Order modifiedOrder)
        {
            this.CurrentSession.Merge(modifiedOrder);
        }

        /// <summary>
        /// Deletes the entity.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        [Transaction(TransactionPropagation.Required)]
        public void Delete(Order entity)
        {
            Order Order = this.CurrentSession.Load<Order>(entity.Id);
            this.CurrentSession.Delete(Order);
        }
    }
}