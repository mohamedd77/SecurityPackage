using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace SecurityLibrary
{
    /// <summary>
    /// The List<int> is row based. Which means that the key is given in row based manner.
    /// </summary>
    public class HillCipher : ICryptographicTechnique<List<int>, List<int>>
    {
        public int det(Matrix<double> matrix)
        {
            double A = matrix[0, 0] * (matrix[1, 1] * matrix[2, 2] - matrix[1, 2] * matrix[2, 1]) -
                       matrix[0, 1] * (matrix[1, 0] * matrix[2, 2] - matrix[1, 2] * matrix[2, 0]) +
                       matrix[0, 2] * (matrix[1, 0] * matrix[2, 1] - matrix[1, 1] * matrix[2, 0]);
            int AI = 0;
            if ((int)A % 26 >= 0)
                AI = (int)A % 26;
            else
                AI = (int)A % 26 + 26;
            //int AI = (int)A % 26 >= 0 ? (int)A % 26 : (int)A % 26 + 26;
            int i = 0;
            do
            {
                if (AI * i % 26 == 1)
                {
                    return i;
                }
                i++;
            } while (i < 26);

            return -1;
        }
        public Matrix<double> Mod_Cofactor(Matrix<double> matrix, int A)
        {
            Matrix<double> res_mat = DenseMatrix.Create(3, 3, 0.0);
            int i = 0;
            do
            {
                int j = 0;
                do
                {
                    int x = i == 0 ? 1 : 0, y = j == 0 ? 1 : 0, x1 = i == 2 ? 1 : 2, y1 = j == 2 ? 1 : 2;
                    double r = ((matrix[x, y] * matrix[x1, y1] - matrix[x, y1] * matrix[x1, y]) * Math.Pow(-1, i + j) * A) % 26;
                    if (r >= 0)
                        res_mat[i, j] = r;
                    else
                        res_mat[i, j] = r + 26;
                    //res_mat[i, j] = r >= 0 ? r : r + 26;
                    j++;
                } while (j < 3);
                i++;
            } while (i < 3);
            return res_mat;
        }
        public List<int> Analyse(List<int> plainText, List<int> cipherText)
        {
            List<double> CD = cipherText.ConvertAll(x => (double)x);
            List<double> PD = plainText.ConvertAll(x => (double)x);
            int matrix = Convert.ToInt32(Math.Sqrt((CD.Count)));
            Matrix<double> CMatrix = DenseMatrix.OfColumnMajor(matrix, (int)cipherText.Count / matrix, CD.AsEnumerable());
            Matrix<double> PMatrix = DenseMatrix.OfColumnMajor(matrix, (int)plainText.Count / matrix, PD.AsEnumerable());
            List<int> mayBeKey = new List<int>();
            int i = 0;
            do
            {
                int j = 0;
                do
                {
                    int k = 0;
                    do
                    {
                        int l = 0;
                        do
                        {
                            mayBeKey = new List<int>(new[] { i, j, k, l });
                            List<int> a = Encrypt(plainText, mayBeKey);
                            if (a.SequenceEqual(cipherText))
                            {
                                return mayBeKey;
                            }
                            l++;
                        } while (l < 26);
                        k++;
                    } while (k < 26);
                    j++;
                } while (j < 26);
                i++;
            } while (i < 26);

            throw new InvalidAnlysisException();
        }


        public List<int> Decrypt(List<int> cipherText, List<int> key)
        {
            List<double> keyD = key.ConvertAll(x => (double)x);
            List<double> CD = cipherText.ConvertAll(x => (double)x);
            int matrix = Convert.ToInt32(Math.Sqrt((key.Count)));
            Matrix<double> key_Matrix = DenseMatrix.OfColumnMajor(matrix, (int)key.Count / matrix, keyD.AsEnumerable());
            Matrix<double> Plain_matrix = DenseMatrix.OfColumnMajor(matrix, (int)cipherText.Count / matrix, CD.AsEnumerable());
            List<int> finalRes = new List<int>();
            if (key_Matrix.ColumnCount != 3)
            {
                key_Matrix = key_Matrix.Inverse();
                Console.WriteLine(key_Matrix.ToString());
                Console.WriteLine(((int)key_Matrix[0, 0]).ToString() + ", " + ((int)key_Matrix[0, 0]).ToString());
            }
            else
            {
                key_Matrix = Mod_Cofactor(key_Matrix.Transpose(), det(key_Matrix));
            }
            if (Math.Abs((int)key_Matrix[0, 0]).ToString() != Math.Abs((double)key_Matrix[0, 0]).ToString())
            {
                throw new SystemException();
            }
            int i = 0;
            do
            {
                List<double> final = new List<double>();
                final = ((((Plain_matrix.Column(i)).ToRowMatrix() * key_Matrix) % 26).Enumerate().ToList());
                int j = 0;
                do
                {
                    int x = 0;
                    if ((int)final[j] >= 0)
                        x = (int)final[j];
                    else
                        x = (int)final[j] + 26;
                    finalRes.Add(x);
                    j++;
                } while (j < final.Count);
                i++;
            } while (i < Plain_matrix.ColumnCount);

            do
            {
                Console.WriteLine(finalRes[i].ToString());
                i++;
            } while (i < finalRes.Count);

            return finalRes;
        }

        public List<int> Encrypt(List<int> plainText, List<int> key)
        {
            List<double> K_D = key.ConvertAll(x => (double)x);
            List<double> Plain_D = plainText.ConvertAll(x => (double)x);
            int matrix = Convert.ToInt32(Math.Sqrt((key.Count)));
            Matrix<double> key_Matrix = DenseMatrix.OfColumnMajor(matrix, (int)key.Count / matrix, K_D.AsEnumerable());
            Matrix<double> plain_matrix = DenseMatrix.OfColumnMajor(matrix, (int)plainText.Count / matrix, Plain_D.AsEnumerable());
            List<int> final_res = new List<int>();
            int i = 0;
            do
            {
                List<double> result = new List<double>();
                result = ((((plain_matrix.Column(i)).ToRowMatrix() * key_Matrix) % 26).Enumerate().ToList());
                int j = 0;
                do
                {
                    final_res.Add((int)result[j]);
                    j++;
                } while (j < result.Count);
                i++;
            } while (i < plain_matrix.ColumnCount);

            return final_res;
        }


        public List<int> Analyse3By3Key(List<int> plainText, List<int> cipherText)
        {
            List<double> cipher_D = cipherText.ConvertAll(x => (double)x);
            List<double> Plain_D = plainText.ConvertAll(x => (double)x);
            int matrix = Convert.ToInt32(Math.Sqrt((cipher_D.Count)));
            Matrix<double> cipher_matrix = DenseMatrix.OfColumnMajor(matrix, (int)cipherText.Count / matrix, cipher_D.AsEnumerable());
            Matrix<double> plain_matrix = DenseMatrix.OfColumnMajor(matrix, (int)plainText.Count / matrix, Plain_D.AsEnumerable());
            List<int> P_key = new List<int>();
            Matrix<double> key_matrix = DenseMatrix.Create(3, 3, 0);
            plain_matrix = Mod_Cofactor(plain_matrix.Transpose(), det(plain_matrix));
            key_matrix = (cipher_matrix * plain_matrix);
            P_key = key_matrix.Transpose().Enumerate().ToList().Select(i => (int)i % 26).ToList();
            P_key.ForEach(i => Console.WriteLine(i.ToString()));
            return P_key;
        }

    }
}
