using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

class Program
{
    static List<Zadacha> zadachi = new List<Zadacha>();
    static List<Polzovatel> polzovateli = new List<Polzovatel>();
    static string path = @"/Users/andrey/Desktop/";

    static async Task Main(string[] args)
    {

        Console.WriteLine("подождите, ваши данные загружаются");
        await Task.Delay(3000);
        await zagruzka();

        while (true)
        {
            Console.WriteLine("выберите 1 для регистрации, 2 - для авторизации и 3, чтобы выйти из программы");
            int rol = int.Parse(Console.ReadLine());

            if (rol == 1)
            {
                Console.WriteLine("вы находитесь в меню регистрации");

                Console.WriteLine("введите имя пользователя:");
                string imya = Console.ReadLine();

                Console.WriteLine("введите пароль для пользователя:");
                string parol = Console.ReadLine();

                Polzovatel polzovatel = new Polzovatel(imya, parol);
                polzovateli.Add(polzovatel);

                Console.WriteLine("пользователь успешно зарегистрирован.");
            }
            else if (rol == 2)
            {
                Console.WriteLine("введите имя пользователя:");
                string imya = Console.ReadLine();

                Console.WriteLine("введите пароль пользователя:");
                string parol = Console.ReadLine();

                Polzovatel polzovatel = polzovateli.FirstOrDefault(p => p.Imya == imya && p.Parol == parol);

                if (polzovatel != null)
                {
                    Console.WriteLine("Авторизация успешна.");

                    while (true)
                    {
                        Console.WriteLine("1) добавить задачу" +
                            "\n2) удалить задачу" +
                            "\n3) редактировать задачу" +
                            "\n4) показать все задачи" +
                            "\n5) Выход");

                        int vybor = int.Parse(Console.ReadLine());

                        switch (vybor)
                        {
                            case 1:
                                Console.WriteLine("введите заголовок задачи:");
                                string zagolovok = Console.ReadLine();

                                Console.WriteLine("введите описание задачи:");
                                string opisanie = Console.ReadLine();

                                Console.WriteLine("введите приоритет задачи (низкий, средний, высокий):");
                                string prioritet = Console.ReadLine();

                                polzovatel.dobavlenye(zadachi, zagolovok, opisanie, prioritet);
                                break;

                            case 2:
                                Console.WriteLine("введите заголовок задачи для удаления:");
                                string zagolovokUdalenie = Console.ReadLine();

                                polzovatel.udalenye(zadachi, zagolovokUdalenie);
                                break;

                            case 3:
                                Console.WriteLine("введите заголовок задачи для редактирования:");
                                string zagolovokRedaktirovanie = Console.ReadLine();

                                Console.WriteLine("введите новое описание задачи:");
                                string novoeOpisanie = Console.ReadLine();

                                Console.WriteLine("введите новый приоритет задачи (низкий, средний, высокий):");
                                string noviyPrioritet = Console.ReadLine();

                                Console.WriteLine("введите новый статус задачи (недоступна, в процессе, завершена):");
                                string noviyStatus = Console.ReadLine();

                                polzovatel.redaktirovanie(zadachi, zagolovokRedaktirovanie, novoeOpisanie, noviyPrioritet, noviyStatus);
                                break;

                            case 4:
                                polzovatel.pokazatVseZadachi(zadachi);
                                break;

                            case 5:
                                await sohranenye();
                                return;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("неверное имя пользователя или пароль");
                }
            }
            else if (rol == 3)
            {
                await sohranenye();
                break;
            }
        }
    }

    static async Task zagruzka()
    {
        string zadachiPath = Path.Combine(path, "zadachi.txt");
        string polzovateliPath = Path.Combine(path, "polzovateli.txt");

        if (File.Exists(zadachiPath))
        {
            var lines = await File.ReadAllLinesAsync(zadachiPath);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                zadachi.Add(new Zadacha(parts[0], parts[1], parts[2], parts[3]));
                await Task.Delay(3000);
            }
        }

        Console.WriteLine("данные о задачах были загружены");
        await Task.Delay(3000);

        if (File.Exists(polzovateliPath))
        {
            var lines = await File.ReadAllLinesAsync(polzovateliPath);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                var polzovatel = new Polzovatel(parts[0], parts[1]);
                polzovateli.Add(polzovatel);
            }
        }

        Console.WriteLine("данные пользователей были загружены");
        Console.WriteLine(" ");
    }

