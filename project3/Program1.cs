using System;

 //сортировка пузырьком
 
class Program
    {
       static void Main(string[] args)
       {
           int[] numbers = { 5, 2, 52, 8, 1 };
           int var = 0;

           for (int i = 0; i < numbers.Length; i++)
           {
               for (int sort = 0; sort < numbers.Length - 1; sort++)
               {
                   if (numbers[sort] > numbers[sort + 1])
                   {
                       var = numbers[sort + 1];
                       numbers[sort + 1] = numbers[sort];
                       numbers[sort] = var;
                   }
               }
           }

           for (int i = 0; i < numbers.Length; i++)
               Console.Write(numbers[i] + " ");

           Console.ReadKey();
       }
    }