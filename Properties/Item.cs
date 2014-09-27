
namespace DoujinGameProject.Properties
{
    public class Item
    {
        public string Name { get; private set; }
        public bool IsEnabled { get; private set; }

        public void Learn()
        {
            IsEnabled = true;
        }

        public Item() { }

        public Item(string name, bool isEnabled)
        {
            Name = name;
            IsEnabled = isEnabled;
        }
    }
}
