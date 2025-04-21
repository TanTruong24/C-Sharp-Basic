CREATE DATABASE Shop
GO

USE Shop
GO 

CREATE TABLE customers (
	id INT IDENTITY(1,1) PRIMARY KEY,
	name NVARCHAR(255) NOT NULL,
	user_id INT NOT NULL
);

CREATE TABLE products (
	id INT IDENTITY(1,1) PRIMARY KEY,
	name NVARCHAR(255) NOT NULL,
	price FLOAT NOT NULL,
	remain_quantity INT NOT NULL
);

CREATE TABLE orders (
	id INT IDENTITY(1,1) PRIMARY KEY,
	customer_id INT NOT NULL FOREIGN KEY REFERENCES customers(id),
	order_reference NVARCHAR (20) NOT NULL,

	CONSTRAINT unqOrderReference UNIQUE (order_reference)
)

CREATE TABLE orders_items (
	id INT IDENTITY(1,1) PRIMARY KEY,
	order_id INT NOT NULL FOREIGN KEY REFERENCES orders(id),
	product_id INT NOT NULL FOREIGN KEY REFERENCES products(id),
	quantity INT NOT NULL,
	price FLOAT NOT NULL
);