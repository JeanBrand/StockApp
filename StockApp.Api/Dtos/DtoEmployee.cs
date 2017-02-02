using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockApp.Api.Dtos
{
    public class DtoEmployee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}