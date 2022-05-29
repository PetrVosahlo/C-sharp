using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace changedDirectory {
    public class StartProgram {
        static void Main(string[] args) {
            FrmContent frmContent = new FrmContent();
            frmContent.ShowDialog(); // formulář pro zadání adresáře se kterým se bude pracovat
            while (!frmContent.EndProgram) { // hlídání stisknutí tlačítka "Konec programu"
                try {
                    try {
                        Comparison comparison = new Comparison(frmContent.Path);
                        comparison.comparison(); // spuštění zpracování zadaného adesáře
                        XMLwrite rwXML = new XMLwrite(comparison.Path, comparison.actualDirectory);
                        rwXML.WriterXML(); // zapsání aktuálního stavu adresáře do XML
                        frmContent.Msg = comparison.Msg; // předání výpisu stavu adresáře do formuláře ke zobrazení
                    } catch (System.IO.IOException) { // zachycení výjimky při otevřeném souboru ve sledovaném adresáři
                        frmContent.Msg.Clear(); // z otevřených souborů nelze spočítat SHA1 hash
                        frmContent.Msg.Append($"V adresáři {frmContent.Path} jsou otevřené soubory." +
                            $"Před kontrolou je třeba je zavřít.");
                    }
                    frmContent.ShowDialog(); // zobrazení stavu adresáře - případně zadání jiného adresáře
                } catch (System.ArgumentNullException) { // zachycení výjimky při zavření formuláře křížkem
                    frmContent.EndProgram = true;
                }
            }
        }

    }
}
