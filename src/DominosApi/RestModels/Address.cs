using System;
using System.Linq;
using Newtonsoft.Json;

namespace DominosApi.RestModels
{
    [JsonObject]
    public class Address
    {
        public enum UnitCategory
        {
            House,
            Apartment
        }

        [JsonConstructor]
        private Address() { }

        public Address(string address, string city, string state, string postalCode, UnitCategory type, int? unitNumber = null)
        {
            Street = address;
            City = city;
            State = state;
            PostalCode = postalCode;
            Type = Enum.GetName(typeof(UnitCategory), type);
            UnitNumber = (unitNumber == null) ? null : unitNumber.ToString();
            UnitCharacter = (unitNumber == null) ? null : "#";

            var firstWhitespaceIndex = address.TakeWhile(x => !char.IsWhiteSpace(x)).Count();
            if(firstWhitespaceIndex < address.Count())
            {
                StreetNumber = address.Substring(0, firstWhitespaceIndex);
                StreetName = address.Substring(firstWhitespaceIndex, address.Count() - firstWhitespaceIndex);
            }
        }

        [JsonProperty(PropertyName = "City")]
        public string City { get; private set; }

        [JsonProperty(PropertyName = "PostalCode")]
        public string PostalCode { get; private set; }

        [JsonProperty(PropertyName = "Region")]
        public string State { get; private set; }

        [JsonProperty(PropertyName = "Street")]
        public string Street { get; set; }

        [JsonProperty(PropertyName = "StreetName")]
        public string StreetName { get; private set; }

        [JsonProperty(PropertyName = "StreetNumber")]
        public string StreetNumber { get; private set; }

        [JsonProperty(PropertyName = "UnitNumber")]
        public string UnitNumber { get; private set; }

        [JsonProperty(PropertyName = "Type")]
        public string Type { get; private set; }

        [JsonProperty(PropertyName = "UnitType")]
        public string UnitCharacter { get; private set; }

        public override string ToString()
        {
            var retval = string.Format("{0}, {1}, {2} {3}", Street, City, State, PostalCode);

            if(UnitNumber != null)
                retval += string.Format(" Unit #{0}", UnitNumber);

            return retval;
        }
    }
}

