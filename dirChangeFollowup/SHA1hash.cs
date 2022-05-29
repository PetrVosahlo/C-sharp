using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace changedDirectory {
    internal class SHA1hash {
        internal static string GetFileHash(string path) { // metoda vrací string který SHA1 hashem souboru na třídě path
            FileInfo file = new FileInfo(path);
            try {
                if (file.Exists) {
                    string chksum;
                    using (FileStream fop = File.OpenRead(path)) {
                        chksum = BitConverter.ToString(System.Security.Cryptography.SHA1.Create().ComputeHash(fop));
                        fop.Close();
                    }
                    return chksum;
                } else {
                    return $"SHA1 hash souboru {path} se nezdařil.";
                }
            } finally {
            }
        }
    }
}
