using System.IO;

namespace DealFinder.Core.Security
{
    public interface IKeyReader
    {
        string GetKey();
    }

    public class KeyReader : IKeyReader
    {
        private static KeyReader _instance;
        private readonly string _key;

        private KeyReader()
        {
            FileStream fileStream = new FileStream("key.txt", FileMode.Open);
            using (var reader = new StreamReader(fileStream))
            {
                _key = reader.ReadLine();
            }
        }

        public static KeyReader Instance()
        {
            if(_instance == null)
                _instance = new KeyReader();

            return _instance;
        }

        public string GetKey()
        {
            return _key;
        }
    }
}
