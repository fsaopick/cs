

Console.WriteLine("Введите число действия: 1 - сложение, 2 - вычитание, 3 - умножение, 4 - деление, 5 - деление с остатком, 6 - нахождение факториала числа, 7 - возведение числа в степень, 8 - нахождение суммы последовательности до числа");
int calc = Convert.ToInt32(Console.ReadLine());

if (calc == 1)
{
    Console.WriteLine("Введите первое число: ");
    int index = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("Введите второе число: ");
    int index1 = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine(Sum(index, index1));
}

else if (calc == 2)
{
    Console.WriteLine("Введите первое число: ");
    int index = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("Введите второе число: ");
    int index1 = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine(Min(index, index1));
}

else if (calc == 3)
{
    Console.WriteLine("Введите первое число: ");
    int index = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("Введите второе число: ");
    int index1 = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine(Umn(index, index1));
}

else if (calc == 4)
{
    Console.WriteLine("Введите первое число: ");
    int index = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("Введите второе число: ");
    int index1 = Convert.ToInt32(Console.ReadLine());

    if (index1 == 0){
        Console.WriteLine("На 0 делить нельзя");
    }
    else{
        Console.WriteLine(Del(index, index1));
    }
}

else if (calc == 5)
{
    Console.WriteLine("Введите первое число: ");
    int index = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("Введите второе число: ");
    int index1 = Convert.ToInt32(Console.ReadLine());

    if (index1 == 0){
        Console.WriteLine("На 0 делить нельзя");
    }
    else{
        Console.WriteLine(Delost(index, index1));
    }
}

else if (calc == 6)
{
    Console.WriteLine("Введите число: ");
    int index = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine(Factorial(index));
}

else if (calc == 7)
{
    Console.WriteLine("Введите число: ");
    int index = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("Введите степень: ");
    int index1 = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine(Stepen(index, index1));
}

else if (calc == 8)
{
    Console.WriteLine("Введите число: ");
    int index = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine(Posl(index));
}

else {
    Console.WriteLine("Вы ввели не то число операции");
}


static int Sum(int a, int b)
{
    return a + b;
}

static int Min(int a, int b)
{
    return a - b;
}

static int Umn(int a, int b)
{
    return a * b;
}
static int Del(int a, int b)
{
    return a / b;
}

static int Delost(int a, int b)
{
    return a % b;
}

static long Factorial(int a)
{
    if (a == 0)
        {
            return 1;
        }
    
    return a * Factorial(a - 1);
}

static double Stepen(int a, int b)
{
    if (b == 0)
        {
            return 1;
        }
    
    else if (b > 0)
        {
            return a * Stepen(a, b - 1);
        }
    else
        {
            return 1.0 / Stepen(a, -b);
        }
}

static int Posl(int a)
{
    if (a < 0)
        {
            return a + Posl(a + 1);
        }
    else if (a == 0)
        {
            return 0;
        }
    else
        {
            return a + Posl(a - 1);
        }
}

