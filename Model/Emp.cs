using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication.Model
{
    public enum Gender { Male, Female }

    public class Emp
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public string City { get; set; }
    }
}