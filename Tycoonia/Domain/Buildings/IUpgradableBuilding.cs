namespace Tycoonia.Domain.Buildings
{
    public interface IUpgradableBuilding
    {
        short Level { get; set; }
        Dictionary<string, long> RecipeUpgradeList { get; set; }
        int ProductionRate { get; set; }
        decimal EnergyConsumption {  get; set; }
        Dictionary<string, int> ProductionItemList { get; set; }
        bool CanUpgrade { get; set; }
    }
}
