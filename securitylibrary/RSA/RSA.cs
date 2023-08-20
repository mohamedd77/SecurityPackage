using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecurityLibrary.AES;

namespace SecurityLibrary.RSA
{
    public class RSA
    {
        public int Encrypt(int p, int q, int M, int e)
        {
            int n = p * q;
            int ciphertext = 1;

            for (int i = 0; i < e; i++)
            {
                ciphertext = (ciphertext * M) % n;
            }

            return ciphertext;

            // throw new NotImplementedException();
        }

        public int Decrypt(int p, int q, int C, int e)
        {
            ExtendedEuclid obj = new ExtendedEuclid();

            int n = p * q;
            int plaintext = 1;
            int a = (p - 1) * (q - 1);
            int w = obj.GetMultiplicativeInverse(e, a);
            for (int i = 0; i < w; i++)
            {
                plaintext = (plaintext * C) % n;
            }

            return plaintext;
            //throw new NotImplementedException();
        }

    }
}
