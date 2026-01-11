namespace Tycoonia.Domain.Resources
{
    public abstract class ResourcesBase
    {
        private string _type;
        private string _name;

        public string Type
        {
            get => _type;
            protected set => _type = value;
        }
        public string Name
        {
            get => _name;
            protected set => _name = value;
        }
    }
}
