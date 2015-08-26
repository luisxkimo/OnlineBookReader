using System.Collections.Generic;
using System.Data.Entity;
using OnlineBookReader.Models;

namespace OnlineBookReader.DB
{
	public class BookStoreContext : DbContext
	{
		public BookStoreContext()
			: base("DefaultConnection")
		{
		}

		public DbSet<Book> Books { get; set; }
	}


	public class BookStoreInitializer : CreateDatabaseIfNotExists<BookStoreContext>
	{

		protected override void Seed(BookStoreContext context)
		{

			
			var books = new List<Book>
			{
				new Book
				{
					Author = "Gambardella, Matthew",
					BookId = "bk101",
					Title = "XML Developer's Guide",
					Price = 44.95,
					Description = "An in-depth look at creating applications with XML"
				},

				new Book
				{
					Author = "Ralls, Kim",
					BookId = "bk102",
					Title = "Midnight Rain",
					Price = 5.95,
					Description = "A former architect battles corporate zombies, an evil sorceress, and her own childhood to become queen of the world"
				},

				new Book
				{
					Author = "Corets, Eva",
					BookId = "bk103",
					Title = "Maeve Ascendant",
					Price = 10.40,
					Description = @"After the collapse of a nanotechnology society in England, the young survivors lay the foundation for a new society.
"
				},
			};

			books.ForEach(b => context.Books.Add(b));
			context.SaveChanges();

		}

		
	}
}