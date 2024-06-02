using System;
using Gtk;
using FileMerger.Models.Xml;

public static class GActions
{
	public static void FileContentMerge (string[] paths, string outDir, string outName, string outExt)
	{
		string content = string.Empty;

		try
		{
			foreach(string path in paths)
			{
				string fileData = File.ReadAllText(path);

				content += "\n\n// ======================\n";
				content += $"// FILE: {path}";
				content += "\n// ======================\n\n";
				content += fileData;
				content += "\n\n";
			}

			string output = $"{outDir}/{outName}.{outExt}";

			File.WriteAllText(output, content);

			Console.WriteLine(output);
			Console.WriteLine("File written to disk");

			MessageDialog dialog = new MessageDialog(
				MainWindow.GetWindow(),
				DialogFlags.DestroyWithParent,
				MessageType.Info,
				ButtonsType.Close,
				"File written to disk successfully."
			);
			dialog.Run();
            dialog.Destroy();
		}
		catch(Exception ex)
		{
			Console.WriteLine(ex.Message);

			MessageDialog dialog = new MessageDialog(
				MainWindow.GetWindow(),
				DialogFlags.DestroyWithParent,
				MessageType.Error,
				ButtonsType.Close,
				"An error ocurred, refer to the console for more information."
			);
			dialog.Run();
            dialog.Destroy();
		}
	}
}