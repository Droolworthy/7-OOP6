using System;

namespace OOP6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CommandBuyProduct = "1";
            const string CommandShowGoodsBuyer = "2";
            const string CommandShowGoodsSeller = "3";
            const string CommandExit = "4";

            Shop shop = new Shop();

            bool isWorking = true;

            Console.WriteLine($"{CommandBuyProduct} - КУПИТЬ ТОВАР" + $"\n{CommandShowGoodsBuyer} - ПОСМОТРЕТЬ ТОВАРЫ У ПОКУПАТЕЛЯ" +
                $"\n{CommandShowGoodsSeller} - ПОСМОТРЕТЬ ТОВАРЫ У ПРОДАВЦА" + $"\n{CommandExit} - ВЫХОД");

            while (isWorking)
            {
                Console.Write("\nВведите команду: ");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandBuyProduct:
                        shop.Trade(shop);
                        break;

                    case CommandShowGoodsBuyer:
                        shop.ShowBuyerBag();
                        break;

                    case CommandShowGoodsSeller:
                        shop.ShowAllProductShop();
                        break;

                    case CommandExit:
                        isWorking = false;
                        break;

                    default:
                        Console.WriteLine($"\nВведите {CommandBuyProduct}, {CommandShowGoodsBuyer}, {CommandShowGoodsSeller} или {CommandExit}");
                        break;
                }
            }
        }
    }

    class Human
    {
        protected List<Product> _listGoods = new List<Product>();
        protected List<Product> _shoppingList = new List<Product>();
        protected int _clientMoney = 500;
        protected int _moneyToPay;

        public Human()
        {
            AddProduct();
        }

        private void AddProduct()
        {
            _listGoods.Add(new Product("Батон", 50));
            _listGoods.Add(new Product("Колбаса", 150));
            _listGoods.Add(new Product("Брокколи", 110));
            _listGoods.Add(new Product("Сосиски", 150));
            _listGoods.Add(new Product("Шоколадка", 55));
            _listGoods.Add(new Product("Зефир", 40));
            _listGoods.Add(new Product("Майонез", 40));
            _listGoods.Add(new Product("Банан", 15));
            _listGoods.Add(new Product("Молоко", 50));
        }
    }

    class Сustomer : Human
    {
        public void ShowBuyerBag()
        {
            for (int i = 0; i < _shoppingList.Count; i++)
            {
                Console.WriteLine("\nУ вас в сумке лежит - " + _shoppingList[i].СommodityName);
            }
        }
    }

    class Salesman : Сustomer
    {
        public void ShowAllProductShop()
        {
            Console.WriteLine("\nПродукты магазина: ");

            for (int i = 0; i < _listGoods.Count; i++)
            {
                Console.WriteLine("Название товара - " + _listGoods[i].СommodityName + ", Цена - " + _listGoods[i].СommodityPrice);
            }
        }
    }

    class Shop : Salesman
    {
        public void Trade(Shop shop)
        {
            Console.WriteLine("\nУ вас: " + _clientMoney + " рублей.");

            Console.Write("\nДайте мне пожалуйста: ");
            string userInput = Console.ReadLine();

            for (int i = 0; i < _listGoods.Count; i++)
            {
                if (shop.CheckSolvencyBuyerBuyProduct(_listGoods[i]))
                {
                    if (userInput.ToLower() == _listGoods[i].СommodityName.ToLower())
                    {
                        _clientMoney -= shop.PayProduct();
                        _shoppingList.Add(_listGoods[i]);
                        _listGoods.RemoveAt(i);
                        Console.WriteLine("Вы купили - " + userInput);
                    }
                    else
                    {
                        Console.WriteLine("Такого товара нет в списке.");
                    }
                }
                else
                {
                    Console.WriteLine("У вас закончились деньги.");
                }
            }
        }

        public bool CheckSolvencyBuyerBuyProduct(Product product)
        {
            _moneyToPay = product.СommodityPrice;

            if (_clientMoney >= _moneyToPay)
            {
                return true;
            }
            else
            {
                _moneyToPay = 0;
                return false;
            }
        }

        public int PayProduct()
        {
            _clientMoney -= _moneyToPay;
            return _moneyToPay;
        }
    }

    class Product
    {
        public Product(string productName, int productPrice)
        {
            СommodityName = productName;
            СommodityPrice = productPrice;
        }

        public string СommodityName { get; private set; }

        public int СommodityPrice { get; private set; }

        public override string ToString()
        {
            string nameArray = string.Join(" ", СommodityName);
            string priceArray = string.Join(" ", СommodityPrice);
            return nameArray + " " + priceArray;
        }
    }
}
