using Newtonsoft.Json;

namespace DominosApi.RestModels.Requests
{
    [JsonObject]
    public class PlaceOrderRequest
    {
        [JsonConstructor]
        private PlaceOrderRequest() { }

        public PlaceOrderRequest(OrderWithPayment order)
        {
            Order = order;
        }

        public OrderWithPayment Order { get; private set; }
    }
}

