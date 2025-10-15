using TechECommercePlatform.Application.Common.Interfaces;

namespace TechECommercePlatform.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime UtcNow => DateTime.UtcNow;
}