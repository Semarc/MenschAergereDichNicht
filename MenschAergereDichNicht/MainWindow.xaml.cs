using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MenschAergereDichNichtLogik;
using System.Linq;
using Color = MenschAergereDichNichtLogik.Color;
using Point = MenschAergereDichNichtLogik.Point;

namespace MenschAergereDichNicht
{

	/// <summary>
	/// Interaktionslogik für MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		#region Image Dictionaries
		private static readonly ReadOnlyDictionary<(Color, int), BitmapImage> HouseDictionary = new ReadOnlyDictionary<(Color, int), BitmapImage>(
		new Dictionary<(Color, int), BitmapImage>
		{
			[(Color.Red, 0)] = BitmapImageFromFilePath(@"Resources\House_Red_0.png"),
			[(Color.Red, 1)] = BitmapImageFromFilePath(@"Resources\House_Red_1.png"),
			[(Color.Red, 2)] = BitmapImageFromFilePath(@"Resources\House_Red_2.png"),
			[(Color.Red, 3)] = BitmapImageFromFilePath(@"Resources\House_Red_3.png"),
			[(Color.Red, 4)] = BitmapImageFromFilePath(@"Resources\House_Red_4.png"),

			[(Color.Black, 0)] = BitmapImageFromFilePath(@"Resources\House_Black_0.png"),
			[(Color.Black, 1)] = BitmapImageFromFilePath(@"Resources\House_Black_1.png"),
			[(Color.Black, 2)] = BitmapImageFromFilePath(@"Resources\House_Black_2.png"),
			[(Color.Black, 3)] = BitmapImageFromFilePath(@"Resources\House_Black_3.png"),
			[(Color.Black, 4)] = BitmapImageFromFilePath(@"Resources\House_Black_4.png"),

			[(Color.Yellow, 0)] = BitmapImageFromFilePath(@"Resources\House_Yellow_0.png"),
			[(Color.Yellow, 1)] = BitmapImageFromFilePath(@"Resources\House_Yellow_1.png"),
			[(Color.Yellow, 2)] = BitmapImageFromFilePath(@"Resources\House_Yellow_2.png"),
			[(Color.Yellow, 3)] = BitmapImageFromFilePath(@"Resources\House_Yellow_3.png"),
			[(Color.Yellow, 4)] = BitmapImageFromFilePath(@"Resources\House_Yellow_4.png"),

			[(Color.Green, 0)] = BitmapImageFromFilePath(@"Resources\House_Green_0.png"),
			[(Color.Green, 1)] = BitmapImageFromFilePath(@"Resources\House_Green_1.png"),
			[(Color.Green, 2)] = BitmapImageFromFilePath(@"Resources\House_Green_2.png"),
			[(Color.Green, 3)] = BitmapImageFromFilePath(@"Resources\House_Green_3.png"),
			[(Color.Green, 4)] = BitmapImageFromFilePath(@"Resources\House_Green_4.png")
		});

		private static readonly ReadOnlyDictionary<(Color, bool), BitmapImage> RegularFieldsDictionary = new ReadOnlyDictionary<(Color, bool), BitmapImage>(
		new Dictionary<(Color, bool), BitmapImage>
		{
			[(Color.Empty, false)] = BitmapImageFromFilePath(@"Resources\Feld_Main_Empty.png"),
			[(Color.Red, false)] = BitmapImageFromFilePath(@"Resources\Feld_Main_Red.png"),
			[(Color.Black, false)] = BitmapImageFromFilePath(@"Resources\Feld_Main_Black.png"),
			[(Color.Yellow, false)] = BitmapImageFromFilePath(@"Resources\Feld_Main_Yellow.png"),
			[(Color.Green, false)] = BitmapImageFromFilePath(@"Resources\Feld_Main_Green.png"),

			[(Color.Empty, true)] = BitmapImageFromFilePath(@"Resources\Feld_Selection_Empty.png"),
			[(Color.Red, true)] = BitmapImageFromFilePath(@"Resources\Feld_Selection_Red.png"),
			[(Color.Black, true)] = BitmapImageFromFilePath(@"Resources\Feld_Selection_Black.png"),
			[(Color.Yellow, true)] = BitmapImageFromFilePath(@"Resources\Feld_Selection_Yellow.png"),
			[(Color.Green, true)] = BitmapImageFromFilePath(@"Resources\Feld_Selection_Green.png")
		});

