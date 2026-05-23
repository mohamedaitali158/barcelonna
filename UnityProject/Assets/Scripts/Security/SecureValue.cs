using System;

namespace Ashfall.Security
{
    [Serializable]
    public struct SecureValue
    {
        private int _obfuscated;
        private int _key;

        public SecureValue(int value)
        {
            _key = new Random().Next(10000, int.MaxValue);
            _obfuscated = value ^ _key;
        }

        public int Get() => _obfuscated ^ _key;
        public void Set(int value) => _obfuscated = value ^ _key;
    }
}
