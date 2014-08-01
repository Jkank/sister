
namespace WindowsFormsApplication1.Properties
{
    public class Item
    {
        public string Name { get; private set; }
        public bool IsEnabled { get; private set; }

        public void Learn()
        {
            IsEnabled = true;
        }
    }
}
