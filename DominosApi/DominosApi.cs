using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;

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
		/// <summary>
		/// A client may attach a logging delegate to keep track of any errors that occur while making requests.
		/// </summary>
		/// <remarks>
		/// This method does not do any sort of pooling/threading/batching/etc.  A client should either implement
		/// that on their own or otherwise make sure that the log function doesn't take a long time.  Additionally,
		/// the onus is on the client to ensure thread-safety.
		/// </remarks>
		public Action<string> LogError 
		{ 
			get 
			{ 
				return _LogError ?? new Action<string>((x) => { return; });
			}
			set{ _LogError = value;	}
		}

		private Action<string> _LogError = null;
		#endregion

		public async Task<LocationQueryResponse> SearchLocations(Address deliveryAddress)
		{
			string requestURI = string.Format(URI.LocationQueryURI, deliveryAddress.Street, 
				deliveryAddress.City, deliveryAddress.State, deliveryAddress.PostalCode, "Delivery");

			var request = RestUtils.PackageRestRequest(requestURI, Method.GET);

			return await RestUtils.SendRestRequest<LocationQueryResponse>(_client, request);
		}

		public async Task<StoreDetailsQueryResponse> GetStoreDetails(int StoreID)
		{
			string requestURI = string.Format(URI.StoreDetailsQueryURI, StoreID);

			var request = RestUtils.PackageRestRequest(requestURI, Method.GET);

			return await RestUtils.SendRestRequest<StoreDetailsQueryResponse>(_client, request);
		}

		public async Task<MenuQueryResponse> GetMenu(int StoreID)
		{
			string requestURI = string.Format(URI.MenuQueryURI, StoreID, "en");

			var request = RestUtils.PackageRestRequest(requestURI, Method.GET);

			return await RestUtils.SendRestRequest<MenuQueryResponse>(_client, request);
		}

		public async Task<OrderResponse> PriceOrder(Order order)
		{
			var request = RestUtils.PackageRestRequest(URI.PriceOrderURI, Method.POST, 
				new PriceOrderRequest(order), _orderHeaders);

			var response = await RestUtils.SendRestRequest<OrderResponse>(_client, request);
			ThrowOnInvalidDominosStatus(response);
			return response;
		}

		public async Task<OrderResponse> PlaceOrder(Order order, List<Payment> payments)
		{
			var requestBodyInnerObj = new OrderWithPayment(order, payments);

			var request = RestUtils.PackageRestRequest(URI.PlaceOrderURI, Method.POST, 
				new PlaceOrderRequest(requestBodyInnerObj), _orderHeaders);

			var response = await RestUtils.SendRestRequest<OrderResponse>(_client, request);
			ThrowOnInvalidDominosStatus(response);
			return response;
		}

		public async Task<List<OrderStatus>> TrackOrder(string phoneNumber)
		{
			string requestURI = string.Format(URI.TrackingOrderURI, phoneNumber);

			var request = RestUtils.PackageRestRequest(requestURI, Method.GET);

			var resp =  await RestUtils.SendRestRequest<SoapTrackingResponse>(
				_trackingClient, request, RestUtils.ResponseBodyType.XML);

			var status = resp.Envelope.Body.TrackingResponse.OrderStatuses.OrderStatus;

			return new List<OrderStatus>(new OrderStatus[] { status });
		}

		private static void ThrowOnInvalidDominosStatus(OrderResponse response)
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

				throw new DominosException(string.Format("Request failed with status {0}, Code(s): [{1}]",
					response.Status, statusCodes ?? "unknown"));
			}
		}
	}
}

