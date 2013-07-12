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

        public Form1()
        {
            InitializeComponent();

            evidenziaSezioneToolStripMenuItem.Enabled = false;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            evidenziaSezioneToolStripMenuItem.Enabled = false;
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
       
        }

        private void aToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
                    toolStripStatusLabel1.Text = "File " + Path.GetFileName(file) + " aperto.";
                    this.Text = "Missing Brackets [" + Path.GetFileName(file) + "]";
                }));
            }).Start();
        }

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

                    if (calcolaRegioniToolStripMenuItem.Checked)
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
                                parentesi_valori[i, 1] = Count_Length(Parser.posizioni_inizio[i, 0], Parser.posizioni_inizio[i, 1], Parser.posizioni_fine[i, 0], Parser.posizioni_fine[i, 1]);
                            }
                        }

                        evidenziaSezioneToolStripMenuItem.Enabled = true;
                    }
                    else evidenziaSezioneToolStripMenuItem.Enabled = false;

                    Parser.Clear();

                    richTextBox1.Enabled = true;

                    // principalmente per motivi di debug (?)
                    // MessageBox.Show("Aperte: " + aperte + " Chiuse: " + chiuse);

                    toolStripStatusLabel1.Text =
                        "Finito. Aperte: " + aperte + ", chiuse: " + chiuse + "." +
                        ((aperte > chiuse) ? " il numero di { non corrisponde al numero di }" :
                        (chiuse > aperte) ? " il numero di } non corrispondono al numero di }" : " codice corretto.");
                }));
            }).Start();
        }
        
        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
        }

        private void pulisciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Text area pulita.";
            richTextBox1.Clear();
            evidenziaSezioneToolStripMenuItem.Enabled = false;
            this.Text = "Missing Brackets";
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
            
            if (current_idx > parentesi_length)
            {
                current_idx = 0;
            }

            richTextBox1.Select(parentesi_valori[current_idx, 0], parentesi_valori[current_idx, 1]);
        }

        // inefficient? to test.
        // A => line start
        // B => finish line
        // cPos => {
        // cPosB => }
        private int Count_Length(int A, int cPos, int B, int cPosB)
        {
            int tot =
                richTextBox1.Lines[A].Substring(cPos).Length + 1;

            for (int i = A+1; i < B; i++)
            {
                tot += richTextBox1.Lines[i].Length + 1;
            }

            tot += richTextBox1.Lines[B].Substring(0, cPosB).Length + 1;

            return tot;
        }

        private void calcolaRegioniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            calcolaRegioniToolStripMenuItem.Checked = !calcolaRegioniToolStripMenuItem.Checked;
        }
    }
}
