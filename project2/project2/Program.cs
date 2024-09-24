namespace project2;
using System;

public class Program1
{
    static void Main()
    {
        Console.WriteLine("Введите число действия: 1 - сложение, 2 - вычитание, 3 - умножение, 4 - деление, 5 - деление с остатком");

        int calc = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Введите первое число: ");

        int index = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Введите второе число: ");
      
        int index1 = Convert.ToInt32(Console.ReadLine());

        switch (calc) {
            case 1:
                {
                    Console.WriteLine("сложение = " + (index + index1));
                    break;
                }

            case 2:
                {
                    Console.WriteLine("вычитание = " + (index - index1));
                    break;
                }

            case 3:
                {
                    Console.WriteLine("умножение = " + (index * index1));
                    break;
                }
            case 4:
                {
                    if (index1 == 0){
                        Console.WriteLine("делить на 0 нельзя");
                    }
                    else {
                        Console.WriteLine("деление = " + (index / index1));
                    }
                    break;
                }

            case 5:
                {
                    if (index1 == 0){
                        Console.WriteLine("делить на 0 нельзя");
                    }
                    else {
                        Console.WriteLine("деление с остатком = " + (index % index1));
                    }
                    break;
                }
            default:
                {
                    Console.WriteLine("Вы ввели не то число действия!");
                    break;
                }
                
        } 
    }
}


