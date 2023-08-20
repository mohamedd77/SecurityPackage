using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class AutokeyVigenere : ICryptographicTechnique<string, string>
    {
        string alph = "abcdefghijklmnopqrstuvwxyz";
        string c_T = ""; string p_T = "";
        string key = "";
        string incr = "";
        public string Analyse(string plainText, string cipherText)
        {

            Console.WriteLine("\n  Analyse: ");
            cipherText = cipherText.ToLower();
            int c_len = cipherText.Length;
            int k_len = key.Length;
            cipherText = cipherText.ToLower();
            //int c_len = cipherText.Length;
            for (int k = 0; k < c_len; k++)
            {
                key += alph[((alph.IndexOf(cipherText[k]) - alph.IndexOf(plainText[k])) + 26) % 26];
            }
            incr = incr + key[0];
            int klength = key.Length;
            for (int i = 1; i < klength; i++)
            {
               if (cipherText.Equals(Encrypt(plainText, incr)))
                {
                    return incr;
                }
                incr += key[i];
            }
            Console.WriteLine("\n key return MLTkey  ### ");
            return key;
        }

        public string Decrypt(string cipherText, string key)
        {
            Console.WriteLine("\n  Decrypt: ");
            string P_T = "";
            P_T = P_T.ToLower();
            key = key.ToLower();
            string newkey= "";
            int k_len = key.Length;
            cipherText = cipherText.ToLower();
            int c_len = cipherText.Length;

            char[] newplain_T = new char[c_len];
            char[] newkeyy = new char[c_len];
            int newkeyy_len = newkeyy.Length;
            int first_index = Convert.ToInt32('a');

            for (int n = 0; n < c_len; n++)
            {
                int ciper_T = Convert.ToInt32(cipherText[n]) - first_index;
                int keyy = Convert.ToInt32(key[n]) - first_index;
                int plain_T = (ciper_T - keyy);

                if (plain_T > 0)
                plain_T %= 26;

                if (plain_T < 0)
                plain_T += 26;
                plain_T += first_index;
                //plain_T += alph[((alph.IndexOf(cipherText[n]) - alph.IndexOf(key[i])) + 26) % 26];
                char increament = Convert.ToChar(plain_T);
                key += increament;
                newplain_T[n] = increament;
            }

//////////////////////////////////////////////////////////////////////////           
            int iteration = 0;
            for (int i = 0; i < newkeyy_len; i++) // iteration for new key_stream
            {
                newkeyy[i] = key[i];
                iteration++;
            }
            int increaament = 0;
            for (int i = iteration; i < c_len; i++) // iteration for new plain_Text
            {
                newkeyy[i] = newplain_T[increaament];
                increaament++;
            }
//////////////////////////////////////////////////////////////////////////    
            for (int i = k_len; i < c_len; i++)
            {
                //for (int i = 0; i < c_len; i++)
                //{
                //    P_T += alph[((alph.IndexOf(cipherText[i]) - alph.IndexOf(key[i])) + 26) % 26];
                //}
                int ciper_T = Convert.ToInt32(cipherText[i]) - first_index;
                int keyy = Convert.ToInt32(newkeyy[i]) - first_index;
                int plain_T = (ciper_T - keyy);///%26);

                if (plain_T > 0)
                plain_T %= 26;

                if (plain_T < 0)
                plain_T += 26;
                plain_T += first_index;
                
                char swap = Convert.ToChar(plain_T);
                newplain_T[i] = swap;
            }


            P_T = new string(newplain_T);
            Console.WriteLine("\n plainText form key_stream  ### ");
            return P_T.ToLower();

        }
        public string Encrypt(string plainText, string key)
        {
            Console.WriteLine("\n  Encrypt: ");
            string c_T = "";
            int increament = 0;
            int plain_len = plainText.Length;
            do
            {
                key += plainText[increament];
                increament++;
            } while (key.Length != plainText.Length);

            for (int n = 0; n < plain_len; n++)
            {

                c_T += alph[(alph.IndexOf(plainText[n]) + alph.IndexOf(key[n])) % 26];

            }
            Console.WriteLine("\n chiperText form key_stream ###  ");
            return c_T;
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
            
        }
    }
}