using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DominosApi
{
	/// <summary>
	/// This, and the other thin classes in this file are used to unpack the SOAP tracking response
	/// into concrete types.  It's ugly, but it works.
	/// </summary>
	[JsonObject]
	public class SoapTrackingResponse
	{
		private SoapTrackingResponse() { }

		[JsonProperty("soap:Envelope")]
		public Envelope Envelope { get; private set; }
	}

	[JsonObject]
	public class Envelope
	{
		private Envelope() { }

		[JsonProperty("soap:Body")]
		public Body Body { get; private set; }
	}

	[JsonObject]
	public class Body
	{
		private Body() { }

		[JsonProperty("GetTrackerDataResponse")]
		public TrackedOrders TrackingResponse { get; private set; }

	}

	[JsonObject]
	public class TrackedOrders
	{
		private TrackedOrders() { }

		public OrderStatusContainer OrderStatuses { get; private set; }
	}

	[JsonObject]
	public class OrderStatusContainer
	{
		public OrderStatus OrderStatus { get; private set; }
	}

}

