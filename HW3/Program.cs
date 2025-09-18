using System;
using System.Collections.Generic;

namespace HW3
{
    // Lớp Employee
    public class Employee
    {
        public string Name { get; set; }

        public Employee(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"Clerk: {Name}";
        }
    }

    // Lớp Item
    public class Item
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; } // Mức giảm giá

        public Item(string name, double price, double discount = 0.0)
        {
            Name = name;
            Price = price;
            Discount = discount;
        }

        public double GetPrice() => Price;
        public double GetDiscount() => Discount;

        public override string ToString()
        {
            return $"{Name} - Price: {Price:C2}, Discount: {Discount:C2}";
        }
    }

    // Lớp GroceryBill
    public class GroceryBill
    {
        protected Employee Clerk;
        protected List<Item> Items;

        public GroceryBill(Employee clerk)
        {
            Clerk = clerk;
            Items = new List<Item>();
        }

        public virtual void Add(Item i)
        {
            Items.Add(i);
        }

        public virtual double GetTotal()
        {
            double total = 0;
            foreach (var item in Items)
            {
                total += item.GetPrice();
            }
            return total;
        }

        public virtual void PrintReceipt()
        {
            Console.WriteLine(Clerk);
            foreach (var item in Items)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine($"Total: {GetTotal():C2}");
        }
    }

    // Lớp DiscountBill kế thừa GroceryBill
    public class DiscountBill : GroceryBill
    {
        private bool Preferred;

        public DiscountBill(Employee clerk, bool preferred) : base(clerk)
        {
            Preferred = preferred;
        }

        public override double GetTotal()
        {
            if (!Preferred) return base.GetTotal();

            double total = 0;
            foreach (var item in Items)
            {
                total += item.GetPrice() - item.GetDiscount();
            }
            return total;
        }

        public int GetDiscountCount()
        {
            if (!Preferred) return 0;

            int count = 0;
            foreach (var item in Items)
            {
                if (item.GetDiscount() > 0) count++;
            }
            return count;
        }

        public double GetDiscountAmount()
        {
            if (!Preferred) return 0;

            double discount = 0;
            foreach (var item in Items)
            {
                discount += item.GetDiscount();
            }
            return discount;
        }

        public double GetDiscountPercent()
        {
            if (!Preferred) return 0;

            double before = base.GetTotal();
            return (GetDiscountAmount() / before) * 100.0;
        }

        public override void PrintReceipt()
        {
            base.PrintReceipt();
            if (Preferred)
            {
                Console.WriteLine($"Discounted Items: {GetDiscountCount()}");
                Console.WriteLine($"Total Discount: {GetDiscountAmount():C2}");
                Console.WriteLine($"Discount Percent: {GetDiscountPercent():F2}%");
                Console.WriteLine($"Final Total: {GetTotal():C2}");
            }
        }
    }

    // Extension: BillLine
    public class BillLine
    {
        public Item Item { get; private set; }
        public int Quantity { get; private set; }

        public BillLine(Item item, int quantity)
        {
            Item = item;
            Quantity = quantity;
        }

        public void SetQuantity(int q) => Quantity = q;
        public int GetQuantity() => Quantity;

        public void SetItem(Item i) => Item = i;
        public Item GetItem() => Item;

        public double GetLineTotal(bool preferred = false)
        {
            if (preferred)
                return (Item.GetPrice() - Item.GetDiscount()) * Quantity;
            return Item.GetPrice() * Quantity;
        }

        public override string ToString()
        {
            return $"{Item.Name} x{Quantity} - Unit: {Item.Price:C2}, Discount: {Item.Discount:C2}";
        }
    }

    // GroceryBill mới dùng BillLine
    public class GroceryBillWithLine
    {
        private Employee Clerk;
        private List<BillLine> BillLines;

        public GroceryBillWithLine(Employee clerk)
        {
            Clerk = clerk;
            BillLines = new List<BillLine>();
        }

        public void Add(BillLine line)
        {
            BillLines.Add(line);
        }

        public double GetTotal(bool preferred = false)
        {
            double total = 0;
            foreach (var line in BillLines)
            {
                total += line.GetLineTotal(preferred);
            }
            return total;
        }

        public void PrintReceipt(bool preferred = false)
        {
            Console.WriteLine(Clerk);
            foreach (var line in BillLines)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine($"Total: {GetTotal(preferred):C2}");
        }
    }

    // Demo Program
    class Program
    {
        static void Main(string[] args)
        {
            Employee clerk = new Employee("Alice");
            Item candy = new Item("Candy Bar", 1.35, 0.25);
            Item milk = new Item("Milk", 2.50);
            Item bread = new Item("Bread", 3.00, 0.50);

            Console.WriteLine("=== GroceryBill (no discount) ===");
            GroceryBill bill = new GroceryBill(clerk);
            bill.Add(candy);
            bill.Add(milk);
            bill.Add(bread);
            bill.PrintReceipt();

            Console.WriteLine("\n=== DiscountBill (preferred customer) ===");
            DiscountBill discountBill = new DiscountBill(clerk, true);
            discountBill.Add(candy);
            discountBill.Add(milk);
            discountBill.Add(bread);
            discountBill.PrintReceipt();

            Console.WriteLine("\n=== Extension: GroceryBillWithLine ===");
            GroceryBillWithLine billWithLine = new GroceryBillWithLine(clerk);
            billWithLine.Add(new BillLine(candy, 3));
            billWithLine.Add(new BillLine(bread, 2));
            billWithLine.PrintReceipt(true);
        }
    }
}