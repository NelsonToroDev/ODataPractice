using NHibernate;
using NHibernate.Context;
using OptimizePOC.Persistence;
using Spring.Context;
using Spring.Context.Support;
using Spring.Data.NHibernate;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OptimizePOC
{
    public class OptimizeDataManager
    {
        /// <summary>
        /// Spring.NET application context.
        /// </summary>
        private IApplicationContext context;

        private static OptimizeDataManager _instance;

        private static LocalSessionFactoryObject sessionFactoryObject;

        private static ISessionFactory sessionFactory;

        private OptimizeDataManager()
        {
            this.context = ContextRegistry.GetContext();
            sessionFactory = (ISessionFactory)this.context.GetObject("sessionFactory");
            //GetCurrentSession();
            var session = sessionFactory.OpenSession();
            GenerateSchema();
        }

        public static OptimizeDataManager GetInstance()
        {
            if(_instance == null)
            {
                _instance = new OptimizeDataManager();
            }

            return _instance;
        }

        public void GenerateSchema()
        {
            //AppLoader.MainAppLoader.FakeMethod();
            IDictionary<string, object> sessionFactoryObjects =
                this.context.GetObjectsOfType(typeof(LocalSessionFactoryObject), false, true);

            // the '&' depends on Spring .Net
            sessionFactoryObject =
                ((LocalSessionFactoryObject)sessionFactoryObjects["&sessionFactory"]);
            //ISessionFactory sessionFactory = (ISessionFactory)sessionFactoryObject;
            //sessionFactory.OpenSession();
            
            sessionFactoryObject.CreateDatabaseSchema();

            //if (sessionFactoryObjects.Count > 1)
            //{
            //    Assert.Inconclusive("Only one single object of type '{0}' was expected but '{1}' were found",
            //                        typeof(LocalSessionFactoryObject), sessionFactoryObjects.Count);
            //}
        }

        public static ISession GetCurrentSession()
        {
            if (CurrentSessionContext.HasBind(sessionFactory))
            {
                return sessionFactory.GetCurrentSession();
            }

            var session = sessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);
            return session;
        }

        //public void DropSchema()
        //{
        //    IDictionary sessionFactoryObjects =
        //        GetContext(ContextKey).GetObjectsOfType(typeof(LocalSessionFactoryObject), false, true);

        //    // the '&' depends on Spring .Net
        //    LocalSessionFactoryObject sessionFactoryObject =
        //        ((LocalSessionFactoryObject)sessionFactoryObjects["&sessionFactory"]);
        //    sessionFactoryObject.DropDatabaseSchema();

        //    if (sessionFactoryObjects.Count > 1)
        //    {
        //        Assert.Inconclusive("Only one single object of type '{0}' was expected but '{1}' were found",
        //                            typeof(LocalSessionFactoryObject), sessionFactoryObjects.Count);
        //    }
        //}

        public IOrderDao OrderDao => (IOrderDao)this.context.GetObject("orderDao");

        public IShipmentDao ShipmentDao => (IShipmentDao)this.context.GetObject("shipmentDao");
    }
}