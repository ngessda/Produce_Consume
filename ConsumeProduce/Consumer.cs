using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace ConsumeProduce
{
    public class Consumer
    {
        private Stack<int> stack;
        private Thread consumer;
        private int total = 0;
        public Consumer(Stack<int> generalStack, int generalTotal)
        {
            total = generalTotal;
            stack = generalStack;
            consumer = new Thread(Consume);
            consumer.Start();
        }
        public void Consume()
        {
            Thread.CurrentThread.Name = "consumer";
            try
            {
                Monitor.Enter(stack);
                if (stack.Count < 3)
                {
                    Monitor.Wait(stack);
                }
                else
                {
                    for (int i = 0; i < 3; i++)
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
