using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DominosApi
{
	[JsonObject]
	public class PlaceOrderRequest
	{
		[JsonConstructor]
		private PlaceOrderRequest() { }

		public PlaceOrderRequest(OrderWithPayment order)
		{
			Order = order;
		}

		public OrderWithPayment Order { get; private set; }
	}
}