    static async Task sohranenye()
    {
        string zadachiPath = Path.Combine(path, "zadachi.txt");
        string polzovateliPath = Path.Combine(path, "polzovateli.txt");

        var zadachiLines = zadachi.Select(z => $"{z.Zagolovok},{z.Opisanie},{z.Prioritet},{z.Status}");
        await File.WriteAllLinesAsync(zadachiPath, zadachiLines);

        var polzovateliLines = polzovateli.Select(p => $"{p.Imya},{p.Parol}");
        await File.WriteAllLinesAsync(polzovateliPath, polzovateliLines);
    }
}

class Zadacha
{
    public string Zagolovok { get; set; }
    public string Opisanie { get; set; }
    public string Prioritet { get; set; }
    public string Status { get; set; }

    public Zadacha(string zagolovok, string opisanie, string prioritet, string status)
    {
        Zagolovok = zagolovok;
        Opisanie = opisanie;
        Prioritet = prioritet;
        Status = status;
    }
}

class Polzovatel
{
    public string Imya { get; set; }
    public string Parol { get; set; }

    public Polzovatel(string imya, string parol)
    {
        Imya = imya;
        Parol = parol;
    }

    public void dobavlenye(List<Zadacha> zadachi, string zagolovok, string opisanie, string prioritet)
    {
        if (!suchesnvuet(zadachi, zagolovok))
        {
            zadachi.Add(new Zadacha(zagolovok, opisanie, prioritet, "недоступна"));
            Console.WriteLine($"задача '{zagolovok}' добавлена");
        }
        else
        {
            Console.WriteLine($"задача '{zagolovok}' уже существует");
        }
    }

    private bool suchesnvuet(List<Zadacha> zadachi, string zagolovok)
    {
        return zadachi.Any(z => z.Zagolovok == zagolovok);
    }

    public void udalenye(List<Zadacha> zadachi, string zagolovok)
    {
        var zadacha = zadachi.FirstOrDefault(z => z.Zagolovok == zagolovok);
        if (zadacha != null)
        {
            zadachi.Remove(zadacha);
            Console.WriteLine($"задача '{zagolovok}' удалена");
        }
        else
        {
            Console.WriteLine($"задача '{zagolovok}' не найдена");
        }
    }

    public void redaktirovanie(List<Zadacha> zadachi, string zagolovok, string novoeOpisanie, string noviyPrioritet, string noviyStatus)
    {
        var zadacha = zadachi.FirstOrDefault(z => z.Zagolovok == zagolovok);
        if (zadacha != null)
        {
            zadacha.Opisanie = novoeOpisanie;
            zadacha.Prioritet = noviyPrioritet;
            zadacha.Status = noviyStatus;
            Console.WriteLine($"задача '{zagolovok}' отредактирована");
        }
        else
        {
            Console.WriteLine($"задача '{zagolovok}' не найдена");
        }
    }

    public void pokazatVseZadachi(List<Zadacha> zadachi)
    {
        foreach (var zadacha in zadachi)
        {
            Console.WriteLine($"заголовок: {zadacha.Zagolovok}, описание: {zadacha.Opisanie}, приоритет: {zadacha.Prioritet}, статус: {zadacha.Status}");
        }
    }
}








//internal class Program
//{
//    static async Task Main(string[] args)
//    {
//        Console.WriteLine("Готовлю чай...");
//        Task coffeeTask = MakeCoffeeAsync();

//        Console.WriteLine("Готовлю бутерброды...");
//        Task butTask = MakeButAsync();

//        Console.WriteLine("Программирую");
//        await coffeeTask;
//        await butTask;

//    }

//    static async Task MakeCoffeeAsync()
//    {
//        await Task.Delay(3000);
//        Console.WriteLine("Чай сделан");
//    }

