using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace StudentApp
{
    // Lớp Data: chịu trách nhiệm đọc/ghi file và lưu trữ danh sách sinh viên
    public class StudentRepository
    {
        private readonly List<Student> _students = new();
        private int _nextId = 1;
        private readonly string filePath = "students.txt";

        public StudentRepository()
        {
            LoadFromFile();
        }

        public List<Student> GetAll() => _students;

        public Student Add(string name, string email, string address, int age, string grade)
        {
            var item = new Student
            {
                Id = _nextId++,
                Name = name,
                Email = email,
                Address = address,
                Age = age,
                Grade = grade
            };
            _students.Add(item);
            SaveToFile();
            return item;
        }

        public bool Delete(int id)
        {
            var item = _students.FirstOrDefault(s => s.Id == id);
            if (item != null)
            {
                _students.Remove(item);
                SaveToFile();
                return true;
            }
            return false;
        }

        public bool Update(int id, string name, string email, string address, int age, string grade)
        {
            var item = _students.FirstOrDefault(s => s.Id == id);
            if (item != null)
            {
                item.Name = name;
                item.Email = email;
                item.Address = address;
                item.Age = age;
                item.Grade = grade;
                SaveToFile();
                return true;
            }
            return false;
        }

        // Tìm kiếm theo Id
        public Student FindById(int id)
        {
            return _students.FirstOrDefault(s => s.Id == id);
        }

        // Tìm kiếm theo Name (không phân biệt hoa thường, cho phép chứa 1 phần)
        public List<Student> FindByName(string name)
        {
            return _students
                .Where(s => s.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        // Tìm kiếm theo Address
        public List<Student> FindByAddress(string address)
        {
            return _students
                .Where(s => s.Address.Contains(address, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        // Tìm kiếm theo Grade
        public List<Student> FindByGrade(string grade)
        {
            return _students
                .Where(s => s.Grade.Equals(grade, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        private void LoadFromFile()
        {
            if (!File.Exists(filePath)) return;

            foreach (var line in File.ReadAllLines(filePath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var item = Student.FromFileString(line);
                _students.Add(item);
                if (item.Id >= _nextId)
                    _nextId = item.Id + 1;
            }
        }

        private void SaveToFile()
        {
            File.WriteAllLines(filePath, _students.Select(s => s.ToFileString()));
        }
    }
}