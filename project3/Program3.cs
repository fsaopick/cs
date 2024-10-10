using System;

// шейкерная сортировка

       class Program
       {
           static void Main(string[] args)
           {
               int[] numbers = { 5, 2, 52, 8, 6 };

               int left = 0,
               right = numbers.Length - 1;

               while (left < right)
               {
                   for (int i = left; i < right; i++)
                   {
                       if (numbers[i] > numbers[i + 1])
                           Sort(numbers, i, i + 1);
                   }
                   right--;

                   for (int i = right; i > left; i--)
                   {
                       if (numbers[i - 1] > numbers[i])
                           Sort(numbers, i - 1, i);
                   }
                   left++;
               }

               for (int i = 0; i < numbers.Length; i++)
                   Console.WriteLine(numbers[i]);

               Console.ReadKey();

           }

           static void Sort(int[] numbers, int i, int j)
           {
               int temp = numbers[i];
               numbers[i] = numbers[j];
               numbers[j] = temp;
           }

       }
    