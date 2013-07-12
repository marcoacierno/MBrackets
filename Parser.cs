using System;
using System.Text;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MBrackets
{
    static class Parser
    {
        static bool in_string = false;
        static bool in_comment = false;

        // improve it using a container (like ?)
        static int level = 0;
        public static int[,] posizioni_inizio = new int[1024, 2];//very very bad anyway
        public static int[,] posizioni_fine = new int[1024, 2];//very very bad anyway
        public static int posizione_idx = -1;//inizia da -1 perchè il primo indice deve essere 0
        static int nline = -1;// inizia da -1 perchè VS fa iniziare le linee da 0

        public static void Analisi(string line, ref int aperte, ref int chiuse, ref RichTextBox rtb)
        {
            if (line == null) return;

            // i dont love this code; but works lul

            char c;             // Rappresenta il carattere del ciclo attuale    (  i  )
            char c2 = '\0';     // Rappresenza il carattere del ciclo precedente (i - 1)

            nline ++;

            for (int i = 0; i < line.Length; ++i)
            {
                c = line[i];

                if (c == '"')
                {
                    in_string = !in_string;
                }
                else if (c == '/')// commento di questo tipo su una linea
                {
                    if (c2 == c)// se i due caratteri corrispondono signifca che sono due //
                    {
                        break;
                    }
                    else if (c2 == '*')
                    {
                        in_comment = false;
                    }
                }
                else if (c == '*')
                {
                    if (c2 == '/')
                    {
                        in_comment = true;
                    }
                }
                else if (c == '{')
                {
                    if (!in_string && !in_comment)
                    {
                        aperte++;
                        level++;

                        if (level == 1)
                        {
                            posizione_idx++;

                            posizioni_inizio[posizione_idx, 0] = nline;
                            posizioni_inizio[posizione_idx, 1] = i;
                        }

                        rtb.SelectionStart = rtb.GetFirstCharIndexFromLine(nline) + i;
                        rtb.SelectionLength = 1;
                        rtb.SelectionColor = Color.Blue;
                    }
                    else
                    {
                        rtb.SelectionStart = rtb.GetFirstCharIndexFromLine(nline) + i;
                        rtb.SelectionLength = 1;
                        rtb.SelectionColor = Color.Red;
                    }
                }
                else if (c == '}')
                {
                    if (!in_string && !in_comment)
                    {
                        chiuse++;
                        level--;

                        if (level == 0)
                        {
                            posizioni_fine[posizione_idx, 0] = nline;
                            posizioni_fine[posizione_idx, 1] = i;
                        }

                        rtb.SelectionStart = rtb.GetFirstCharIndexFromLine(nline) + i;
                        rtb.SelectionLength = 1;
                        rtb.SelectionColor = Color.Blue;
                    }
                    else
                    {
                        rtb.SelectionStart = rtb.GetFirstCharIndexFromLine(nline) + i;
                        rtb.SelectionLength = 1;
                        rtb.SelectionColor = Color.Red;
                    }
                }

                c2 = c;
            }
        }

        public static void Clear()
        {
            in_comment = false;
            in_string = false;
            posizione_idx = -1;
            
            Array.Clear(posizioni_inizio, 0, posizioni_inizio.Length);
            Array.Clear(posizioni_fine, 0, posizioni_fine.Length);

            nline = -1;
            level = 0;

            // pulisce il colore
            //rtb.Select(rtb.TextLength, 0);
            //rtb.SelectionColor = Color.Black;
        }
    }
}
