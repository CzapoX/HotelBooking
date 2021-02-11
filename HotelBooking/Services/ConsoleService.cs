using System;
using System.Text.RegularExpressions;

namespace HotelBooking.Services
{
    class ConsoleService : IConsoleService
    {
        public void WriteToConsole(string text)
        {
            Console.WriteLine();
            Console.WriteLine(text);
        }

        private string ReadLine()
        {
            return Console.ReadLine();
        }

        public DateTime GetDateTimeFromUser()
        {
            if (DateTime.TryParse(ReadLine(), out DateTime date) == false)
            {
                WriteToConsole("Podałeś nieprawidłową datę");
                return GetDateTimeFromUser();
            }

            if (date < DateTime.Now)
            {
                WriteToConsole("Podałeś za wczesną datę");
                return GetDateTimeFromUser();
            }

            return date;
        }

        public int GetNumberFromUser()
        {
            if (int.TryParse(ReadLine(), out int number) == false)
            {
                WriteToConsole("Podana wartość nie jest liczbą");
                return GetNumberFromUser();
            }

            if (number < 1)
            {
                WriteToConsole("Podana liczba jest za mała");
                return GetNumberFromUser();
            }

            return number;
        }

        public bool GetBoolFromUser()
        {
            if (ReadLine().ToLower() == "y")
                return true;
            if (ReadLine().ToLower() == "n")
                return false;

            else
            {
                WriteToConsole("Wprowadzono nieprawidłowy znak");
                return GetBoolFromUser();
            }
        }

        public int GetCreditCardFromUser()
        {
            //TODO regex to prove that card number is real 
            var input = Regex.Replace(ReadLine(), @"\s+", "");
            if (int.TryParse(input, out int creditCardNumber) == false && input.Length != 16)
            {
                WriteToConsole("Podana wartość nie jest poprawnym numerem karty płatniczej");
                return GetCreditCardFromUser();
            }

            return creditCardNumber;
        }
    }

    public interface IConsoleService
    {
        void WriteToConsole(string text);
        DateTime GetDateTimeFromUser();
        int GetNumberFromUser();
        bool GetBoolFromUser();
        int GetCreditCardFromUser();
    }
}
