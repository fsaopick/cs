using System;
using System.IO;
using System.Collections.Generic;
class Program
{
    public static void Main()
    {
        string path = "/Users/andrey/Desktop/";

        Directory.CreateDirectory(path + "test/");

        if (Directory.Exists(path + "test/"))
        {
            path += "test/";
            File.Create(path + "text.txt").Close();
            FileInfo Info = new FileInfo(path + "text.txt");

            Console.WriteLine(Info.FullName);
            Console.WriteLine(Info.Length);
            Console.WriteLine(Info.CreationTime);
        }

        List<Product> products = new List<Product>
        {
            new Product("Яблоко", 50),
            new Product("Банан", 30),
            new Product("Апельсин", 40),
            new Product("Груша", 60),
            new Product("Виноград", 100),
            new Product("Мандарин", 55),
            new Product("Арбуз", 65),
            new Product("Капуста", 40),
            new Product("Грейпфрут", 150),
            new Product("Помидор", 52),
        };

        List<CartItem> cart = new List<CartItem>();
        string input;

        do
        {
            Console.WriteLine("Выберите продукт (1-10) из списка продуктов, введите 'удалить' для удаления товара из корзины или введите 'оплата' для выхода:");
            for (int i = 0; i < products.Count; i++)
            {
                Console.WriteLine($"                        {i + 1}. {products[i].Name} - {products[i].Price} руб.");
            }
            for (int i = 0; i < cart.Count; i++)
            {
                Console.WriteLine($"{i + 1} товар корзины: {cart[i].Product.Name} - {cart[i].Quantity} шт.");
                Console.WriteLine(" ");
            }

            Console.WriteLine("Выбор: ");
            input = Console.ReadLine();

            if (int.TryParse(input, out int viborproducta) && viborproducta >= 1 && viborproducta <= 10)
            {
                Console.WriteLine("Введите количество:");
                string colvoInput = Console.ReadLine();
                if (int.TryParse(colvoInput, out int colvo) && colvo > 0)
                {
                    cart.Add(new CartItem(products[viborproducta - 1], colvo));
                    Console.WriteLine($"Продукт {products[viborproducta - 1].Name} добавлен в корзину.");
                }
                else
                {
                    Console.WriteLine("Неверное количество. Попробуйте снова.");
                }
            }
            else if (input.ToLower() == "удалить")
            {
                Console.WriteLine("Введите номер товара для удаления из корзины:");
                string deleteInput = Console.ReadLine();
                if (int.TryParse(deleteInput, out int deleteIndex) && deleteIndex >= 1 && deleteIndex <= cart.Count)
                {
                    cart.RemoveAt(deleteIndex - 1);
                    Console.WriteLine("Товар удален из корзины.");
                }
                else
                {
                    Console.WriteLine("Неверный номер товара. Попробуйте снова.");
                }
            }
            else if (input.ToLower() != "оплата")
            {
                Console.WriteLine("Неверный выбор. Попробуйте снова.");
            }

        } while (input.ToLower() != "оплата");

        Console.WriteLine("Оплата успешно прошла, вам выдан чек"); 

        decimal total = 0;
        using (StreamWriter sw = new StreamWriter(path + "text.txt", false, System.Text.Encoding.Default))
        {
            foreach (var item in cart)
            {
                total += item.Product.Price * item.Quantity;
                sw.WriteLine($"{item.Product.Name} - {item.Quantity} шт. - {item.Product.Price * item.Quantity} руб.");
            }
            sw.WriteLine($"Общая сумма: {total} руб.");
        }
    }
}
class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }

    public Product(string name, decimal price)
    {
        Name = name;
        Price = price;
    }
}
class CartItem
{
    public Product Product { get; set; }
    public int Quantity { get; set; }

    public CartItem(Product product, int quantity)
    {
        Product = product;
        Quantity = quantity;
    }
}
































// // class Store
// // {
// //     private List<string> items = new List<string>();
// //     private List<decimal> prices = new List<decimal>();

// //     public void AddItem(string item, decimal price)
// //     {
// //         items.Add(item);
// //         prices.Add(price);
// //         Console.WriteLine($"Товар '{item}' добавлен за {price}.");
// //     }

// //     public void RemoveItem(string item)
// //     {
// //         int index = items.IndexOf(item);
// //         if (index >= 0)
// //         {
// //             items.RemoveAt(index);
// //             prices.RemoveAt(index);
// //             Console.WriteLine($"Товар '{item}' удален.");
// //         }
// //         else
// //         {
// //             Console.WriteLine($"Товар '{item}' не найден.");
// //         }
// //     }

// //     public void DisplayItems()
// //     {
// //         Console.WriteLine("Список товаров:");
// //         for (int i = 0; i < items.Count; i++)
// //         {
// //             Console.WriteLine($"{items[i]} - {prices[i]}");
// //         }
// //     }

// // }
