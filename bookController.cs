using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LinqToSQLMvcApplication.Models;


namespace LinqToSQLMvcApplication.Controllers
{
    public class bookController : Controller
    {

        private DataClasses1DataContext context;
        public bookController()
        {
            context = new DataClasses1DataContext();
        }
        private void Preparepublisher(bookmodel model)
        {
            model.publishers = context.Publishers.AsQueryable<Publisher>().Select(x => new
               SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()


            });
        }
        [HttpGet]
        // GET: book
        public ActionResult Index()
        {
            IList<bookmodel> booklist = new List<bookmodel>();
            var query = from book in context.Books
                        join publisher in
                      context.Publishers
                    on book.PublisherId
                     equals publisher.Id
                        select new bookmodel
                        {
                            Id = book.Id,
                            Title = book.Title,
                            PublisherName = publisher.Name,
                            Author = book.Author,
                            Year = book.Year,
                            Price = book.Price

                        };
            booklist = query.ToList();

            return View(booklist);
        }

        public ActionResult Details(int id)
        {
            bookmodel model = context.Books.Where(x => x.Id == id).Select(x => new bookmodel()
            {
                Id = x.Id,
                Title = x.Title,
                Author = x.Author,
                Price = x.Price,
                Year = x.Year,
                PublisherName = x.PublisherName
            }).SingleOrDefault();
            {
                return View(model);

            }

        }
        [HttpGet]
        public ActionResult create()
        {
            bookmodel model = new bookmodel();
            Preparepublisher(model);
            return View(model);

        }
        [HttpPost]
        public ActionResult create(bookmodel model)
        {
            try
            {
                Book BOOk = new Book()
                {
                    Title = model.Title,
                    Author = model.Author,
                    Year = model.Year,
                    Price = model.Price,
                    PublisherId = model.PublisherId,
                    PublisherName = model.PublisherName

                };
                context.Books.InsertOnSubmit(BOOk);
                context.SubmitChanges();
                return RedirectToAction("Index");

            }
            catch
            {
                return View(model);
            }
        }
        public ActionResult Edit(int id)
        {
            bookmodel model = context.Books.Where(x => x.Id == id).Select(x => new bookmodel()
            {
                Id = x.Id,
                Title = x.Title,
                Author = x.Author,
                Price = x.Price,
                Year = x.Year,
                PublisherId = x.Publisher.Id
            }).SingleOrDefault();
            Preparepublisher(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(bookmodel model)
        {
            try
            {
                Book BOOK = context.Books.Where(x => x.Id == model.Id).Single<Book>();
                BOOK.Title = model.Title;
                BOOK.Author = model.Author;
                BOOK.Price = model.Price;
                BOOK.Year = model.Year;
                BOOK.PublisherId = model.PublisherId;
                context.SubmitChanges();
                return RedirectToAction("Index");

            }
            catch
            {
                return View(model);
            }
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            bookmodel model = context.Books.Where(x => x.Id == id).Select(x => new bookmodel()
            {
                Id = x.Id,
                Title = x.Title,
                Author = x.Author,
                Price = x.Price,
                Year = x.Year,
                PublisherName = x.PublisherName

            }).SingleOrDefault();

            return View(model);

        }

       [HttpPost]
        public ActionResult Delete(bookmodel model)
        {
            try
            {

                Book BOOK = context.Books.Where(x => x.Id == model.Id).Single<Book>();
                context.Books.DeleteOnSubmit(BOOK);
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