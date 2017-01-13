using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DominosApi
{
	[JsonObject]
	public class OrderWithPayment : Order
	{
		public OrderWithPayment(Customer customer, string storeID, List<Product> products, List<Payment> payments, List<Coupon> coupons) 
			: base (customer, storeID, products, coupons)
		{
			Payments = payments;
		}

		public OrderWithPayment(Order order, List<Payment> payments, List<Coupon> coupons) 
			: base (new Customer(order.FirstName, order.LastName, order.Address, order.Email, 
				order.Phone, order.Extension), order.StoreID, order.Products, coupons)
		{
			Payments = payments;
		}

		public List<Payment> Payments { get; private set; }
	}
}

