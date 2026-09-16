using StudentApp.Data;
using StudentApp.Models;

namespace StudentApp.Services
{
    public class StudentService
    {
        private readonly StudentRepository _repository;

        public StudentService(StudentRepository repository)
        {
            _repository = repository;
        }

        public List<Student> GetAllStudents()
        {
            return _repository.GetAll();
        }

        public Student? SearchById(string id)
        {
            return _repository.GetById(id);
        }

        public List<Student> SearchByName(string name)
        {
            return _repository.FindByName(name);
        }

        public List<Student> SearchByAddress(string address)
        {
            return _repository.FindByAddress(address);
        }

        public List<Student> SearchByGrade(double grade)
        {
            return _repository.FindByGrade(grade);
        }

        public string AddStudent(string name, string email, string address, int age, double grade)
        {
            if (string.IsNullOrWhiteSpace(name))
                return "Tên không được để trống!";

            var student = new Student
            {
                Name = name,
                Email = email,
                Address = address,
                Age = age,
                Grade = grade
            };

            _repository.Add(student);
            return "Thêm sinh viên thành công!";
        }

        public string UpdateStudent(string id, string name, string email, string address, int age, double grade)
        {
            var existing = _repository.GetById(id);
            if (existing == null)
                return "Không tìm thấy sinh viên!";

            existing.Name = name;
            existing.Email = email;
            existing.Address = address;
            existing.Age = age;
            existing.Grade = grade;

            bool result = _repository.Update(id, existing);
            return result ? "Cập nhật thành công!" : "Cập nhật thất bại!";
        }

        public string DeleteStudent(string id)
        {
            bool result = _repository.Delete(id);
            return result ? "Xoá thành công!" : "Không tìm thấy sinh viên để xoá!";
        }
    }
}