using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class RepeatingkeyVigenere : ICryptographicTechnique<string, string>
    {
        string alph = "abcdefghijklmnopqrstuvwxyz";
        string c_T = "";string p_T = "";
        string key = "";
        string incr = "";
        public string Analyse(string plainText, string cipherText)
        {
            Console.WriteLine("\n  Analyse: ");
            cipherText = cipherText.ToLower();     
            int C_len = cipherText.Length;
            //while (key.Length != C_len) ;
            for (int k = 0; k < C_len; k++)
            {
                key += alph[((alph.IndexOf(cipherText[k]) - alph.IndexOf(plainText[k])) + 26) % 26];
            }
            incr += key[0];
            int k_len = key.Length;
            //int n = 1;
            for (int n = 1; n < k_len; n++)
            //while (n < k_len )
            {    
                //n++;
                while (cipherText.Equals(Encrypt(plainText, incr)))
                {
                    return incr;
                }
                incr += key[n];
            }
            Console.WriteLine("\n key return MLTkey  ### ");
            return key;
        }

        public string Decrypt(string cipherText, string key)
        {
            Console.WriteLine("\n  Decrypt: ");
            string p_T = "";
            cipherText = cipherText.ToLower();
            int C_len = cipherText.Length;
            int increament = 0;

            do
            {
                key += key[increament];
                increament++;
            } while (key.Length != C_len);
            for (int i = 0; i < C_len; i++)
            {
                //foreach (int i in plainText)
                // i++; }
                //int n < plain_len;
                //alph.IndexOf(p_T|[i])=(alph.IndexOf(cipherText[i]) - alph.IndexOf(key[i]));
                //alph.IndexOf(p_T[i]) += 26;
                //p_T = alph.IndexOf(p_T[i]) % 26;
                p_T += alph[((alph.IndexOf(cipherText[i]) - alph.IndexOf(key[i])) + 26) % 26];

            }//while (i < C_len) ;
            Console.WriteLine("\n plainText form key_stream  ### ");

            return p_T;

        }
        /// <summary>
        /// ..................................................................................
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Encrypt(string plainText, string key)
        {
            Console.WriteLine("\n  Encrypt: ");
            string c_T = "";
            int increament = 0;
            int plain_len = plainText.Length;
            do
            {
                key += key[increament];
                increament++;
            } while (key.Length != plainText.Length);

            for (int n = 0; n < plain_len; n++)
            {
                //foreach (int i in plainText)
                // i++; }
                //int n < plain_len;
                c_T += alph[((alph.IndexOf(plainText[n]) + alph.IndexOf(key[n]))) % 26];

            }
            Console.WriteLine("\n chiperText form key_stream ###  ");
            return c_T;
            
        }
    }
}

//while (key.Length != plainText.Length)
//{
//    key += key[tryy];
//    tryy++;
//}
//    //else { }
//else
//{
//    Console.WriteLine("it not possible");
//}
