# MiniShop

MiniShop is a .NET console application that manages product categories and products with Dapper and PostgreSQL.

## Requirements

- .NET 10 SDK
- PostgreSQL
- A PostgreSQL user that can connect to the server and create tables in `minishop`

## Database setup

Create the empty database once:

```sql
CREATE DATABASE minishop;
```

The application creates the tables and inserts its three sample categories and six products on startup. Seed inserts are idempotent.

The connection string is configured in one place: `Infrastructure/Data/Database.cs`. By default it uses:

```text
Host=localhost;Port=5432;Database=minishop;Username=postgres;Password=postgres
```

Override the default without editing source code by setting `MINISHOP_CONNECTION_STRING` in the environment to a PostgreSQL connection string for your local server.

## Run

From the repository root:

```powershell
$env:MINISHOP_CONNECTION_STRING = "Host=localhost;Port=5432;Database=minishop;Username=postgres;Password=your-password"
dotnet run --project .\Consoleapp\Consoleapp.csproj
```

## Menu functions

- List, retrieve, add, update, and delete categories and products
- List products by category
- Count products and categories, including products in a category
- View minimum, maximum, and average product prices by category
- View the sum of product prices

## Schema

- `categories`: `id SERIAL PRIMARY KEY`, `name VARCHAR(100) NOT NULL UNIQUE`
- `products`: `id SERIAL PRIMARY KEY`, `name VARCHAR(150) NOT NULL`, `price NUMERIC(10,2) NOT NULL CHECK (price > 0)`, `quantity INT NOT NULL DEFAULT 0 CHECK (quantity >= 0)`, `category_id INT NOT NULL REFERENCES categories(id)`
- `stock_movements`: `id SERIAL PRIMARY KEY`, `product_id INT NOT NULL REFERENCES products(id)`, `change_qty INT NOT NULL CHECK (change_qty <> 0)`, `moved_at TIMESTAMP NOT NULL DEFAULT NOW()`
