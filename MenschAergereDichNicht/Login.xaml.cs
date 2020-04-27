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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MenschAergereDichNicht;
using MenschAergereDichNichtLogik;

namespace MenschAergereDichNicht
{
    /// <summary>
    /// Interaktionslogik für Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btn_NamenBestaetigen_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbx_ErsterSpieler.Text) == false)
            {
                if (string.IsNullOrWhiteSpace(tbx_ZweiterSpieler.Text) == false)
                {
                    List<string> NamenderSpieler = new List<string>();
                    NamenderSpieler.Add(tbx_ErsterSpieler.Text);
                    NamenderSpieler.Add(tbx_ZweiterSpieler.Text);

                    if (string.IsNullOrWhiteSpace(tbx_DritterSpieler.Text) == false)
                    {
                        NamenderSpieler.Add(tbx_DritterSpieler.Text);
                        if (string.IsNullOrWhiteSpace(tbx_VierterSpieler.Text) == false)
                        {
                            NamenderSpieler.Add(tbx_VierterSpieler.Text);
                        }
                    }
                    Logik.GameStart(NamenderSpieler);
                    MainWindow frm_Spielfeld = new MainWindow();
                    frm_Spielfeld.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Ein Spieler ist zu wenig, bitte zwei Namen eingeben.");
                }
            }
            else
            {
                MessageBox.Show("Bitte gebe einen Spielernamen ein");
            }
        }
    }
}
