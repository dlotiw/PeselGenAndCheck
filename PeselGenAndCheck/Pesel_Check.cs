using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeselGenAndCheck
{
    internal class Pesel_Check
    {
        private int[] pesel_int= { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        private char[] pesel_char;
        private bool valid;        
        public  Pesel_Check (String pesel)
        {
            pesel_char = pesel.ToCharArray();
            for (int i = 0; i < pesel_char.Length; i++)
            {
                pesel_int[i] = Convert.ToInt32(pesel_char[i] - '0');
            }
            if (pesel.Length != 11)
            {
                valid = false;
            }
            else
            {
                if (checkSum() && checkMonth() && checkDay())
                {
                    valid = true;
                }
                else
                {
                    valid = false;
                }
            }

        }
        public bool isValid()
        {
            return valid;
        }
        public int getBirthYear()
        {
            int year;
            int month;
            year = 10 * pesel_int[0];
            year += pesel_int[1];
            month = 10 * pesel_int[2];
            month += pesel_int[3];
            if (month > 80 && month < 93)
            {
                year += 1800;
            }
            else if (month > 0 && month < 13)
            {
                year += 1900;
            }
            else if (month > 20 && month < 33)
            {
                year += 2000;
            }
            else if (month > 40 && month < 53)
            {
                year += 2100;
            }
            else if (month > 60 && month < 73)
            {
                year += 2200;
            }
            return year;
        }

        public int getBirthMonth()
        {
            int month;
            month = 10 * pesel_int[2];
            month += pesel_int[3];
            if (month > 80 && month < 93)
            {
                month -= 80;
            }
            else if (month > 20 && month < 33)
            {
                month -= 20;
            }
            else if (month > 40 && month < 53)
            {
                month -= 40;
            }
            else if (month > 60 && month < 73)
            {
                month -= 60;
            }
            return month;
        }


        public int getBirthDay()
        {
            int day;
            day = 10 * pesel_int[4];
            day += pesel_int[5];
            return day;
        }

        public String getSex()
        {
            if (valid)
            {
                if (pesel_int[9] % 2 == 1)
                {
                    return "Mężczyzna";
                }
                else
                {
                    return "Kobieta";
                }
            }
            else
            {
                return "---";
            }
        }
        public bool checkSum()
        {
            int sum = getcheckSum();
            
            if (sum == pesel_int[10])
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public  int getcheckSum()
        {
            int sum = 1 * pesel_int[0] +
            3 * pesel_int[1] +
            7 * pesel_int[2] +
            9 * pesel_int[3] +
            1 * pesel_int[4] +
            3 * pesel_int[5] +
            7 * pesel_int[6] +
            9 * pesel_int[7] +
            1 * pesel_int[8] +
            3 * pesel_int[9];
            sum %= 10;
            sum = 10 - sum;
            sum %= 10;
            return sum;
        }

        public bool checkMonth()
        {
            int month = getBirthMonth();
            int day = getBirthDay();
            if (month > 0 && month < 13)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
       

        public bool checkDay()
        {
            int year = getBirthYear();
            int month = getBirthMonth();
            int day = getBirthDay();
            if ((day > 0 && day < 32) &&
            (month == 1 || month == 3 || month == 5 ||
            month == 7 || month == 8 || month == 10 ||
            month == 12))
            {
                return true;
            }
            else if ((day > 0 && day < 31) &&
            (month == 4 || month == 6 || month == 9 ||
            month == 11))
            {
                return true;
            }
            else if ((day > 0 && day < 30 && leapYear(year)) ||
            (day > 0 && day < 29 && !leapYear(year)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool leapYear(int year)
        {
            if (year % 4 == 0 && year % 100 != 0 || year % 400 == 0)
                return true;
            else
                return false;
        }

    }
}
