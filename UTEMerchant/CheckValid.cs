using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UTEMerchant
{
    public class CheckValid
    {
        public bool IsNumeric(string input)
        {
            bool isNumeric = int.TryParse(input, out int intValue) || double.TryParse(input, out double doubleValue);
            return isNumeric;
        }
        public bool IsDateFormatValid(string input)
        {
            bool isDateValid = DateTime.TryParseExact(input, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result);
            return isDateValid;
        }
        public bool IsDateValid(string input)
        {
            var a = input.Split('/');
            int day = int.Parse(a[0]);
            int month = int.Parse(a[1]);
            int year = int.Parse(a[2]);
            bool isDateValid = false;

            try
            {
                DateTime date = new DateTime(year, month, day);
                isDateValid = true;
            }
            catch (ArgumentOutOfRangeException)
            {
                isDateValid = false;
            }

            return isDateValid;
        }
        public static bool IsValidEmail(string email)
        {
            string pattern = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+[a-zA-Z]{2,}))$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }
    }
}
