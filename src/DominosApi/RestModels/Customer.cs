using Newtonsoft.Json;

namespace DominosApi.RestModels
{
    [JsonObject]
    public class Customer
    {
        [JsonConstructor]
        private Customer() { }

        public Customer(string firstName, string lastName, Address address, string email, 
                        string phone, string extension = null)
        {
            Address = address;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Extension = extension ?? string.Empty;
        }

        public Address Address { get; private set; }
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Phone { get; private set; }
        public string Extension { get; private set; }
    }
}

