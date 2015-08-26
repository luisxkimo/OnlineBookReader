using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using Microsoft.AspNet.Identity;
using OnlineBookReader.DB;
using OnlineBookReader.Models;
using OnlineBookReader.Services;

namespace OnlineBookReader.Controllers
{
	[Authorize(Roles = "Admin")]
    public class BooksController : Controller
    {
        private readonly BookStoreContext db = new BookStoreContext();

        public ActionResult Index()
        {
            return View(db.Books.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        public ActionResult Create()
        {
            return View();
        }

		public ActionResult Upload()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Upload(HttpPostedFileBase file)
		{
			var allowedExtensions = new[] { ".xml" };
			var extension = Path.GetExtension(file.FileName);
			if (!allowedExtensions.Contains(extension))
			{
				AddErrors("Archivo no permitido. Solo XML");
				return View();
			}
			else
			{
				if (file != null && file.ContentLength > 0 && file.ContentType == "text/xml")
				{
					var document = XDocument.Load(file.InputStream);
					try
					{
						XMLBookParser.Parse(document);
					}
					catch (ValidateDocumentException exception)
					{
						AddErrors(extension);
						return View();
					}
					ViewBag.Success = "Successful upload!";
					return View();
				}
				else
				{
					return View();
				}
				
			}
			
		}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,BookId,Author,Title,Price,Description")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,BookId,Author,Title,Price,Description")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

		private void AddErrors(string errors)
		{
			ModelState.AddModelError("", errors);
			
		}
		

		
    }
}
