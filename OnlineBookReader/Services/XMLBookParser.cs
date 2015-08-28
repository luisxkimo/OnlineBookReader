using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using OnlineBookReader.Models;

namespace OnlineBookReader.Services
{
	public static class XMLBookParser
	{
		private static readonly XMLValidator xmlValidator = new XMLValidator();

		public static IEnumerable<Book> Parse(XDocument uploadDocument)
		{
			xmlValidator.ValidateXml(uploadDocument);

			var books = uploadDocument.Descendants().Where(x => x.Name == "book");

			var collectionBooks = books.Select(b => new Book
			{
				BookId = b.GetStringAttribute("id"),
				Author = b.GetString("author"),
				Title = b.GetString("title"),
				Genre = b.GetString("genre"),
				Price = b.GetDecimal("price"),
				Description = b.GetString("description"),
				ISBN = b.GetString("description")
				
				
			}).ToList();

			return collectionBooks;
		}
		
	}

}