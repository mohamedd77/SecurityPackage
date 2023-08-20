using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.AES
{
    public class ExtendedEuclid
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <param name="baseN"></param>
        /// <returns>Mul inverse, -1 if no inv</returns>
        public int GetMultiplicativeInverse(int number, int baseN)
        {
            List<int> a = new List<int>();
            List<int> L = new List<int>();
            List<List<int>> table1 = new List<List<int>>();
            List<List<int>> table2 = new List<List<int>>();

            L.Add(1);
            L.Add(0);
            L.Add(baseN);
            table1.Add(new List<int>(L));
            L.Clear();

            L.Add(0);
            L.Add(1);
            L.Add(number);
            table2.Add(new List<int>(L));

            for (int k = 0; k < table2.Count && table2[k][2] != 0 && table2[k][2] != 1; k++)
            {
                a.Add(table1[k][2] / table2[k][2]);

                for (int i = 0; i < 3; i++)
                {
                    L.Add(table2[k][i]);
                }
                table1.Add(new List<int>(L));
                L.Clear();

                for (int i = 0; i < 3; i++)
                {
                    L.Add(table1[k][i] - a[k] * table2[k][i]);
                }
                table2.Add(new List<int>(L));
                L.Clear();
            }
            if (table2[table2.Count - 1][2] == 1)
            {
                if (table2[table2.Count - 1][1] < 0)
                {
                    while (table2[table2.Count - 1][1] < 0)
                    {
                        table2[table2.Count - 1][1] += baseN;
                    }
                }
                return table2[table2.Count - 1][1];
            }

            return -1;
        }


    }
}
