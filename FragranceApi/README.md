# FragranceApi

## Project Description

FragranceApi is an ASP.NET Core Web API for managing customers, fragrance products, inventory, and orders.

The API provides endpoints for creating and retrieving customer and product information, managing customer records, searching and filtering products, and creating and retrieving orders.

## Technologies Used

* ASP.NET Core Web API
* C#
* Entity Framework Core
* SQL Server
* Swagger / OpenAPI
* AutoMapper
* FluentValidation

## Project Structure

```text
FragranceApi
├── Controllers
├── Mapping
├── Middleware
└── Validators

FragranceApi.BLL
├── Interfaces
└── Services

FragranceApi.DAL
├── Data
└── Repositories

FragranceApi.Models
├── Customer.cs
├── Product.cs
├── Order.cs
└── OrderItem.cs

FragranceApi.DTOs
├── Customers
├── Products
└── Orders
```

## Database Relationships

The API uses four main entities:

* Customer
* Order
* Product
* OrderItem

Relationships:

* One Customer can have many Orders.
* One Order can contain many OrderItems.
* One Product can be included in many OrderItems.
* Order and Product have a many-to-many relationship through OrderItem.

## API Endpoints

### Customers

| Method | Endpoint              | Description          |
| ------ | --------------------- | -------------------- |
| GET    | `/api/customers`      | Get all customers    |
| GET    | `/api/customers/{id}` | Get a customer by ID |
| POST   | `/api/customers`      | Create a customer    |
| DELETE | `/api/customers/{id}` | Delete a customer    |

### Products

| Method | Endpoint        | Description                                          |
| ------ | --------------- | ---------------------------------------------------- |
| GET    | `/api/products` | Get products with filtering, sorting, and pagination |
| POST   | `/api/products` | Create a product                                     |

### Orders

| Method | Endpoint           | Description        |
| ------ | ------------------ | ------------------ |
| GET    | `/api/orders/{id}` | Get an order by ID |
| POST   | `/api/orders`      | Create an order    |

## HTTP Status Codes

The API uses standard HTTP status codes:

* **200 OK** — request completed successfully.
* **201 Created** — a new resource was created.
* **204 No Content** — a resource was successfully deleted.
* **400 Bad Request** — request data failed validation.
* **404 Not Found** — the requested customer, product, or order was not found.
* **409 Conflict** — an order cannot be completed because of a business rule, such as insufficient product stock.
* **500 Internal Server Error** — an unexpected server error occurred.

## Product Filtering and Pagination

The product endpoint supports query parameters for:

* Name
* Minimum price
* Maximum price
* Minimum stock
* Sorting
* Sort direction
* Page number
* Page size

Example:

```text
GET /api/products?name=candle&minPrice=10&maxPrice=50&pageNumber=1&pageSize=10
```

## Order Creation

When an order is created, the API:

1. Checks that the customer exists.
2. Checks that each product exists.
3. Checks that enough stock is available.
4. Creates the order and order items.
5. Reduces the product stock.
6. Saves the order to the database.

## Validation and Error Handling

The API uses FluentValidation to validate incoming request data.

Exception handling is provided through custom middleware. It returns appropriate HTTP responses for not-found errors, conflicts, validation-related requests, and unexpected server errors.

## Database

The application uses Entity Framework Core with SQL Server.

The database contains:

* Customers
* Products
* Orders
* OrderItems

Entity Framework Core migrations are used to create and update the database.

## Running the Project

1. Open the solution in Visual Studio.
2. Make sure SQL Server is available.
3. Verify the database connection string in `appsettings.json`.
4. Build the solution.
5. Run the API.
6. Open Swagger to view and test the available endpoints.

