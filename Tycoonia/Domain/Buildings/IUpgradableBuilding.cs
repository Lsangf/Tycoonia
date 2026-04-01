namespace Tycoonia.Domain.Buildings
{
    public interface IUpgradableBuilding
    {
        short Level { get; set; }
        Dictionary<string, long> RecipeUpgradeList { get; set; }
        decimal ProductionRate { get; set; }
        DateTime LastUpdateTime { get; set; }
        decimal TargetOutput { get; set; }
        decimal Produced { get; set; }
        decimal EnergyConsumption {  get; set; }
        Dictionary<string, decimal> ProductionItemList { get; set; }
        bool CanUpgrade { get; set; }
        bool WorkFlag { get; set; }
    }
}
