using System;
using System.Collections.Generic;

class Program
{
    class Product
    {
        private string _name;
        private string _id;
        private double _price;
        private int _quantity;

        public Product(string name, string id, double price, int quantity)
        {
            _name = name;
            _id = id;
            _price = price;
            _quantity = quantity;
        }

        public double GetTotalCost()
        {
            return _price * _quantity;
        }

        public string GetLabel()
        {
            return $"{_name} (ID: {_id}) x{_quantity}";
        }
    }

    class Address
    {
        private string _street;
        private string _city;
        private string _state;
        private string _country;

        public Address(string street, string city, string state, string country)
        {
            _street = street;
            _city = city;
            _state = state;
            _country = country;
        }

        public bool IsInUSA()
        {
            return _country.ToLower() == "usa" || _country.ToLower() == "united states";
        }

        public string GetFullAddress()
        {
            return $"{_street}\n{_city}, {_state}\n{_country}";
        }
    }

    class Customer
    {
        private string _name;
        private Address _address;

        public Customer(string name, Address address)
        {
            _name = name;
            _address = address;
        }

        public bool LivesInUSA()
        {
            return _address.IsInUSA();
        }

        public string GetName()
        {
            return _name;
        }

        public string GetAddressString()
        {
            return _address.GetFullAddress();
        }
    }

    class Order
    {
        private List<Product> _products;
        private Customer _customer;

        public Order(Customer customer)
        {
            _customer = customer;
            _products = new List<Product>();
        }

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public double GetTotalCost()
        {
            double total = 0;
            foreach (var product in _products)
            {
                total += product.GetTotalCost();
            }

            total += _customer.LivesInUSA() ? 5.0 : 35.0;
            return total;
        }

        public string GetPackingLabel()
        {
            string label = "Packing Label:\n";
            foreach (var product in _products)
            {
                label += product.GetLabel() + "\n";
            }
            return label;
        }

        public string GetShippingLabel()
        {
            return $"Shipping Label:\n{_customer.GetName()}\n{_customer.GetAddressString()}";
        }
    }

    static void Main(string[] args)
    {
        Address addr1 = new Address("123 Maple St", "Dallas", "TX", "USA");
        Customer cust1 = new Customer("Alice Johnson", addr1);
        Order order1 = new Order(cust1);
        order1.AddProduct(new Product("Water Bottle", "WB001", 8.99, 2));
        order1.AddProduct(new Product("Notebook", "NB002", 3.50, 5));

        Address addr2 = new Address("456 King Rd", "Toronto", "ON", "Canada");
        Customer cust2 = new Customer("Liam Smith", addr2);
        Order order2 = new Order(cust2);
        order2.AddProduct(new Product("Pen Pack", "PP003", 1.25, 10));
        order2.AddProduct(new Product("Desk Organizer", "DO004", 12.75, 1));
        order2.AddProduct(new Product("Sticky Notes", "SN005", 2.00, 3));

        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Cost: ${order1.GetTotalCost():0.00}\n");

        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Total Cost: ${order2.GetTotalCost():0.00}");
    }
}
