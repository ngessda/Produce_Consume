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
        private static Stack<int> stack = new Stack<int>();
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
            Random rand = new Random();
            Thread.CurrentThread.Name = "producer";
            int total = 9;
            for (int i = 0; i < total; i++) 
            {
                try
                {
                    Monitor.Enter(stack);
                    stack.Push(rand.Next(0, 256));
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
            int[] stack1 = new int[stack.Count];
            Thread.CurrentThread.Name = "consumer";
            int total = 9;
            int count = 1;
            while (count <= total)
            {
                try
                {
                    Monitor.Enter(stack);
                    if (stack.Count > 3)
                    {
                        for (int i = 0; i < stack.Count; i++)
                        {
                            stack1[i] = stack.Pop();
                        }
                    }
                    else
                    {
                        Monitor.Wait(stack);
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
