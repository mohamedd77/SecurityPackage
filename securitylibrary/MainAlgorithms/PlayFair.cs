using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class PlayFair : ICryptographic_Technique<string, string>
    {
        private int row1, row2, col1, col2;
        private List<char> alphabets = new List<char>();
        private char[,] Build_matrix(string s)
        {
            char[,] matrix = new char[5, 5];
            alphabets.AddRange("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
            string key = s.ToUpper();
            int x = 0;
            int y = 0;
            int key_len = key.Length;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (x < key_len)
                    {
                        if (!is_Containing_Element(matrix, key[x]))
                        {
                            if (key[x] == 'J' && !is_Containing_Element(matrix, 'I'))
                            {
                                matrix[i, j] = 'I';
                            }
                            else if (alphabets[y] == 'J' && is_Containing_Element(matrix, 'I'))
                            {
                                j--;
                            }
                            else
                            {
                                matrix[i, j] = key[x];
                            }

                        }
                        else
                        {
                            j--;
                        }
                        x++;
                    }
                    else
                    {
                        if (!is_Containing_Element(matrix, alphabets[y]))
                        {
                            if (alphabets[y] == 'J' && !is_Containing_Element(matrix, 'I'))
                            {
                                matrix[i, j] = 'I';
                            }
                            else if (alphabets[y] == 'J' && is_Containing_Element(matrix, 'I'))
                            {
                                j--;
                            }
                            else
                                matrix[i, j] = alphabets[y];
                        }
                        else
                        {
                            j--;
                        }
                        y++;
                    }

                }
            }
            return matrix;
        }
        private List<string> splitting(string text)
        {
            List<string> split = new List<string>();
            int w = 0;
            int count = 0;
            int text_len = text.Length;
            split.Add("");
            for (int i = 0; i < text_len; i++)
            {
                if (count != 2)
                {
                    if (split[w].Length > 0)
                    {
                        if (split[w][0].Equals(text[i]))
                        {
                            split[w] += 'X';
                            i--;
                        }
                        else
                        {
                            split[w] += text[i];
                        }
                    }
                    else
                    {
                        split[w] += text[i];
                    }
                    count++;
                }
                else
                {
                    count = 0;
                    w++;
                    i--;
                    split.Add("");
                }
            }
            if (split[split.Count - 1].Length == 1)
            {
                split[split.Count - 1] += 'X';
            }
            return split;
        }
        private bool is_Containing_Element(char[,] matrix, char element)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (matrix[i, j].Equals(element))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private int search(string pieces, char[,] matrix)
        {
            int type = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (matrix[i, j].Equals(pieces[0]))
                    {
                        row1 = i;
                        col1 = j;
                    }
                    if (matrix[i, j].Equals(pieces[1]))
                    {
                        row2 = i;
                        col2 = j;
                    }
                }
            }
            if (row1 == row2)
            {
                type = 1;
            }
            else if (col1 == col2)
            {
                type = 2;
            }
            else
            {
                type = 3;
            }

            return type;
        }
        public string Decrypt(string cipherText, string key)
        {

            char[,] matrix = new char[5, 5];
            matrix = Build_matrix(key);
            string decrypt = "";
            List<string> splitted = new List<string>();
            splitted = splitting(cipherText);
            int type;
            foreach (string pieces in splitted)
            {
                type = search(pieces, matrix);
                int r1, r2, c1, c2, index;
                if (type == 1)
                {
                    index = col1 - 1;
                    if (index < 0) index = 5 + index;
                    c1 = index % 5;
                    decrypt += matrix[row1, c1];
                    index = col2 - 1;
                    if (index < 0) index = 5 + index;
                    c2 = index % 5;
                    decrypt += matrix[row2, c2];
                }
                if (type == 2)
                {
                    index = row1 - 1;
                    if (index < 0) index = 5 + index;
                    r1 = index % 5;
                    decrypt += matrix[r1, col1];
                    index = row2 - 1;
                    if (index < 0) index = 5 + index;
                    r2 = index % 5;
                    decrypt += matrix[r2, col2];
                }
                if (type == 3)
                {
                    decrypt += matrix[row1, col2];
                    decrypt += matrix[row2, col1];
                }
            }
            int count = 0;
            int j = 0;
            List<string> splited = new List<string>();
            splited.Add("");
            for (int i = 0; i < decrypt.Length; i++)
            {
                if (count != 2)
                {
                    if (splited[j].Length > 0)
                    {
                        splited[j] += decrypt[i];
                    }
                    else
                    {
                        splited[j] += decrypt[i];
                    }
                    count++;
                }
                else
                {
                    count = 0;
                    j++;
                    i--;
                    splited.Add("");
                }
            }
            string res = "";
            for (int i = 0; i < splited.Count; i++)
            {
                if (i < splited.Count - 1)
                    if (splited[i][0] == splited[i + 1][0] && splited[i][1].Equals('X'))
                    {
                        splited[i].Remove(1, 1);
                        res += splited[i][0];
                    }
                    else
                    {
                        res += splited[i][0];
                        res += splited[i][1];
                    }
            }

            if (splited[splited.Count - 1][1].Equals('X'))
            {
                res += splited[splited.Count - 1][0];
            }
            else
            {
                res += splited[splited.Count - 1][0];
                res += splited[splited.Count - 1][1];
            }
            decrypt = res;
            return decrypt;
        }

        public string Encrypt(string plainText, string key)
        {
            char[,] matrix = new char[5, 5];
            matrix = Build_matrix(key.ToUpper());
            List<string> splitted = new List<string>();
            string encrypt = "";
            splitted = splitting(plainText.ToUpper());
            int type;
            foreach (string piece in splitted)
            {
                type = search(piece, matrix);
                if (type == 1)
                {
                    encrypt += matrix[row1, (col1 + 1) % 5];
                    encrypt += matrix[row2, (col2 + 1) % 5];
                }
                if (type == 2)
                {
                    encrypt += matrix[(row1 + 1) % 5, col1];
                    encrypt += matrix[(row2 + 1) % 5, col2];
                }
                if (type == 3)
                {
                    encrypt += matrix[row1, col2];
                    encrypt += matrix[row2, col1];
                }
            }

            return encrypt;
        }

    }
}