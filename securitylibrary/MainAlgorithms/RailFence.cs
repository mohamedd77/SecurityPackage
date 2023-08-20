using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class RailFence : ICryptographicTechnique<string, int>
    {
        public int Analyse(string plainText, string cipherText)
        {
            String.Join(plainText, plainText.Split(' '));
            String.Join(cipherText, cipherText.Split(' '));

            cipherText = cipherText.ToLower();

            if (String.Equals(cipherText, plainText.ToLower()))
            {
                return 1; 
            }

            List<int> P_Keys = new List<int>();

            /*
            for (int i = 0; i < plainText.Length; i++)
            {
                P_Keys.Add(i);
            }
            */

            
            char sec_letter = cipherText[1];
            
            int index = 0; 
            do
            {
                if (plainText[index] == sec_letter)
                {
                    P_Keys.Add(index);
                }
                index++; 
            }while (index < plainText.Length);



            int i = 0; 

            while (i < P_Keys.Count)
            {
                string CT = Encrypt(plainText, P_Keys[i]).ToLower();

                if (String.Equals(cipherText, CT))
                {

                    return P_Keys[i];
                }
                i++; 
            }

           
            return 0;
            
        }



        public string Decrypt(string cipherText, int key)
        {
            cipherText = cipherText.ToLower();
            int PTLength = (int)Math.Ceiling((double)cipherText.Length / key);
          /*
            List<List<char>> lst_letter = new List<List<char>>();

            int number_of_list = 0;
            do
            {
                lst_letter.Add(new List<char>());
                number_of_list++;
            } while (number_of_list < key);


            int index_letter = 0; 

            for (int i = 0; i < lst_letter.Count(); i++)
            {
                for (int j = 0; j < PTLength; j++)
                {
                    lst_letter[i].Add(cipherText[index_letter]); 
                    index_letter++;
                    if (index_letter == cipherText.Length) break;  
                }    
            }

            string CT = "";

            int counter = 0;

            for (int i = 0; i < PTLength; i++)
            {
                for (int j = 0; j < lst_letter.Count() ; j++)
                {
                    if (counter == cipherText.Length) break;
                    CT += lst_letter[j][i]; 
                    counter++;
                }
                if (counter == cipherText.Length) break;
            }


            return CT.ToLower();
           */ 
             return Encrypt(cipherText, PTLength).ToLower();

        }

        public string Encrypt(string plainText, int key)
        {
            if (key < 2)
            {
                return plainText.ToUpper();
            }
            

            String.Join(plainText, plainText.Split(' '));
            List< List<char> > lst_letter = new List < List<char> > ();


            int number_of_list = 0 ;

            do
            {
                lst_letter.Add(new List<char>());
                number_of_list++;
            } while (number_of_list < key);


            int max_row = (int)Math.Ceiling((double)plainText.Length / key);
            int index_char = 0;

            for (int i = 0; i < max_row; i++)
            {
                for (int j = 0; j < key ; j++)
                {
                    lst_letter[j].Add(plainText[index_char]);
                    index_char++;
                    if (index_char == plainText.Length) 
                        break;
                }
            }

            string CipherText = "";

            int index_list = 0;
            while (index_list < lst_letter.Count)
            {
                for (int i = 0;i < lst_letter[index_list].Count;i++) 
                {
                    CipherText = CipherText + lst_letter[index_list][i]; 
                } 

                index_list++;   
            }

            return CipherText.ToUpper();
        }
    }
}