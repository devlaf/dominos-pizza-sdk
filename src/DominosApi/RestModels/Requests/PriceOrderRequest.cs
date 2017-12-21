using Newtonsoft.Json;

namespace DominosApi.RestModels.Requests
{
    [JsonObject]
    public class PriceOrderRequest
    {
        [JsonConstructor]
        private PriceOrderRequest() { }

        public PriceOrderRequest(Order order)
        {
            Order = order;
        }

        public Order Order { get; private set; }
    }
}

