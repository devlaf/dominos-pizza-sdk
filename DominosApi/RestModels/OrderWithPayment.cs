using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DominosApi
{
	[JsonObject]
	public class OrderWithPayment : Order
	{
		public OrderWithPayment(Customer customer, string storeID, List<Product> products, List<Payment> payments) 
			: base (customer, storeID, products)
		{
			Payments = payments;
		}

		public OrderWithPayment(Order order, List<Payment> payments) 
			: base (new Customer(order.FirstName, order.LastName, order.Address, order.Email, 
				order.Phone, order.Extension), order.StoreID, order.Products)
		{
			Payments = payments;
		}

		public List<Payment> Payments { get; private set; }
	}
}

