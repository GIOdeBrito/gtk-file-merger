using System;
using Gtk;
using Pango;
using FileMerger.FileDialog;

class Program
{
	static void Main (string[] args)
	{
		Application.Init();

		MainWindow.StartWindow();
		MainWindow.GetWindow().DeleteEvent += quit_event;

		SetWindowElements();

		MainWindow.Update();
		Application.Run();
	}

	static void SetWindowElements ()
	{
		FontDescription titleFont = FontDescription.FromString("Arial 20");

		VBox box1 = new VBox(false, 10);
		MainWindow.GetWindow().Add(box1);

		Label title = new Label("GIO's File Merger");
		Gtk.Alignment alignTitle = new Gtk.Alignment(0.5f, 0.5f, 0, 0);
		alignTitle.Add(title);
		box1.PackStart(alignTitle, true, true, 0);
		title.ModifyFont(titleFont);

		string belPath = Path.Combine(AppContext.BaseDirectory, "Assets", "bel.png");

		Image belImg = new Image(belPath);
		Gtk.Alignment alignBelImage = new Gtk.Alignment(0.5f, 0.5f, 0, 0);
		alignBelImage.Add(belImg);
		box1.PackStart(alignBelImage, true, true, 0);

		Button chooser = new Button("Select XML File");
		Gtk.Alignment alignChooser = new Gtk.Alignment(0.5f, 0.5f, 0, 0);
		alignChooser.Add(chooser);
		box1.PackStart(alignChooser, true, true, 0);

		chooser.Clicked += (sender, e) =>
		{
			string path = Searcher.OpenFileManager();

			if(String.IsNullOrEmpty(path))
			{
				return;
			}

			FileMerger.Models.Xml.XmlNamespaceMerger xml = FileMerger.Xml.XmlOpenner.GetSettingsXml(path);

			if(xml is null)
			{
				MessageDialog dialog = new MessageDialog(
					MainWindow.GetWindow(),
					DialogFlags.DestroyWithParent,
					MessageType.Error,
					ButtonsType.Close,
					"Could not open the selected file."
				);
				dialog.Run();
	            dialog.Destroy();

				return;
			}

			chooser.Destroy();
			title.Destroy();
			titleFont.Dispose();
			belImg.Destroy();
			box1.Destroy();

			SetWindowPanel(xml);
		};
	}

	static void SetWindowPanel (FileMerger.Models.Xml.XmlNamespaceMerger xml)
	{
		VBox box1 = new VBox(false, 10);
		MainWindow.GetWindow().Add(box1);

		FontDescription titleFont = FontDescription.FromString("Arial 20");

		Label title = new Label(xml.title);
		Gtk.Alignment alignTitle = new Gtk.Alignment(0.5f, 0.5f, 0, 0);
		alignTitle.Add(title);
		box1.PackStart(alignTitle, true, true, 0);
		title.ModifyFont(titleFont);

		Label desc = new Label(xml.description);
		Gtk.Alignment alignDesc = new Gtk.Alignment(0.5f, 0.5f, 0, 0);
		alignDesc.Add(desc);
		box1.PackStart(alignDesc, true, true, 0);

		Label version = new Label($"v{xml.version}");
		Gtk.Alignment alignVersion = new Gtk.Alignment(0.5f, 0.5f, 0, 0);
		alignVersion.Add(version);
		box1.PackStart(alignVersion, true, true, 0);

		HBox buttonsBox = new HBox(false, 10);
		box1.PackStart(buttonsBox, true, true, 0);

		Button mergeFiles = new Button("Merge Files");
		Gtk.Alignment alignMergeButton = new Gtk.Alignment(0.5f, 0.5f, 0, 0);
		alignMergeButton.Add(mergeFiles);
		buttonsBox.PackStart(alignMergeButton, true, true, 0);

		mergeFiles.Clicked += (sender, e) =>
		{
			string[] files = xml.fileCollection.filePath;
			string dir = xml.settings.outDir.Trim();
			string ext = xml.settings.outExt.Trim();
			string name = xml.settings.outName.Trim();

			GActions.FileContentMerge(files, dir, name, ext);
		};

		Button editXml = new Button("Edit XML");
		Gtk.Alignment alignEditButton = new Gtk.Alignment(0.5f, 0.5f, 0, 0);
		alignEditButton.Add(editXml);
		buttonsBox.PackStart(alignEditButton, true, true, 0);

		editXml.Clicked += (sender, e) =>
		{
			MessageDialog dialog = new MessageDialog(
				MainWindow.GetWindow(),
				DialogFlags.DestroyWithParent,
				MessageType.Error,
				ButtonsType.Close,
				"Not yet implemented."
			);
			dialog.Run();
            dialog.Destroy();
		};

		Button returnMain = new Button("Return");
		Gtk.Alignment alignReturnButton = new Gtk.Alignment(0.5f, 0.5f, 0, 0);
		alignReturnButton.Add(returnMain);

		returnMain.Clicked += (sender, e) =>
		{
			box1.Destroy();
			titleFont.Dispose();

			SetWindowElements();
			MainWindow.Update();
		};

		box1.PackStart(alignReturnButton, true, true, 0);

		MainWindow.Update();
	}

	static void quit_event (object obj, DeleteEventArgs args)
	{
		Console.WriteLine("Shutting down...");
		MainWindow.GetWindow().Dispose();
		Application.Quit();
	}
}