//    static async Task MakeButAsync()
//    {
//        await Task.Delay(5000);
//        Console.WriteLine("Бутерброды сделаны");
//    }
//}



//internal class Program
//{
//    static async Task Main(string[] args)
//    {
//        Console.WriteLine("Готовлю кофе...");
//        Task coffeeTask = MakeCoffeeAsync();

//        Console.WriteLine("Программирую");
//        await coffeeTask;

//    }

//    static async Task MakeCoffeeAsync()
//    {
//        await Task.Delay(3000);
//        Console.WriteLine("Кофе сделан");
//    }
//}



//internal class Program
//{
//    static async Task Main(string[] args)
//    {
//        int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

//        int max = await FindMaxIndex(array);
//        Console.WriteLine("Максимальный элемент: " + max);
//    }

//    static async Task<int> FindMaxIndex(int[] array)
//    {
//        await Task.Delay(1000);
//        return array.Min();
//    }
//}


//internal class Program
//{
//    static async Task Main(string[] args)
//    {
//        int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 12, 10 };

//        int[] max = await Sort(array);
//        Console.WriteLine("Отсортированный массив");
//        Console.WriteLine(string.Join(", ", max));

//        foreach (var i in array)
//        {
//            Console.WriteLine(i + " ");
//        }

//    }

//    static async Task<int[]> Sort(int[] array)
//    {
//        await Task.Delay(1000);
//        return array.OrderBy(x => x).ToArray();
//    }
//}



//class Program
//{
//    static void Main(string[] args)
//    {
//        budilnik();
//        zapravitkrovat();
//        umitsya();
//        pozavtarakat();
//        rukzak();
//        odetsya();
//        put();
//    }

//    static void budilnik()
//    {
//        Console.WriteLine("Выключить будильник");
//        System.Threading.Thread.Sleep(1000);
//    }

//    static void zapravitkrovat()
//    {
//        Console.WriteLine("Заправить кровать");
//        System.Threading.Thread.Sleep(1000);
//    }

//    static void umitsya()
//    {
//        Console.WriteLine("Умыться");
//        System.Threading.Thread.Sleep(1000);
//    }

//    static void pozavtarakat()
//    {
//        Console.WriteLine("Позавтракать");
//        System.Threading.Thread.Sleep(1000);
//    }

//    static void rukzak()
//    {
//        Console.WriteLine("Собрать рюкзак");
//        System.Threading.Thread.Sleep(1000);
//    }

//    static void odetsya()
//    {
//        Console.WriteLine("Одеться");
//        System.Threading.Thread.Sleep(1000);
//    }

//    static void put()
//    {
//        Console.WriteLine("Выйти из дома и доехать до техникума");
//        System.Threading.Thread.Sleep(1000);
//    }
//}


//class Program
//{
//    static void Main(string[] args)
//    {
//        Console.WriteLine("Начал готовить");
//        NalVody();
//        VaruSoup();
//        CookMeat();
//        Vash();
//        Console.WriteLine("Готово");

//    }

//    static void NalVody()
//    {
//        Console.WriteLine("Налить воду");
//        System.Threading.Thread.Sleep(1000);
//    }

//    static void VaruSoup()
//    {
//        Console.WriteLine("Варю суп");
//        System.Threading.Thread.Sleep(1000);
//    }

//    static void CookMeat()
//    {
//        Console.WriteLine("Жарю мясо");
//        System.Threading.Thread.Sleep(1000);
//    }

//    static void Vash()
//    {
//        Console.WriteLine("Мою тарелки");
//        System.Threading.Thread.Sleep(1000);
//    }
//}



//class Program
//{
//    private static object lockObject = new object();
//    private static int kol = 0;

//    static void Main(string[] args)
//    {
//        Thread thread = new Thread(Task);
//        Thread thread1 = new Thread(Task);

//        thread.Start();
//        thread1.Start();
//        thread.Join();
//        thread1.Join();

//        Console.WriteLine("Результат" + kol);

