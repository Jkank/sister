
namespace DoujinGameProject.Properties
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

        public Skill() { }

        public Skill(string name, int maxlevel, int level)
        {
            Name = name;
            MaxLevel = maxlevel;
            Level = level;
        }

    }
}
