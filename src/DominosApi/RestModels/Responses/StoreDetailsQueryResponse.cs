using System.Collections.Generic;
using Newtonsoft.Json;

namespace DominosApi.RestModels.Responses
{
    [JsonObject]
    public class StoreDetailsQueryResponse : Store
    {
        private StoreDetailsQueryResponse() : base() { }

        public Hours ServiceHours { get; private set; }
        public int? CustomerCloseWarningMinutes { get; private set; }
        public List<PaymentType> AcceptablePaymentTypes { get; private set; }
        public List<CreditCardType> AcceptableCreditCards { get; private set; }
        public int? MinimumDeliveryOrderAmount { get; private set; }
        public List<string> AcceptableWalletTypes { get; private set; }
        public Dictionary<string, string> SocialReviewLinks { get; private set; }
        public bool? IsAVSEnabled { get; private set; }
        public bool? AllowDineInOrders { get; private set; }

        [JsonProperty("ServiceMethodEstimatedWaitMinutes")]
        public EstimatedWaitTime EstimatedWaitInMinutes { get; private set; }

    }

    public class Hours
    {
        private Hours() { }

        public WeeklyHourListing CarryOut { get; private set; }
        public WeeklyHourListing Delivery { get; private set; }
    }

    public class WeeklyHourListing
    {
        public WeeklyHourListing() { }

        public List<DailyHourListing> Sun { get; private set; }
        public List<DailyHourListing> Mon { get; private set; }
        public List<DailyHourListing> Tue { get; private set; }
        public List<DailyHourListing> Wed { get; private set; }
        public List<DailyHourListing> Thu { get; private set; }
        public List<DailyHourListing> Fri { get; private set; }
        public List<DailyHourListing> Sat { get; private set; }
    }

    public class DailyHourListing
    {
        private DailyHourListing() { }

        public string OpenTime { get; private set; }
        public string CloseTime { get; private set; }
    }

    public class EstimatedWaitTime
    {
        public WaitTimeBoundary Delivery { get; private set; }
        public WaitTimeBoundary Carryout { get; private set; }

        public class WaitTimeBoundary
        {
            [JsonProperty("Min")]
            public int MinInMinutes { get; private set; }

            [JsonProperty("Max")]
            public int MaxInMinutes { get; private set; }
        }

    }
}

