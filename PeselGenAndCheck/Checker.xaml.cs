using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PeselGenAndCheck
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Checker : Page
    {
        ContentDialog wiadomosc;
        Pesel_Check pesel;
        public Checker()
        {
            this.InitializeComponent();
        }

        private async void pesel_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(pesel_dany.Text, "[^0-9]"))
            {
                wiadomosc = new ContentDialog();
                string messageBoxText = "Wpisz tylko numery";
                wiadomosc.Title = messageBoxText;
                wiadomosc.PrimaryButtonText = "ok";
                pesel_dany.Text = pesel_dany.Text.Remove(pesel_dany.Text.Length - 1);
                await wiadomosc.ShowAsync();
            }
        }
        private void cofaj_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void sprawdz_Click(object sender, RoutedEventArgs e)
        {
            pesel = new Pesel_Check(pesel_dany.Text);
            if (pesel.isValid()) 
            {
                odp.Text = "Pesel jest poprawny";
                plec.Text = "Twoja płeć to: "+pesel.getSex();
                urodziny.Text = "Twoja data urodzin to: " + Convert.ToString(pesel.getBirthDay()) +"."+ Convert.ToString(pesel.getBirthMonth()) +"."+ Convert.ToString(pesel.getBirthYear());
            }
            else
            {
                if (!pesel.checkSum() && !pesel.checkDay())
                {
                    odp.Text = "Pesel jest nie poprawny\nBłąd sumy kontrolnej i błąd w miesiącu";
                }
                else if (!pesel.checkSum())
                {
                    odp.Text = "Pesel jest nie poprawny\nBłąd sumy kontrolnej";
                }
                else if (!pesel.checkDay())
                {
                    odp.Text = "Pesel jest nie poprawny\nBłąd w miesiącu";
                }
                else 
                {
                    odp.Text = "Pesel jest nie poprawny";
                }
                plec.Text = "";
                urodziny.Text = "";
            }
        }
    }
}
