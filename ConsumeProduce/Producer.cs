using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsumeProduce
{
    public class Producer
    {
        private static Stack<int> stack;
        private Thread producer;
        private static Random rand = new Random();
        private int total = 0;
        public Producer(Stack<int> generalStack, int generalTotal)
        {
            stack = generalStack;
            total = generalTotal;
            producer = new Thread(Produce);
            producer.Start();
        }
        public void Produce()
        {
            Thread.CurrentThread.Name = "producer";
            if (stack.Count < total)
            {
                try
                {
                    Monitor.Enter(stack);
                    for (int i = 0; i < total; i++)
                    {
                        stack.Push(rand.Next(0, 256));
                        if (stack.Count >= 3)
                        {
                            Monitor.Pulse(stack);
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
