## GameShop

GameShop app allow user and anonymous to buy product. Admin is able to do things like: manage products, manage users roles. 
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
