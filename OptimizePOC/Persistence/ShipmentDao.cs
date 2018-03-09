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
    /// The Shipment DAO.
    /// </summary>
    [Repository]
    public class ShipmentDao : HibernateDao, IShipmentDao
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Finds by primary id.
        /// </summary>
        /// <param name="id">
        /// The id to find the Shipment.
        /// </param>
        /// <returns>
        /// The Shipment found.
        /// </returns>
        [Transaction(TransactionPropagation.Supports, ReadOnly = true)]
        public Shipment FindByPrimaryId(int id)
        {
            return this.CurrentSession.Get<Shipment>(id);
        }

        /// <summary>
        /// Finds all.
        /// </summary>
        /// <returns>
        /// The Shipments found.
        /// </returns>
        [Transaction(TransactionPropagation.Supports, ReadOnly = true)]
        public IList<Shipment> FindAll()
        {
            return this.FindAll<Shipment>();
        }

        /// <summary>
        /// Creates the entity.
        /// </summary>
        /// <param name="Shipment">
        /// The new Shipment.
        /// </param>
        /// <returns>
        /// The created id.
        /// </returns>
        [Transaction(TransactionPropagation.Required)]
        public int Create(Shipment Shipment)
        {
            return (int)this.CurrentSession.Save(Shipment);
        }

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <param name="modifiedShipment">
        /// The entity.
        /// </param>
        [Transaction(TransactionPropagation.Required)]
        public void Update(Shipment modifiedShipment)
        {
            this.CurrentSession.Merge(modifiedShipment);
        }

        /// <summary>
        /// Deletes the entity.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        [Transaction(TransactionPropagation.Required)]
        public void Delete(Shipment entity)
        {
            Shipment Shipment = this.CurrentSession.Load<Shipment>(entity.Id);
            this.CurrentSession.Delete(Shipment);
        }
    }
}