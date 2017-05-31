using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionExercise1
{
    class Program
    {
        static void Main(string[] args)
        {
            var order = new Order()
            {
                Name = "Widget A",
                Price = 3.14m,
                Quantity = 100,
                Paid = false
            };

            var processSteps = new List<Action<Order>>();

            Action<Order> InStockFormat = a => a.Name = a.Name + " - in stock";
            Action<Order> AddTax = a => a.Price = (a.Price * 1.25M) ;
            Action<Order> QuantityAndPaidFlag = a => {
                a.Name = a.Name + " - paid";
                a.Paid = true;
                a.Quantity -= 10;
                };

            processSteps.Add(InStockFormat);
            processSteps.Add(AddTax);
            processSteps.Add(QuantityAndPaidFlag);

            ProcessOrder(order, processSteps);

            Console.WriteLine(order.Name);
            Console.WriteLine(order.Price);
            Console.WriteLine(order.Quantity);
            Console.ReadKey();






        }
        public static void ProcessOrder(Order order, List<Action<Order>> actions)
        {
            foreach (var a in actions)
            {
                a(order);
            }
        }
    }
    public class Order
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool Paid { get; set; }
    }
}
