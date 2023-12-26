 using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsumeProduce
{
    public class Program
    {
        private static Random rand = new Random();
        private static Stack<int> stack = new Stack<int>();
        private const int total = 3;
        static void Main(string[] args)
        {
            var producer = new Thread(Produce);
            var consumer = new Thread(Consume);
            producer.Start();
            consumer.Start();
            Console.ReadKey();
        }
        public static void Produce()
        {
            Thread.CurrentThread.Name = "producer";
            if (stack.Count < total)
            {
                try
                {
                    Monitor.Enter(stack);
                    for (int i = 0; i < total; i++)
                    {
                        stack.Push(rand.Next(0,256));
                    }
                    Monitor.Pulse(stack);
                }
                finally
                {
                    Monitor.Exit(stack);
                }
            }
        }
        public static void Consume()
        {
            Thread.CurrentThread.Name = "consumer";
            while (true)
            {
                try
                {
                    Monitor.Enter(stack);
                    if (stack.Count != total)
                    {
                        Monitor.Wait(stack);
                    }
                    else
                    {
                        for (int i = 0; i < total; i++)
                        {
                            Console.WriteLine(stack.Pop());
                        }
                    }
                }
                finally
                {
                    Monitor.Exit(stack);
                }
                
            }
        }
    }
}
