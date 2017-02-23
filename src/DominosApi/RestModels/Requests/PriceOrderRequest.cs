using System;
using Newtonsoft.Json;

namespace DominosApi
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

