using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.DiffieHellman
{
    public class DiffieHellman 
    {
        public int Power(int a, int b, int c)
        {
            int result = 1;
            int x = 0;
            do
            {
                result = (result * a) % c;
                x++;
            }while(x < b) ;
            return result;
        }
        public List<int> GetKeys(int q, int alpha, int xa, int xb)
        {
            //throw new NotImplementedException();
            int Y_A = Power(alpha, xa, q);
            int Y_B = Power(alpha, xb, q);
            int K_XA = Power(Y_B, xa, q);
            int K_XB = Power(Y_A, xb, q);

            List<int> result = new List<int>();
            result.Add(K_XA);
            result.Add(K_XB);

            return result;
        }
    }
}
