using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

Console.WriteLine("Перед вами список 3-х студентов оценки теста по математике");

Console.WriteLine("Выберите цифру действия, которое хотите сделать:");
Console.WriteLine("1 - вывести весь список, 2 - добавить нового студента, 3 - удалить студента, 4 - редактировать что-то у студента, 5 - просмотр конкретно студента и его оценок");

int calc = Convert.ToInt32(Console.ReadLine());

if (calc == 1)
{
    Entity e = new Entity("Орозалиев", "Альберт", "Максатович", 5,"02.10.24");
    Entity a = new Entity("Шипунов", "Владимир", "Сергееевич", 4, "02.10.24");
    Entity b = new Entity("Бабусенков", "Андрей", "Александрович", 3, "02.10.24");

    Console.WriteLine(e.name + " " + e.surname + " " + e.middle_name + "\n   Математика - " + e.mat + "\n   Время выставления: " + e.time);
    Console.WriteLine(a.name + " " + a.surname + " " + a.middle_name + "\n   Математика - " + a.mat + "\n   Время выставления: " + e.time);
    Console.WriteLine(b.name + " " + b.surname + " " + b.middle_name + "\n   Математика - " + b.mat + "\n   Время выставления: " + e.time);
    
}

else if (calc == 2)
{
    Entity e = new Entity("Орозалиев", "Альберт", "Максатович", 5,"02.10.24");
    Entity a = new Entity("Шипунов", "Владимир", "Сергееевич", 4, "02.10.24");
    Entity b = new Entity("Бабусенков", "Андрей", "Александрович", 3, "02.10.24");

    Console.WriteLine("Введите фамилию нового студента");
    string n = Console.ReadLine();

    Console.WriteLine("Введите имя нового студента");
    string z = Console.ReadLine();

    Console.WriteLine("Введите отчество нового студента");
    string x = Console.ReadLine();

    Console.WriteLine("Введите оценку");
    int v = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("Введите дату выставления оценки - в формате xx.xx.xx");
    string m = Console.ReadLine();

    Entity c = new Entity(n, z, x, v, m);
   
    Console.WriteLine(e.name + " " + e.surname + " " + e.middle_name + "\n   Математика - " + e.mat + "\n   Время выставления: " + e.time);
    Console.WriteLine(a.name + " " + a.surname + " " + a.middle_name + "\n   Математика - " + a.mat + "\n   Время выставления: " + e.time);
    Console.WriteLine(b.name + " " + b.surname + " " + b.middle_name + "\n   Математика - " + b.mat + "\n   Время выставления: " + e.time);
    Console.WriteLine(c.name + " " + c.surname + " " + c.middle_name + "\n   Математика - " + b.mat + "\n   Время выставления: " + e.time);
    
}

else if (calc == 3)
{
    Entity e = new Entity("Орозалиев", "Альберт", "Максатович", 5,"02.10.24");
    Entity a = new Entity("Шипунов", "Владимир", "Сергееевич", 4, "02.10.24");
    Entity b = new Entity("Бабусенков", "Андрей", "Александрович", 3, "02.10.24");

    Console.WriteLine(e.name + " " + e.surname + " " + e.middle_name + "\n   Математика - " + e.mat + "\n   Время выставления: " + e.time);
    Console.WriteLine(a.name + " " + a.surname + " " + a.middle_name + "\n   Математика - " + a.mat + "\n   Время выставления: " + e.time);
    Console.WriteLine(b.name + " " + b.surname + " " + b.middle_name + "\n   Математика - " + b.mat + "\n   Время выставления: " + e.time);

    Console.WriteLine("Какого студента вы хотите удалить: 1 - Орозалиев Альберт Максатович, 2 - Шипунов Владимир Сергеевич, 3 - Бабусенков Андрей Александрович ");
    int ud = Convert.ToInt32(Console.ReadLine());
    
    if (ud == 1){
        Console.WriteLine(a.name + " " + a.surname + " " + a.middle_name + "\n   Математика - " + a.mat + "\n   Время выставления: " + e.time);
        Console.WriteLine(b.name + " " + b.surname + " " + b.middle_name + "\n   Математика - " + b.mat + "\n   Время выставления: " + e.time);
    }
    if (ud == 2){
        Console.WriteLine(e.name + " " + e.surname + " " + e.middle_name + "\n   Математика - " + e.mat + "\n   Время выставления: " + e.time);
        Console.WriteLine(b.name + " " + b.surname + " " + b.middle_name + "\n   Математика - " + b.mat + "\n   Время выставления: " + e.time);

    }
    if (ud == 3){
        Console.WriteLine(e.name + " " + e.surname + " " + e.middle_name + "\n   Математика - " + e.mat + "\n   Время выставления: " + e.time);
        Console.WriteLine(a.name + " " + a.surname + " " + a.middle_name + "\n   Математика - " + a.mat + "\n   Время выставления: " + e.time);
    }
    else {
        Console.WriteLine("Вы ввели не ту цифру студента");
    }
    
}

