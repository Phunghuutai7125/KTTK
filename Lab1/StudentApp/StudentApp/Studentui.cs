using System;
using System.Collections.Generic;

namespace StudentApp
{
    // Lớp UI: chỉ chịu trách nhiệm hiển thị và nhận input từ người dùng
    public class StudentUI
    {
        private readonly StudentService studentService = new();

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                ShowMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowAllStudents();
                        break;
                    case "2":
                        AddStudent();
                        break;
                    case "3":
                        EditStudent();
                        break;
                    case "4":
                        DeleteStudent();
                        break;
                    case "5":
                        SearchMenu();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ!");
                        break;
                }

                Console.WriteLine("\nNhấn Enter để tiếp tục...");
                Console.ReadLine();
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine("=== QUẢN LÝ SINH VIÊN ===");
            Console.WriteLine("1. Hiển thị danh sách sinh viên");
            Console.WriteLine("2. Thêm sinh viên");
            Console.WriteLine("3. Sửa sinh viên");
            Console.WriteLine("4. Xoá sinh viên");
            Console.WriteLine("5. Tìm kiếm sinh viên");
            Console.WriteLine("0. Thoát");
            Console.Write("Chọn: ");
        }

        private void ShowAllStudents()
        {
            var students = studentService.GetStudents();
            PrintList(students);
        }

        private void PrintList(List<Student> students)
        {
            Console.WriteLine($"\n{"ID",-4} | {"Tên",-20} | {"Email",-25} | {"Địa chỉ",-15} | {"Tuổi",-3} | Lớp");
            Console.WriteLine(new string('-', 90));
            foreach (var s in students)
                Console.WriteLine(s);

            if (students.Count == 0)
                Console.WriteLine("Không có sinh viên nào.");
        }

        private void AddStudent()
        {
            Console.Write("Tên: ");
            string name = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Địa chỉ: ");
            string address = Console.ReadLine();

            int age = ReadInt("Tuổi: ");

            Console.Write("Lớp (Grade): ");
            string grade = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Tên không được để trống!");
                return;
            }

            var s = studentService.AddStudent(name, email, address, age, grade);
            Console.WriteLine($"Đã thêm sinh viên có Id = {s.Id}");
        }

        private void EditStudent()
        {
            int id = ReadInt("Nhập Id sinh viên cần sửa: ");
            var existing = studentService.SearchById(id);
            if (existing == null)
            {
                Console.WriteLine("Không tìm thấy sinh viên!");
                return;
            }

            Console.WriteLine($"Thông tin hiện tại: {existing}");

            Console.Write($"Tên mới ({existing.Name}): ");
            string name = ReadOrDefault(existing.Name);

            Console.Write($"Email mới ({existing.Email}): ");
            string email = ReadOrDefault(existing.Email);

            Console.Write($"Địa chỉ mới ({existing.Address}): ");
            string address = ReadOrDefault(existing.Address);

            Console.Write($"Tuổi mới ({existing.Age}): ");
            string ageInput = Console.ReadLine();
            int age = string.IsNullOrWhiteSpace(ageInput) ? existing.Age : int.Parse(ageInput);

            Console.Write($"Lớp mới ({existing.Grade}): ");
            string grade = ReadOrDefault(existing.Grade);

            bool ok = studentService.EditStudent(id, name, email, address, age, grade);
            Console.WriteLine(ok ? "Cập nhật thành công!" : "Cập nhật thất bại!");
        }

        private void DeleteStudent()
        {
            int id = ReadInt("Nhập Id sinh viên cần xoá: ");
            bool ok = studentService.RemoveStudent(id);
            Console.WriteLine(ok ? "Đã xoá!" : "Không tìm thấy sinh viên!");
        }

        private void SearchMenu()
        {
            Console.WriteLine("\nTìm kiếm theo:");
            Console.WriteLine("1. Id");
            Console.WriteLine("2. Tên");
            Console.WriteLine("3. Địa chỉ");
            Console.WriteLine("4. Lớp (Grade)");
            Console.Write("Chọn: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    int id = ReadInt("Nhập Id: ");
                    var byId = studentService.SearchById(id);
                    PrintList(byId == null ? new List<Student>() : new List<Student> { byId });
                    break;
                case "2":
                    Console.Write("Nhập tên (hoặc 1 phần tên): ");
                    PrintList(studentService.SearchByName(Console.ReadLine()));
                    break;
                case "3":
                    Console.Write("Nhập địa chỉ (hoặc 1 phần địa chỉ): ");
                    PrintList(studentService.SearchByAddress(Console.ReadLine()));
                    break;
                case "4":
                    Console.Write("Nhập lớp: ");
                    PrintList(studentService.SearchByGrade(Console.ReadLine()));
                    break;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ!");
                    break;
            }
        }

        // Helper: đọc số nguyên, lặp lại nếu nhập sai
        private int ReadInt(string prompt)
        {
            int value;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out value))
                    return value;
                Console.WriteLine("Vui lòng nhập số hợp lệ!");
            }
        }

        // Helper: nếu người dùng nhấn Enter (bỏ trống) thì giữ giá trị cũ
        private string ReadOrDefault(string defaultValue)
        {
            string input = Console.ReadLine();
            return string.IsNullOrWhiteSpace(input) ? defaultValue : input;
        }
    }
}