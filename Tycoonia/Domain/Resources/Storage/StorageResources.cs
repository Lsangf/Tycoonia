namespace Tycoonia.Domain.Resources.Storage
{
    public class StorageResources
    {
        private long _capacity;
        private Dictionary<string, long> _storage = new() 
        {
            { "Clay", 10 },
            { "Coal", 5 },
            { "Uranium", 10 },
            { "Bricks", 0 }
        };

        public long Capacity
        {
            get => _capacity;
            set => _capacity = value;
        }
        public Dictionary<string, long> Storage
        {
            get => _storage;
            set => _storage = value;
        }
    }
}
