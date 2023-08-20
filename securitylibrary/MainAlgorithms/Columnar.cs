using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Columnar : ICryptographicTechnique<string, List<int>>
    {
        public List<int> Analyse(string plainText, string cipherText)
        {
            IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> vars)
            {
                if (vars.Count() != 1)
                {
                    foreach (var x_var in vars)
                    {
                        var remainingItems = vars.Where(x => !x.Equals(x_var));
                        foreach (var permutation in GetPermutations(remainingItems))
                        {
                            yield return new[] { x_var }.Concat(permutation);
                        }
                    }
                }
                else
                {
                    yield return vars;
                }
            }



            int PTlen = plainText.Length;
            int CTlen = cipherText.Length;

            int rows_n = (int)Math.Ceiling(Math.Sqrt(CTlen));
            var possibleColumns = GetPermutations(Enumerable.Range(1, rows_n)).Select(x => x.ToList());

            List<List<int>> keys = new List<List<int>>();

            foreach (var cols in possibleColumns)
            {
                int numCols = PTlen / rows_n;
                if (PTlen % rows_n != 0)
                {
                    numCols += 1;
                }
                if (numCols != rows_n)
                {
                    keys.Add(cols);
                }
            }

            foreach (var key in keys)
            {
                string s = Encrypt(plainText, key).ToLower();

                if (String.Equals(cipherText, s))
                {

                    return key;
                }
            }

            return null; 
            
        }
        public string Decrypt(string cipherText, List<int> key)
        {
            cipherText = cipherText.ToUpper();
            int x = cipherText.Length;
            if (x % key.Count == 0)
            {
            }
            else
            {
                x += key.Count;
            }
            double col = x / key.Count;
            int c = (int)(col);
            string fin_res = "";
            char[,] enc = new char[c, key.Count];
            int k = 0;
            int temp = 0;
            int i = 0;
            do
            {
                k = key.IndexOf(i + 1);

                for (int j = 0; j < col; j++)
                {
                    if (temp < cipherText.Length)
                    {
                        enc[j, k] = cipherText[temp];
                        temp++;
                    }
                }
                i++;
            } while (i < key.Count);
            for (i = 0; i < col; i++)
            {
                for (int j = 0; j < key.Count; j++)
                {
                    fin_res = fin_res + enc[i, j];
                }
            }

            return fin_res.ToUpper();
        }


        public string Encrypt(string plainText, List<int> key)
        {
            int columns = key.Count;
            int rows = (int)Math.Ceiling((double)plainText.Length / columns);


            List<List<char>> lst_letter = new List<List<char>>();

            int number_of_list = 0;

            do
            {
                lst_letter.Add(new List<char>());
                number_of_list++;
            } while (number_of_list < rows);

           
            if (plainText.Length != rows * columns)
            {
                int x = (rows * columns) - plainText.Length;
                string appender = new string('x', x);
                plainText += appender;
            }
           
            int counter = 0;

            int ind = 0;

            while (ind < rows)
            {
                int j = 0;
                do
                {
                    lst_letter[ind].Add((char)plainText[counter]);
                    counter++;
                    if (counter == plainText.Length) 
                        break;
                    j++;
                } while (j < columns);
                if (counter == plainText.Length) 
                    break;
                ind++;
            }

            Dictionary<int, string> define_columns = new Dictionary<int, string>();
            int i = 0;
            do
            {
                string tmp = "";
                for (int j = 0; j < rows; j++)
                {
                    tmp = tmp + lst_letter[j][i];
                    define_columns[key[i]] = tmp;
                }
                i++;
            } while (i < columns);

            string CipherText = "";

            for (i = 1; i <= define_columns.Count; i++)
            {
                CipherText = CipherText + define_columns[i];
            }
            return CipherText.ToUpper();
        }
    }
}
