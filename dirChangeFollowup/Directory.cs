using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace changedDirectory {
    public class Directory:ItemFile { // model adresáře se kterým se pracuje
        internal List<string> Dirs = new List<string>();
        internal List<ItemFile> Files = new List<ItemFile>();
        internal bool followedDir;
        public Directory() {
        }
        public Directory(string path, string previous):base(path) {
            ReaderXML();
        }
        public Directory(string path):base(path) {
            createActualDirContent();
        }
        internal void createActualDirContent() { // vytvoření modelu aktuálního adresáře
            string[] fileContent = new string[500];
            string[] dirContent = new string[500];

            try {
                fileContent = System.IO.Directory.GetFiles(Path);
                dirContent = System.IO.Directory.GetDirectories(Path);
                foreach (string file in fileContent) {
                    ItemFile foreachFile = new ItemFile(file);
                    if (!foreachFile.Name.Equals("dirContent.xml")) Files.Add(foreachFile);
                    //soubor dirContent.xml ukládá údaje o uživateli - nesledováno
                }
                foreach (string dir in dirContent) {
                    Dirs.Add(dir);
                }
            } finally {
                // zachycení výjimky při zavření formuláře křížkem - výjimka probublala do volající metody
            }
        }
        public void ReaderXML() { // načtení posledního uloženého stavu adresáře z xlm do previousDirectory
            string lPath = Path + "\\dirContent.xml";
            FileInfo fileInfo = new FileInfo(lPath);
            if (fileInfo.Exists) {
                followedDir = true; // zpráva třídě Comparison, že existuje dirContent.xml == adresář je již sledován
                XmlReader readXML = new XmlTextReader(lPath);
                while (readXML.Read()) {
                    if (readXML.HasAttributes) { // čti dokud existují atributy
                        if (readXML.Name.Equals("dir")) { // řádek s tagem dir uloží do previousDirectory.Dirs
                            for (int i = 0; i < readXML.AttributeCount; i++) {
                                readXML.MoveToAttribute(i);
                                Dirs.Add(readXML.Value);
                            }
                        }
                        if (readXML.Name.Equals("File")) { // řádek s tagem file uloží do previousDirectory.Files
                            for (int j = 0; j < readXML.AttributeCount;) {
                                ItemFile itFile = new ItemFile(); // vytvoř instanci třídy ItemFile jejíž datové složky se budou plnit
                                readXML.MoveToAttribute(j++);
                                itFile.Version = int.Parse(readXML.Value); // naplň datovou složku Version prvním atributem
                                readXML.MoveToAttribute(j++);
                                itFile.Path = readXML.Value; // naplň datovou složku Path druhým atributem
                                itFile.Name = itFile.Path.Remove(0, itFile.Path.LastIndexOf('\\') + 1);
                                //naplň datovou složku Name jménem souboru bez jeho cesty
                                readXML.MoveToAttribute(j++);
                                itFile.HashSHA1 = readXML.Value; // naplň datovou složku HashSHA1 třetím atributem
                                if(!Name.Equals("dirContent.xml")) Files.Add(itFile); // ulož instanci třídy file do kolekce previousDirectory.Files
                                //soubor dirContent.xml ukládá údaje o uživateli - nesledováno
                            }
                        }
                    }
                }
                readXML.Close();
            } else {
                followedDir = false; // zpráva třídě Comparison, že neexistuje dirContent.xml == adresář není dosud sledován
            }
        }
    }
}
