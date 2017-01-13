using System;
using Newtonsoft.Json;

namespace DominosApi
{
    [JsonObject]
    public class Store
    {
        protected Store() { }

        public int? StoreID { get; private set; }
        public bool? AllowCarryOutOrders { get; private set; }
        public bool? AllowDeliveryOrders { get; private set; }
        public bool? IsDeliveryStore { get; private set; }
        public bool? IsNEONow { get; private set; }
        public bool? IsOnlineCapable { get; private set; }
        public bool? IsOnlineNow { get; private set; }
        public bool? IsOpen { get; private set; }
        public bool? IsSpanish { get; private set; }
        public double? MaxDistance { get; private set; }
        public double? MinDistance { get; private set; }
        public string LocationInfo { get; private set; }
        public string HolidayDescription { get; private set; }
        public string HoursDescription { get; private set; }
        public string AddressDescription { get; private set; }
        public string Phone { get; private set; }
        public ServiceHoursDescription ServiceHoursDescription { get; private set; }
        public ServiceIsOpen ServiceIsOpen { get; private set; }
        public LanguageLocationInfo LanguageLocationInfo { get; private set; }
    }

    public class ServiceHoursDescription
    {
        public string CarryOut { get; private set; }
        public string Delivery { get; private set; }
    }

    public class ServiceIsOpen
    {
        public bool? CarryOut { get; private set; }
        public bool? Delivery { get; private set; }
    }

    public class LanguageLocationInfo
    {
        public string es { get; private set; }
    }
}