//        for (int i = 0; i < 5; i++)
//        {
//            Console.WriteLine("Основной поток " + i);
//            Thread.Sleep(500);
//        }


//    }

//    static void Task()
//    {
//        for (int i = 0; i < 5; i++)
//        {
//            Console.WriteLine("Дополнительный поток " + i);
//            Thread.Sleep(1000);
//        }
//    }
//}




















//class Pryamougolnik
//{

//    private double width;
//    private double height;

//    public Pryamougolnik()
//    {
//        width = 5;
//        height = 10;
//        Console.WriteLine("Конструктор по умолчанию");
//    }

//    public Pryamougolnik(double width, double height)
//    {
//        this.width = width;
//        this.height = height;
//        Console.WriteLine("Конструктор с параметрами");
//    }

//    public Pryamougolnik(Pryamougolnik other)
//    {
//        this.width = other.width;
//        this.height = other.height;
//        Console.WriteLine("Конструктор копирования");
//    }

//    public double Ploshad()
//    {
//        return width * height;
//    }

//    public double Perimetr()
//    {
//        return 2 * (width * height);
//    }

//    public void infa()
//    {
//        Console.WriteLine($"Ширина: {width}, Высота: {height} , Площадь: {Ploshad()} , Периметр: {Perimetr()}");
//    }

//    public void ochistka()
//    {
//        Console.WriteLine($"Деструктор вызван для ширины: {width} и высоты: {height}");
//    }

//    ~Pryamougolnik()
//    {
//        ochistka();
//    }

//}


//class Program
//{
//    static void Main(string[] args)
//    {
//        Pryamougolnik Pryamougolnik = new Pryamougolnik();
//        Pryamougolnik.infa();

//        Pryamougolnik Pryamougolnik2 = new Pryamougolnik(6, 10);
//        Pryamougolnik2.infa();

//        Pryamougolnik Pryamougolnik3 = new Pryamougolnik(Pryamougolnik);
//        Pryamougolnik3.infa();

//        Console.WriteLine(" ");

//        Pryamougolnik.ochistka();
//        Pryamougolnik2.ochistka();
//        Pryamougolnik3.ochistka();

//    }
//}




//class Pryamougolnik
//{

//    private double width;
//    private double height;

//    public Pryamougolnik()
//    {
//        width = 5;
//        height = 10;
//        Console.WriteLine("Конструктор по умолчанию");
//    }

//    public Pryamougolnik(double width, double height)
//    {
//        this.width = width;
//        this.height = height;
//        Console.WriteLine("Конструктор с параметрами");
//    }

//    public Pryamougolnik(Pryamougolnik other)
//    {
//        this.width = other.width;
//        this.height = other.height;
//        Console.WriteLine("Конструктор копирования");
//    }

//    public double Ploshad()
//    {
//        return width * height;
//    }

//    public double Perimetr()
//    {
//        return 2 * (width * height);
//    }

//    public void infa()
//    {
//        Console.WriteLine($"Ширина: {width}, Высота: {height} , Площадь: {Ploshad()} , Периметр: {Perimetr()}");
//    }

//    public void Print()
//    {
//       Console.WriteLine("Деструктор вызван");
//    }

//    ~Pryamougolnik()
//    {
//        Print();
//    }

//}


//class Program
//{
//    static void Main(string[] args)
//    {
//        Pryamougolnik Pryamougolnik = new Pryamougolnik();
//        Pryamougolnik.infa();
//        Pryamougolnik.Print();

//        Pryamougolnik Pryamougolnik2 = new Pryamougolnik(6, 10);
//        Pryamougolnik2.infa();
//        Pryamougolnik2.Print();

//        Pryamougolnik Pryamougolnik3 = new Pryamougolnik(Pryamougolnik);
//        Pryamougolnik3.infa();
//        Pryamougolnik3.Print();

//        Console.WriteLine(" ");
//    }
//}





//class Pryamougolnik
//{
//    private double width;
//    private double height;

//    public Pryamougolnik()
//    {
//        width = 5;
//        height = 10;
//        Console.WriteLine("Конструктор по умолчанию");
//    }

