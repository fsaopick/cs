class Program
{
     static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Выберите действие: 1 - Добавить студента, 2 - Удалить студента, 3 - Добавить оценку, 4 - Удалить оценку, 5 - Просмотреть студентов,\n 6 - редактировать оценку, 0 - Выход");
            var vibor = Console.ReadLine();

            switch (vibor)
            {
                case "1":
                    DobavlenyeStudenta();
                    break;
                case "2":
                    UdalenieStudenta();
                    break;
                case "3":
                    DobavlenyeOcenki();
                    break;
                case "4":
                    UdalenieOcenki();
                    break;
                case "5":
                    ProsmotrStudentov();
                    break;
                case "6":  
                    IzmenenieOcenki();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный выбор, попробуйте снова.");
                    break;
            }
        }
    }
    static List<Student> students = new List<Student>();

    public class Grade
    {
        public DateTime Date { get; set; }
        public int Value { get; set; }

        public Grade(DateTime date, int value)
        {
            Date = date;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Date.ToShortDateString()}: {Value}";
        }
    }

    public class Student
    {
        public string FullName { get; set; }
        public List<Grade> Grades { get; set; }

        public Student(string fullName)
        {
            FullName = fullName;
            Grades = new List<Grade>();
        }

        public void DobavlenyeOcenki(Grade grade)
        {
            Grades.Add(grade);
        }

        public void IzmenenieOcenki(int index, DateTime newDate, int newValue)
    {
        if (index >= 0 && index < Grades.Count)
        {
            Grades[index].Date = newDate;
            Grades[index].Value = newValue;
        }
    }
        public void UdalenieOcenki(Grade grade)
        {
            Grades.Remove(grade);
        }

        public override string ToString()
        {
            return $"{FullName} - Оценки: {string.Join(", ", Grades)}";
        }
    }
    
    static void DobavlenyeOcenki()
    {
        Console.Write("Введите ФИО студента: ");
        var fullName = Console.ReadLine();
        var student = students.Find(s => s.FullName == fullName);
        if (student != null)
        {
            Console.Write("Введите дату оценки (дд.мм.гггг): ");
            var date = DateTime.Parse(Console.ReadLine());
            Console.Write("Введите значение оценки: ");
            var value = int.Parse(Console.ReadLine());

            if (value <= 5){
                student.DobavlenyeOcenki(new Grade(date, value));
                Console.WriteLine("Оценка добавлена.");
            }
            else{
                Console.WriteLine("Вы ввели оценку недопустимую для 5-ти бальной шкалы");
            }
        }
        else
        {
            Console.WriteLine("Студент не найден.");
        }
    }

    static void DobavlenyeStudenta()
    {
        Console.Write("Введите ФИО студента: ");
        var fullName = Console.ReadLine();
        students.Add(new Student(fullName));
        Console.WriteLine("Студент добавлен.");
    }

    static void UdalenieStudenta()
    {
        Console.Write("Введите ФИО студента для удаления: ");
        var fullName = Console.ReadLine();
        var student = students.Find(s => s.FullName == fullName);
        if (student != null)
        {
            students.Remove(student);
            Console.WriteLine("Студент удален.");
        }
        else
        {
            Console.WriteLine("Студент не найден.");
        }
    }


    static void UdalenieOcenki()
    {
        Console.Write("Введите ФИО студента: ");
        var fullName = Console.ReadLine();
        var student = students.Find(s => s.FullName == fullName);
        if (student != null)
        {
            Console.Write("Введите дату оценки для удаления (в формате - xx.xx.xxxx): ");
            var date = DateTime.Parse(Console.ReadLine());
            var grade = student.Grades.Find(g => g.Date == date);
            if (grade != null)
            {
                student.UdalenieOcenki(grade);
                Console.WriteLine("Оценка удалена.");
            }
            else
            {
                Console.WriteLine("Оценка не найдена.");
            }
        }
        else
        {
            Console.WriteLine("Студент не найден.");
        }
    }
    
    static void IzmenenieOcenki()
    {
        Console.Write("Введите ФИО студента: ");
        var fullName = Console.ReadLine();
        var student = students.Find(s => s.FullName == fullName);
        if (student != null)
        {
            Console.Write("Введите индекс оценки для редактирования(начинается с 0): ");
            var index = int.Parse(Console.ReadLine());
            if (index >= 0 && index < student.Grades.Count)
            {
                Console.Write("Введите новую дату оценки (дд.мм.гггг): ");
                var newDate = DateTime.Parse(Console.ReadLine());
                Console.Write("Введите новое значение оценки: ");
                var newValue = int.Parse(Console.ReadLine());
                student.IzmenenieOcenki(index, newDate, newValue);
                Console.WriteLine("Оценка отредактирована.");
            }
            else
            {
                Console.WriteLine("Некорректный индекс.");
            }
        }
        else
        {
            Console.WriteLine("Студент не найден.");
        }
    }

    static void ProsmotrStudentov()
    {
        Console.WriteLine("Список студентов:");
        foreach (var student in students)
        {
            Console.WriteLine(student);
        }
    }
}