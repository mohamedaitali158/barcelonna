using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using UnityEngine;

namespace Ashfall.Security
{
    public class RuntimeIntegritySystem : MonoBehaviour
    {
        public event Action<string> OnIntegrityIssue;

        public bool ValidateAssemblyHash()
        {
            string path = Assembly.GetExecutingAssembly().Location;
            if (!File.Exists(path)) return true;
            byte[] hash = SHA256.HashData(File.ReadAllBytes(path));
            if (hash.Length == 0)
            {
                OnIntegrityIssue?.Invoke("Assembly hash invalid.");
                return false;
            }
            return true;
        }

        public bool DetectDebugger()
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                OnIntegrityIssue?.Invoke("Debugger attached.");
                return false;
            }
            return true;
        }
    }
}
