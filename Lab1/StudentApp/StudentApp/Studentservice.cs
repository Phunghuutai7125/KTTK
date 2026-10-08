using System.Collections.Generic;

namespace StudentApp
{
    // Lớp Logic: đứng giữa UI và Repository, xử lý nghiệp vụ (nếu có validate thêm thì đặt ở đây)
    public class StudentService
    {
        private readonly StudentRepository _repo = new();

        public List<Student> GetStudents() => _repo.GetAll();

        public Student AddStudent(string name, string email, string address, int age, string grade)
            => _repo.Add(name, email, address, age, grade);

        public bool RemoveStudent(int id) => _repo.Delete(id);

        public bool EditStudent(int id, string name, string email, string address, int age, string grade)
            => _repo.Update(id, name, email, address, age, grade);

        public Student SearchById(int id) => _repo.FindById(id);

        public List<Student> SearchByName(string name) => _repo.FindByName(name);

        public List<Student> SearchByAddress(string address) => _repo.FindByAddress(address);

        public List<Student> SearchByGrade(string grade) => _repo.FindByGrade(grade);
    }
} 