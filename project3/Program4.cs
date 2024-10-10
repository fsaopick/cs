using System;

 // сортировка вставками
    class Program
    {
        static void Main(string[] args)
        {

            int[] numbers = { 5, 2, 52, 8, 9 };

            for (int i = 1; i < numbers.Length; i++)
            {
                int k = numbers[i];
                int j = i - 1;

                while (j >= 0 && numbers[j] > k)
                {
                    numbers[j + 1] = numbers[j];
                    numbers[j] = k;
                    j--;
                }
            }

            for (int i = 0; i < numbers.Length; i++)
            {
                Console.WriteLine(numbers[i]);
            }

            Console.ReadKey();

        }

    }