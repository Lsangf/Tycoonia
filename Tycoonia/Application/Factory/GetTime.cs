using Tycoonia.Domain.Buildings;
using Tycoonia.Domain.Buildings.Factory;

namespace Tycoonia.Application.Factory
{
    public class GetTime
    {
        public static TimeSpan GetProgressTime(IUpgradableBuilding building)
        {
            var remaining = building.TimeEnd - DateTime.UtcNow;
            return remaining < TimeSpan.Zero ? TimeSpan.Zero : remaining;
        }

        public static TimeSpan GetProgressTimeUi(IUpgradableBuilding building)
        {
            var remaining = building.TimeEnd - DateTime.UtcNow;

            if (remaining <= TimeSpan.Zero)
                return TimeSpan.Zero;

            double seconds = Math.Ceiling(remaining.TotalSeconds);
            return TimeSpan.FromSeconds(seconds);
        }
    }
}
