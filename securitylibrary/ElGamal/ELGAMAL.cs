using System;
using System.Collections.Generic;

namespace SecurityLibrary.ElGamal
{
    public class ElGamal
    {
        /// <summary>
        /// Encryption
        /// </summary>
        /// <param name="alpha"></param>
        /// <param name="q"></param>
        /// <param name="y"></param>
        /// <param name="k"></param>
        /// <returns>list[0] = C1, List[1] = C2</returns>
        public List<long> Encrypt(int q, int alpha, int y, int k, int m)
        {
            int a = calc_exp(alpha, k, q);
            int b = (m * calc_exp(y, k, q)) % q;
            return new List<long> { a, b };
        }

        public int Decrypt(int c1, int c2, int x, int q)
        {
            int shared_key = calc_exp(c1, x, q);

            int modular_inverse = calc_exp(shared_key, q - 2, q);

            int plaintext = (c2 * modular_inverse) % q;

            return plaintext;
        }

        private int calc_exp(int baseNum, int exp, int modulus)
        {
            int result = 1;
            while (exp > 0)
            {
                if (exp % 2 == 1)
                {
                    result = (result * baseNum) % modulus;
                }
                baseNum = (baseNum * baseNum) % modulus;
                exp = exp / 2;
            }
            return result;
        }
    }
}