		private static readonly ReadOnlyDictionary<Color, BitmapImage> StartFinishPointDictionary = new ReadOnlyDictionary<Color, BitmapImage>(
		new Dictionary<Color, BitmapImage>
		{
			[Color.Red] = BitmapImageFromFilePath(@"Resources\Feld_Start_Ziel_Red.png"),
			[Color.Black] = BitmapImageFromFilePath(@"Resources\Feld_Start_Ziel_Black.png"),
			[Color.Yellow] = BitmapImageFromFilePath(@"Resources\Feld_Start_Ziel_Yellow.png"),
			[Color.Green] = BitmapImageFromFilePath(@"Resources\Feld_Start_Ziel_Green.png")
		});

		private static readonly ReadOnlyDictionary<int, BitmapImage> DicePictureDictionary = new ReadOnlyDictionary<int, BitmapImage>(
		new Dictionary<int, BitmapImage>
		{
			[1] = BitmapImageFromFilePath(@"Resources\Wuerfel1.png"),
			[2] = BitmapImageFromFilePath(@"Resources\Wuerfel2.png"),
			[3] = BitmapImageFromFilePath(@"Resources\Wuerfel3.png"),
			[4] = BitmapImageFromFilePath(@"Resources\Wuerfel4.png"),
			[5] = BitmapImageFromFilePath(@"Resources\Wuerfel5.png"),
			[6] = BitmapImageFromFilePath(@"Resources\Wuerfel6.png"),
		});


		private static BitmapImage BitmapImageFromFilePath(string Filepath)
		{
			return new BitmapImage(new Uri(Filepath, UriKind.Relative));
		}

		#endregion

		#region Grafic Elements

		private readonly Image[,] images;
		private readonly ReadOnlyDictionary<Color, Image> StarthouseImageDictionary;
		private readonly Grid Spielfeld_Grid;
		private readonly StackPanel MainStackpanel;
		private readonly StackPanel WuerfelStackpanel;
		private readonly Button btn_wuerfeln;
		private readonly TextBlock wuerfelzahl_textblock;
		#endregion

