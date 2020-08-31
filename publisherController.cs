using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LinqToSQLMvcApplication.Models;

namespace LinqToSQLMvcApplication.Controllers
{
    public class publisherController : Controller
    {
        
        private DataClasses1DataContext context;
        public publisherController()
        {
            context = new DataClasses1DataContext();
        }
        // GET: publisher
        public ActionResult Index()
        {
            IList<publishermodel> publisherlist = new List<publishermodel>();
            var query = from Publisher in context.Publishers select Publisher;
            var publishers = query.ToList();
            foreach (var publisherdata in publishers)
            {
                publisherlist.Add(new publishermodel()
                {
                    Id = publisherdata.Id,
                    Name = publisherdata.Name,
                    Year = publisherdata.Year

                });
            }
            return View(publisherlist);

        }
[HttpGet]
        public ActionResult Create()
        {
            publishermodel model = new publishermodel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(publishermodel model)
        {

            try
            {
                Publisher publisher = new Publisher()
                {
                    Name = model.Name,
                    Year = model.Year
                };

                context.Publishers.InsertOnSubmit(publisher);
                context.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }

        }
    }
}

