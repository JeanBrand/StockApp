using StockApp.Api.Dtos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;

namespace StockApp.Api.Controllers
{
    public class EmployeeController : ApiController
    {
        private stock_dbEntities db = new stock_dbEntities();

        // GET api/employee
        public IEnumerable<DtoEmployee> GetEmployee()
        {
            var list = new List<DtoEmployee>();

            //This will use AutoMapper
            foreach (var emp in db.Employees)
            {
                list.Add(new DtoEmployee(){
                    Id = emp.Id ,
                    FirstName = emp.FirstName ,
                    LastName = emp.LastName ,
                    Email = emp.Email,
                    Password = emp.Password 
                });
            }

            return list;
        }

        // POST api/employee
        public HttpResponseMessage PostEmployee(DtoEmployee emp)
        {
           var employee = new Employee()
            {
                Id = emp.Id,
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                Email = emp.Email,
                Password = emp.Password
            };

            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, employee);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = employee.Id }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }  

        // PUT api/employee/5
        public HttpResponseMessage PutEmployee(int id, DtoEmployee emp)
        {
            var employee = new Employee()
            {
                Id = emp.Id,
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                Email = emp.Email,
                Password = emp.Password
            };

            if (ModelState.IsValid && id == employee.Id)
            {
                db.Entry(employee).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }  

        // DELETE api/employee/5
        public HttpResponseMessage DeleteEmployee(int id)
        {
            var employee = db.Employees.Find(id);
            if (employee == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            db.Employees.Remove(employee);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, employee);
        }  
    }
}
