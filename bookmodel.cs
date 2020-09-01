using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LinqToSQLMvcApplication.Models
{
    public class bookmodel
    {
        public bookmodel()
        {
            publishers = new List<SelectListItem>();
        }
        public int Id { get; set; }
        public String Title { get; set; }
        public String Author { get; set; }
        public String Year { get; set; }
        public String Price { get; set; }
        [DisplayName("Publisher")]
        public int PublisherId { get; set; }
        public String PublisherName { get; set; }
        public IEnumerable<SelectListItem> publishers { get; set; }

    

    }
}