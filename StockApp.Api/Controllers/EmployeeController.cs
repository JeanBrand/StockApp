using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StockApp.Api.Controllers
{
    public class EmployeeController : ApiController
    {
        private stock_dbEntities db = new stock_dbEntities();

        // GET api/employee
        public IQueryable<Employee> GetEmployee()
        {
            return db.Employees;
        }

        // POST api/employee
        public HttpResponseMessage PostEmployee(Employee employee)
        {
           // employee = new Employee() { Id = 3 };
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
        public HttpResponseMessage PutEmployee(int id, Employee employee)
        {
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
