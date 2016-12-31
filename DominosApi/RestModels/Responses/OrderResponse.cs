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
		public AmountBreakdown Amounts { get; private set; }
	}

	[JsonObject]
	public class AmountBreakdown
	{
		private AmountBreakdown() { }

		public double? Menu { get; private set; }
		public double? Discount { get; private set; }
		public double? Surcharge { get; private set; }
		public double? Adjustment { get; private set; }
		public double? Net { get; private set; }
		public double? Tax { get; private set; }
		public double? Tax1 { get; private set; }
		public double? Tax2 { get; private set; }
		public double? Bottle { get; private set; }
		public double? Customer { get; private set; }
		public double? Payment { get; private set; }
	}
}

