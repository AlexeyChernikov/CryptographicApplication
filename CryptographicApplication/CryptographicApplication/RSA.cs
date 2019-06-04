using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;
namespace CryptographicApplication
{
    class RSA
    {
        Alphabet alph = new Alphabet();

        private int[] key = null;

        public void SetKey(int[] _key)
        {
            key = new int[_key.Length];

            for (int i = 0; i < _key.Length; i++)
                key[i] = _key[i];
        }

        public void SetKey(string[] _key)
        {
            key = new int[_key.Length];

            for (int i = 0; i < _key.Length; i++)
                key[i] = Convert.ToInt32(_key[i]);
        }

        public void SetKey(string _key)
        {
            SetKey(_key.Split(' '));
        }

        public string Encrypt(string sourcetext) //генерация ключа
        {
            StringBuilder code = new StringBuilder();

            long e = Convert.ToInt64(key[0]);
            long n = Convert.ToInt64(key[1]);

            MessageBox.Show(Convert.ToString(key[0]));
            MessageBox.Show(Convert.ToString(key[1]));

            List<string> result = RSA_Endoce(sourcetext, e, n);

            foreach (string item in result)
                code.Append(item + "\n");

            return code.ToString();
        }

        public string Decrypt(List<string> sourcetext)
        {
            StringBuilder code = new StringBuilder();

            long d = Convert.ToInt64(key[0]);
            long n = Convert.ToInt64(key[1]);

            code.Append(RSA_Dedoce(sourcetext, d, n));

            return code.ToString();
        }

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

        public long Calculate_e(long fi) //вычисление открытой экспоненты
        {
            long e = 2;

            for (int i = 2; i <= fi; i++)
                if ((fi % i == 0) && (e % i == 0) && e < fi) //если имеют общие делители
                {
                    e++;
                    i = 1;
                }

            return e;
        }

        public long Calculate_d(long e, long fi) //вычисление закрытой экспоненты 
        {
            long d = 2;

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