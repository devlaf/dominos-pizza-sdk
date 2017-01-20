using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DominosApi
{
    [JsonObject]
    public class Product
    {
        [JsonConstructor]
        private Product() { }

        public Product(string code, int quantity, string[] ToppingOptionkeys)
        {
            Code = code;
            Quantity = quantity;
            IsNew = true;
            Options = new JObject();
            //Console.WriteLine(ToppingOptionkeys);
            if (ToppingOptionkeys.Length > 0)
            {
                //Console.WriteLine("Build options");
                foreach (string type in ToppingOptionkeys)
                {
                    //Console.WriteLine(type);
                    JObject subtype = new JObject();
                    subtype["1/1"] = "1";
                    Console.WriteLine(subtype);
                    Options[type] = subtype;
                }
                //Console.WriteLine(Options);
                //Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(Options));
            }

            //Console.WriteLine(Options);




        }

        public Product(string code, int quantity)
        {
            Code = code;
            Quantity = quantity;
            IsNew = true;
            Options = new JObject();
        }


        public string Code { get; private set; }

        [JsonProperty("Qty")]
        public int Quantity { get; private set; }

        public JObject Options { get; private set; }

        [JsonProperty("isNew")]
        public bool IsNew { get; private set; }
    }











    [JsonObject]
    public class ToppingOptionDefault
    {
        [JsonConstructor]
        private ToppingOptionDefault() { }

        internal ToppingOptionDefault(string optValue)
        {
            C = new ToppingOptionDefaultSubType(optValue);
            X = new ToppingOptionDefaultSubType(optValue);
        }

        public ToppingOptionDefaultSubType C { get; private set; }
        public ToppingOptionDefaultSubType X { get; private set; }
    }


    [JsonObject]
    public class ToppingOptions
    {
        [JsonConstructor]
        private ToppingOptions() { }

        public ToppingOptions(string[] toppingtypes)
        {
            JObject toppings = new JObject();
            foreach (String type in toppingtypes)
            {
                JObject subtype = new JObject();
                subtype["1/1"] = "1";

                toppings[type] = subtype;
            }

        }


        public JObject toppings { get; private set; }



    }



    [JsonObject]
    public class ToppingOptionDefaultSubType
    {
        [JsonConstructor]
        private ToppingOptionDefaultSubType() { }

        public ToppingOptionDefaultSubType(string value)
        {
            opt = value;
        }

        [JsonProperty("1/1")]
        public string opt { get; private set; }
    }
}

