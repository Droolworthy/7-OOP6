//Существует продавец, он имеет у себя список товаров, и при нужде, может вам его показать, 
//также продавец может продать вам товар. После продажи товар переходит к вам, и вы можете также посмотреть свои вещи.
//Возможные классы – игрок, продавец, товар.
//Вы можете сделать так, как вы видите это.
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
            Customer customer = new Customer();
            Salesman salesman = new Salesman();

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
                        shop.Trade(customer, salesman);
                        break;

                    case CommandShowGoodsBuyer:
                        salesman.ShowAllProduct();
                        break;

                    case CommandShowGoodsSeller:
                        customer.ShowBuyerBag();
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
        protected int _clientMoney = 500;

        public Human()
        {
            AddProduct();
        }

        public void ShowAllProduct()
        {
            Console.WriteLine("\nПродукты магазина: ");

            for (int i = 0; i < _listGoods.Count; i++)
            {
                Console.WriteLine("Название товара - " + _listGoods[i].СommodityName + ", Цена - " + _listGoods[i].СommodityPrice);
            }
        }

        public void ShowClientMoney()
        {
            Console.WriteLine("\nУ вас: " + _clientMoney + " рублей.");
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

    class Customer : Human
    {
        private List<Product> _shoppingList = new List<Product>();
        private int _moneyToPay;

        public bool CanPay(Product product)
        {
            _moneyToPay = product.СommodityPrice;

            if (_clientMoney >= _moneyToPay)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Вам не хватает денег купить товар.");
                _moneyToPay = 0;
                return false;
            }
        }

        public void Buy(Product product)
        {
            int moneyClientToPay = product.СommodityPrice;
            _shoppingList.Add(product);
            _clientMoney -= moneyClientToPay;
        }

        public void ShowBuyerBag()
        {
            for (int i = 0; i < _shoppingList.Count; i++)
            {
                Console.WriteLine("\nУ вас в сумке лежит - " + _shoppingList[i].СommodityName);
            }
        }
    }

    class Salesman : Human
    {
        private int _buyerMoney = 0;

        public bool TryGetProduct(out Product product)
        {
            product = null;
        
            Console.WriteLine("У продавца: " + _buyerMoney + " рублей.");

            Console.Write("\nВведите название товара: ");
            string userInput = Console.ReadLine();

            for (int i = 0; i < _listGoods.Count; i++)
            {
                if (userInput.ToLower() == _listGoods[i].СommodityName.ToLower())
                {
                    product = _listGoods[i];
                    return true;
                }
            }

            return false;
        }

        public void Sell(Product product)
        {
            int moneyBuyerToPay = product.СommodityPrice;
            _listGoods.Remove(product);
            _buyerMoney += moneyBuyerToPay;
        }
    }

    class Shop
    {
        public void Trade(Customer customer, Salesman salesman)
        {
            customer.ShowClientMoney();

            if (salesman.TryGetProduct(out Product product))
            {
                if (customer.CanPay(product))
                {
                    customer.Buy(product);
                    salesman.Sell(product);
                    Console.WriteLine("Вы купили товар.");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Такого товара нет в списке.");
                return;
            }
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
