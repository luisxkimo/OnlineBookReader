using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace OnlineBookReader.Models
{
	public class Book
	{
		[Key]
		public int ID { get; set; }
		public string BookId { get; set; }
		public string Author { get; set; }
		public string Title { get; set; }
		public string Genre { get; set; }
		public decimal Price { get; set; }
		public string Description { get; set; }
		public string ISBN { get; set; }

	}
}