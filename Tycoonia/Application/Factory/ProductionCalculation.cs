using Tycoonia.Domain.Buildings.Factory;
using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Application.Factory
{
    public class ProductionCalculation
    {
        public static void ProductionCalculationFactory(StorageResources storageResources, FactoryBase factory, EnergyStorage energyStorage)
        {
            if (!factory.WorkFlag)
                return;

            DateTime now = DateTime.UtcNow;

            decimal seconds = (decimal)(now - factory.LastUpdateTime).TotalSeconds;

            if (seconds <= 0)
                return;

            decimal produced = factory.ProductionRate * seconds;

            decimal remaining = factory.TargetOutput - factory.Produced;

            if (produced > remaining)
                produced = remaining;

            bool bufferCheck = ResourcesBufferBool.CheckResourcesBuffer(factory.ResourceBuffer, factory.RecipeList, produced);

            if (!bufferCheck)
            {
                factory.WorkFlag = false;
                return;
            }

            ResourcesSubtraction(factory, produced);

            energyStorage.SubtractSafe(factory.EnergyConsumption * produced);

            SaveInStorage.Save(storageResources, factory, produced);

            factory.Produced += produced;

            factory.LastUpdateTime = now;

            if (factory.Produced >= factory.TargetOutput)
                factory.WorkFlag = false;
        }

        public static void ResourcesSubtraction(FactoryBase factory, decimal produced)
        {
            foreach (var recipe in factory.RecipeList)
            {
                decimal need = recipe.Value * produced;

                factory.ResourceBuffer[recipe.Key].CurrentQuantity -= need;
            }
        }

        //public static void EnergySubtraction(EnergyStorage energyStorage, decimal energyNeeded, FactoryBase factory)
        //{
        //    energyStorage.SubtractSafe(energyNeeded * factory.ProductionRate);
        //}
    }
}
