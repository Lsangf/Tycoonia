using Tycoonia.Domain.Buildings.EnergyPlant;
using Tycoonia.Domain.Buildings.EnergyPlant.NPP;
using Tycoonia.Domain.Buildings.EnergyPlant.TPP;
using Tycoonia.Domain.Buildings.Factory;
using Tycoonia.Domain.Buildings.Mine;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.EnergyResources;
using Tycoonia.Domain.Resources.ProcessedResources;
using Tycoonia.Domain.Resources.ProducedResources;
using Tycoonia.Domain.Resources.RawResources;
using Tycoonia.Domain.Resources.Storage;

namespace Tycoonia.Core
{
    public class InitializationCore
    {
        private static Random random = new();
        public static void Main()
        {
            PlayerReal player = CreatePlayer();

            // raw resources
            Bauxite bauxite = new();
            Clay clay = new();
            Coal coal= new();
            Copper copper = new();
            Gold gold = new();
            Gravel gravel = new();
            Iron iron = new();
            Limenite limenite = new();
            Limestone limestone = new();
            Lithium lithium = new();
            Oil oil = new();
            QuartzSand quartzSand = new();
            RoughDiamonds roughDiamonds = new();
            Silver silver = new();
            Thorium232 thorium232 = new();
            Uranium uranium = new();
            Water water = new();

            // produced resources
            Aluminum aluminum = new();
            Batteries batteries = new();
            Bricks bricks = new();
            Concrete concrete = new();
            CopperWire copperWire = new();
            Diamonds diamonds = new();
            ElectronicComponents electronicComponents = new();
            Domain.Resources.ProducedResources.EnergyStorage energyStorageProduced = new ();
            Fuel fuel = new();
            Glass glass = new();
            GoldBars goldBars = new();
            Plastic plastic = new();
            Plutonium239 plutonium239 = new();
            PurifiedLithium purifiedLithium = new();
            Silicon silicon = new();
            SilverBars silverBars = new();
            SolidFuel solidFuel = new();
            Steel steel = new();
            ThoriumRod thoriumRod = new();
            Titanium titanium = new();
            Uranium235 uranium235 = new();
            Uranium238 uranium238 = new();
            UraniumRod uraniumRod = new();

            Energy energy = new();

            // factories and plants
            FactoryAluminum factoryAluminum = new();
            FactoryBatteries factoryBatteries = new();
            FactoryBricks factoryBricks = new();
            FactoryConcrete factoryConcrete = new();
            FactoryCopperWire factoryCopperWire = new();
            FactoryDiamonds factoryDiamonds = new();
            FactoryElectronicComponents factoryElectronicComponents = new();
            FactoryEnergyStorage factoryEnergyStorage = new();
            FactoryFuel factoryFuel = new();
            FactoryGlass factoryGlass = new();
            FactoryGoldBars factoryGoldBars = new();
            FactoryPlastic factoryPlastic = new();
            FactoryPurifiedLithium factoryPurifiedLithium = new();
            FactorySilicon factorySilicon = new();
            FactorySilverBars factorySilverBars = new();
            FactorySolidFuel factorySolidFuel = new();
            FactorySteel factorySteel = new();
            FactoryThoriumRod factoryThoriumRod = new();
            FactoryTitanium factoryTitanium = new();
            FactoryUraniumRod factoryUraniumRod = new();

            CoalTPP coalTPP = new();
            FuelTPP fuelTPP = new();
            SolidFuelTPP solidFuelTPP = new();
            UraniumNPP uraniumNPP = new();
            ThoriumNPP thoriumNPP = new();

            // storage
            StorageResources storageResources = new();
            Domain.Resources.Storage.EnergyStorage energyStorage = new();

            // gameloop

            List<MineBase> mines = null;

            List<FactoryBase> factories =
                [
                factoryAluminum, factoryBatteries, factoryBricks, factoryConcrete,
                factoryCopperWire, factoryDiamonds, factoryElectronicComponents, factoryEnergyStorage,
                factoryFuel, factoryGlass, factoryGoldBars, factoryPlastic,
                factoryPurifiedLithium, factorySilicon, factorySilverBars, factorySolidFuel,
                factorySteel, factoryThoriumRod, factoryTitanium, factoryUraniumRod
                ];

            List<EnergyPlantBase> energyPlants =
            [
                coalTPP, fuelTPP, solidFuelTPP, uraniumNPP, thoriumNPP
            ];


            GameLoop.StartGameLoop(mines, player, factories, energyPlants, storageResources, energyStorage);
        }

        public static PlayerReal CreatePlayer()
        {
            byte lengthName = 5;
            string name = GenerateRandomName(lengthName);

            return new PlayerReal(name, 100000);
        }

        public static string GenerateRandomName(byte length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
            char[] buffer = new char[length];

            for (int i = 0; i < length; i++)
            {
                buffer[i] = chars[random.Next(chars.Length)];
            }

            return new string(buffer);
        }
    }
}
