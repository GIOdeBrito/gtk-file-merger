using System;
using Gtk;

namespace FileMerger.FileDialog
{
	public static class Searcher
	{
		public static string OpenFileManager ()
		{
			/* Opens the file manager's dialog and
			gets a file's path */

			Gtk.FileChooserDialog fileChooser = new Gtk.FileChooserDialog
			(
				"Select XML file",
				null,
				FileChooserAction.Open,
				"Cancel", ResponseType.Cancel,
				"Confirm", ResponseType.Accept
			);

			string fileName = string.Empty;

			if(fileChooser.Run() == (int) ResponseType.Accept)
			{
				fileName = fileChooser.Filename;
			}

			fileChooser.Destroy();

			return fileName;
		}
	}
}