else if (calc == 4)
{
    Console.WriteLine("У какого студента вы хотите что - то изменить? 1 - Орозалиев Альберт Максатович, 2 - Шипунов Владимир Сергеевич, 3 - Бабусенков Андрей Александрович");
    int red = Convert.ToInt32(Console.ReadLine());

    if (red == 1){
        Console.WriteLine("Что вы хотите изменить? 1 - оценку, 2 - дату проставления");
        int ocenka = Convert.ToInt32(Console.ReadLine());

        if (ocenka == 1){

            Entity e = new Entity("Орозалиев", "Альберт", "Максатович", 5,"02.10.24");

            Console.WriteLine(e.name + " " + e.surname + " " + e.middle_name + "\n   Математика - " + e.mat + "\n   Время выставления: " + e.time);
           
            Console.WriteLine("Какая будет оценка вместо 5?");
            int n = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine(e.name + " " + e.surname + " " + e.middle_name + "\n   Математика - " + n  + "\n   Время выставления: " + e.time);

        }
        if (ocenka == 2){
            Entity e = new Entity("Орозалиев", "Альберт", "Максатович", 5,"02.10.24");

            Console.WriteLine(e.name + " " + e.surname + " " + e.middle_name + "\n   Математика - " + e.mat + "\n   Время выставления: " + e.time);
           
            Console.WriteLine("Какая дата будет вместо 02.10.24?");
            int z = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine(e.name + " " + e.surname + " " + e.middle_name + "\n   Математика - " + z  + "\n   Время выставления: " + z);
        }
        else{
            Console.WriteLine("Вы выбрали не тот номер действия");
        }
    }
    if (red == 2){
        Console.WriteLine("Что вы хотите изменить? 1 - оценку, 2 - дату проставления");
        int ocenka = Convert.ToInt32(Console.ReadLine());

        if (ocenka == 1){

            Entity a = new Entity("Шипунов", "Владимир", "Сергееевич", 4, "02.10.24");

            Console.WriteLine(a.name + " " + a.surname + " " + a.middle_name + "\n   Математика - " + a.mat + "\n   Время выставления: " + a.time);
           
            Console.WriteLine("Какая будет оценка вместо 4?");
            int n = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine(a.name + " " + a.surname + " " + a.middle_name + "\n   Математика - " + n  + "\n   Время выставления: " + a.time);

        }
        if (ocenka == 2){

            Entity a = new Entity("Шипунов", "Владимир", "Сергееевич", 4, "02.10.24");

            Console.WriteLine(a.name + " " + a.surname + " " + a.middle_name + "\n   Математика - " + a.mat + "\n   Время выставления: " + a.time);
           
            Console.WriteLine("Какая дата будет вместо 02.10.24?");
            int z = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine(a.name + " " + a.surname + " " + a.middle_name + "\n   Математика - " + a.mat  + "\n   Время выставления: " + z);
        }
    }
    if (red == 3){
        Console.WriteLine("Что вы хотите изменить? 1 - оценку, 2 - дату проставления");
        int ocenka = Convert.ToInt32(Console.ReadLine());

        if (ocenka == 1){

            Entity b = new Entity("Бабусенков", "Андрей", "Александрович", 3, "02.10.24");

            Console.WriteLine(b.name + " " + b.surname + " " + b.middle_name + "\n   Математика - " + b.mat + "\n   Время выставления: " + b.time);
           
            Console.WriteLine("Какая будет оценка вместо 3?");
            int n = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine(b.name + " " + b.surname + " " + b.middle_name + "\n   Математика - " + n  + "\n   Время выставления: " + b.time);

        }
        if (ocenka == 2){

            Entity b = new Entity("Бабусенков", "Андрей", "Александрович", 3, "02.10.24");

            Console.WriteLine(b.name + " " + b.surname + " " + b.middle_name + "\n   Математика - " + b.mat + "\n   Время выставления: " + b.time);
           
            Console.WriteLine("Какая дата будет вместо 02.10.24?");
            int z = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine(b.name + " " + b.surname + " " + b.middle_name + "\n   Математика - " + b.mat  + "\n   Время выставления: " + z);
        }
    }
    else{
        Console.WriteLine("Вы выбрали не тот номер студента");
    }
    
}

else if (calc == 5)
{
    Console.WriteLine("Какого студента из всего списка вы хотите конкретно посмотреть? 1 - Орозалиев Альберт Максатович, 2 - Шипунов Владимир Сергеевич, 3 - Бабусенков Андрей Александрович ");
    int vib = Convert.ToInt32(Console.ReadLine());

    Entity e = new Entity("Орозалиев", "Альберт", "Максатович", 5,"02.10.24");
    Entity a = new Entity("Шипунов", "Владимир", "Сергееевич", 4, "02.10.24");
    Entity b = new Entity("Бабусенков", "Андрей", "Александрович", 3, "02.10.24");

    if (vib == 1){
        Console.WriteLine(e.name + " " + e.surname + " " + e.middle_name + "\n   Математика - " + e.mat + "\n   Время выставления: " + e.time);
    }
    else if (vib == 2){
        Console.WriteLine(a.name + " " + a.surname + " " + a.middle_name + "\n   Математика - " + a.mat + "\n   Время выставления: " + e.time);
    }
    else if (vib == 3){
       Console.WriteLine(b.name + " " + b.surname + " " + b.middle_name + "\n   Математика - " + b.mat + "\n   Время выставления: " + e.time);
    }
    else {
        Console.WriteLine("Вы ввели не ту цифру студена");
    }

}
else{
    Console.WriteLine("Вы ввели не ту цифру студена");
}

public class Entity
{
    public string name;
    public string surname;
    public string middle_name;

    public int mat;
    public string time;


    public Entity(string name, string surname, string middle_name, int mat, string time)
    {
        this.name = name;
        this.surname= surname;
        this.middle_name = middle_name;
        this.mat = mat;
        this.time = time;
    }
}