		public MainWindow()
		{

			InitializeComponent();


			MainStackpanel = new StackPanel()
			{
				Orientation = Orientation.Vertical
			};
			Content = MainStackpanel;

			WuerfelStackpanel = new StackPanel()
			{
				Width = 400,
			};
			MainStackpanel.Children.Add(WuerfelStackpanel);

			Spielfeld_Grid = new Grid()
			{
				Name = "Spielfeld_Grid",
				Background = Brushes.Aquamarine
			};
			MainStackpanel.Children.Add(Spielfeld_Grid);


			btn_wuerfeln = new Button()
			{
				Margin = new Thickness(30),
				Content = "Wuerfeln"
			};
			btn_wuerfeln.Click += Wuerfeln_Click;
			WuerfelStackpanel.Children.Add(btn_wuerfeln);

			wuerfelzahl_textblock = new TextBlock()
			{
				TextAlignment = TextAlignment.Center,
				Margin = new Thickness(0),
				Text = "Wuerfelzahl"
			};
			WuerfelStackpanel.Children.Add(wuerfelzahl_textblock);



			for (int i = 0; i < 11; i++)
			{
				Spielfeld_Grid.ColumnDefinitions.Add(new ColumnDefinition());
				Spielfeld_Grid.RowDefinitions.Add(new RowDefinition());
			}

			images = new Image[11, 11];
			for (int x = 0; x < Logik.Board.Count; x++)
			{
				for (int y = 0; y < Logik.Board[x].Count; y++)
				{
					if (Logik.Board[x][y] != null)
					{
						Image tempimage = CreateNewImageAtGridPosition(x, y);
						tempimage.MouseDown += FeldKlick;

						if (Logik.Board[x][y] is FinishField finish)
						{
							tempimage.Source = StartFinishPointDictionary[finish.FinishPointColor];
						}
						else if (Logik.Board[x][y] is Field)
							tempimage.Source = RegularFieldsDictionary[(Color.Empty, false)];
					}
				}
			}


			Dictionary<Color, Image> tempDict = new Dictionary<Color, Image>();
			int j = 1;
			tempDict.Add((Color)j, CreateStartHouse((Color)j, 9, 9));
			j++;
			tempDict.Add((Color)j, CreateStartHouse((Color)j, 0, 9));
			j++;
			tempDict.Add((Color)j, CreateStartHouse((Color)j, 0, 0));
			j++;
			tempDict.Add((Color)j, CreateStartHouse((Color)j, 9, 0));
			StarthouseImageDictionary = new ReadOnlyDictionary<Color, Image>(tempDict);


			#region Startpunkte

			foreach (KeyValuePair<Color, Point> keyValuePair in Logik.StartPointsDictionary)
			{
				images[keyValuePair.Value.X, Helpermethods.PointToGridPoint(keyValuePair.Value).Y].Source = StartFinishPointDictionary[keyValuePair.Key];
			}


			#endregion

			#region Playername-Labels
			switch (Logik.PlayerList.Count)
			{
				case 1:
					var Label1 = new Label()
					{
						Content = Logik.PlayerList[0].Name
					};
					Grid.SetRow(Label1, 8);
					Grid.SetColumn(Label1, 10);
					Spielfeld_Grid.Children.Add(Label1);
					break;
				case 2:
					var Label2 = new Label()
					{
						Content = Logik.PlayerList[1].Name
					};
					Grid.SetRow(Label2, 8);
					Grid.SetColumn(Label2, 0);
					Spielfeld_Grid.Children.Add(Label2);
					goto case 1;
				case 3:
					var Label3 = new Label()
					{
						Content = Logik.PlayerList[2].Name
					};
					Grid.SetRow(Label3, 2);
					Grid.SetColumn(Label3, 0);
					Spielfeld_Grid.Children.Add(Label3);
					goto case 2;
				case 4:
					var Label4 = new Label()
					{
						Content = Logik.PlayerList[3].Name
					};
					Grid.SetRow(Label4, 2);
					Grid.SetColumn(Label4, 10);
					Spielfeld_Grid.Children.Add(Label4);
					goto case 3;
			}


			Grafikupdates();




			#endregion

			Image CreateNewImageAtGridPosition(int x, int y)
			{
				Image tempimage = new Image();
				Grid.SetColumn(tempimage, x);
				Grid.SetRow(tempimage, y);
				images[x, y] = tempimage;
				Spielfeld_Grid.Children.Add(tempimage);
				return tempimage;
			}

			void SetRowspanTwo(Image HouseImage)
			{
				Grid.SetRowSpan(HouseImage, 2);
				Grid.SetColumnSpan(HouseImage, 2);
			}

			Image CreateStartHouse(Color StarthouseColor, int X, int Y)
			{
				Image House = CreateNewImageAtGridPosition(X, Y);

				House.Tag = StarthouseColor;
				House.MouseDown += HouseMouseDown;
				SetRowspanTwo(House);
				if (Logik.PlayerList.Count >= (int)StarthouseColor)
				{
					House.Source = HouseDictionary[(StarthouseColor, 4)];
				}
				else
				{
					House.Source = HouseDictionary[(StarthouseColor, 0)];
				}
				return House;
			}
		}

		#region Eventhandler
		private void Wuerfeln_Click(object sender, RoutedEventArgs e)
		{

			Logik.DiceKlick();
			Grafikupdates();
		}

		private void FeldKlick(object sender, RoutedEventArgs e)
		{
			Image image = sender as Image;
			int row = 10 - Grid.GetRow(image);
			int column = Grid.GetColumn(image);
			if (Logik.FieldClick(column, row) == false)
			{
				MessageBox.Show("Mit dem angeklickten Feld ist keine Aktion möglich");
			}

			Grafikupdates();
		}

		private void HouseMouseDown(object sender, RoutedEventArgs e)
		{
			//Logik.FieldClick(e.Source)

			//int row = Grid.GetRow(HausSchwarz);
			//int row = Grid.GetRow(HausSchwarz);
			//int column = Grid.GetColumn(HausSchwarz);

			//UIElement element = (UIElement) Grid.InputHitTest(e.GetPosition(Spielfeld_Grid));
			//row = Grid.GetRow(element);

			//Logik.HomeClick(Logik.PlayerList[0].Color = MenschAergereDichNichtLogik.Color.Black);



			if (sender is Image image)
			{
				Logik.HomeClick((Color)image.Tag);
			}
			Grafikupdates();
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Application.Current.Shutdown();
		}

		#endregion

