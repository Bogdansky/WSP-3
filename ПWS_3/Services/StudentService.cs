using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ПWS_3.Helpers;
using ПWS_3.Models;

namespace ПWS_3.Services
{
    public class StudentService
    {
        private Context _context;

        public StudentService()
        {
            _context = new Context();
        }
        #region Select
        public Student GetById(int id)
        {
            var student = _context.Students.FirstOrDefault(s => s.Id == id);
            if (student is null)
            {
                throw new OperationException(ErrorEnum.StudentNotExist);
            }
            return student;
        }

        public List<Student> GetStudents()
        {
            return _context.Students.Count() > 0 ? _context.Students.ToList() : throw new OperationException(ErrorEnum.StudentsNotExist);
        }

        public List<Student> GetStudents(StudentFilter filter)
        {
            var students = _context.Students.AsNoTracking().AsQueryable();
            if (filter.MinId > 0)
            {
                students = students.Where(s => s.Id >= filter.MinId);
            }
            if (filter.MaxId > 0 && filter.MinId <= filter.MaxId)
            {
                students = students.Where(s => s.Id <= filter.MaxId);
            }
            if (filter.Like != null)
            {
                students = students.Where(s => s.Name.Contains(filter.Like));
            }
            if (filter.GlobalLike != null)
            {
                students = students.Where(s => string.Concat(s.Id, s.Name, s.Phone).Contains(filter.GlobalLike));
            }
            return students.ToList();
        }

        public List<Student> GetStudents(PaginationFilter filter)
        {
            var students = _context.Students.AsQueryable();
            if (filter.Sort)
            {
                students = students.OrderBy(s => s.Name);
            }
            else
            {
                students = students.OrderBy(s => s.Id);
            }
            if (filter.Offset > 0)
            {
                students = students.Skip(filter.Offset);
            }
            if (filter.Limit > 0)
            {
                students = students.Take(filter.Limit);
            }
            return students.ToList();
        }

        public List<Student> GetPaginatedStudents(StudentFilter sfilter, PaginationFilter pfilter)
        {
            var students = GetStudents(sfilter);
            if (pfilter.Offset > 0)
            {
                students = students.Skip(pfilter.Offset).ToList();
            }
            if (pfilter.Limit > 0)
            {
                students = students.Take(pfilter.Limit).ToList();
            }
            if (pfilter.Sort)
            {
                students = students.OrderBy(s => s.Name).ToList();
            }
            return students.ToList();
        }

        public List<dynamic> SelectStudentFields(string fields)
        {
            if (fields.Length == 0)
            {
                throw new OperationException(ErrorEnum.InCorrectParams);
            }
            bool haveId = fields.Contains("id");
            bool haveName = fields.Contains("name");
            bool havePhone = fields.Contains("phone");
            var origin = _context.Students.AsNoTracking().ToList();
            List<dynamic> selection = new List<dynamic>();
            for(int i = 0; i < origin.Count; i++)
            {
                selection.Add(new System.Dynamic.ExpandoObject());
                if (haveId)
                {
                    selection[i].Id = origin[i].Id;
                }
                if (haveName)
                {
                    selection[i].Name = origin[i].Name;
                }
                if (havePhone)
                {
                    selection[i].Phone = origin[i].Phone;
                }
            }

            return selection;
        }
#endregion

        public Student Create(StudentDTO model)
        {
            try
            {
                var result = _context.Students.Add(new Student
                {
                    Name = model.Name,
                    Phone = model.Phone
                });
                return _context.SaveChanges() > 0 ? result : default; 
            }
            catch
            {
                throw new OperationException(ErrorEnum.CreateEntityError);
            }
        }

        public Student Update(int id, StudentDTO model)
        {
            if (id < 0)
            {
                throw new OperationException(ErrorEnum.InCorrectParams);
            }
            var student = _context.Students.Find(id);
            if (student != null)
            {
                student.Name = model.Name ?? student.Name;
                student.Phone = model.Phone ?? student.Phone;
                return _context.SaveChanges() > 0 ? student : default;
            }   
            throw new OperationException(ErrorEnum.StudentNotExist);
        }

        public bool Delete(int id)
        {
            var student = _context.Students.Find(id);
            Func<Student, bool> Delete = (x) => 
            {
                _context.Entry(x).State = System.Data.Entity.EntityState.Deleted;
                return _context.SaveChanges() > 0;
            };
            return student is null ? throw new OperationException(ErrorEnum.StudentNotExist) : Delete(student);
        }
    }
}