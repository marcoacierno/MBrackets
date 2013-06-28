using System;
using System.Text;

namespace MBrackets
{
    static class Parser
    {
        static bool in_string = false;
        static bool in_comment = false;
        static int  nline = 0;

        public static void Analisi(string line, ref int aperte, ref int chiuse)
        {
            if (line == null) return;
            nline++;

            // i dont love this code; but works lul

            char c;             // Rappresenta il carattere del ciclo attuale    (  i  )
            char c2 = '\0';     // Rappresenza il carattere del ciclo precedente (i - 1)

            /*
             * 
             */

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
                    }
                }
                else if (c == '}')
                {
                    if (!in_string && !in_comment)
                    {
                        chiuse++;
                    }
                }

                c2 = c;
            }
        }

        public static void Clear()
        {
            in_comment = false;
            in_string  = false;
            nline = 0;
        }
    }
}