//    public Pryamougolnik(double width, double height)
//    {
//        this.width = width;
//        this.height = height;
//        Console.WriteLine("Конструктор с параметрами");
//    }

//    public Pryamougolnik(Pryamougolnik other)
//    {
//        this.width = other.width;
//        this.height = other.height;
//        Console.WriteLine("Конструктор копирования");
//    }

//    public double Ploshad()
//    {
//        return width * height;
//    }

//    public double Perimetr()
//    {
//        return 2 * (width + height); 
//    }

//    public void infa()
//    {
//        Console.WriteLine($"Ширина: {width}, Высота: {height}, Площадь: {Ploshad()}, Периметр: {Perimetr()}");
//    }

//    public void Cleanup()
//    {
//        Console.WriteLine("Очистка ресурсов (вызвано вручную)");
//    }

//    ~Pryamougolnik()
//    {
//        Cleanup(); 
//    }
//}

//class Program
//{
//    static void Main(string[] args)
//    {
//        Pryamougolnik Pryamougolnik = new Pryamougolnik();
//        Pryamougolnik.infa();

//        Pryamougolnik Pryamougolnik2 = new Pryamougolnik(6, 10);
//        Pryamougolnik2.infa();

//        Pryamougolnik Pryamougolnik3 = new Pryamougolnik(Pryamougolnik);
//        Pryamougolnik3.infa();

//        Pryamougolnik.Cleanup();
//        Pryamougolnik2.Cleanup();
//        Pryamougolnik3.Cleanup();

//        GC.Collect();
//        GC.WaitForPendingFinalizers();

//        Console.WriteLine("Завершение программы.");
//    }
//}


//class Animal

//{
//    public int kol;
//    public String name;
//    public Animal(int kol, string name)
//    {
//        this.kol = kol;
//        this.name = name;
//    }

//    public Animal(Animal vce) 
//    {
//        kol = vce.kol;
//        name = vce.name;
//    }
//}


//class Animal

//{
//    public static int age;

//    static Animal()
//    {
//        age = 40;
//    }
//}

//class Animal

//{
//    public static int age;

//    ~Animal()
//    {
//        age = 40;
//    }
//}




//class Animal
//{
//    public string Name { get; set; }
//    public int Age;

//    public Animal()
//    {
//        Name = "Ptica";
//        Age = 100;
//        Console.WriteLine("Конструктор по умолчанию");
//    }

//    public Animal(string name, int age)
//    {
//        Name = name;
//        Age = age;
//        Console.WriteLine("Конструктор с параметрами");
//    }

//    public Animal(Animal other)
//    {
//        Name = other.Name;
//        Age = other.Age;
//        Console.WriteLine("Конструктор копирования");
//    }

//    ~Animal()
//    {
//        Console.WriteLine($"Деструктор {Name}" );
//    }

//}


//class Program
//{
//    static void Main(string[] args)
//    {
//        Animal animal = new Animal();
//        Animal animal2 = new Animal("dfdsfs", 2);
//        Animal animal3 = new Animal(animal);
//    }
//}













































//class People
//{
//    public virtual void Zahod()
//    {
//        Console.WriteLine("Вы зашли за человека");
//    }
//}

//class Pols : People
//{
//    public override void Zahod()
//    {
//        Console.WriteLine("Вы зашли за пользователя");
//    }
//}

//class Bibl : People
//{
//    public override void Zahod()
//    {
//        Console.WriteLine("Вы заходите за библиотекаря");
//    }
//}

//class Program
//{
//    static List<Kniga> knigi = new List<Kniga>();
//    static List<Polzovatel> polzovateli = new List<Polzovatel>();
//    static string path = @"/Users/andrey/Desktop/";

//    static void Main(string[] args)
//    {
//        zagruzka();

//        while (true)
//        {
//            Console.WriteLine("Выберите роль(либо напишите 3, чтобы выйти из программы): 1 - Библиотекарь, 2 - Пользователь");
//            int rol = int.Parse(Console.ReadLine());

//            if (rol == 1)
//            {

