using System;
using System.ComponentModel.DataAnnotations;
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
            string userInput = ReadLine().ToLower();
            if (userInput == "y")
                return true;
            if (userInput == "n")
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
            if (int.TryParse(input, out int creditCardNumber) == false && input.Length == 4)
            {
                WriteToConsole("Podana wartość nie jest poprawnym numerem karty płatniczej");
                return GetCreditCardFromUser();
            }

            return creditCardNumber;
        }

        public string GetEmailFromUser()
        {
            var input = ReadLine();
            EmailAddressAttribute e = new EmailAddressAttribute();
            if (e.IsValid(input))
                return input;
            else
            {
                WriteToConsole("Podany adres email jest nieprawidłowy");
                return GetEmailFromUser();
            }
        }
    }

    public interface IConsoleService
    {
        void WriteToConsole(string text);
        DateTime GetDateTimeFromUser();
        int GetNumberFromUser();
        bool GetBoolFromUser();
        int GetCreditCardFromUser();
        string GetEmailFromUser();
    }
}
