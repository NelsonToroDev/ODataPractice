namespace OptimizePOC.Persistence
{
    using System.Collections.Generic;
    using global::NHibernate;
    using Spring.Stereotype;

    /// <summary>
    /// The hibernate DAO.
    /// </summary>
    [Repository]
    public abstract class HibernateDao
    {
        /// <summary>
        /// The session factory.
        /// </summary>
        private ISessionFactory sessionFactory;

        /// <summary>
        /// Gets or sets SessionFactory.
        /// </summary>
        public ISessionFactory SessionFactory
        {
            protected get { return this.sessionFactory; }
            set { this.sessionFactory = value; }
        }

        /// <summary>
        /// Gets the current active session.
        /// </summary>
        protected ISession CurrentSession
        {
            get { return this.SessionFactory.GetCurrentSession(); }
        }

        /// <summary>
        /// Finds all.
        /// </summary>
        /// <typeparam name="T">
        /// The entity type.
        /// </typeparam>
        /// <returns>
        /// The entities found.
        /// </returns>
        protected IList<T> FindAll<T>() where T : class
        {
            ICriteria criteria = this.CurrentSession.CreateCriteria<T>();
            return criteria.List<T>();
        }
    }
}