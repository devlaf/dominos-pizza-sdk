using System.Collections.Generic;
using Newtonsoft.Json;

namespace DominosApi.RestModels
{
    [JsonObject]
    public class Order
    {
        [JsonConstructor]
        protected Order() { }

        public Order(Customer customer, string storeID, List<Product> products, List<Coupon> coupons)
        {
            Address = customer.Address;
            Email = customer.Email;
            FirstName = customer.FirstName;
            LastName = customer.LastName;
            Phone = customer.Phone;
            Extension = customer.Extension;
            StoreID = storeID;
            Products = products;
            Coupons = coupons;

            PhonePrefix = string.Empty;
            OrderID = string.Empty;		// TODO: May need to change this, not sure what it does.
            LanguageCode = "en";
            OrderChannel = "OLO";
            OrderMethod = "Web";
            ServiceMethod = "Delivery";
            SourceOrganizationURI = "order.dominos.com";
            Version = "1.0";
            NoCombine = true;
            NewUser = true;

            Tags = new OrderTags();
            Tags.KillPOE = true;
        }

        public Address Address { get; private set; }
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string LanguageCode { get; private set; }
        public string OrderChannel { get; private set; }
        public string OrderMethod { get; private set; }
        public string OrderTaker { get; private set; }
        public string OrderID { get; private set; }
        public string Phone { get; private set; }
        public string PhonePrefix { get; private set; }
        public string Extension { get; private set; }
        public string ServiceMethod { get; private set; }
        public string SourceOrganizationURI { get; private set; }
        public string StoreID { get; private set; }
        public string Version { get; private set; }
        public bool? NoCombine { get; private set; }
        public bool? NewUser { get; private set; }
        public OrderTags Tags { get; private set; }
        public List<Product> Products { get; private set; }
        public List<Coupon> Coupons { get; private set; }
    }

    [JsonObject]
    public class OrderTags
    {
        public bool KillPOE { get; set; } // Should be set to true, no idea what this does...
    }
}

