namespace Tycoonia.Application
{
    public class TimeSubtractionBuilding
    {
        public static void TimeSubtraction(dynamic building)
        {
            if (building.ProductionTime > 0)
            {
                building.ProductionTime -= 1;
            }
            else
            {
                throw new Exception("ERR timeCalculation");
            }
        }
        //int totalSecondsToSubtract = hours * 3600 + minutes * 60 + seconds;
        //int totalBuildingSeconds = buildingHours * 3600 + buildingMinutes * 60 + buildingSeconds;
        //if (totalSecondsToSubtract >= totalBuildingSeconds)
        //{
        //    buildingHours = 0;
        //    buildingMinutes = 0;
        //    buildingSeconds = 0;
        //}
        //else
        //{
        //    totalBuildingSeconds -= totalSecondsToSubtract;
        //    buildingHours = totalBuildingSeconds / 3600;
        //    totalBuildingSeconds %= 3600;
        //    buildingMinutes = totalBuildingSeconds / 60;
        //    buildingSeconds = totalBuildingSeconds % 60;
        //}
    }
}
