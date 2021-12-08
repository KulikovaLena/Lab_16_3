﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.IO;

namespace Lab_16_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            Product[] product = new Product[5];
            product[0] = new Product(1, "Товар1", 1);
            product[1] = new Product(2, "Товар2", 2);
            product[2] = new Product(3, "Товар3", 3);
            product[3] = new Product(4, "Товар4", 4);
            product[4] = new Product(5, "Товар5", 5);
            foreach (var i in product)
            {
                i.Info();
            }
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true,
            };
            string jsonString = JsonSerializer.Serialize(product, options);
            Console.WriteLine(jsonString);
            string path = "D:\\Lena\\BIM\\Lab_16_1\\Product.json";
            if (!File.Exists(path))
            {
                File.Create(path);
            }
            using (StreamWriter sw = new StreamWriter(path, false))
            {
                sw.WriteLine(jsonString);
            }
            StreamReader sr = new StreamReader(path);
            Console.WriteLine(sr.ReadToEnd());
            jsonString = Convert.ToString(Console.ReadLine());
            sr.Close();

            /*Product product1 = JsonSerializer.Deserialize<Product>(path);
            Console.WriteLine(product1.Price);*/

            if (File.Exists(path))
            {
                string data = File.ReadAllText(path);
                Product product2 = JsonSerializer.Deserialize<Product>(data);
                Console.WriteLine(product2.Price);
            }
            

            /*char[] deaf = new char[]{ ' ', '\n', '}', '{', };
            foreach (char c in deaf)
            {
                jsonString = jsonString.Replace(c.ToString(), "");
            }
            Console.WriteLine(jsonString);*/

            /*Console.WriteLine(jsonString);
            char[] delimiterChar = { ' ', '\n', '}', '{', };
            Console.WriteLine("Orig.text{0}", jsonString);
            string[] words = jsonString.Split(delimiterChar);
            foreach (var word in words)
            {
                Console.WriteLine(word);
            }
            /*string jsonString1 = "{\"Code\":1,\"Name\":\"aaa\",\"Price\":20,\"Code\":2,\"Name\":\"bbb\",\"Price\":15,\"Code\":3,\"Name\":\"ccc\",\"Price\":58,\"Code\":4,\"Name\":\"ddd\",\"Price\":12,\"Code\":5,\"Name\":\"eee\",\"Price\":60}";
            Product product1 = JsonSerializer.Deserialize<Product>(jsonString1);*/
            Console.ReadKey();
        }
    }
    public class Product
    {
        public int Code { get; set; }
        public string Name { get; set; }
        double price;
        public double Price
        {
            get
            {
                return price;
            }
            set
            {
                if (value > 0)
                {
                    price = value;
                }
                else
                {
                    Console.WriteLine("Цена должна быть больше нуля");
                }
            }
        }

        public Product(int code, string name, double price)
        {
            Code = code;
            Name = name;
            Price = price;
        }
        public void Info()
        {
            try
            {
                Console.Write("введите артикул {0} ", Code);
                Code = Convert.ToInt32(Console.ReadLine());
                Console.Write("введите название {0} ", Name);
                Name = Convert.ToString(Console.ReadLine());
                Console.Write("введите цену {0} ", Price);
                Price = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine();
            }
            catch
            {
                Console.WriteLine("Ошибка! Входная строка имела неверный формат");
            }
        }
    }
}
