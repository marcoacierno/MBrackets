using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;

namespace MBrackets
{
    public partial class Form1 : Form
    {
        int parentesi_length;
        int[,] parentesi_valori = new int[1024,2];

        int current_idx = -1;// -1 perchè il primo indice è 0

        bool file_open = false;
        string file_in_use = string.Empty;

        public Form1()
        {
            InitializeComponent();

            evidenziaSezioneToolStripMenuItem.Enabled = false;
            toolStripStatusLabel2.Visible = false;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            evidenziaSezioneToolStripMenuItem.Enabled = false;

            if(file_open)
            {
                toolStripStatusLabel2.Visible = true;
            }
        }

        private void apriFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string file = openFileDialog1.FileName;

            richTextBox1.Clear();

            new Thread( () =>
            {
                BeginInvoke(new MethodInvoker(() =>
                {
                    using (StreamReader sr = new StreamReader(file))
                    {
                        richTextBox1.AppendText(sr.ReadToEnd());
                    }

                    evidenziaSezioneToolStripMenuItem.Enabled = false;
                    richTextBox1.SelectionStart = 0;

                    string fname = Path.GetFileName(file);

                    toolStripStatusLabel1.Text = "File " + fname + " aperto.";
                    this.Text = "Missing Brackets [" + fname + "]";
                    file_in_use = file;
                    file_open = true;
                    toolStripStatusLabel2.Visible = false;
                }));
            }).Start();
        }

        #region Analisi
        private void iniziaAnalisiToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Attendere..";

            new Thread(() =>
            {
                BeginInvoke(new MethodInvoker(() =>
                {
                    richTextBox1.Enabled = false;

                    int aperte = 0;
                    int chiuse = 0;

                    int sel = richTextBox1.SelectionStart;
                    int len = richTextBox1.SelectionLength;

                    // clear colors
                    richTextBox1.Select(0, richTextBox1.TextLength);
                    richTextBox1.SelectionColor = Color.Black;

                    // parser config
                    //coloraCodiceToolStripMenuItem1
                    Parser.use_colors = coloraCodiceToolStripMenuItem1.Checked;
                    Parser.read_regions = calcolaRegioniToolStripMenuItem1.Checked;
                    Parser.parse_defines = valutaDefinesToolStripMenuItem.Checked;

                    foreach (string line in richTextBox1.Lines)
                    {
                        Parser.Analisi(line, ref aperte, ref chiuse, ref this.richTextBox1);
                    }

                    if (parentesi_valori != null)
                    {
                        // se precendemente già ha eseguito un analisi
                        // pulisco le array

                        Array.Clear(parentesi_valori, 0, parentesi_valori.Length);
                    }

                    if (calcolaRegioniToolStripMenuItem1.Checked)
                    {
                        parentesi_valori = new int[Parser.posizione_idx, 2];
                        parentesi_length = Parser.posizione_idx;

                        for (int i = 0; i < Parser.posizione_idx; i++)
                        {
                            if (Parser.posizioni_inizio[i, 0] == Parser.posizioni_fine[i, 0])
                            {
                                parentesi_valori[i, 0] = richTextBox1.GetFirstCharIndexFromLine(Parser.posizioni_inizio[i, 0]) + Parser.posizioni_inizio[i, 1];
                                parentesi_valori[i, 1] = (Parser.posizioni_fine[i, 1] - Parser.posizioni_inizio[i, 1]) + 1;
                            }
                            else
                            {
                                parentesi_valori[i, 0] = richTextBox1.GetFirstCharIndexFromLine(Parser.posizioni_inizio[i, 0]) + Parser.posizioni_inizio[i, 1];           
                                parentesi_valori[i, 1] = ((richTextBox1.GetFirstCharIndexFromLine(Parser.posizioni_fine[i, 0]) + Parser.posizioni_fine[i, 1]) - parentesi_valori[i, 0]) + 1;
                            }
                        }

                        evidenziaSezioneToolStripMenuItem.Enabled = true;
                    }
                    else evidenziaSezioneToolStripMenuItem.Enabled = false;

                    Parser.Clear(ref this.richTextBox1);

                    richTextBox1.Enabled = true;

                    //ripristino il punto dove stava puntato
                    richTextBox1.SelectionStart = sel;
                    richTextBox1.SelectionLength = len;
                    // principalmente per motivi di debug (?)
                    // MessageBox.Show("Aperte: " + aperte + " Chiuse: " + chiuse);

                    toolStripStatusLabel1.Text =
                        "Finito. Aperte: " + aperte + ", chiuse: " + chiuse + "." +
                        ((aperte > chiuse) ? " il numero di { non corrisponde al numero di }" :
                        (chiuse > aperte) ? " il numero di } non corrispondono al numero di }" : " codice corretto.");
                }));
            }).Start();
        }
        #endregion

        #region Menu - settings
        private void pulisciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Text area pulita.";
            richTextBox1.Clear();
            evidenziaSezioneToolStripMenuItem.Enabled = false;
            this.Text = "Missing Brackets";
            file_open = false;
            toolStripStatusLabel2.Visible = false;
            file_in_use = string.Empty;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void copiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionLength > 0)
            {
                richTextBox1.Copy();
            }
        }

        private void incollToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void evidenziaSezioneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            current_idx++;
            
            if (current_idx >= parentesi_length)
            {
                current_idx = 0;
            }

            try
            {
                richTextBox1.Select(parentesi_valori[current_idx, 0], parentesi_valori[current_idx, 1]);
            }
            catch(Exception)
            {
                current_idx = 0;
                richTextBox1.Select(parentesi_valori[0, 0], parentesi_valori[0, 1]);
            }
        }

        private void coloraCodiceToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            coloraCodiceToolStripMenuItem1.Checked = !coloraCodiceToolStripMenuItem1.Checked;
        }

        private void calcolaRegioniToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            calcolaRegioniToolStripMenuItem1.Checked = !calcolaRegioniToolStripMenuItem1.Checked;
        }

        private void valutaDefinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            valutaDefinesToolStripMenuItem.Checked = !valutaDefinesToolStripMenuItem.Checked;
        }

        private void selezionaTuttoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }
        #endregion

        #region Salvataggio
        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {
            if (file_open && file_in_use != string.Empty)
            {
                DoSave();
            }
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            file_open = true;
            file_in_use = saveFileDialog1.FileName;

            File.WriteAllLines(file_in_use, richTextBox1.Lines);
 
            string fname = Path.GetFileName(file_in_use);

            toolStripStatusLabel1.Text = "File " + fname + " aperto.";
            this.Text = "Missing Brackets [" + fname + "]";
        }

        private void salvaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // verifico che non ho già un altro file aperto
            if (!file_open && file_in_use == string.Empty)
            {
                saveFileDialog1.ShowDialog();

            }
            else
            {
                // salvo il file precedente
                DoSave();
            }
        }

        private void DoSave()
        {
            // cancella una vecchia copia
            if (File.Exists(file_in_use + ".backup"))
            {
                File.Delete(file_in_use + ".backup");
            }

            try
            {
                File.Move(file_in_use, file_in_use + ".backup");

                File.WriteAllLines(file_in_use, richTextBox1.Lines);

                File.Delete(file_in_use + ".backup");

                toolStripStatusLabel2.Visible = false;
            }
            catch (Exception ee)
            {
                toolStripStatusLabel1.Text = "Il salvataggio è andato storto (" + ee.Message + ")";
            }
        }

        #endregion
    }
}
