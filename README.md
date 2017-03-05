# DominosPizzaSDK
An unofficial C# SDK to interface with the Dominos online ordering API

# About
This SDk targets endpoints on the Dominos online ordering API, which is both private and unsupported.  As such, this SDK may break at the whim of whoever builds the dominos site if they decide to change anything server-side.

# Building This Code
As of version 2.0, this library is designed to build against .net core.

Given the recent (and developing) situation with decision in the .net core effort to [transition to the csproj project format from the previous project.json format,](https://blogs.msdn.microsoft.com/dotnet/2016/11/16/announcing-net-core-tools-msbuild-alpha/) there are some restrictions on how code in this repo may be built.  This project has opted to support the new csproj format going forward; however, given the state of tooling at the current time what that means is that **building this project will require either Visual Studio 2017 RC or a version of the dotnet CLI tools greater than 1.0.0-RC3.**

# Examples
### Finding a Store
```C#
var deliveryAddress = new Address("9 Lupine Road", "Lowell", "MA", "01850", Address.UnitCategory.House);
var locationData = await SearchLocations(deliveryAddress);

int? nearestStoreID = locationData.Stores.OrderBy(x => x.MinDistance)
    .FirstOrDefault(x => x.ServiceIsOpen?.Delivery.Value ?? false)?.StoreID;
```

### Searching the Menu
```C#
var menu = await GetMenu(nearestStoreID.Value);
var practicalMenuInfo = menu.PreconfiguredProducts.Select(x =>
    new Tuple<string, string, string>(x.Value.Code, x.Value.Name, x.Value.Description));
```

### Placing an Order
```C#
var deliveryAddress = new Address("43 Elm Avenue", "Quincy", "MA", "02170", Address.UnitCategory.House);
var customerInfo = new Customer("John", "Cheever", deliveryAddress, "thecheeves@gmail.com", "6173456789");
string storeId = "3712";    // Get this value from a call to SearchLocations(...)

// Two medium pepperoni pizzas, one medium cheese, and cinnastix
var products = new List<Product> {  new Product("14SCREEN", 2, new List<string> { "C", "P", "X" }),
                                    new Product("14SCREEN", 1, new List<string> { "C", "X" }),
                                    new Product("CINNASTIX8", 1)};

var coupons = new List<Coupon> { new Coupon("3510", 1) };   // This particular coupon saves us money on three medium pizzas.
                                                            // Get this value from a call to GetMenu(...)

var myOrder = new Order(customerInfo, storeId, products, coupons);

var pricedOrder = await PriceOrder(myOrder);   // This may throw if, for instance, the store is closed.

decimal totalAmountToPay = pricedOrder.Order.Amounts.Payment.Value;

var payments = new List<Payment> { new Payment("1112223334444", CreditCardType.Visa, "0918", "111", "02170", totalAmountToPay) };

return await PlaceOrder(myOrder, payments);

```

### Tracking an Order
```C#
var myOrderStatuses = await TrackOrder("6173456789");   // Order state seems to update every 90 seconds or so, so it doesn't
                                                        // make sense to poll it any more frequently than that.

var mostRecentOrder = myOrderStatuses.OrderBy(x => x.AsOfTime).DefaultIfEmpty(null).FirstOrDefault();

if (mostRecentOrder != null)
{
    switch (mostRecentOrder.Status)
    {
        case OrderLocation.RoutingStation:
            break;
        case OrderLocation.MakeLine:
            break;
        case OrderLocation.Oven:
            break;
        case OrderLocation.OutTheDoor:
            break;
        case OrderLocation.Complete:
            break;
        default:
            break;
    }
}

```

# Contributing
Feel free to submit a PR against this repo.

# TODO
* As a reference, it would be nice to have a txt file or directory with a dump of various example curl commands and responses that show the request/response payloads.
* A lot of the request/response models are pretty gross.  This is somewhat necessary as we want to serialize/deserialize all of the weirdly-formatted request bodies required by dominos into strong types.  But perhaps there is a way to clean this up using more dynamic types without sacrificing discoverability.
