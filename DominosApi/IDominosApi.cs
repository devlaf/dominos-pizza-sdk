using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace DominosApi
{
	public interface IDominosApi
	{
		Task<LocationQueryResponse> SearchLocations(Address deliveryAddress);

		Task<StoreDetailsQueryResponse> GetStoreDetails(int StoreID);

		Task<MenuQueryResponse> GetMenu(int StoreID);

		Task<OrderResponse> PriceOrder(Order order);

		Task<OrderResponse> PlaceOrder(Order order, List<Payment> payments, List<Coupon> coupons = null);

		Task<List<OrderStatus>> TrackOrder(string phoneNumber);
	}

	/// <summary>
	/// An exception that represents a generic HTTP error returned in the response 
	/// from the dominos platform, with a non-200 status.
	/// </summary>
	[Serializable]
	public class RestRequestFailureException : Exception
	{
		public RestRequestFailureException(string message) 
			: base(message) { }
		public RestRequestFailureException(string message, Exception innerException) 
			: base(message, innerException) { }
		protected RestRequestFailureException(SerializationInfo info, StreamingContext ctxt) 
			: base(info, ctxt) { }
	}

	/// <summary>
	/// A dominos-specific error message, returned as part of the body of a sucessful (200)
	/// HTTP request.
	/// </summary>
	/// <remarks>
	/// The API seems to split its error handling between standard REST-style HTTP responses
	/// and this custom error payload within a successful request.  I am not sure exactly 
	/// why it is done this way - I'm sure there was some good reason - and this requires
	/// us to make sure client-side that we are checking in two places for errors.
	/// </remarks>
	[Serializable]
	public class DominosException : Exception
	{
		public DominosException(string message) 
			: base(message) { }
		public DominosException(string message, Exception innerException) 
			: base(message, innerException) { }
		protected DominosException(SerializationInfo info, StreamingContext ctxt) 
			: base(info, ctxt) { }
	}
}

