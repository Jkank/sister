
namespace WindowsFormsApplication1.Properties
{
    public class Parameter
    {
        private int currentValue;
        public int CurrentValue
        {
            get
            {
                return currentValue;
            }
            set
            {
                currentValue = value;

                if (value > MaxValue)
                {
                    currentValue = MaxValue;
                }
            }
        }

        public int MaxValue { get; set; }

        public void Recover( ){
            CurrentValue = MaxValue;
        }
    }
}
