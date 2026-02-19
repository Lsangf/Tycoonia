namespace Tycoonia.Domain.Player
{
    public class PlayerReal
    {
        private string _name;
        private long _ballance;
        private readonly object _lock = new();

        public string Name
        {
            get => _name;
            set => _name = value;
        }
        public long Ballance
        {
            get { lock (_lock) return _ballance; }
            set { lock (_lock) _ballance = value; }
        }
        public void SubtractSafe(long amount)
        {
            lock (_lock)
            {
                if (_ballance < amount)
                    throw new Exception("Insufficient money!");
                _ballance -= amount;
            }
        }
        public void AddSafe(long amount)
        {
            lock (_lock)
            {
                _ballance += amount;
            }
        }

        public PlayerReal(string name, long ballance)
        {
            Name = name;
            Ballance = ballance;
        }
    }
}
