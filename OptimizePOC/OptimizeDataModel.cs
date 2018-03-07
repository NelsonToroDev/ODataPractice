using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using OptimizePOC.Models;

namespace OptimizePOC
{
    public class OptimizeDataModel
    {
        private object _lock = new object();

        private static List<OrderModel> _orders = new List<OrderModel>();

        public OptimizeDataModel()
        {
            var processProjection = from p in Process.GetProcesses()
                                    select new OrderModel()
                                    {
                                        Id = p.Id,
                                        Name = p.ProcessName
                                    };
            Orders = processProjection.AsQueryable();
        }

        public IQueryable<OrderModel> Orders
        {
            get
            ;
            set
            ;
        }


        public IQueryable<Ints> Ints { get; set; }

        public OrderModel Get(int id)
        {
            return Find(id);
        }

        public OrderModel Find(int id)
        {
            return Orders.FirstOrDefault(o => o.Id == id);
        }

        //public OrderModel Add(OrderModel newOrder)
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

        //public OrderModel Save(OrderModel updatedOrder)
        //{
        //    if (updatedOrder == null)
        //    {
        //        throw new ArgumentException("Order cannot be null");
        //    }

        //    OrderModel existingOrderModel = null;

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