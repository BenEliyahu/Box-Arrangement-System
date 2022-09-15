using _1060DS;
using BoxesProject;
using LinkedListAssignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Porgram
{
    internal class Program
    {
        //static void Main(string[] args)
        //{
        //    NotificInterfaceImplementaion b = new NotificInterfaceImplementaion();
        //    Manager n = new Manager(b);
        //    OpenShop();
        //    Console.ReadLine();
        //}
        static void Main(string[] args)
        {
            //var consoleNotifier = new NotificationImplementation();
            //Manager n = new Manager(consoleNotifier, 30);
            //n.Supply(5, 10, 10);
            //n.Supply(5, 6, 10);
            //n.Supply(5, 8, 10);
            //n.Supply(7, 10, 10);
            //n.Supply(3, 70, 8);

            //Console.WriteLine(n.Show());

            //double pe1;
            //double pe2;

            // Console.Write("Check if it match: ");
            // Console.WriteLine(n.Purchase(6, 10, 10, out pe1, out pe2));
            // Console.WriteLine(n.Show());
            // Console.WriteLine($"percenrage height: {pe1}%");
            // Console.WriteLine($"percenrage width:  {pe2}%");


            // LogicList<int> logicList = new LogicList<int>();
            // logicList.AddFirst(1);
            // logicList.AddFirst(2);
            // logicList.AddFirst(3);
            // logicList.AddFirst(4);
            // logicList.AddFirst(5);


            // Console.WriteLine(logicList);
            // LogicList<int>.Node node = new LogicList<int>.Node(3);
            // //LogicList<int>.Node node1 = new LogicList<int>.Node(5);
            // logicList.RellocateToStart(node);
            //// Console.WriteLine(logicList);
            //// logicList.ReAllowcateToEnd(node1);
            // Console.WriteLine(logicList);

            //Console.WriteLine(n.ShowList());
            //  n.CheckExpireredBoxes();
            //Console.WriteLine(n.ShowList());

            //Console.WriteLine(n.Purchase(5,10,5,out pe1,out pe2));
            //Console.WriteLine(n.ShowList());
            //Console.WriteLine(n.Show());

            NotificationImplementation b = new NotificationImplementation();
            Manager n = new Manager(b, 0);
            OpenShop();

            Console.ReadKey();
        }
        static void OpenShop()
        {
            NotificationImplementation b = new NotificationImplementation();
            Manager n = new Manager(b, 0);
            string shop;
            double pe1;
            double pe2;
            n.Supply(5, 5, 20);
            n.Supply(7, 10, 17);
            n.Supply(9, 15, 24);
            n.Supply(11, 20, 29);
            n.Supply(13, 25, 13);
            n.Supply(15, 30, 17);
            n.Supply(17, 35, 15);
            n.Supply(19, 40, 9);
            n.Supply(21, 45, 21);
            n.Supply(23, 50, 18);
            n.Supply(25, 55, 11);

            bool inp = n.Show(out shop);
            if (inp)
            {
                Console.WriteLine("Welcome To IKEA NORTH!\nThis is our box shop:\n");
                Console.WriteLine(shop);
                Console.Write("Lets Start shop, first put your size of box that you want:\ninsert WIDTH of the box:");
                int width = int.Parse(Console.ReadLine());
                Console.Write("insert HEIGHT of the box:");
                int height = int.Parse(Console.ReadLine());
                Console.Write("insert AMOUNT of boxes:");
                int amount = int.Parse(Console.ReadLine());
                Console.WriteLine(n.Purchase(width, height, amount, out pe1, out pe2));

                // n.Purchase(5, 5, 10, out pe1, out pe2);
                n.Show(out shop);
                Console.WriteLine(shop);
                Console.WriteLine("Thank You And Have A Good Day!");
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}
