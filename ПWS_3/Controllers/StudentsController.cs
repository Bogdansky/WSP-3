using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;
using ПWS_3.Helpers;
using ПWS_3.Models;
using ПWS_3.Services;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;

namespace ПWS_3.Controllers
{
    public class StudentsController : ApiController
    {
        private StudentService studentService;
        public StudentsController()
        {
            studentService = new StudentService();
        }

        [HttpGet]
        public IHttpActionResult Get(int? id)
        {
            try
            {
                if(id is null)
                {
                    return Ok(new { StatusCode = 200, Students = studentService.GetStudents() });
                }
                var student = studentService.GetById(id ?? 0);
                HATEOASModel links = new HATEOASModel();
                var origin = $"{Request.RequestUri.Authority}/api/students";
                if (studentService.GetById(id.Value + 1) != null)
                {
                    links.Next = $"{origin}/Get?id={id + 1}";
                }
                if (id != 1 && studentService.GetById(id.Value - 1) != null)
                {
                    links.Previous = $"{origin}/Get?id={id - 1}";
                }
                return Ok(new { StatusCode = 200, Student = student, Links = links });
            }
            catch (OperationException e)
            {
                var errorURI = $"{Request.RequestUri.Authority}/api/error/Get?id={e.Status}";
                return Ok(new { e.StatusCode, Info = errorURI });
            }
        }

        [HttpGet]
        public IHttpActionResult Get(string fields)
        {
            //
            try
            {
                var res = studentService.SelectStudentFields(fields.ToLower());
                return Ok(new { StatusCode = 200, Students = res });
            }
            catch (OperationException e)
            {
                var errorURI = $"{Request.RequestUri.Authority}/api/error/Get?id={e.Status}";
                return Ok(new { e.StatusCode, Info = errorURI });
            }
        }

        [HttpGet]
        public IHttpActionResult GetFiltered([FromBody]StudentFilter filter)
        {
            try
            {
                if (filter != null)
                {
                    return Ok(new { StatusCode = 200, Students = studentService.GetStudents(filter) });
                }
                throw new OperationException(ErrorEnum.InCorrectParams);
            }
            catch(OperationException e)
            {
                var errorURI = $"{Request.RequestUri.Authority}/api/error/Get?id={e.Status}";
                return Ok(new { e.StatusCode, Info = errorURI });
            }
        }

        [HttpGet]
        public IHttpActionResult GetPaginated([FromBody]PaginationFilter filter)
        {
            try
            {
                if (filter != null)
                {
                    return Ok(new { StatusCode = 200, Students = studentService.GetStudents(filter) });
                }
                throw new OperationException(ErrorEnum.InCorrectParams);
            }
            catch (OperationException e)
            {
                var errorURI = $"{Request.RequestUri.Authority}/api/error/Get?id={e.Status}";
                return Ok(new { e.StatusCode, Info = errorURI });
            }
        }

        public IHttpActionResult GetFilteredAndPaginated(GlobalFilter filter)
        {
            try
            {
                if (filter != null)
                {
                    return Ok(new { StatusCode = 200, Students = studentService.GetPaginatedStudents(filter.Filter, filter.Pagination) });
                }
                throw new OperationException(ErrorEnum.InCorrectParams);
            }
            catch (OperationException e)
            {
                var errorURI = $"{Request.RequestUri.Authority}/api/error/Get?id={e.Status}";
                return Ok(new { e.StatusCode, Info = errorURI });
            }
        }

        public IHttpActionResult Post(StudentDTO student)
        {
            try
            {
                return Ok(new { StatusCode = 200, Student = studentService.Create(student) });
            }
            catch (OperationException e)
            {
                var errorURI = $"{Request.RequestUri.Authority}/api/error/Get?id={e.Status}";
                return Ok(new { e.StatusCode, Info = errorURI });
            }
        }

        public IHttpActionResult Put(int id, StudentDTO student)
        {
            try
            {
                return Ok(new { StatusCode = 200, Student = studentService.Update(id, student)});
            }
            catch (OperationException e)
            {
                var errorURI = $"{Request.RequestUri.Authority}/api/error/Get?id={e.Status}";
                return Ok(new { e.StatusCode, Info = errorURI });
            }
        }

        public IHttpActionResult Delete(int id)
        {
            try
            {
                var isDeleted = studentService.Delete(id);
                if (!isDeleted)
                {
                    throw new OperationException(ErrorEnum.StudentDeleteError);
                }
                return Ok(new { StatusCode = 200, Message = "Удаление прошло успешно"});
            }
            catch (OperationException e)
            {
                var errorURI = $"{Request.RequestUri.Authority}/api/error/Get?id={e.Status}";
                return Ok(new { e.StatusCode, Info = errorURI });
            }
        }
    }
}