//                Bibl bibl = new Bibl();
//                bibl.Zahod();

//                Console.WriteLine("Введите имя библиотекаря:");
//                string imya = Console.ReadLine();

//                Bibliotekar bibliotekar = new Bibliotekar(imya);

//                while (true)
//                {
//                    Console.WriteLine("1) Добавить книгу" +
//                        "\n2) Удалить книгу" +
//                        "\n3) Зарегистрировать пользователя" +
//                        "\n4) Показать всех пользователей" +
//                        "\n5) Показать все книги" +
//                        "\n6) Выход");

//                    int vybor = int.Parse(Console.ReadLine());

//                    switch (vybor)
//                    {
//                        case 1:

//                            Console.WriteLine("Введите название и автора:");
//                            bibliotekar.dobavlenye(knigi, Console.ReadLine(), Console.ReadLine());

//                            break;

//                        case 2:

//                            Console.WriteLine("Введите название книги:");
//                            bibliotekar.udalenye(knigi, Console.ReadLine());

//                            break;

//                        case 3:

//                            Console.WriteLine("Введите имя пользователя:");
//                            bibliotekar.registr(polzovateli, Console.ReadLine());

//                            break;

//                        case 4:

//                            bibliotekar.polzovat(polzovateli);
//                            break;

//                        case 5:

//                            bibliotekar.vseknigi(knigi);
//                            break;

//                        case 6: 
//                            break; 
//                    }

//                    if (vybor == 6) 
//                    {
//                        break;
//                    }

//                }

//            } 
//            else if (rol == 2)
//            {
//                Pols pols = new Pols();
//                pols.Zahod();

//                Console.WriteLine("Введите имя пользователя:");
//                string imya = Console.ReadLine();
//                Polzovatel polzovatel = polzovateli.FirstOrDefault(p => p.Imya == imya) ?? new Polzovatel(imya);

//                while (true)
//                {
//                    Console.WriteLine("1) Показать доступные книги" +
//                        "\n2) Взять книгу" +
//                        "\n3) Вернуть книгу" +
//                        "\n4) Показать мои книги" +
//                        "\n5) Выход");

//                    int vybor = int.Parse(Console.ReadLine());

//                    switch (vybor)
//                    {
//                        case 1:

//                            foreach (var k in knigi)
//                                if (k.Status)
//                                    Console.WriteLine($"{k.Nazvanie} Автор: {k.Avtor}");
//                            break;

//                        case 2:

//                            Console.WriteLine("Введите название книги:");
//                            string nazvanie = Console.ReadLine();

//                            Kniga kniga = knigi.FirstOrDefault(k => k.Nazvanie == nazvanie);
//                            if (kniga != null && kniga.Status)
//                            {
//                                kniga.Status = false;
//                                polzovatel.knigivz.Add(kniga);
//                                Console.WriteLine($"Вы взяли книгу '{kniga.Nazvanie}'.");

//                            }
//                            else
//                            {
//                                Console.WriteLine("Книга недоступна или не найдена.");
//                            }
//                            break;

//                        case 3:

//                            Console.WriteLine("Введите название книги:");
//                            nazvanie = Console.ReadLine();

//                            kniga = polzovatel.knigivz.FirstOrDefault(k => k.Nazvanie == nazvanie);

//                            if (kniga != null)
//                            {
//                                kniga.Status = true;
//                                polzovatel.knigivz.Remove(kniga);
//                                Console.WriteLine($"Вы вернули книгу '{kniga.Nazvanie}'.");
//                            }
//                            else
//                            {
//                                Console.WriteLine("Вы не брали эту книгу.");
//                            }
//                            break;

//                        case 4:

//                            Console.WriteLine("Ваши книги:");
//                            foreach (var k in polzovatel.knigivz)
//                                Console.WriteLine($"{k.Nazvanie} Автор: {k.Avtor}");
//                            break;

//                        case 5:
//                            break;
//                    }

