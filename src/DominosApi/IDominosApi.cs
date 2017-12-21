using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DominosApi.RestModels;
using DominosApi.RestModels.Responses;

namespace DominosApi
{
    public interface IDominosApi
    {
        /// <summary>
        /// Retrieves a list of Dominos stores that will deliver to the provided address.
        /// </summary>
        Task<LocationQueryResponse> SearchLocations(Address deliveryAddress);

        /// <summary>
        /// Retrieves store hours, etc. for provided store.
        /// </summary>
        Task<StoreDetailsQueryResponse> GetStoreDetails(int StoreID);

        /// <summary>
        /// Retrieves menu information (product codes, etc.) available for the provided store.
        /// </summary>
        Task<MenuQueryResponse> GetMenu(int StoreID);

        /// <summary>
        /// Calculates the total price of an order.  This is the price that must be covered by the
        /// payments object provided to a subsequent PlaceOrder call.
        /// </summary>
        Task<OrderResponse> PriceOrder(Order order);

        /// <summary>
        /// Submits an order to the Dominos platform.  
        /// </summary>
        /// <remarks>
        /// The total amount you elect to pay across all of the payments in the provided list must
        /// be equal to the total cost of the order, which can be determined by a call to PriceOrder(...).
        /// Also, it is wise to look for coupons because they get you a long way with Dominos.  The
        /// item pricing doesn't make sense if you are not using their deals, and your order will 
        /// cost significantly more.
        /// </remarks>
        Task<OrderResponse> PlaceOrder(Order order, List<Payment> payments, List<Coupon> coupons = null);

        /// <summary>
        /// Gets the order status (in-the-oven, out-for-delivery, etc.) for recent orders that have been
        /// placed and are associated with the provided phone number.
        /// </summary>
        Task<List<OrderStatus>> TrackOrder(string phoneNumber);

        /// <summary>
        /// A user may attach an optional logging delegate to keep track of any errors that occur while making requests.
        /// </summary>
        /// <remarks>
        /// This method need not be wrapped internally by any sort of pooling/threading/batching/etc.  A client 
        /// should either implement that on their own or otherwise make sure that the log function doesn't take 
        /// a long time.  Additionally, the onus is on the user to ensure thread-safety.
        /// </remarks>
        Action<string> LogError { get; set; }
    }

    /// <summary>
    /// An exception that represents a generic HTTP error returned in the response 
    /// from the dominos platform, with a non-200 status.
    /// </summary>
    public class RestRequestFailureException : Exception
    {
        public RestRequestFailureException(string message) 
            : base(message) { }
        public RestRequestFailureException(string message, Exception innerException)
            : base(message, innerException) { }
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
    public class DominosException : Exception
    {
        public DominosException(string message)
            : base(message) { }
        public DominosException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}

