using System;
using System.Globalization;
using System.Xml.Linq;

namespace OnlineBookReader.Services
{
	public static class XElementExtensions
	{
		public static decimal GetDecimalAttribute(this XElement element, string attribute)
		{
			return decimal.Parse(element.Attribute(attribute).Value, CultureInfo.InvariantCulture);
		}

		public static int GetInt(this XElement element, string attribute)
		{
			return int.Parse(element.Attribute(attribute).Value);
		}

		public static int GetInt(this XElement element, string attribute, int defaultValue)
		{
			var xAttribute = element.Attribute(attribute);
			return xAttribute == null ? defaultValue : int.Parse((String.IsNullOrEmpty(xAttribute.Value) ? "0" : xAttribute.Value), CultureInfo.InvariantCulture);
		}

		public static bool GetBool(this XElement element, string attribute)
		{
			return bool.Parse(element.Attribute(attribute).Value);
		}

		public static DateTime GetDate(this XElement element, string attribute)
		{
			return Convert.ToDateTime(element.Attribute(attribute).Value);
		}

		public static string GetStringAttribute(this XElement element, string attribute)
		{
			return element.Attribute(attribute) != null ? element.Attribute(attribute).Value : string.Empty;
		}

		public static string GetString(this XElement element, string node)
		{
			var xElement = element.Element(node);
			return xElement != null ? xElement.Value : string.Empty;
		}

		public static decimal GetDecimal(this XElement element, string node)
		{
			var xElement = element.Element(node);
			return xElement == null ? 0.0m : decimal.Parse(xElement.Value, CultureInfo.InvariantCulture);
		}
	}
}