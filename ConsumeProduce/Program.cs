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
        static Stack<int> stack = new Stack<int>();
        static void Main(string[] args)
        {
            int total = 28;
            Producer producer = new Producer(stack, total);
            Consumer consumer = new Consumer(stack, total);
            Console.ReadKey();
        }
        

    }
}
