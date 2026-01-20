using Tycoonia.Domain.Buildings.EnergyPlant;
using Tycoonia.Domain.Buildings.EnergyPlant.Storage;
using Tycoonia.Domain.Buildings.EnergyPlant.TPP;
using Tycoonia.Domain.Buildings.Factory;
using Tycoonia.Domain.Player;
using Tycoonia.Domain.Resources.EnergyResources;
using Tycoonia.Domain.Resources.ProcessedResources;
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

            // resources
            Clay clay = new();
            Coal coal= new();
            Bricks bricks = new();
            Energy energy = new();

            // factories and plants
            FactoryBricks factoryBricks = new();

            CoalTPP coalTPP = new();

            // storage
            StorageResources storageResources = new();
            EnergyStorage energyStorage = new();

            // gameloop

            List<FactoryBase> factories =
            [
                factoryBricks
            ];

            List<EnergyPlantBase> energyPlantBases =
            [
                coalTPP
            ];


            GameLoop.StartGameLoop(
                player, 
                clay, coal, bricks, energy, 
                factories, energyPlantBases,
                storageResources, energyStorage);
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
