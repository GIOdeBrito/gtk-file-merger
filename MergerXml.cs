using System;
using System.IO;
using System.Runtime;
using System.Xml.Serialization;
using FileMerger.Models.Xml;

namespace FileMerger.Xml
{
	public static class XmlOpenner
	{
		public static T? OpenXml<T> (string path) where T : class
		{
			try
			{
				XmlSerializer serializer = new XmlSerializer(typeof(T));

				using(StreamReader reader = new StreamReader(path))
				{
					return serializer.Deserialize(reader) as T;
				}
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
				return null;
			}
		}

		public static XmlNamespaceMerger? GetSettingsXml (string path)
		{
			return OpenXml<XmlNamespaceMerger>(path);
		}
	}
}