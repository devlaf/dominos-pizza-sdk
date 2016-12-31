using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DominosApi
{
	/// <summary>
	/// When an order object is returned in the response to a place/price request from 
	/// Dominos, they add some extra crap.  This represents that version of the order object.
	/// </summary>
	[JsonObject]
	public class ReturnedOrder : Order
	{
		protected ReturnedOrder() { }

		public string IP { get; private set; }
		public string Market { get; private set; }
		public string Currency { get; private set; }
		public int Status { get; private set; }
		public List<ErrorCode> StatusItems { get; private set; }
	}

	[JsonObject]
	public class ErrorCode
	{
		private ErrorCode() { }
		public string Code { get; private set; }
	}
}

