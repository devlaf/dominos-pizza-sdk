using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DominosApi
{
    [JsonObject]
    public class Product
    {
        [JsonConstructor]
        private Product() { }

        /// <summary>
        /// Represents a dominos menu item, like a pizza.
        /// </summary>
        /// <param name="code">The product code, (ex. a 12-in pizza's code is "14SCREEN".)  This data will
        /// be returned as part of menu query requests.</param>
        /// <param name="quantity">How many of this product to order.</param>
        /// <param name="toppingModifiers">Some menu items can take modifiers.  For example, a 
        /// 14-in pizza can have topping modifiers that specify to add peperoni or sausage. In 
        /// the pepperoni example, we ultimatly need to build a modifier JSON string that looks 
        /// like "{C: {1/1: "1"}, P: {1/1: "1"}, X: {1/1: "1"}}}".  So for a pepperoni pizza, the 
        /// user would pass in a list with three strings: "C", "P", and "X". Topping specifications
        /// may be found via a menu query request.</param>
        public Product(string code, int quantity, List<string> toppingModifiers = null)
        {
            Code = code;
            Quantity = quantity;
            IsNew = true;

            Options = toppingModifiers?.Aggregate(new JObject(), (retval, modifierLabel) => {
                var modifier = new JObject();
                modifier["1/1"] = "1";  // Unclear where this comes from, but I've never seen it be anything else.
                retval[modifierLabel] = modifier;
                return retval;
            });
        }

        public string Code { get; private set; }

        public JObject Options { get; private set; }

        [JsonProperty("Qty")]
        public int Quantity { get; private set; }

        [JsonProperty("isNew")]
        public bool IsNew { get; private set; }
    }
}