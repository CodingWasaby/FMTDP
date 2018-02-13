using FocusMediaMQ;
using System;
using System.Threading;

namespace UnitTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = DateTime.Now;

            Console.WriteLine("Hello World!");

            var mq = MQFactory.GetMQ();
            Action act1 = () => { Test1(); };
            Action act2 = () => { Test2(); };

            mq.JoinQueue("test", act2, null);
            mq.JoinQueue("test", act1, null);

            Console.ReadKey();
        }

        static void Test1()
        {
            Console.WriteLine("Test1");
        }

        static void Test2()
        {
            Console.WriteLine("wait 5 second");
            Thread.Sleep(5000);
            Console.WriteLine("Test2");
        }
    }
}
