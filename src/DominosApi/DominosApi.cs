using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;

namespace DominosApi
{
    public class DominosApi : IDominosApi
    {
        private RestClient _client = new RestClient(URI.DominosBaseURL);
        private RestClient _trackingClient = new RestClient(URI.TrackingBaseURL);

        private Dictionary<string, string> _orderHeaders = new Dictionary<string, string> {
            { "Accept", "application/json" },
            { "Referer", "https://order.dominos.com/en/pages/order/" },
            { "Content-Type", "application/json" }
        };

        #region (Optional) Logger

        public Action<string> LogError { 
            get { return _LogError ?? new Action<string>((x) => { return; }); }
            set { _LogError = value; }
        }

        private Action<string> _LogError = null;

        #endregion

        public async Task<LocationQueryResponse> SearchLocations(Address deliveryAddress)
        {
            string requestURI = string.Format(URI.LocationQueryURI, deliveryAddress.Street, 
                           deliveryAddress.City, deliveryAddress.State, deliveryAddress.PostalCode, "Delivery");

            var request = RestUtils.PackageRestRequest(requestURI, Method.GET);

            return await RestUtils.SendRestRequest<LocationQueryResponse>(_client, request, log: LogError);
        }

        public async Task<StoreDetailsQueryResponse> GetStoreDetails(int StoreID)
        {
            string requestURI = string.Format(URI.StoreDetailsQueryURI, StoreID);

            var request = RestUtils.PackageRestRequest(requestURI, Method.GET);

            return await RestUtils.SendRestRequest<StoreDetailsQueryResponse>(_client, request, log: LogError);
        }

        public async Task<MenuQueryResponse> GetMenu(int StoreID)
        {
            string requestURI = string.Format(URI.MenuQueryURI, StoreID, "en");

            var request = RestUtils.PackageRestRequest(requestURI, Method.GET);

            return await RestUtils.SendRestRequest<MenuQueryResponse>(_client, request, log: LogError);
        }

        public async Task<OrderResponse> PriceOrder(Order order)
        {
            var request = RestUtils.PackageRestRequest(URI.PriceOrderURI, Method.POST, 
                     new PriceOrderRequest(order), _orderHeaders);

            var response = await RestUtils.SendRestRequest<OrderResponse>(_client, request, log: LogError);
            ThrowOnInvalidDominosStatus(response);
            return response;
        }

        public async Task<OrderResponse> PlaceOrder(Order order, List<Payment> payments, List<Coupon> coupons = null)
        {
            var requestBodyInnerObj = new OrderWithPayment(order, payments, coupons);

            var request = RestUtils.PackageRestRequest(URI.PlaceOrderURI, Method.POST, 
                     new PlaceOrderRequest(requestBodyInnerObj), _orderHeaders);

            var response = await RestUtils.SendRestRequest<OrderResponse>(_client, request, log: LogError);
            ThrowOnInvalidDominosStatus(response);
            return response;
        }

        public async Task<List<OrderStatus>> TrackOrder(string phoneNumber)
        {
            string requestURI = string.Format(URI.TrackingOrderURI, phoneNumber);

            var request = RestUtils.PackageRestRequest(requestURI, Method.GET);

            var resp = await RestUtils.SendRestRequest<SoapTrackingResponse>(
                  _trackingClient, request, RestUtils.ResponseBodyType.XML, log: LogError);

            var status = resp.Envelope.Body.TrackingResponse.OrderStatuses.OrderStatus;

            return new List<OrderStatus>(new OrderStatus[] { status });
        }

        /// <summary>
        /// Occasionally, a request will return a HTTP 200 response code, but will still indicate
        /// that an error occured as part of the repsonse body.  We must check the returned 
        /// Status and StatusItems properties for this secondary type of error.
        /// </summary>
        private void ThrowOnInvalidDominosStatus(OrderResponse response)
        {
            if(response.Status < 0)
            {
                string statusCodes = null;

                try
                {
                    statusCodes = response.Order.StatusItems.Aggregate(string.Empty, (y, x) => 
						string.Format("{0} {{code: {1}}}", y, x));
                } 
                catch(Exception ex)
                {
                    if(!(ex is NullReferenceException))
                        throw;
                }

                string err = string.Format("Request failed with status {0}, Code(s): [{1}]",
                     response.Status, statusCodes ?? "unknown");
                LogError(err);
                throw new DominosException(err);
            }
        }
    }
}

