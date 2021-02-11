namespace HotelBooking.Helpers
{
    public static class BooleanExtension
    {
        public static string TranslateToPolish(this bool status)
        {
            if (status == true)
                return "Udane";
            else
                return "Nieudane";
        }
    }
}
