namespace Leetcode.learn
{
    public sealed class MySingleton
    {
        private MySingleton(){}
        private static readonly MySingleton _instance;

        static MySingleton()
        {
            _instance = new MySingleton();
        }

        public static MySingleton Instance => _instance;
    }
}