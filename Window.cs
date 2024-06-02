using System;
using Gtk;

public static class MainWindow
{
	private const int WINX = 600;
	private const int WINY = 700;

	public static int X { get => WINX; }
	public static int Y { get => WINY; }

	private static Window? _MainWindow = null;

	public static void StartWindow ()
	{
		Window window = new Window("GIO's File Merger");
		window.SetDefaultSize(WINX, WINY);
		window.Resizable = false;

		_MainWindow = window;
	}

	public static void Update ()
	{
		_MainWindow.ShowAll();
	}

	public static Window GetWindow ()
	{
		return _MainWindow;
	}
}