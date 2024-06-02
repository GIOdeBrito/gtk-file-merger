using System;
using System.Xml.Serialization;

namespace FileMerger.Models.Xml
{
	[XmlRoot("Merger")]
	public class XmlNamespaceMerger
	{
		[XmlElement("Title")]
		public string? title { get; set; }
		[XmlElement("Description")]
		public string? description { get; set; }
		[XmlElement("Version")]
		public string? version { get; set; }
		[XmlElement("MergerFilesCollection")]
		public XmlMergerFileCollection? fileCollection { get; set; }
		[XmlElement("MergerItemSettings")]
		public XmlMergerItem? settings { get; set; }
	}

	public class XmlMergerItem
	{
		[XmlElement("OutDir")]
		public string? outDir { get; set; }
		[XmlElement("OutExt")]
		public string? outExt { get; set; }
		[XmlElement("OutName")]
		public string? outName { get; set; }
	}

	public class XmlMergerFileCollection
	{
		[XmlElement("FilePath")]
		public string?[] filePath { get; set; }
	}
}