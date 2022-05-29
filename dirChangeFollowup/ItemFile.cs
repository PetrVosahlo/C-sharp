using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace changedDirectory {
    public class ItemFile { // definice datových složek uchovávajících informace o souboru
        public int Version { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public string HashSHA1 { get; set; }
        public ItemFile() {
        }
        public ItemFile(string path) {
            Version = 1;
            Path = path;
            Name = path.Remove(0, path.LastIndexOf('\\') + 1);
            try {
                HashSHA1 = SHA1hash.GetFileHash(path);
            } finally {

            }
        }
    }
}
