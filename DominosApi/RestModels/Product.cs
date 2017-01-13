using System;
using Newtonsoft.Json;

namespace DominosApi
{
    [JsonObject]
    public class Product
    {
        [JsonConstructor]
        private Product() { }

        public Product(string code, int quantity, dynamic options = null)
        {
            Code = code;
            Quantity = quantity;
            IsNew = true;
            Options = options ?? new OptionDefault("1");
        }

        public string Code { get; private set; }

        [JsonProperty("Qty")]
        public int Quantity { get; private set; }

        public OptionDefault Options { get; private set; }

        [JsonProperty("isNew")]
        public bool IsNew { get; private set; }
    }

    [JsonObject]
    public class OptionDefault
    {
        [JsonConstructor]
        private OptionDefault() { }

        internal OptionDefault(string optValue)
        {
            C = new OptionDefaultSubType(optValue);
            X = new OptionDefaultSubType(optValue);
        }

        public OptionDefaultSubType C { get; private set; }

        public OptionDefaultSubType X { get; private set; }
    }

    [JsonObject]
    public class OptionDefaultSubType
    {
        [JsonConstructor]
        private OptionDefaultSubType() { }

        public OptionDefaultSubType(string value)
        {
            opt = value;
        }

        [JsonProperty("1/1")]
        public string opt { get; private set; }
    }
}

