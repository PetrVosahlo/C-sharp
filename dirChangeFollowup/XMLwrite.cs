using System.Xml;

namespace changedDirectory {
    public class XMLwrite : Directory {
        private Directory actualDirectory;
        private string path;
        public XMLwrite() {
        }
        public XMLwrite(string path, Directory actualDirectory) {
            this.actualDirectory = actualDirectory;
            this.path = path;
        }
        
        public void WriterXML() { // zápis aktuálního stavu adresáře na disk do XML
            string lPath = path + "\\dirContent.xml";
            using (XmlWriter xmlWriter = XmlTextWriter.Create(lPath)) {
                xmlWriter.WriteStartElement("Content_of_directory"); // zápis hlavičky xml
                xmlWriter.WriteStartElement("Dirs"); // zápis tagu adresářů
                foreach (string it in actualDirectory.Dirs) { // zápis aktuálního stavu adresářů
                    xmlWriter.WriteStartElement("dir");
                    xmlWriter.WriteAttributeString("adresar", it);
                    xmlWriter.WriteEndElement();
                }
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("Files"); // zápis tagu souborů
                foreach (ItemFile it in actualDirectory.Files) {  // zápis aktuálního stavu souborů
                    xmlWriter.WriteStartElement("File");
                    xmlWriter.WriteAttributeString("version", it.Version.ToString());
                    xmlWriter.WriteAttributeString("path", it.Path);
                    xmlWriter.WriteAttributeString("SHA1", it.HashSHA1);
                    xmlWriter.WriteEndElement();

                }
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();
                xmlWriter.Flush();
            }
        }
        
    }
}
