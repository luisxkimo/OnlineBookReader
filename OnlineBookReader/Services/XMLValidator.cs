using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace OnlineBookReader.Services
{
	public class XMLValidator
	{
		public void ValidateXml(XDocument document)
		{
			var schemas = new XmlSchemaSet
			{
				CompilationSettings = { EnableUpaCheck = false }
			};

			schemas.Add("", LoadSchema());

			var errors = new List<string>();
			var warnings = new List<string>();

			document.Validate(schemas, (o, e) =>
			{
				switch (e.Severity)
				{
					case XmlSeverityType.Error:
						errors.Add(String.Format("Error en nodo: {0} : {1}", o, e.Message));
						break;
					case XmlSeverityType.Warning:
						warnings.Add(String.Format("Error en nodo: {0} : {1}", o, e.Message));
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}

			});

			if (errors.Count > 0 || warnings.Count > 0)
			{
				var msg = string.Format("El fichero '{0}' no es válido. Errores:\r\n\r\n{1}",
					document.BaseUri,
					FormatErrors(new XsdErrorsAndWarnigns(errors, warnings)));

				throw new ValidateDocumentException(msg);
			}

			return;
		}

		private static XmlReader LoadSchema()
		{
			var assembly = Assembly.GetExecutingAssembly();
			var @namespace = typeof(XMLValidator).Namespace;
			var resourceName = string.Format("{0}.{1}", @namespace, "validator.xsd");
			var stream = assembly.GetManifestResourceStream(resourceName);

			return XmlReader.Create(stream);
		}

		private static string FormatErrors(XsdErrorsAndWarnigns result)
		{
			var builder = new StringBuilder();
			if (result.HasErrors())
			{
				builder.AppendLine("-------Errores:-------");
				foreach (var error in result.Errors)
				{
					builder.AppendFormat("{0}", error).AppendLine();
				}
			}

			if (result.HasWarnings())
			{
				builder.AppendLine("-------Advertencias:-------");
				foreach (var warning in result.Warnings)
				{
					builder.AppendFormat("{0}", warning).AppendLine();
				}
			}

			builder.AppendLine();

			return builder.ToString();
		}

		private class XsdErrorsAndWarnigns
		{
			private readonly List<string> errors;
			private readonly List<string> warnings;

			public XsdErrorsAndWarnigns(List<string> errors, List<string> warnings)
			{
				this.errors = errors;
				this.warnings = warnings;
			}

			public bool HasErrors()
			{
				return errors.Count > 0;
			}
			public bool HasWarnings()
			{
				return warnings.Count > 0;
			}

			public IEnumerable<string> Errors
			{
				get { return errors; }
			}

			public IEnumerable<string> Warnings
			{
				get { return warnings; }
			}
		}
	}

	public class ValidateDocumentException : ApplicationException
	{
		public ValidateDocumentException(string message) : base(message) { }
	}
}