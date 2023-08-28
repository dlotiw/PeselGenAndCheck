using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
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
    public sealed partial class Generator : Page
    {
        Random random;
        private string pesel;
        Pesel_Check pesel_t;
        DateTime data_urodzin = new DateTime(2008,5,6);
        public Generator()
        {
            this.InitializeComponent();
            data_urodzenia.MinYear = new DateTimeOffset(new DateTime(1890, 1, 1));
            data_urodzenia.MaxYear = new DateTimeOffset(new DateTime(2299, 12, 31));
        }
       
        private void generuj_Click(object sender, RoutedEventArgs e)
        {

            do
            {
                pesel = getRok() + getMiesiac() + getDzien() + numer_seryjny() + getPlec();
                pesel_t = new Pesel_Check(pesel);
                pesel += Convert.ToString(pesel_t.getcheckSum());
                if (data_urodzenia.SelectedDate is null)
                {
                    random = new Random();
                    data_urodzin = new DateTime(random.Next(1800, 2299), random.Next(1, 12), random.Next(1, 31));
                }

            } while (pesel.Length < 11 && pesel_t.isValid() != true);
            pesel_gen.Text=pesel;
        }

        
        private void cofaj_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
            
        }

        private void pesel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var dataPackage = new DataPackage();
            dataPackage.SetText(pesel_gen.Text);
            Clipboard.SetContent(dataPackage);
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }
        private int getPlec()
        {
            random = new Random();

            if (M.IsChecked == true)
            {
                return random.Next(0, 4) * 2 + 1;
            }
            if (K.IsChecked == true)
            {
                return random.Next(0, 4) * 2;
            }
            else
            {
                return random.Next(0, 9);
            }

        }
        private int numer_seryjny()
        {
            random = new Random();
            int numer = random.Next(0, 999);
            return numer;
        }
        private string getRok()
        {
            int year = data_urodzin.Year % 100;
            if (year < 10)
            {
                return "0" + Convert.ToString(year);
            }
            return Convert.ToString(year);
        }

        private string getMiesiac()
        {
            var year = data_urodzin.Year;
            var monthFields = (data_urodzin.Month);
            if (year >= 1800 && year <= 1899)
            {
                monthFields += 80;
            }
            if (year >= 2000 && year <= 2099)
            {
                monthFields += 20;
            }
            if (year >= 2100 && year <= 2199)
            {
                monthFields += 40;
            }
            if (year >= 2200 && year <= 2299)
            {
                monthFields += 60;
            }
            if (monthFields < 10)
            {
                return ""+"0" + monthFields.ToString();
            }
            return monthFields.ToString();
        }

        private string getDzien()
        {
            var dayFields = data_urodzin.Day;
            if (dayFields < 10)
            {
                return "0" + dayFields.ToString();
            }
            return Convert.ToString(dayFields);
        }

        private void data_urodzenia_SelectedDateChanged(DatePicker sender, DatePickerSelectedValueChangedEventArgs args)
        {
            
            if (data_urodzenia.SelectedDate != null)
            {
                data_urodzin = new DateTime(args.NewDate.Value.Year, args.NewDate.Value.Month, args.NewDate.Value.Day);
                
            }
            
        }

       
    }
}
