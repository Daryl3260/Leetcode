namespace Leetcode.learn.learnProperty
{
    public class StringWrapper
    {
        private string name;

        public string Name
        {
            get => name;
            private set => name = value;
        }

        public void SetMethod(string val)
        {
            Name = val;
        }
    }
}