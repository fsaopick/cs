CREATE DATABASE magazin_productov_pr55;
GO

USE magazin_productov_pr55;
GO

CREATE TABLE categories (
 ID_category INT PRIMARY KEY IDENTITY (1,1),
 title VARCHAR(50) NOT NULL UNIQUE
);
GO

CREATE TABLE suppliers (
 ID_supplier INT PRIMARY KEY IDENTITY (1,1),
 name_of_company VARCHAR(25) NOT NULL,
 number INT NOT NULL,
 mail VARCHAR(25) NULL,
 address_company VARCHAR(25) NOT NULL
);

CREATE TABLE products (
 ID_product INT PRIMARY KEY IDENTITY (1,1),
 title VARCHAR(50) NOT NULL UNIQUE,
 description_pr VARCHAR(1000) NOT NULL,
 cost DECIMAL (10,2) NOT NULL,
 category_ID INT NOT NULL,
 supplier_ID INT NOT NULL,
 FOREIGN KEY (supplier_ID) REFERENCES suppliers(ID_supplier),
 FOREIGN KEY (category_ID) REFERENCES categories(ID_category)
);

INSERT INTO categories(title) VALUES 
('Электроника'),
('Косметика'),
('Одежда');

INSERT INTO suppliers(name_of_company, number, mail, address_company) VALUES 
('Chinchanchon', 123456789, 'yaponya@mail.com', 'ул. Хуньшунь, 1'),
('ChinaGoods', 987654321, 'china@mail.com', 'ул. Мао, 10'),
('Chinkpop', 555666777, 'koreya@mail.com', 'ул. Джаома, 5');

INSERT INTO products (title, description_pr, cost, category_ID, supplier_ID) VALUES 
('Массажер', 'Крутой массажер с 52 режимами', 29999.99, 1, 1),
('Холодильник Ice', 'Идеальный переносной холодильник для поездок', 45999.50, 2, 2),
('Джинсы Classic', 'Классические мужские джинсы', 3499.00, 3, 3);




SELECT * FROM categories;
GO

SELECT * FROM suppliers;
GO

SELECT * FROM products;
GO

--USE master;
--GO

--DROP DATABASE magazin_productov;
--GO