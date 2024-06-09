using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SmartEats.Casts
{
    public class TimeOnlyConverter : ValueConverter<TimeOnly, TimeSpan>
    {
        public TimeOnlyConverter() : base(
            d => d.ToTimeSpan(),
            d => TimeOnly.FromTimeSpan(d))
        { }
    }
}
