using System;
using System.Collections.Generic;
using System.Linq;
using Course_Linq.Entities;
using System.Collections.Generic;

namespace Course_Linq
{
    class Program
    {
        static void Print<T>(String message, IEnumerable<T> collection)
        {
            Console.WriteLine(message);
            foreach(T obj in collection)
            {
                Console.WriteLine(obj);
            }
            Console.WriteLine("*Total results: " + collection.Count() + "*\n");

        }

        static void Main(string[] args)
        {
            //Case 1:

            int[] array = new int[] { 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            IEnumerable<int> result = array.
                Where(x => x % 2 == 0).
                Select(x => x * 10);

            foreach (int x in result)
            {
                Console.WriteLine(x);
            }
            //Another case: 

            Category c1 = new Category() { Id = 1, Name = "Tools", Tier = 2 };
            Category c2 = new Category() { Id = 2, Name = "Computer", Tier = 1 };
            Category c3 = new Category() { Id = 3, Name = "Eletronics", Tier = 1 };

            List<Product> products = new List<Product>()
            {
                new Product(1, "Computer", 1200.00, c2),
                new Product(2, "Hammer", 40.00, c1),
                new Product(3, "TV", 1700.00, c3),
                new Product(4, "Notebook", 1200.00, c2),
                new Product(5, "Saw", 80.00, c1),
                new Product(6, "Tablet", 500, c2),
                new Product(7, "Camera", 400, c3),
                new Product(8, "Printer", 500.88, c3),
                new Product(9, "MacBook", 1300, c3),
                new Product(10, "SoundBar", 400.00, c3),
                new Product(11, "Level", 70.89, c1),
            };

            var r1 = products.Where(p => p.Category.Tier == 1 && p.Price < 900.00);
            Print("TIER 1 AND PRICE LOWER THAN $900.00: ", r1);

            var r2 = products.Where(p => p.Category.Name == "Tools").Select(p => p.Name);
            Print("TOOLS' NAMES: ", r2);

            var r3 = products.Where(p => p.Name[0] == 'C').Select(p => new { p.Name, p.Price, CategoryName = p.Category.Name});
            Print("PRODUCTS STARTING WITH C: (Anonymous object)", r3);

            var r4 = products.Where(p => p.Category.Tier == 1).OrderBy(p => p.Price).ThenBy(p => p.Name);
            Print("TIER 1 ORDERED BY PRICE THEN NAME: ", r4);

            try
            {
                var r10 = products.Max(p => p.Price);
                Console.WriteLine("Max: $" + r10);
                var r11 = products.Min(p => p.Price);
                Console.WriteLine("Min: $" + r11);
                var r12 = products.Sum(p => p.Price);
                Console.WriteLine("Sum: $" + r12);
                var r13 = products.Where(p => p.Category.Id == 5).Select(p => p.Price).DefaultIfEmpty(0.0);
                Console.WriteLine("Category 5: ");
                var r15 = products.Where(p => p.Category.Id == 1).Select(p => p.Price).Aggregate(0.0, (x, y) => x + y);
                Console.WriteLine("Category 1 aggregate sum: ");
                var r16 = products.GroupBy(p => p.Category);
                Console.WriteLine();
                foreach (IGrouping<Category, Product> group in r16)
                {
                    Console.WriteLine("Category: " + group.Key.Name + ": ");
                    foreach (Product p in group)
                    {
                        Console.WriteLine(p);
                    }
                    Console.WriteLine();
                }
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Error: " + e.Message); 
            }
        }
    }
}
