using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Players generally sit in a circle. The player designated
// to go first says the number "1", and each player thenceforth
// counts one number in turn. However, any number divisible
// by three is replaced by the word fizz and any divisible
// by five by the word buzz. Numbers divisible by both become
// fizz buzz.

// For example, a typical round of fizz buzz would start as follows:

// 1, 2, Fizz, 4, Buzz, Fizz, 7, 8, Fizz, Buzz, 11, Fizz, 13,
// 14, Fizz Buzz, 16, 17, Fizz, 19, Buzz, Fizz, 22, 23, Fizz, ...

// Print a fizz buzz game from 1 to 100.

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            for (int i = 1; i<=100; i++)
            {
                if((i % 3 == 0) && (i % 5 == 0))
                {
                    Console.WriteLine("Fizz Buzz");
                }else if((i % 3 == 0) && (i % 5 > 0))
                {
                    Console.WriteLine("Fizz");
                }
                else if((i % 3 > 0) && (i % 5 == 0))
                {
                    Console.WriteLine("Buzz");
                }
                else
                {
                    Console.WriteLine(i);
                }
            }
            Console.ReadKey();

        }
    }
}
