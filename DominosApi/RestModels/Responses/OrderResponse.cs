using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DominosApi
{
	[JsonObject]
	public class OrderResponse
	{
		[JsonConstructor]
		private OrderResponse() { }

		public int Status { get; private set; }

		public PricedOrder Order { get; private set; }
	}

	[JsonObject]
	public class PricedOrder : ReturnedOrder
	{
		public string EstimatedWaitMinutes { get; private set; }
		public string PriceOrderTime { get; private set; }
		public string StoreOrderId { get; private set;}
		public AmountBreakdown Amounts { get; private set; }
	}

	[JsonObject]
	public class AmountBreakdown
	{
		private AmountBreakdown() { }

		public decimal? Menu { get; private set; }
		public decimal? Discount { get; private set; }
		public decimal? Surcharge { get; private set; }
		public decimal? Adjustment { get; private set; }
		public decimal? Net { get; private set; }
		public decimal? Tax { get; private set; }
		public decimal? Tax1 { get; private set; }
		public decimal? Tax2 { get; private set; }
		public decimal? Bottle { get; private set; }
		public decimal? Customer { get; private set; }
		public decimal? Payment { get; private set; }
	}
}

