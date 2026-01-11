namespace Tycoonia.Domain.Player
{
    internal abstract class PlayerBase
    {
        private string _name;
        private long _ballance;

        protected PlayerBase(string name, int ballance)
        {
            this._name = name;
            this._ballance = ballance;
        }
        public string Name
        {
            get => _name;
            protected set => _name = value;
        }
        public long Ballance
        {
            get => _ballance;
            set => _ballance = value;
        }
    }
}
