using System;


    // быстрая сортировка

    class Program
    {
       static void Main(string[] args)
       {

           int[] numbers = { 5, 2, 52, 8, 11 };
           QuickSort(numbers, 0, 4);

           foreach (int w in numbers)
           {
               Console.WriteLine(w);
           }

           Console.ReadKey();

       }

       private static int[] QuickSort(int[] numbers, int i, int j)
       {
           if (i < j)
           {
               int q = Partition(numbers, i, j);
               numbers = QuickSort(numbers, i, q);
               numbers = QuickSort(numbers, q + 1, j);
           }
           return numbers;
       }

       private static int Partition(int[] a, int p, int r)
       {
           int x = a[p];
           int i = p - 1;
           int j = r + 1;

           while (true)
           {
               do
               {
                   j--;
               } while (a[j] > x);

               do
               {
                   i++;
               } while (a[i] < x);

               if (i < j)
               {
                   int tmp = a[i];
                   a[i] = a[j];
                   a[j] = tmp;
               }

               else
               {
                   return j;
               }
           }
       }

    }