//                    if (vybor == 5) 
//                    {
//                        break;
//                    }
//                }
//            }
//            else if (rol == 3)
//            {
//                Console.WriteLine("Выход из программы.");
//                sohranenye();
//                break; 
//            }
//            else
//            {
//                Console.WriteLine("Вы ввели не то, запутите программу снова");
//            }

//        }

//    }
//    static void zagruzka() 
//    {
//        string knigiPath = Path.Combine(path, "knigi.txt");
//        string polzovateliPath = Path.Combine(path, "polzovateli.txt");

//        if (File.Exists(knigiPath))
//        {
//            foreach (var line in File.ReadAllLines(knigiPath))
//            {
//                var parts = line.Split(',');
//                knigi.Add(new Kniga(parts[0], parts[1]) { Status = bool.Parse(parts[2]) });
//            }
//        }

//        if (File.Exists(polzovateliPath))
//        {
//            foreach (var line in File.ReadAllLines(polzovateliPath))
//            {
//                var parts = line.Split(',');
//                var polzovatel = new Polzovatel(parts[0]);
//                for (int i = 1; i < parts.Length; i++)
//                {
//                    var kniga = knigi.FirstOrDefault(k => k.Nazvanie == parts[i]);
//                    if (kniga != null)
//                    {
//                        polzovatel.knigivz.Add(kniga);
//                        kniga.Status = false;
//                    }
//                }
//                polzovateli.Add(polzovatel);
//            }
//        }
//    }
//    static void sohranenye() 
//    {
//        string knigiPath = Path.Combine(path, "knigi.txt");
//        string polzovateliPath = Path.Combine(path, "polzovateli.txt");

//        File.WriteAllLines(knigiPath, knigi.Select(k => $"{k.Nazvanie},{k.Avtor},{k.Status}"));
//        File.WriteAllLines(polzovateliPath, polzovateli.Select(p => $"{p.Imya},{string.Join(",", p.knigivz.Select(k => k.Nazvanie))}"));
//    }
//}



//class Kniga
//{
//    public string Nazvanie { get; set; }
//    public string Avtor { get; set; }
//    public bool Status { get; set; }

//    public Kniga(string nazvanie, string avtor)
//    {
//        Nazvanie = nazvanie;
//        Avtor = avtor;
//        Status = true;
//    }
//}

//class Polzovatel 
//{
//    public string Imya { get; set; } 
//    public List<Kniga> knigivz { get; set; } 

//    public Polzovatel(string imya) 
//    {
//        Imya = imya;
//        knigivz = new List<Kniga>();
//    }
//}

//class Bibliotekar : Polzovatel
//{
//    public Bibliotekar(string imya) : base(imya) { }

//    public void dobavlenye(List<Kniga> knigi, string nazvanie, string avtor)
//    {
//        if (!suchesnvuet(knigi, nazvanie, avtor))
//        {
//            knigi.Add(new Kniga(nazvanie, avtor));
//            Console.WriteLine($"Книга '{nazvanie}' добавлена.");
//        }
//        else
//        {
//            Console.WriteLine($"Книга '{nazvanie}' уже существует.");
//        }
//    }

//    private bool suchesnvuet(List<Kniga> knigi, string nazvanie, string avtor)
//    {
//        return knigi.Any(k => k.Nazvanie == nazvanie && k.Avtor == avtor);
//    }

//    public void udalenye(List<Kniga> knigi, string nazvanie)
//    {
//        var kniga = knigi.FirstOrDefault(k => k.Nazvanie == nazvanie);
//        if (kniga != null)
//        {
//            knigi.Remove(kniga);
//            Console.WriteLine($"Книга '{nazvanie}' удалена.");
//        }
//        else
//        {
//            Console.WriteLine($"Книга '{nazvanie}' не найдена.");
//        }
//    }

//    public void registr(List<Polzovatel> polzovateli, string imya)
//    {
//        polzovateli.Add(new Polzovatel(imya));
//        Console.WriteLine($"Пользователь '{imya}' зарегистрирован.");
//    }

//    public void polzovat(List<Polzovatel> polzovateli)
//    {
//        Console.WriteLine("Список пользователей:");
//        foreach (var p in polzovateli)
//        {
//            Console.WriteLine(p.Imya);
//        }
//    }

