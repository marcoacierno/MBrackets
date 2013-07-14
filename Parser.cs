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

        static int long_comment_start = 0;
        static int string_start = 0;

        public static Dictionary<string, string> defines = new Dictionary<string, string>(); // L'accesso è public
        // perchè sarà usato in futuro per altre code 
        //public static Dictionary<int, ToolTip> informazioni = new Dictionary<int, ToolTip>();

        /**
         * parser settings
         */
        static Color comment_color = Color.ForestGreen; // colore dei commenti
        static Color graffa_color = Color.FromKnownColor(KnownColor.DarkOrchid); // colore delle graffe che infuliscono sul codice
        //static Color graffa_invalid = Color.Red; // colore delle graffe che non influiscono sul codice
        static Color stringa_color = Color.FromArgb(163, 21, 21); // colore delle stringhe
        static Color define_color = Color.Blue;

        public static bool use_colors = true;         // Indica se il parser deve evidenziare alcune parti del codice
        public static bool read_regions = false;      // Indica se il parser deve leggere le regioni
        public static bool parse_defines = false;     // Indica se il parser deve analizzare i defines

        public static void Analisi(string line, ref int aperte, ref int chiuse, ref RichTextBox rtb)
        {
            if (line == null) return;

            //MessageBox.Show(line);
            nline++; // automento il numero di linea; è comunque una linea cazzo.

            // controllo se la linea è un define
            Match match = Regex.Match(line, @"^#define\s+(?<name>[:):;\[\]\w\d\{\}\%\(\),]+)\s+(?<code>.+)", RegexOptions.IgnoreCase | RegexOptions.Singleline);

            if (match.Success)
            {
                defines.Add(match.Groups["name"].Value, match.Groups["code"].Value);

                // la coloro di blu in modo da far capire che è stata interpretata come define
                if(use_colors)
                {
                    rtb.Select(rtb.GetFirstCharIndexFromLine(nline), line.Length);
                    rtb.SelectionColor = define_color;
                }
                return;
            }

            // controllo se la linea è un "undef"
            match = Regex.Match(line, @"^#undef\s+(?<name>.+)", RegexOptions.IgnoreCase | RegexOptions.Singleline);

            if (match.Success)
            {
                defines.Remove(match.Groups["name"].Value);

                if (use_colors)
                {
                    rtb.Select(rtb.GetFirstCharIndexFromLine(nline), line.Length);
                    rtb.SelectionColor = define_color;
                }
                return;
            }

            // ok, vediamo se la linea in questione ha qualche define al suo interno

            //defines.Keys
            if (parse_defines)
            {
                foreach (KeyValuePair<string, string> pair in defines)
                {
                    // MessageBox.Show("Pair, key: " + pair.Key + ", value: " + pair.Value);
                    int idx = pair.Key.IndexOf('(');

                    if (idx > -1)
                    {
                        line = Regex.Replace(line, pair.Key.Substring(0, idx), pair.Value);
                    }
                    else
                    {
                        line = Regex.Replace(line, pair.Key, pair.Value);
                    }
                }
            }

            // i dont love this code; but works lul

            char c;             // Rappresenta il carattere del ciclo attuale    (   i   )
            char c2 = '\0';     // Rappresenza il carattere del ciclo precedente ( i - 1 )

            for (int i = 0; i < line.Length; ++i)
            {
                c = line[i];
                // https://fbcdn-sphotos-d-a.akamaihd.net/hphotos-ak-prn2/1045226_562994603762863_653904693_n.png
                if (c == '"')
                {
                    // il carattere " si trova in una commento, ignora.
                    if (in_comment)
                        continue;

                    if (!in_string)
                    {
                        if (use_colors)
                            string_start = rtb.GetFirstCharIndexFromLine(nline) + i;

                        in_string = true;
                    }
                    else
                    {
                        in_string = false;

                        if (use_colors)
                        {
                            rtb.Select(string_start, (rtb.GetFirstCharIndexFromLine(nline) + i) - string_start + 1);
                            rtb.SelectionColor = stringa_color;
                        }
                    }
                }
                else if (c == '/')// commento di questo tipo su una linea
                {
                    if (c2 == c)// se i due caratteri corrispondono signifca che sono due //
                    {
                        //il // si trova in una stringa, ignora.
                        if (in_string)
                            continue;

                        //line comment
                        if (use_colors)
                        {
                            rtb.Select(rtb.GetFirstCharIndexFromLine(nline) + i - 1,
                                line.Length - i + 1
                            );
                            rtb.SelectionColor = comment_color;
                        }

                        break;
                    }
                    else if (c2 == '*')
                    {
                        //si trova in una stringa; non è valido.
                        if (in_string)
                            continue;
                        // long comment finish
                        if (use_colors)
                        {
                            rtb.Select(
                                long_comment_start - 1,
                                (rtb.GetFirstCharIndexFromLine(nline) + i) - long_comment_start + 2
                                );

                            rtb.SelectionColor = comment_color;
                        }

                        in_comment = false;
                    }
                }
                else if (c == '*')
                {
                    if (c2 == '/')
                    {
                        //si trova in una stringa; non è valido.
                        if (in_string)
                            continue;

                        if (use_colors)
                            long_comment_start = rtb.GetFirstCharIndexFromLine(nline) + i;

                        in_comment = true;
                    }
                }
                else if (c == '{')
                {
                    if (!in_string && !in_comment)
                    {
                        aperte++;

                        if (read_regions)
                        {
                            level++;

                            if (level == 1)
                            {
                                posizione_idx++;

                                posizioni_inizio[posizione_idx, 0] = nline;
                                posizioni_inizio[posizione_idx, 1] = i;
                            }
                        }

                        if (use_colors)
                        {
                            int fc = rtb.GetFirstCharIndexFromLine(nline);
                            rtb.SelectionStart = fc + i;
                            rtb.SelectionLength = 1;
                            rtb.SelectionColor = graffa_color;

                            rtb.SelectionStart = fc + i + 1;
                            rtb.SelectionLength = 1;
                            rtb.SelectionColor = Color.Black;
                        }
                    }
                }
                else if (c == '}')
                {
                    if (!in_string && !in_comment)
                    {
                        chiuse++;

                        if (read_regions)
                        {
                            level--;

                            if (level == 0)
                            {
                                posizioni_fine[posizione_idx, 0] = nline;
                                posizioni_fine[posizione_idx, 1] = i;
                            }
                        }

                        if (use_colors)
                        {
                            int fc = rtb.GetFirstCharIndexFromLine(nline);
                            rtb.SelectionStart = fc + i;
                            rtb.SelectionLength = 1;
                            rtb.SelectionColor = graffa_color;

                            rtb.SelectionStart = fc + i + 1;
                            rtb.SelectionLength = 1;
                            rtb.SelectionColor = Color.Black;
                        }
                    }
                }

                c2 = c;
            }
        }

        public static void Clear(ref RichTextBox rtb)
        {
            in_comment = false;
            in_string = false;
            posizione_idx = -1;
            
            Array.Clear(posizioni_inizio, 0, posizioni_inizio.Length);
            Array.Clear(posizioni_fine, 0, posizioni_fine.Length);

            nline = -1;
            level = 0;

            // pulisce il colore
            rtb.Select(rtb.TextLength, 0);
            rtb.SelectionColor = Color.Black;

            string_start = 0;
            long_comment_start = 0;

            defines.Clear();// pulisco le definizioni
        }
    }
}
