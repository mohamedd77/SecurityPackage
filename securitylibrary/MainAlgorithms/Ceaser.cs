using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Ceaser : ICryptographicTechnique<string, int>
    {
        public static string abc = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        static char[] characters = abc.ToCharArray();

        public int indx(char c) {
            var charindex = new Dictionary<char, int>();
            for (int i = 0; i < 26; i++)
            {
                charindex[characters[i]] = i;
            }

            return charindex[c];
        }
        public string Encrypt(string plainText, int key)
        {
            char[] plain_text = plainText.ToUpper().ToCharArray();
            int size = plain_text.Length;
            char[] cipherText= new char[size];
            int index;
            for (int i = 0; i < size; i++)
            {
                index = indx(plain_text[i]);
                index = (index + key) % 26;
                cipherText[i] = characters[index];
            }
            string cipher = new string(cipherText);
            return cipher;
        }
        public string Decrypt(string cipherText, int key)
        {
            int index;
            char[] cipher = cipherText.ToUpper().ToCharArray();
            int size = cipherText.Length;
            char[] plain_text = new char[size];
            for (int i = 0; i < size; i++)
            {
                index = indx(cipher[i]) - key;
                if (index < 0)
                {
                    index = index + 26;
                }
                plain_text[i] = characters[index];
            }
            string plain = new string(plain_text);
            return plain;
        }

        public int Analyse(string plainText, string cipherText)
        {
            char[] plain_text = plainText.ToUpper().ToCharArray();
            char[] cipher = cipherText.ToUpper().ToCharArray();
            int size1 = plain_text.Length;
            int size2 = cipherText.Length;
            int key;
            key = indx(cipherText[0])-indx(plain_text[0]);
            if (key < 0)
            {
                key=key+26;
            }
            Console.WriteLine(key);
            return key;

        }
    }
}
