namespace Tycoonia.Domain.Buildings
{
    public interface IUpgradableBuilding
    {
        short Level { get; set; }
        Dictionary<string, long> RecipeUpgradeList { get; set; }
        decimal ProductionRate { get; set; }
        decimal ProductionTime { get; set; }
        decimal ProductionTimePerIteration { get; set; }
        TimeSpan ProgressTime { get; set; }
        TimeSpan ProgressTimeUi { get; set; }
        decimal EnergyConsumption {  get; set; }
        Dictionary<string, decimal> ProductionItemList { get; set; }
        bool CanUpgrade { get; set; }
        bool WorkFlag { get; set; }
        DateTime TimeStart { get; set; }
        DateTime TimeEnd { get; set; }
    }
}
