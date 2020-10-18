## App in Development

## GameShop

GameShop app will allow user and anonymous to buy product and admin will be able to: edit products, track orders, etc.
### Store already have the following functionalities:
- basket functionality with holding Stock for concrete customer and synchronize basket,
- returning expired stock that is hold for specific customer to the stock that is available to retrieve by another customer,
- payment functionality (only basic implementation allowing create charge to Stripe service)
- order creation (hole process from creating basket through filling customer information and payment information to putting order in Database)
- user registration and login functionality,
- user authentication through JWT,
- browsing products (implemented pagination)
#### Administrator panel
- creating and editing products,
- adding and removing images for product that are stored in cloud
- editing stock quanitity for products,
- editing roles for users



### Store will have the following functionalities:
- extended product purchase functionality (for example sending e-mail about order etc.),
- extended user registration functionality,
- additional refresh token,
- user panel,
- extended browsing products functionality by adding filtering,
- extended payment functionality by adding more payment options and sending info about payment to an e-mail etc.,
- generating invoice for order,
- chat system which will allow customers to talk with shop employee,
- and so on...
#### Administrator panel
- extended administrator panel,
- data import, data export,
- creating new categories and subcategories,
- and so on...

## Angular packages installation

Use the command on {workspace}/GameShop.UI/ClientApp

```bash
npm install 
```

## ASP.NET Core packages installation

Use the command on {workspace}/UnitTests

```bash
dotnet restore
```

## Running appliaction

Use the command on {workspace}/GameShop.UI

```bash
dotnet run
```


## Technology Stack

- ASP.NET Core
- EntityFramework Core
- Angular
- Bootstrap 4
- XUnit
- Moq
- FluentAssertions
