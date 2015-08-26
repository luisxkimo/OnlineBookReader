using System.Xml.Linq;

namespace OnlineBookReader.Services
{
	public static class XMLBookParser
	{
		private static readonly XMLValidator xmlValidator = new XMLValidator();

		public static void Parse(XDocument document)
		{
			xmlValidator.ValidateXml(document);
		}

		
	}

}