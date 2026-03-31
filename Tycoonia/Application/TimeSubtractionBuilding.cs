using Tycoonia.Domain.Buildings;

namespace Tycoonia.Application
{
    public class TimeSubtractionBuilding
    {
        public static void TimeSubtraction(IUpgradableBuilding building)
        {
            if (!building.WorkFlag || building.ProgressTime.TotalSeconds <= 0)
                throw new Exception("ERR timeCalculation");

            building.ProgressTime -= TimeSpan.FromSeconds(1);
            if (building.ProgressTime < TimeSpan.Zero)
                building.ProgressTime = TimeSpan.Zero;
            
            building.ProgressTimeUi -= TimeSpan.FromSeconds(1);
            if (building.ProgressTimeUi < TimeSpan.Zero)
                building.ProgressTimeUi = TimeSpan.Zero;
        }
    }
}
