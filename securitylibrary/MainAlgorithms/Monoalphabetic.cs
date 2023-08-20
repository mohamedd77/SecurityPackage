using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Monoalphabetic : ICryptographicTechnique<string, string>
    {
        static string abc = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        static char[] characters = abc.ToCharArray();

        public string Analyse(string plainText, string cipherText)
        {
            char[] keyarr = new char[26];
            Dictionary<char, bool> chars = new Dictionary<char, bool>();
            SortedDictionary<char, char> cipherKey = new SortedDictionary<char, char>();
            int size = plainText.Length;
            plainText = plainText.ToLower();
            cipherText = cipherText.ToLower();
            string characters = abc.ToLower();
            for (int i = 0; i < size; i++)
            {
                if (cipherKey.ContainsKey(plainText[i])==false) 
                {
                    cipherKey[plainText[i]] = cipherText[i];
                    chars[cipherText[i]] = true;          
                }
            }    
            for (int i = 0; i < 26; i++)
            {
                if (cipherKey.ContainsKey(characters[i])==false)
                {
                    for (int j = 0; j < 26; j++)
                    {
                        if (chars.ContainsKey(characters[j])==false)
                        {
                            chars[characters[j]]=true;
                            cipherKey[characters[i]]=characters[j];
                            break;
                        }
                    }
                }
            }
            
            for(int i = 0; i < 26; i++)
            {
                keyarr[i]=cipherKey[characters[i]];
            }
            string key = new string(keyarr);
            return key;

        }
        public string Decrypt(string cipherText, string key)
        {
            char[] cipher = cipherText.ToUpper().ToCharArray();
            int size = cipherText.Length;
            char[] plain_text = new char[size];
            char[] keyarr = key.ToUpper().ToCharArray();
            var charindex = new Dictionary<char, char>();
            for (int i = 0; i < 26; i++)
            {
                charindex[keyarr[i]] = characters[i];
            }
            for (int i = 0; i < size; i++)
            {
                plain_text[i] = charindex[cipher[i]];
            }
            string plain = new string(plain_text);
            return plain;
            // throw new NotImplementedException();


        }

        public string Encrypt(string plainText, string key)
        {
            var charindex = new Dictionary<char, char>();
            char[] plain_text = plainText.ToUpper().ToCharArray();
            char[] keyarr =key.ToUpper().ToCharArray();
            int size = plain_text.Length;
            char[] cipherText = new char[size];
            for (int i = 0; i < 26; i++)
            {
                charindex[characters[i]] = keyarr[i];
            }
            for (int i = 0; i < size; i++)
            {
                cipherText[i] = charindex[plain_text[i]];
            }
            string ciphe = new string(cipherText);
            return ciphe;
        }

        /// <summary>
        /// Frequency Information:
        /// E   12.51%
        /// T	9.25
        /// A	8.04
        /// O	7.60
        /// I	7.26
        /// N	7.09
        /// S	6.54
        /// R	6.12
        /// H	5.49
        /// L	4.14
        /// D	3.99
        /// C	3.06
        /// U	2.71
        /// M	2.53
        /// F	2.30
        /// P	2.00
        /// G	1.96
        /// W	1.92
        /// Y	1.73
        /// B	1.54
        /// V	0.99
        /// K	0.67
        /// X	0.19
        /// J	0.16
        /// Q	0.11
        /// Z	0.09
        /// </summary>
        /// <param name="cipher"></param>
        /// <returns>Plain text</returns>
        public string AnalyseUsingCharFrequency(string cipher)
        {
            string chars = "ETAOINSRHLDCUMFPGWYBVKXJQZ".ToLower();
            Dictionary<char, int> characters = new Dictionary<char, int>();
            SortedDictionary<char, char> cipherKey = new SortedDictionary<char, char>();
            cipher = cipher.ToLower();
            int size = cipher.Length;
            char[] keyarr = new char[size];
            int index = 0;
            for (int i = 0; i < size; i++)
            {
                if (characters.ContainsKey(cipher[i])==false)
                {
                    characters[cipher[i]] = 0;     
                }
                else
                {
                    characters[cipher[i]]+=1;
                }
            }

            characters = characters.OrderBy(x => x.Value).Reverse().ToDictionary(x => x.Key, x => x.Value);
            foreach (var item in characters)
            {
                cipherKey[item.Key]= chars[index];
                index++;
            }
            for (int i = 0; i < size; i++)
            {
                keyarr[i] = cipherKey[cipher[i]];
            }
            string key = new string(keyarr);
            return key;
        
    }
        
    //throw new NotImplementedException();
}
    
}
