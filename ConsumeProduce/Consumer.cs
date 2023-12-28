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
            int count = 0;
            Thread.CurrentThread.Name = "consumer";
            while(total >= count)
            {
                try
                {
                    Monitor.Enter(stack);
                    if (total - count < 3)
                    {
                        for (int i = 0; i < stack.Count; i++)
                        {
                            Console.WriteLine(stack.Pop());
                            count++;
                        }
                    }
                    else if (stack.Count < 3)
                    {
                        Monitor.Wait(stack);
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            Console.WriteLine(stack.Pop());
                            count++;
                        }
                        Monitor.Pulse(stack);
                        Console.WriteLine("====================================================");
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
