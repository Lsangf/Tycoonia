namespace Tycoonia.Domain.Player
{
    public class PlayerReal
    {
        private string _name;
        private long _ballance;

        public string Name
        {
            get => _name;
            set => _name = value;
        }
        public long Ballance
        {
            get => _ballance;
            set => _ballance = value;
        }

        public PlayerReal(string name, long ballance)
        {
            Name = name;
            Ballance = ballance;
        }
    }
}
