using System;
using Newtonsoft.Json;

namespace DominosApi
{
	[JsonObject]
	public class Coupon
	{
		[JsonConstructor]
		private Coupon() { }

		public Coupon(string code, int quantity)
		{
			Code = code;
			Quantity = quantity;


		}
        [JsonProperty("Code")]
        public string Code { get; private set; }

		[JsonProperty("Qty")]
		public int Quantity { get; private set; }

	}

}

