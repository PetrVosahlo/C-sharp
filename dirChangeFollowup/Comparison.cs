using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace changedDirectory {
    public class Comparison: Directory {
        internal Directory actualDirectory;
        internal Directory previousDirectory;
        private StringBuilder msg;
        internal StringBuilder Msg { get => msg; set => msg = value; }
        public Comparison() {
        }
        public Comparison(string path) {
            this.previousDirectory = new Directory(path, "previous");
            try {
                this.actualDirectory = new Directory(path);
            } finally {
                // zachycení výjimky při zavření formuláře křížkem - výjimka probublala do volající metody
            }
            base.Path= path;
            this.Msg = new StringBuilder($"V adresáři {path} provedeny tyto změny.\r\nAdresáře:\r\n");
        }
        // hlavní metoda zajišťující srovnání modelů předchozího - previousDirectory a aktuálního - previousDirectory stavu adresáře 
        public void comparison() {
            if (!previousDirectory.followedDir) {
                Msg.Clear();
                Msg.Append("Změny v adresáři dosud nebyly sledovány. Sledování zahájeno.");
            }
            // if (previousDirectory.followedDir) supportItems.RemoveDirContent();
            if (actualDirectory.Dirs.Count > 0 && previousDirectory.Dirs.Count > 0&& previousDirectory.followedDir) {
                if (dirComparison() && !(previousDirectory.Dirs.Count > 0)) {
                    // dirComparison() našla ekvivalent všech adresářů z actualDirectory v previousDirectory, kterou komplet promazala
                    Msg.Append("\tAdresáře se od poslední kontroly nezměnily.\r\n");
                }
                if (previousDirectory.Dirs.Count > 0)
                    listOfPreviousDir();
                // dirComparison() z previousDirectory.Dirs smazala aktuálně existující  adresáře - ty co tam zůstaly jsou smazané
            } else if (!(actualDirectory.Dirs.Count > 0) && previousDirectory.Dirs.Count > 0 && previousDirectory.followedDir) {
                listOfPreviousDir(); // výpis adresářů z previousDirectory.Dirs do msg jako smazané
            } else if (actualDirectory.Dirs.Count > 0 && !(previousDirectory.Dirs.Count > 0) && previousDirectory.followedDir) {
                listOfActualDir(); // výpis adresářů z actualDirectory.Dirs do msg jako nové
            } else if(previousDirectory.followedDir) {
                Msg.Append($"\tAktuálně ani při poslední kontrole adresář {Path} neobsahoval podadresáře.\r\n");
            }

            if (previousDirectory.followedDir) Msg.Append("Soubory:\r\n");

            if (actualDirectory.Files.Count > 0 && previousDirectory.Files.Count > 0 && previousDirectory.followedDir) {
                if (fileComparison()) {
                    if (!(previousDirectory.Files.Count > 0)) { // previousDirectory.Files.Count se od prvního porovnání změnil
                        // fileComparison() našla ekvivalent všech souborů z actualDirectory v previousDirectory, kterou komplet promazala
                        Msg.Append("\tSoubory se od poslední kontroly nezměnily.");
                    }
                }
                if (previousDirectory.Files.Count > 0)
                    listOfPreviousFile();
                // dirComparison() z previousDirectory.Files smazala aktuálně existující soubory - ty co tam zůstaly jsou smazané
            } else if (!(actualDirectory.Files.Count > 0) && previousDirectory.Files.Count > 0 && previousDirectory.followedDir) {
                listOfPreviousFile(); // výpis adresářů z previousDirectory.Dirs do msg jako smazané
            } else if (actualDirectory.Files.Count > 0 && !(previousDirectory.Files.Count > 0) && previousDirectory.followedDir) {
                listOfActualFile(); // výpis adresářů z actualDirectory.Dirs do msg jako nové
            } else if (previousDirectory.followedDir) {
                Msg.Append($"\tAktuálně ani při poslední kontrole adresář {Path} neobsahoval soubory.");
            }
            //Console.WriteLine(base.msg);
        }
        bool fileComparison() {
            int k = 0;
            bool noChange = true; // soubory se od poslední kontroly nezměnily
            foreach (var itActual in actualDirectory.Files) { // cyklus_1
                int h = 0;
                bool isOld = false;  // soubor nezměněn / nepřejmenován od poslední kontroly
                bool renamed = false; // soubor nezměněn , ale přejmenován od poslední kontroly
                bool changed = false; // soubor změněn , ale nepřejmenován od poslední kontroly
                while (previousDirectory.Files.Count > h && !isOld && !renamed && !changed) {
                    if (itActual.Path.Equals(previousDirectory.Files[h].Path)
                        && itActual.HashSHA1.Equals(previousDirectory.Files[h].HashSHA1)) {
                        //pokud se cesta i SHA1 iterovaného souboru cyklu_1 a souboru na který ukazuje "h" v předchozích shoduje
                        previousDirectory.Files.RemoveAt(h); // odstraň soubor "h" z předchozích
                        isOld = true; // soubor nezměněn / nepřejmenován od poslední kontroly
                    } else if (!itActual.Path.Equals(previousDirectory.Files[h].Path)
                              && itActual.HashSHA1.Equals(previousDirectory.Files[h].HashSHA1)) {
                        //pokud se cesta iterovaného souboru cyklu_1 a souboru na který ukazuje "h" neshoduje, ale SHA1 ano
                        itActual.Version = previousDirectory.Files[h].Version; // sjednocení aktuální a previous vese
                        itActual.Version++; // zvýšení čísla změny souboru
                        Msg.Append($"\tpřejmenován soubor \"{previousDirectory.Files[h].Name}\" na \"{itActual.Name}\"" +
                            $", číslo změny: { itActual.Version}.\r\n");
                        previousDirectory.Files.RemoveAt(h); // odstraň soubor "h" z předchozích
                        renamed = true; // soubor nezměněn , ale přejmenován od poslední kontroly
                        noChange = false; // došlo ke změně souborů od poslední kontroly
                    } else if (itActual.Path.Equals(previousDirectory.Files[h].Path)
                         && !itActual.HashSHA1.Equals(previousDirectory.Files[h].HashSHA1)) {
                        //pokud se SHA1 iterovaného souboru cyklu_1 a souboru na který ukazuje "h" neshoduje, ale cesta ano
                        itActual.Version = previousDirectory.Files[h].Version; // sjednocení aktuální a previous vese
                        itActual.Version++; // zvýšení čísla změny souboru
                        Msg.Append($"\tzměněn soubor {itActual.Path} , číslo změny: { itActual.Version}.\r\n\t\t\tnová SHA1:\t{itActual.HashSHA1}");
                        if (!previousDirectory.Files[h].HashSHA1.Equals("")) { // existuje-li předchozí SHA1
                            Msg.Append($"\r\n\t\t\tpůvodní SHA1:\t{previousDirectory.Files[h].HashSHA1}\r\n");
                        }
                        previousDirectory.Files.RemoveAt(h); // odstraň soubor "h" z předchozích
                        changed = true; // soubor změněn , ale nepřejmenován od poslední kontroly
                        noChange = false; // došlo ke změně souborů od poslední kontroly
                    }
                    h++;
                }
                // iteruj dokud ukazovátko "h" nedojde na konec předchozích nebo není nalezena shoda aktuální - předchozí

                if (!isOld && !renamed && !changed) {
                    Msg.Append($"\tvytvořen nový soubor {itActual.Path}\r\n");
                    noChange = false; // došlo ke změně souborů od poslední kontroly
                }
            }
            return noChange;
        }
        void listOfPreviousFile() {
            foreach (var file in previousDirectory.Files) {
                Msg.Append($"\todebrán soubor {file.Path}\r\n");
            }
        }

        void listOfActualFile() {
            foreach (var file in actualDirectory.Files) {
                Msg.Append($"\tnový soubor {file.Path}\r\n");
            }
        }

        bool dirComparison() {
            int k = 0;
            bool noChange = true; // adresáře se od poslední kontroly nezměnily
            for (int i = 0; i < actualDirectory.Dirs.Count; i++) { // cyklus_1
                if (previousDirectory.Dirs.Count > 0) { // adresáře jsou mazány - hlídáno odstranění všech záznamů z předchozí
                    if (actualDirectory.Dirs[i].Equals(previousDirectory.Dirs[k])) {
                        //pokud se cesta adresáře iterovaného cyklem_1 a adresáře na který ukazuje "k" v previousDirectory.Dirs shoduje
                        previousDirectory.Dirs.RemoveAt(k);
                        // odstraň adresář "k" z předchozích => ukazovátko "k" ukazuje na následující adresar_"k+1"
                    } else { //pokud se neshoduje cesta adresáře iterovaného cyklem_1 a adresáře na který ukazuje "k" v previousDirectory.Dirs
                        int j = k + 1; // ukazovátko "j" cyklu_2 ukazuje na ukazuja na následující adresar_"k+1" v previousDirectory.Dirs
                        // adresar_"k" porovnán v předchozím kroku
                        bool newDir = true; // jde o nový adresář
                        while (newDir && j < previousDirectory.Dirs.Count) { // cyklus_2 - vyhledání shodného adresáře od pozice "j" do konce
                            if (actualDirectory.Dirs[i].Equals(previousDirectory.Dirs[j])) {
                                //pokud se cesta adresáře iterovaného cyklem_2 a adresáře na který ukazuje "j" v previousDirectory.Dirs shoduje
                                previousDirectory.Dirs.RemoveAt(j);
                                // odstraň adresář "j" z předchozích => ukazovátko "j" ukazuje na následující adresar_"j+1"
                                if (j < previousDirectory.Dirs.Count) k = j;
                                // pokud ukazovátko "j" není na konci previousDirectory.Dirs => existuje adresar_"j+1" nastav na něj ukazovátko "k"
                                // pozice mezi "j" a "k" netřeba procházet jde o vymazané adresáře
                                newDir = false; // adresář není nový
                            }
                            j++;
                        }
                        if (newDir) {
                            Msg.Append($"\tpřidán nový adresář {actualDirectory.Dirs[i]}\r\n");
                            noChange = false; // adresáře od poslední kontsroly změněny
                        }
                    }
                } else { // v previousDirectory.Dirs nic není, ale v actualsDirectory.Dirs ano - jde o nové adresáře
                    Msg.Append($"\tpřidán nový adresář {actualDirectory.Dirs[i]}\r\n");
                    noChange = false; // adresáře od poslední kontsroly změněny
                }
            }
            return noChange;
        }

        void listOfPreviousDir() {
            foreach (string dir in previousDirectory.Dirs) {
                Msg.Append($"\todebrán adresář {dir}\r\n");
            }
        }
        void listOfActualDir() {
            foreach (string dir in actualDirectory.Dirs) {
                Msg.Append($"\tnový adresář {dir}\r\n");
            }
        }
    }
}