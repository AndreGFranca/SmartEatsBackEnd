namespace SmartEats.Extensions
{
    public static class TimeZoneInfoExtensions
    {
        public static DateTime ConvertToBrasiliaTime(this DateTime utcDateTime)
        {
            // Definindo o fuso horário de Brasília
            TimeZoneInfo brasiliaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Brazil/East");

            // Convertendo o DateTime UTC para o fuso horário de Brasília
            DateTime brasiliaDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, brasiliaTimeZone);

            return brasiliaDateTime;
        }
    }
}
