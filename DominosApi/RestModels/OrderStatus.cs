using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace DominosApi
{
	public enum OrderLocation
	{
		[EnumMember(Value = "MakeLine")]
		MakeLine,
		[EnumMember(Value = "Oven")]
		Oven,
		[EnumMember(Value = "Routing Station")]
		RoutingStation,
		[EnumMember(Value = "Out The Door")]
		OutTheDoor,
		[EnumMember(Value = "Complete")]
		Complete
	}

	[JsonObject]
	public class OrderStatus
	{
		private OrderStatus() { }

		[JsonProperty("OrderStatus")]
		public OrderLocation Status { get; private set; }

		public string Version { get; private set; }
		public string AsOfTime { get; private set; }
		public string StoreID { get; private set; }
		public string OrderID { get; private set; }
		public string Phone { get; private set; }
		public string ServiceMethod { get; private set; }
		public string OrderDescription { get; private set; }
		public string OrderTakeCompleteTime { get; private set; }
		public string OrderSourceCode { get; private set; }
		public string StartTime { get; private set; }
		public string MakeTimeSecs { get; private set; }
		public string OvenTime { get; private set; }
		public string OvenTimeSecs { get; private set; }
		public string RackTime { get; private set; }
		public string RackTimeSecs { get; private set; }
		public string RouteTime { get; private set; }
		public string DriverID { get; private set; }
		public string DriverName { get; private set; }
		public string OrderDeliveryTimeSecs { get; private set; }
		public string DeliveryTime { get; private set; }
		public string OrderKey { get; private set; }
		public string ManagerID { get; private set; }
		public string ManagerName { get; private set; }
	}
}

