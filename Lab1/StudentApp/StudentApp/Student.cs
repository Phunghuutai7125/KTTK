using System;

namespace StudentApp
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public string Grade { get; set; }

        public override string ToString()
        {
            return $"{Id,-4} | {Name,-20} | {Email,-25} | {Address,-15} | {Age,-3} | {Grade}";
        }

        // Chuyển đối tượng Student thành 1 dòng string để ghi ra file
        // Dùng dấu | để phân tách các trường
        public string ToFileString()
        {
            return $"{Id}|{Name}|{Email}|{Address}|{Age}|{Grade}";
        }

        // Parse 1 dòng string từ file thành đối tượng Student
        public static Student FromFileString(string line)
        {
            var parts = line.Split('|');
            return new Student
            {
                Id = int.Parse(parts[0]),
                Name = parts[1],
                Email = parts[2],
                Address = parts[3],
                Age = int.Parse(parts[4]),
                Grade = parts[5]
            };
        }
    }
}