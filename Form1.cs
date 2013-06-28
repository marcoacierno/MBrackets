using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace MBrackets
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            //toolStripStatusLabel1.Text = richTextBox1.SelectionStart.ToString();
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
                        Parser.Analisi(line, ref aperte, ref chiuse);
                    }

                    Parser.Clear();
                    richTextBox1.Enabled = true;

                    MessageBox.Show("Aperte: " + aperte + " Chiuse: " + chiuse);

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
    }
}
