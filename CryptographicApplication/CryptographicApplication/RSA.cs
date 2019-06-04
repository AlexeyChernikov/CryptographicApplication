using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace CryptographicApplication
{
    class RSA
    {
        Alphabet alph = new Alphabet();

        public string Encrypt(string sourcetext, string key_p, string key_q) //генерация ключа
        {
            StringBuilder code = new StringBuilder();

            int p = Convert.ToInt32(key_p);
            int q = Convert.ToInt32(key_q);

            if (IsTheNumberSimple(p) && IsTheNumberSimple(q))
            {
                int n = p * q;
                int fi = (p - 1) * (q - 1);
                int e = Calculate_e(fi);
                int d = Calculate_d(e, fi);

                List<string> result = RSA_Endoce(sourcetext, e, n);

                foreach (string item in result)
                    code.Append(item + "\n");
            }

            return code.ToString();
        }

        public string Decrypt(List<string> sourcetext, string key_d, string key_n)
        {
            StringBuilder code = new StringBuilder();

            long d = Convert.ToInt64(key_d);
            long n = Convert.ToInt64(key_n);

            string result = RSA_Dedoce(sourcetext, d, n);

            return result;
        } //изменить

        private List<string> RSA_Endoce(string sourcetext, long e, long n) //шифрование
        {
            List<string> result = new List<string>();

            BigInteger bi;

            for (int i = 0; i < sourcetext.Length; i++)
            {
                int index = Array.IndexOf(alph.lang, sourcetext[i]);

                bi = new BigInteger(index);
                bi = BigInteger.Pow(bi, (int)e);

                BigInteger bn = new BigInteger((int)n);

                bi = bi % bn;

                result.Add(bi.ToString());
            }

            return result;
        }

        private string RSA_Dedoce(List<string> sourcetext, long d, long n) //дешифрование
        {
            string result = "";

            BigInteger bi;

            foreach (string item in sourcetext)
            {
                bi = new BigInteger(Convert.ToDouble(item));
                bi = BigInteger.Pow(bi, (int)d);

                BigInteger bn = new BigInteger((int)n);

                bi = bi % bn;

                int index = Convert.ToInt32(bi.ToString());

                if (index == -1)
                {
                    result += " ";
                }
                else
                {
                    result += alph.lang[index].ToString();
                }
            }

            return result;
        }

        public bool IsTheNumberSimple(long n) //является ли число простым
        {
            if (n < 2)
                return false;

            if (n == 2)
                return true;

            for (int i = 2; i < n; i++)
                if (n % i == 0)
                    return false;

            return true;
        }

        public int Calculate_e(int fi) //вычисление открытой экспоненты
        {
            int e = 2;

            for (int i = 2; i <= fi; i++)
                if ((fi % i == 0) && (e % i == 0) && e < fi) //если имеют общие делители
                {
                    e++;
                    i = 1;
                }

            return e;
        }

        public int Calculate_d(int e, int fi) //вычисление закрытой экспоненты 
        {
            int d = 2;

            while (true)
            {
                if ((d * e % fi == 1) && d != e)
                    break;
                else
                    d++;
            }

            return d;
        }
    }
}