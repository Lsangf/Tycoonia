using Tycoonia.Domain.Buildings;

namespace Tycoonia.Application
{
    public class TimeSubtractionBuilding
    {
        public static void TimeSubtraction(IUpgradableBuilding building)
        {
            if (building.ProgressTime.TotalSeconds > 0d)
            {
                building.ProgressTime -= TimeSpan.FromSeconds(1);
            }
            else
            {
                throw new Exception("ERR timeCalculation");
            }
        }
    }
}
