using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Providers;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web.DynamicData;
using OptimizePOC.Models;

namespace OptimizePOC
{
    public class OptimizeDataSource : IUpdatable
    {
        private object _lock = new object();

        private static List<Order> _orders;

        private static List<Shipment> _shipments;

        private OptimizeDataManager optimizeDataManager;

        public OptimizeDataSource()
        {
            optimizeDataManager = OptimizeDataManager.GetInstance();

            var processProjection = Process.GetProcesses().Take(5);
            if (_orders == null)
            {
                _orders = (from p in processProjection
                    select new Order()
                    {
                        Id = p.Id,
                        Name = p.ProcessName
                    }).ToList();
            }

            if (_shipments == null)
            {
                _shipments = (from p in processProjection
                    select new Shipment()
                    {
                        Id = p.Id,
                        Name = p.ProcessName
                    }).ToList();
            }
        }

        public IQueryable<Order> Orders
        {
            get => optimizeDataManager.OrderDao.FindAll().AsQueryable();
            set => _orders = new List<Order>(value);
        }

        public IQueryable<Shipment> Shipments
        {
            get => optimizeDataManager.ShipmentDao.FindAll().AsQueryable();
            set => _shipments = new List<Shipment>(value);
        }

        private void AddResource(object resourceToAdd, Type resourceType)
        {
            if (resourceType == typeof(Order))
            {
                _orders.Add((Order)resourceToAdd);
            }
            else if (resourceType == typeof(Shipment))
            {
                _shipments.Add((Shipment)resourceToAdd);
            }
        }

        private void RemoveResource(object resourceToRemove, Type resourceType)
        {
            if (resourceType == typeof(Order))
            {
                _orders.Remove((Order)resourceToRemove);
            }
            else if (resourceType == typeof(Shipment))
            {
                _shipments.Remove((Shipment)resourceToRemove);
            }
        }

        // Creates an object in the container.
        object IUpdatable.CreateResource(string containerName, string fullTypeName)
        {
            // create the object using reflection
            var objType = Type.GetType(fullTypeName);
            var resourceToAdd = Activator.CreateInstance(objType);
            AddResource(resourceToAdd, objType);
            return resourceToAdd;
        }

        // Gets the object referenced by the resource.
        object IUpdatable.GetResource(IQueryable query, string fullTypeName)
        {
            object resource = query.Cast<object>().SingleOrDefault();

            // fullTypeName can be null for deletes
            if (fullTypeName != null && resource.GetType().FullName != fullTypeName)
                throw new ApplicationException("Unexpected type for this resource.");
            return resource;
        }


        // Resets the value of the object to its default value.
        object IUpdatable.ResetResource(object resource)
        {
            Type t = resource.GetType();
            object dummyResource = Activator.CreateInstance(t);
            resource = dummyResource;
            return resource;
        }

        // Sets the value of the given property on the object.
        void IUpdatable.SetValue(object targetResource, string propertyName, object propertyValue)
        {
            // get the property info using reflection
            Type targetType = targetResource.GetType();
            PropertyInfo property = targetType.GetProperty(propertyName);
            
            // set the property value
            property.SetValue(targetResource, propertyValue, null);
        }

        // Gets the value of a property on an object.
        object IUpdatable.GetValue(object targetResource, string propertyName)
        {
            // get the property info using reflection
            var targetType = targetResource.GetType();
            var targetProperty = targetType.GetProperty(propertyName);

            // retrun the value of the property
            return targetProperty.GetValue(targetResource, null);
        }

        // Sets the related object for a reference.
        void IUpdatable.SetReference(
            object targetResource, string propertyName, object propertyValue)
        {
            ((IUpdatable)this).SetValue(targetResource, propertyName, propertyValue);
        }

        // Adds the object to the related objects collection.
        void IUpdatable.AddReferenceToCollection(
            object targetResource, string propertyName, object resourceToBeAdded)
        {
            PropertyInfo pi = targetResource.GetType().GetProperty(propertyName);
            if (pi == null)
                throw new Exception("Can't find property");
            IList collection = (IList)pi.GetValue(targetResource, null);
            collection.Add(resourceToBeAdded);
        }

        // Removes the object from the related objects collection.
        void IUpdatable.RemoveReferenceFromCollection(
            object targetResource, string propertyName, object resourceToBeRemoved)
        {
            PropertyInfo pi = targetResource.GetType().GetProperty(propertyName);
            if (pi == null)
                throw new Exception("Can't find property");
            IList collection = (IList)pi.GetValue(targetResource, null);
            collection.Remove(resourceToBeRemoved);
        }

        // Deletes the resource.
        void IUpdatable.DeleteResource(object targetResource)
        {
            // create the object using reflection
            var objType = targetResource.GetType();
            RemoveResource(targetResource, objType);
        }

        // Saves all the pending changes.
        void IUpdatable.SaveChanges()
        {
            // object in memory – do nothing
        }

        // Returns the actual instance of the resource represented 
        // by the resource object.
        object IUpdatable.ResolveResource(object resource)
        {
            return resource;
        }

        // Reverts all the pending changes.
        void IUpdatable.ClearChanges()
        {
            // Raise an exception as there is no real way to do this with LINQ to SQL.
            // Comment out the following line if you'd prefer a silent failure
            throw new NotSupportedException();
        }


        
        public Order Get(int id)
        {
            return Find(id);
        }

        public Order Find(int id)
        {
            return Orders.FirstOrDefault(o => o.Id == id);
        }

        //public Order Add(Order newOrder)
        //{
        //    if (newOrder == null)
        //    {
        //        throw new ArgumentException("Order cannot be null");
        //    }

        //    lock (_lock)
        //    {
        //        newOrder.Id = Orders.Max(o => o.Id) + 1;
        //        _orders.Add(newOrder);
        //    }

        //    return newOrder;
        //}

        //public bool Remove(int id)
        //{
        //    int deleted = _orders.RemoveAll(o => o.Id == id);
        //    return deleted > 0;
        //}

        //public Order Save(Order updatedOrder)
        //{
        //    if (updatedOrder == null)
        //    {
        //        throw new ArgumentException("Order cannot be null");
        //    }

        //    Order existingOrderModel = null;

        //    lock (_lock)
        //    {
        //        existingOrderModel = Orders.FirstOrDefault(o => o.Id == updatedOrder.Id);
        //        if (existingOrderModel != null)
        //        {
        //            existingOrderModel.Name = updatedOrder.Name;
        //            return existingOrderModel;
        //        }

        //        throw new ArgumentException("Order was not found");
        //    }
        //}
    }
}