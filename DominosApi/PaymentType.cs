using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DominosApi
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum PaymentType
	{
		Cash,
		GiftCard,
		CreditCard
	}

	[JsonConverter(typeof(StringEnumConverter))]
	public enum CreditCardType
	{
		[EnumMember(Value = "American Express")]
		AmericanExpress,

		[EnumMember(Value = "Discover Card")]
		DiscoverCard,

		[EnumMember(Value = "MasterCard")]
		MasterCard,

		[EnumMember(Value = "Optima")]
		Optima,

		[EnumMember(Value = "Visa")]
		Visa
	}
}

