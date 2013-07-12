namespace MBrackets
{
    partial class Form1
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Liberare le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.iniziaAnalisiToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.evidenziaSezioneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.pulisciToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copiaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.incollToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.apriFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.calcolaRegioniToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iniziaAnalisiToolStripMenuItem1,
            this.calcolaRegioniToolStripMenuItem,
            this.evidenziaSezioneToolStripMenuItem,
            this.toolStripSeparator3,
            this.pulisciToolStripMenuItem,
            this.copiaToolStripMenuItem,
            this.incollToolStripMenuItem,
            this.toolStripSeparator1,
            this.apriFileToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(185, 170);
            // 
            // iniziaAnalisiToolStripMenuItem1
            // 
            this.iniziaAnalisiToolStripMenuItem1.Name = "iniziaAnalisiToolStripMenuItem1";
            this.iniziaAnalisiToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.iniziaAnalisiToolStripMenuItem1.Size = new System.Drawing.Size(184, 22);
            this.iniziaAnalisiToolStripMenuItem1.Text = "Inizia analisi";
            this.iniziaAnalisiToolStripMenuItem1.Click += new System.EventHandler(this.iniziaAnalisiToolStripMenuItem1_Click);
            // 
            // evidenziaSezioneToolStripMenuItem
            // 
            this.evidenziaSezioneToolStripMenuItem.Name = "evidenziaSezioneToolStripMenuItem";
            this.evidenziaSezioneToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.evidenziaSezioneToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.evidenziaSezioneToolStripMenuItem.Text = "Evidenzia sezione";
            this.evidenziaSezioneToolStripMenuItem.Click += new System.EventHandler(this.evidenziaSezioneToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(181, 6);
            // 
            // pulisciToolStripMenuItem
            // 
            this.pulisciToolStripMenuItem.Name = "pulisciToolStripMenuItem";
            this.pulisciToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.pulisciToolStripMenuItem.Text = "Pulisci";
            this.pulisciToolStripMenuItem.Click += new System.EventHandler(this.pulisciToolStripMenuItem_Click);
            // 
            // copiaToolStripMenuItem
            // 
            this.copiaToolStripMenuItem.Name = "copiaToolStripMenuItem";
            this.copiaToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.copiaToolStripMenuItem.Text = "Copia";
            this.copiaToolStripMenuItem.Click += new System.EventHandler(this.copiaToolStripMenuItem_Click);
            // 
            // incollToolStripMenuItem
            // 
            this.incollToolStripMenuItem.Name = "incollToolStripMenuItem";
            this.incollToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.incollToolStripMenuItem.Text = "Incolla";
            this.incollToolStripMenuItem.Click += new System.EventHandler(this.incollToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(181, 6);
            // 
            // apriFileToolStripMenuItem
            // 
            this.apriFileToolStripMenuItem.Name = "apriFileToolStripMenuItem";
            this.apriFileToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.apriFileToolStripMenuItem.Text = "Apri file";
            this.apriFileToolStripMenuItem.Click += new System.EventHandler(this.apriFileToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 628);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(802, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // richTextBox1
            // 
            this.richTextBox1.AcceptsTab = true;
            this.richTextBox1.ContextMenuStrip = this.contextMenuStrip1;
            this.richTextBox1.DetectUrls = false;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(802, 628);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            this.richTextBox1.WordWrap = false;
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // calcolaRegioniToolStripMenuItem
            // 
            this.calcolaRegioniToolStripMenuItem.Name = "calcolaRegioniToolStripMenuItem";
            this.calcolaRegioniToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.calcolaRegioniToolStripMenuItem.Text = "Calcola regioni";
            this.calcolaRegioniToolStripMenuItem.Click += new System.EventHandler(this.calcolaRegioniToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 650);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Form1";
            this.Text = "Missing Brackets";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem copiaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem incollToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem apriFileToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem iniziaAnalisiToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem pulisciToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem evidenziaSezioneToolStripMenuItem;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem calcolaRegioniToolStripMenuItem;
    }
}

