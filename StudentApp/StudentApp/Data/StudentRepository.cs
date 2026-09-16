using MongoDB.Driver;
using StudentApp.Models;

namespace StudentApp.Data
{
    public class StudentRepository
    {
        private readonly IMongoCollection<Student> _students;

        public StudentRepository(MongoDbContext context)
        {
            _students = context.Students;
        }

        public List<Student> GetAll()
        {
            return _students.Find(FilterDefinition<Student>.Empty).ToList();
        }

        public Student? GetById(string id)
        {
            return _students.Find(s => s.Id == id).FirstOrDefault();
        }

        public List<Student> FindByName(string name)
        {
            var filter = Builders<Student>.Filter.Regex(s => s.Name, new MongoDB.Bson.BsonRegularExpression(name, "i"));
            return _students.Find(filter).ToList();
        }

        public List<Student> FindByAddress(string address)
        {
            var filter = Builders<Student>.Filter.Regex(s => s.Address, new MongoDB.Bson.BsonRegularExpression(address, "i"));
            return _students.Find(filter).ToList();
        }

        public List<Student> FindByGrade(double grade)
        {
            return _students.Find(s => s.Grade == grade).ToList();
        }

        public void Add(Student student)
        {
            _students.InsertOne(student);
        }

        public bool Update(string id, Student student)
        {
            var result = _students.ReplaceOne(s => s.Id == id, student);
            return result.ModifiedCount > 0;
        }

        public bool Delete(string id)
        {
            var result = _students.DeleteOne(s => s.Id == id);
            return result.DeletedCount > 0;
        }
    }
}