		public void Grafikupdates()
		{
			if (Uebergabe.PlayerHasWon != Color.Empty)
			{
				MessageBox.Show(this, $"{Logik.PlayerList[Logik.CurrentPlayerIndex].Name} mit der Farbe {Logik.PlayerList[Logik.CurrentPlayerIndex].Color.ColorToProperString()} hat das Spiel gewonnen!{Environment.NewLine}Herzlichen Glückwunsch");
			}



			//while (Uebergabe.GeaenderteSpielpunkte.Count > 0)
			//{
			//	Point Temppoint = Uebergabe.GeaenderteSpielpunkte[0];
			//	Image tempImage = images[Temppoint.X, Helpermethods.PointToGridPoint(Temppoint).Y];
			//	if (Logik.Board[Temppoint.X][Temppoint.Y] is FinishField finishfield && finishfield.Color == Color.Empty && finishfield.IsAusgewaehlt == false)
			//	{
			//		tempImage.Source = StartFinishPointDictionary[finishfield.FinishPointColor];
			//	}
			//	else if (Logik.Board[Temppoint.X][Temppoint.Y].Color == Color.Empty && Logik.Board[Temppoint.X][Temppoint.Y].IsAusgewaehlt == false && ((ICollection<Point>)Logik.StartPointsDictionary.Values).Contains(Temppoint))
			//	{
			//		foreach (KeyValuePair<Color, Point> keyValuePair in Logik.StartPointsDictionary)
			//		{

			//			if (keyValuePair.Value == Temppoint)
			//			{
			//				tempImage.Source = StartFinishPointDictionary[keyValuePair.Key];
			//				break;
			//			}
			//		}
			//	}
			//	else
			//	{
			//		tempImage.Source = RegularFieldsDictionary[(Logik.Board[Temppoint.X][Temppoint.Y].Color, Logik.Board[Temppoint.X][Temppoint.Y].IsAusgewaehlt)];
			//	}

			//	Uebergabe.GeaenderteSpielpunkte.RemoveAt(0);
			//}





			for (int Column = 0; Column < Logik.Board.Count; Column++)
			{
				for (int Row = 0; Row < Logik.Board[Column].Count; Row++)
				{
					if (Logik.Board[Column][Row] != null)
					{
						Image tempImage = images[Column, 10 - Row];
						if (Logik.Board[Column][Row] is FinishField finishfield && finishfield.Color == Color.Empty && finishfield.IsAusgewaehlt == false)
						{
							tempImage.Source = StartFinishPointDictionary[finishfield.FinishPointColor];
						}
						else if (Logik.Board[Column][Row].Color == Color.Empty && Logik.Board[Column][Row].IsAusgewaehlt == false && ((ICollection<Point>)Logik.StartPointsDictionary.Values).Contains(new Point(Column, Row))) 
						{
							foreach (KeyValuePair<Color, Point> keyValuePair in Logik.StartPointsDictionary)
							{

								if (keyValuePair.Value == new Point(Column, Row))
								{
									tempImage.Source = StartFinishPointDictionary[keyValuePair.Key];
									break;
								}
							}
						}
						else
						{
							tempImage.Source = RegularFieldsDictionary[(Logik.Board[Column][Row].Color, Logik.Board[Column][Row].IsAusgewaehlt)];
						}

					}
				}
			}

			wuerfelzahl_textblock.Text = $"Wuerfelzahl: {Logik.Wuerfelzahl}{Environment.NewLine}Aktueller Spieler: {Logik.PlayerList[Logik.CurrentPlayerIndex].Name}, {Logik.PlayerList[Logik.CurrentPlayerIndex].Color.ColorToProperString()}";

			if (Uebergabe.Starthauserveraendert == true)
			{
				Uebergabe.Starthauserveraendert = false;
				for (int i = 0; i < Logik.PlayerList.Count; i++)
				{
					StarthouseImageDictionary[(Color)i + 1].Source = HouseDictionary[((Color)i + 1, Logik.PlayerList[i].NumberHome)];
				}
			}
		}

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Space)
				Wuerfeln_Click(null, null);
		}
	}

	public static class Helpermethods
	{
		public static Point PointToGridPoint(Point point)
		{
			return new Point(point.X, 10 - point.Y);
		}
		public static string ColorToProperString(this Color color)
		{
			switch (color)
			{
				case Color.Empty:
					return "Leer";
				case Color.Red:
					return "Rot";
				case Color.Black:
					return "Schwarz";
				case Color.Yellow:
					return "Gelb";
				case Color.Green:
					return "Grün";
				default:
					return null;
			}
		}
	}
}