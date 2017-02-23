using System;
using Newtonsoft.Json;

namespace DominosApi
{
    [JsonObject]
    public class Payment
    {
        [JsonConstructor]
        private Payment() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cardNumber">The 16-digit card number.</param>
        /// <param name="cardType">The provider of the card (visa, mastercard, etc.)</param>
        /// <param name="expiration">Expiration date for card in format "MMYY".</param>
        /// <param name="securityCode">Cvv code on card.</param>
        /// <param name="postalCode">Zip code of billing address.</param>
        /// <param name="amount">The amount to charge to the card.  You must make a PriceOrder(...) query 
        /// to find out what the expected total is, and use that.</param>
        public Payment(string cardNumber, CreditCardType cardType, string expiration, 
                 string securityCode, string postalCode, decimal amount)
        {
            CardNumber = cardNumber;
            CardType = cardType;
            Expiration = expiration;
            SecurityCode = securityCode;
            PostalCode = postalCode;
            Amount = amount;
            Type = "CreditCard";
        }

        public string Type { get; private set; }

        public decimal Amount { get; private set; }

        [JsonProperty("Number")]
        public string CardNumber { get; private set; }

        public CreditCardType CardType { get; private set; }

        public string Expiration { get; private set; }

        public string SecurityCode { get; private set; }

        public string PostalCode { get; private set; }
    }
}

