
namespace WindowsFormsApplication1.Properties
{
    public class Skill
    {
        private int level;

        public string Name { get; private set; }
        public int MaxLevel { get; private set; }
        public int Level { 
            get
            {
                return level;
            }

            set
            {
                level = value;

                if (level > MaxLevel)
                {
                    level = MaxLevel;
                } 
            }
        }

        public bool IsEnabled
        {
            get
            {
                if (Level > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void Learn()
        {
            Level = 1;
        }

    }
}
