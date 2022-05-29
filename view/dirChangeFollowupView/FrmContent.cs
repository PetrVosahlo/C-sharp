using System.Threading;
using System;
using System.IO;
using System.Windows.Forms;
using System.Text;

namespace changedDirectory {
    public partial class FrmContent : Form {
        bool endProgram = false;  // přepínač ukončující program
        StringBuilder msg; // zpráva ze třídy Comparison, která se zobrazí v textboxu
        public bool EndProgram { get => endProgram; set => endProgram = value; }
        public string Path { get; set; }
        public StringBuilder Msg { get => msg; set => msg = value; }

        public FrmContent() {
            InitializeComponent();
            this.Msg = new StringBuilder();
        }

        private void btnDir_Click(object sender, EventArgs e) { // tlačítko pro výběr adresářů na disku
            string selectedPath = "";
            Thread t = new Thread((ThreadStart)(() => {
                using (var fbd = new FolderBrowserDialog()) {
                    DialogResult result = fbd.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath)) {
                        selectedPath = fbd.SelectedPath;
                    }
                }
            }));
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
            tbDir.Text = selectedPath;

            //string selectedPath = "";

            //Thread t = new Thread((ThreadStart)(() => {
            //    OpenFileDialog saveFileDialog1 = new OpenFileDialog();

            //    saveFileDialog1.Filter = "Vše|*.*|JSON Files (*.json)|*.json";
            //    saveFileDialog1.FilterIndex = 1;
            //    saveFileDialog1.RestoreDirectory = true;

            //    if (saveFileDialog1.ShowDialog() == DialogResult.OK) {
            //        selectedPath = saveFileDialog1.FileName;
            //    }
            //}));

            //// Run your code from a thread that joins the STA Thread
            //t.SetApartmentState(ApartmentState.STA);
            //t.Start();
            //t.Join();

            //// e.g C:\Users\MyName\Desktop\myfile.json
            //Console.WriteLine(selectedPath);

            //MessageBox.Show(fileContent, "File Content at path: " + filePath, MessageBoxButtons.OK);
            //var fileContent = string.Empty;
            //var filePath = string.Empty;
            //DialogResult diResul;
            //this.openFile.Filter = "Zobraz vše|*.*|Zobraz .dri|*.dri";
            //this.openFile.FilterIndex = 1;
            //this.openFile.Title = "Výběr adresáře";
            //this.openFile.CheckPathExists = true;
            //diResul = this.openFile.ShowDialog();
            //if (diResul == DialogResult.OK) {
            //    filePath = openFile.FileName;

            //    var fileStream = openFile.OpenFile();

            //    using (StreamReader reader = new StreamReader(fileStream)) {
            //        fileContent = reader.ReadToEnd();
            //    }
            //}
            //this.lbChanged.Text = fileContent;
        }

        private void btChanges_Click(object sender, EventArgs e) { // tlačítko pro spuštění porovnání změn v adresáři

            Path = tbDir.Text;
            DirectoryInfo dirInfo = null;
            try { // zachycení neplatného zadání adresáře výjimkou
                dirInfo = new DirectoryInfo(Path);
                if (dirInfo.Exists) {
                    this.Hide();
                } else {
                    tbChanged.Text = "Zadána neplatná cesta k adresáři.\r\nOpravte zadání a stiskněte tlačítko \"Zobraz změny\" nebo tlačítko \"Konec programu\" pro ukončení programu."; ;
                }
            } catch (ArgumentException) {
                tbChanged.Text = "Chybně zadaná cesta k adresáři - zadány nepovolené znaky." +
                    "\r\nOpravte zadání a stiskněte tlačítko \"Zobraz změny\" nebo tlačítko \"Konec programu\" pro ukončení programu.";
            }
        }
        private void FrmContent_Load(object sender, EventArgs e) {
            tbChanged.Text = Msg.ToString();
        }
        private void button1_Click(object sender, EventArgs e) { // ukončení programu
            this.Close();
            EndProgram = true;
        }

        private void FrmContent_FormClosing(object sender, FormClosingEventArgs e) {
            if (e.CloseReason == CloseReason.UserClosing ) { // při zavření formuláře červeným křížkem nastaví EndProgram = true
                this.EndProgram = true;
            }
        }
    }
}