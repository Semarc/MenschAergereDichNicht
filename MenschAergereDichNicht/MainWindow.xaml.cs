using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MenschAergereDichNichtLogik;

namespace MenschAergereDichNicht
{
	/// <summary>
	/// Interaktionslogik für MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		public MainWindow()
		{
			InitializeComponent();
			switch (Logik.PlayerList.Count)
			{
				case 1:
					lbl_ErsterSpielerName.Content = Logik.PlayerList[0].Name;
					break;
				case 2:
					lbl_ZweiterSpielerName.Content = Logik.PlayerList[1].Name;
					goto case 1;
				case 3:
					lbl_DritterSpielerName.Content = Logik.PlayerList[2].Name;
					goto case 2;
				case 4:
					lbl_VierterSpielerName.Content = Logik.PlayerList[3].Name;
					goto case 3;
			}





		}

		#region Eventhandler
		private void btn_wuerfeln_Click(object sender, RoutedEventArgs e)
		{

			Logik.DiceKlick();
			wuerfelzahl_textblock.Text = Logik.Wuerfelzahl.ToString();
		}

		private void FeldKlick(object sender, RoutedEventArgs e)
		{
			Image image = sender as Image;
			int row = Grid.GetRow(image);
			int column = Grid.GetColumn(image);
			if (Logik.FieldClick(row, column) == false)
			{
				MessageBox.Show(" Mit dem angeklickten Stein ist kein Zug möglich");
			}
			image.Source = new BitmapImage(new Uri("Resources/Feld_Selection_Black.png", UriKind.Relative));

#if DEBUG //Code wird Nur im Debugmodus ausgeführt
			MessageBox.Show($"Row {row} und Column {column} ");
#endif
		}

		private void HausMouseUp(object sender, RoutedEventArgs e)
		{
			//Logik.FieldClick(e.Source)

			//int row = Grid.GetRow(HausSchwarz);
			//int row = Grid.GetRow(HausSchwarz);
			//int column = Grid.GetColumn(HausSchwarz);

			//UIElement element = (UIElement) Grid.InputHitTest(e.GetPosition(Spielfeld_Grid));
			//row = Grid.GetRow(element);

			//Logik.HomeClick(Logik.PlayerList[0].Color = MenschAergereDichNichtLogik.Color.Black);

			Button btn = sender as Button;


			if (btn != null && Enum.TryParse(btn.Tag.ToString(), out Color color))
			{
				Logik.HomeClick(color);

#if DEBUG //Code wird Nur im Debugmodus ausgeführt
				MessageBox.Show($"Row {Grid.GetRow(btn)} und Column {Grid.GetColumn(btn)}, Farbe ist {color}");
#endif
			}
			else
			{
				MessageBox.Show("Button ist null gewesen, oder invalid Tag");
			}

		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Application.Current.Shutdown();
		}

		#endregion

		public void Grafikupdates()
		{
			while (Uebergabe.GeaenderteSpielpunkte.Count > 0)
			{
				UIElement temp = Spielfeld_Grid.Children
				.Cast<UIElement>()
				.First(e => Grid.GetRow(e) == Uebergabe.GeaenderteSpielpunkte[0].Y && Grid.GetColumn(e) == Uebergabe.GeaenderteSpielpunkte[0].X);

				if (temp is Image image)
				{
					image.Source = new BitmapImage(new Uri("Resources/Feld_Selection_Black.png", UriKind.Relative));
				}
			}

		}
	}
}