//    public void vseknigi(List<Kniga> knigi)
//    {
//        Console.WriteLine("Список книг:");
//        foreach (var k in knigi)
//        {
//            Console.WriteLine($"{k.Nazvanie} , Автор: {k.Avtor} ({(k.Status ? "Доступна" : "Недоступна")})");
//        }
//    }
//}

















































//using System;
//using System.Xml.Linq;


//interface A
//{
//    public void imya()
//    {
//        Console.Write("Введите возраст: ");

//    }
//}

//interface B
//{
//    public void vozrast()
//    {
//        Console.Write("Введите возраст: ");
//    }
//}


//class Animal : A, B
//{
//    private A aa = new A();
//    private B bb = new B();

//    public void imya()
//    {
//        aa.imya();
//    }

//    public void vozrast()
//    {
//        bb.vozrast();
//    }

//    public virtual void est()
//    {
//        Console.WriteLine("ест");

//    }
//}


//class krolik : Animal
//{

//    public override void est()
//    {
//        Console.WriteLine("ест морковь");
//    }
//}

//class sobaka : Animal
//{
//    public override void est()
//    {
//        Console.WriteLine("ест корм");
//    }
//}


//class Programm
//{
//    static void Main(string[] args)
//    {
//        krolik kr = new krolik();
//        sobaka sob = new sobaka();
//        kr.est();
//        sob.est();
//    }
//}

















//class Programm
//{
//    void sum(int a,int b)
//    {
//        System.Console.WriteLine(a + b);
//    }

//    void sum(double a, double b)
//    {
//        System.Console.WriteLine(a + b);
//    }

//    static void Main(string[] args)
//    {
//        Programm pr = new Programm();
//        pr.sum(5, 8);
//        pr.sum(12.1, 3.1);
//    }
//}



//class Animal
//{
//    public virtual void MakeNoise()
//    {
//        Console.WriteLine("Издают звук");
//    }
//}

//class Cat : Animal
//{
//    public override void MakeNoise()
//    {
//        Console.WriteLine("Мяу");
//    }
//}

//class Dog : Animal
//{
//    public override void MakeNoise()
//    {
//        Console.WriteLine("Гав");
//    }
//}


//class Programm
//{
//    static void Main(string[] args)
//    {
//        Cat cat = new Cat();
//        Dog dog = new Dog();
//        dog.MakeNoise();
//        cat.MakeNoise();
//    }
//}



//class A
//{
//    public void Sleep()
//    {
//        Console.WriteLine("");
//    }
//}

//class B
//{
//    public void Fly()
//    {
//        Console.WriteLine("");
//    }
//}

//class Animal 
//{
//    private A a = new A();
//    private B b = new B();

//    public void Sleep()
//    {
//        a.Sleep();
//    }

//    public void Fly()
//    {
//        b.Fly();
//    }

//}

//class Programm
//{
//    static void Main(string[] args)
//    {
//        Animal an = new Animal();
//        an.Sleep();
//        an.Fly();
//    }
//}



// interface A
//{
//   void Sleep();
//}

// interface B
//{
//    void Fly();
//}

//class Animal : A,B
//{
//    public void Fly()
//    {
//        Console.WriteLine("Полет");
//    }

//    public void Sleep()
//    {
//        Console.WriteLine("Спим");
//    }

//}

//class Programm
//{
//    static void Main(string[] args)
//    {
//        Animal an = new Animal();
//        an.Sleep();
//        an.Fly();
//    }
//}



//interface letaushie
//{
//    void letat();
//}

//interface plavueshie
//{
//    void plavat();
//}



//class Animal : letaushie, plavueshie
//{

//    public void letat()
//    {
//        Console.WriteLine("Летает");
//    }

//    public void plavat()
//    {
//        Console.WriteLine("Плавает");
//    }

//}

//class Programm
//{
//    static void Main(string[] args)
//    {
//        Animal an = new Animal();
//        an.letat();
//        an.plavat();
//    }
//}