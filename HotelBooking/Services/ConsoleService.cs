using System;

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
    }


    internal interface IConsoleService
    {
        void WriteToConsole(string text);
        DateTime GetDateTimeFromUser();
        int GetNumberFromUser();
    }
}
