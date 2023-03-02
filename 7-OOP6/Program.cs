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

            Сustomer сustomer = new Сustomer();
            Salesman salesman = new Salesman();

            bool isWorking = true;

            Console.WriteLine($"{CommandBuyProduct} - КУПИТЬ ТОВАР" + $"\n{CommandShowGoodsBuyer} - ПОСМОТРЕТЬ ТОВАРЫ У ПОКУПАТЕЛЯ" +
                $"\n{CommandShowGoodsSeller} - ПОСМОТРЕТЬ ТОВАРЫ У ПРОДАВЦА" + $"\n{CommandExit} - ВЫХОД");

            while (isWorking)
            {
                Console.Write("\nВведите команду: ");
                string userInput = Console.ReadLine();

                Console.Clear();

                Console.WriteLine($"{CommandBuyProduct} - КУПИТЬ ТОВАР" + $"\n{CommandShowGoodsBuyer} - ПОСМОТРЕТЬ ТОВАРЫ У ПОКУПАТЕЛЯ" +
                $"\n{CommandShowGoodsSeller} - ПОСМОТРЕТЬ ТОВАРЫ У ПРОДАВЦА" + $"\n{CommandExit} - ВЫХОД");

                switch (userInput)
                {
                    case CommandBuyProduct:
                        сustomer.BuyingGoods(salesman, сustomer);
                        break;

                    case CommandShowGoodsBuyer:
                        сustomer.ShowBuyerBag();
                        break;

                    case CommandShowGoodsSeller:
                        salesman.ShowAllProductSeller();
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

    class Сustomer
    {
        List<Product> _shoppingList = new List<Product>();
        private int _clientMoney = 500;
        private int _moneyToPay;

        public void BuyingGoods(Salesman salesman, Сustomer сustomer)
        {
            Console.WriteLine("\nУ вас: " + _clientMoney + " рублей.");

            Console.Write("\nДайте мне пожалуйста: ");
            string userInput = Console.ReadLine();

            if (salesman.GetListCount() > 0)
            {
                for (int i = 0; i < salesman.GetListCount(); i++)
                {
                    if (сustomer.CheckSolvency(salesman.GetListGoodsByIndex(i)))
                    {
                        if (userInput == salesman.GetListGoodsByIndex(i).СommodityName)
                        {
                            _clientMoney -= сustomer.PayProduct();
                            _shoppingList.Add(salesman.GetRemoveListGoods());
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
            else
            {
                Console.WriteLine("Товары в магазине закончились. Вы всё скупили.");
            }
        }

        public void ShowBuyerBag()
        {
            for (int i = 0; i < _shoppingList.Count; i++)
            {
                Console.WriteLine("\nУ вас в сумке лежит - " + _shoppingList[i].СommodityName);
            }
        }

        public bool CheckSolvency(Product product)
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

    class Salesman
    {
        List<Product> _listGoods = new List<Product>();

        public Salesman()
        {
            AddProduct();
        }

        public void ShowAllProductSeller()
        {
            Console.WriteLine("\nПродукты магазина: ");

            for (int i = 0; i < _listGoods.Count; i++)
            {
                Console.WriteLine("Название товара - " + _listGoods[i].СommodityName + ", Цена - " + _listGoods[i].СommodityPrice);
            }
        }

        public Product GetListGoodsByIndex(int index)
        {
            return _listGoods.ElementAt(index);
        }

        public Product GetRemoveListGoods()
        {
            Product product = _listGoods[0];
            _listGoods.Remove(product);
            return product;
        }

        public int GetListCount()
        {
            return _listGoods.Count;
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