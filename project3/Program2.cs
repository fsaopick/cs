using System;

//cлучайная сортировка

    class Program
    {
       static void Main(string[] args)
       {
           int[] numbers = { 5, 2, 52, 8, 3 };
           Random random = new Random();
           int var = 0;

           for (int i = 0; i < numbers.Length; i++)
           {
               int j = random.Next(numbers.Length);

               var = numbers[i];
               numbers[i] = numbers[j];
               numbers[j] = var;
           }


           for (int i = 0; i < numbers.Length; i++)
               Console.WriteLine(numbers[i]);

           Console.ReadKey();
       }
    }