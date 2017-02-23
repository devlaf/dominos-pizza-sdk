using System;

namespace DominosApi
{
    public static class URI
    {
        public const string DominosBaseURL = "https://order.dominos.com";
        public const string TrackingBaseURL = "https://trkweb.dominos.com";

        public const string LocationQueryURI = "/power/store-locator?s={0}&c={1},{2},{3}&type={4}";
        public const string StoreDetailsQueryURI = "/power/store/{0}/profile";
        public const string MenuQueryURI = "/power/store/{0}/menu?lang={1}&structured=true";

        public const string ValidateOrderURI = "/power/validate-order";
        public const string PriceOrderURI = "/power/price-order";
        public const string PlaceOrderURI = "/power/place-order";

        public const string TrackingOrderURI = "/orderstorage/GetTrackerData?Phone={0}";
    }
}

