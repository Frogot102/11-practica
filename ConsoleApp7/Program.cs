using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Admin admin = new Admin("Админ", "admin@example.com", 8000);
        admin.ManageUsers();
        Customer customer = new Customer("Покупатель", "frogot158@gmail.com", 100);
        VipCustomer vipCustomer = new VipCustomer("VIP Покупатель", "vip@gmail.com", 100);
        customer.SetBalance(2000);
        vipCustomer.SetBalance(5400);
        customer.DisplayInfo();
        vipCustomer.DisplayInfo();
        Product product1 = new Product("Телевизор", 500, "Электроника");
        Product product2 = new Product("Футболка", 20, "Одежда");
        Product product3 = new Product("Книга", 15, "Книги");
        customer.AddToCart(product1);
        customer.AddToCart(product2);
        customer.AddToCart(product3);
        customer.PlaceOrder();
        vipCustomer.AddToCart(product1);
        vipCustomer.AddToCart(product2);
        vipCustomer.AddToCart(product3);
        vipCustomer.PlaceOrder();
        customer.DisplayInfo();
        vipCustomer.DisplayInfo();
    }
}

class User
{
    public string name;
    public string email;
    public double balance;

    public User(string name, string email, double balance)
    {
        this.name = name;
        this.email = email;
        this.balance = balance;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Имя: {name}, Email: {email}");
    }
}

class Customer : User
{
    public double balance;
    public List<Product> cart = new List<Product>();

    public Customer(string name, string email, double balance) : base(name, email, balance)
    {
        this.balance = balance;
    }

    public void SetBalance(double newBalance)
    {
        balance = newBalance;
    }

    public double GetBalance()
    {
        return balance;
    }

    public void AddToCart(Product product)
    {
        cart.Add(product);
        Console.WriteLine($"Товар {product.GetName()} добавлен в корзину.");
    }

    public void RemoveFromCart(Product product)
    {
        if (cart.Remove(product))
        {
            Console.WriteLine($"Товар {product.GetName()} был удален из корзины.");
        }
        else
        {
            Console.WriteLine($"Товар {product.GetName()} не найден в корзине.");
        }
    }

    public void PlaceOrder()
    {
        Order order = new Order(this, new List<Product>(cart));
        double totalPrice = order.GetTotalPrice();

        if (balance >= totalPrice)
        {
            balance -= totalPrice;
            Console.WriteLine("Заказ оформлен.");
            Console.WriteLine($"Общая стоимость заказа: {totalPrice}");
            cart.Clear();
        }
        else
        {
            Console.WriteLine("Недостаточно средств для оформления заказа.");
        }
    }

    public void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"Баланс: {balance}");
    }
}

class VipCustomer : Customer
{
    public double discountRate = 0.10;

    public VipCustomer(string name, string email, double balance) : base(name, email, balance) { }

    public void PlaceOrder()
    {
        Order order = new Order(this, new List<Product>(cart));
        double totalPrice = order.GetTotalPrice();
        totalPrice *= (1 - discountRate);

        if (GetBalance() >= totalPrice)
        {
            SetBalance(GetBalance() - totalPrice);
            Console.WriteLine("VIP Заказ оформлен с учетом скидки.");
            Console.WriteLine($"Общая стоимость заказа с учетом скидки: {totalPrice}");
            cart.Clear();
        }
        else
        {
            Console.WriteLine("Недостаточно средств для оформления VIP заказа.");
        }
    }

    public void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine("Статус: VIP клиент");
    }
}

class Product
{
    public string name;
    public double price;
    public string category;

    public Product(string name, double price, string category)
    {
        string Name = name;
        this.price = price;
        string Category = category;
    }

    public string GetName()
    {
        return name;
    }

    public double GetPrice()
    {
        return price;
    }
}

class Order
{
    public Customer customer;
    public List<Product> products;

    public Order(Customer customer, List<Product> products)
    {
        this.customer = customer;
        this.products = products;
    }

    public double GetTotalPrice()
    {
        double total = 0;
        foreach (var product in products)
        {
            total += product.GetPrice();
        }
        return total;
    }
}

class Admin : User
{
    public Admin(string name, string email, double balance) : base(name, email, balance) { }

    public void ManageUsers()
    {
        Console.WriteLine("Администратор управляет пользователями.");
    }

    public void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine("Роль: Администратор");
    }
}