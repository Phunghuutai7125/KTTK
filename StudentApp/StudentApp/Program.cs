using StudentApp.Data;
using StudentApp.Models;
using StudentApp.Services;

MongoDbContext context = new MongoDbContext();
StudentRepository repository = new StudentRepository(context);
StudentService service = new StudentService(repository);

bool running = true;

while (running)
{
    Console.Clear();
    Console.WriteLine("===== QUAN LY SINH VIEN =====");
    Console.WriteLine("1. Hien thi danh sach sinh vien");
    Console.WriteLine("2. Them sinh vien");
    Console.WriteLine("3. Sua sinh vien");
    Console.WriteLine("4. Xoa sinh vien");
    Console.WriteLine("5. Tim kiem theo Id");
    Console.WriteLine("6. Tim kiem theo Name");
    Console.WriteLine("7. Tim kiem theo Address");
    Console.WriteLine("8. Tim kiem theo Grade");
    Console.WriteLine("0. Thoat");
    Console.Write("Chon chuc nang: ");
    string choice = Console.ReadLine() ?? "";

    switch (choice)
    {
        case "1":
            ShowStudents(service.GetAllStudents());
            break;

        case "2":
            Console.Write("Name: ");
            string name = Console.ReadLine() ?? "";
            Console.Write("Email: ");
            string email = Console.ReadLine() ?? "";
            Console.Write("Address: ");
            string address = Console.ReadLine() ?? "";
            Console.Write("Age: ");
            int.TryParse(Console.ReadLine(), out int age);
            Console.Write("Grade: ");
            double.TryParse(Console.ReadLine(), out double grade);
            Console.WriteLine(service.AddStudent(name, email, address, age, grade));
            break;

        case "3":
            Console.Write("Nhap Id sinh vien can sua: ");
            string updId = Console.ReadLine() ?? "";
            Console.Write("Name moi: ");
            string uName = Console.ReadLine() ?? "";
            Console.Write("Email moi: ");
            string uEmail = Console.ReadLine() ?? "";
            Console.Write("Address moi: ");
            string uAddress = Console.ReadLine() ?? "";
            Console.Write("Age moi: ");
            int.TryParse(Console.ReadLine(), out int uAge);
            Console.Write("Grade moi: ");
            double.TryParse(Console.ReadLine(), out double uGrade);
            Console.WriteLine(service.UpdateStudent(updId, uName, uEmail, uAddress, uAge, uGrade));
            break;

        case "4":
            Console.Write("Nhap Id sinh vien can xoa: ");
            string delId = Console.ReadLine() ?? "";
            Console.WriteLine(service.DeleteStudent(delId));
            break;

        case "5":
            Console.Write("Nhap Id: ");
            string sId = Console.ReadLine() ?? "";
            Student? found = service.SearchById(sId);
            if (found != null)
                ShowStudents(new List<Student> { found });
            else
                Console.WriteLine("Khong tim thay!");
            break;

        case "6":
            Console.Write("Nhap Name: ");
            string sName = Console.ReadLine() ?? "";
            ShowStudents(service.SearchByName(sName));
            break;

        case "7":
            Console.Write("Nhap Address: ");
            string sAddress = Console.ReadLine() ?? "";
            ShowStudents(service.SearchByAddress(sAddress));
            break;

        case "8":
            Console.Write("Nhap Grade: ");
            double.TryParse(Console.ReadLine(), out double sGrade);
            ShowStudents(service.SearchByGrade(sGrade));
            break;

        case "0":
            running = false;
            break;

        default:
            Console.WriteLine("Lua chon khong hop le!");
            break;
    }

    if (running)
    {
        Console.WriteLine("\nNhan phim bat ky de tiep tuc...");
        Console.ReadKey();
    }
}

void ShowStudents(List<Student> students)
{
    if (students.Count == 0)
    {
        Console.WriteLine("Khong co du lieu!");
        return;
    }

    Console.WriteLine("{0,-26} {1,-15} {2,-20} {3,-15} {4,-5} {5,-5}", "Id", "Name", "Email", "Address", "Age", "Grade");
    foreach (Student s in students)
    {
        Console.WriteLine("{0,-26} {1,-15} {2,-20} {3,-15} {4,-5} {5,-5}", s.Id, s.Name, s.Email, s.Address, s.Age, s.Grade);
    }
}