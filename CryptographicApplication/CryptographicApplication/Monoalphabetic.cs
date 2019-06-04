using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptographicApplication
{
    class Monoalphabetic
    {
        Alphabet alph = new Alphabet();

        public string Encrypt(string sourcetext, int shift)
        {
            StringBuilder code = new StringBuilder();

            for (int i = 0; i < sourcetext.Length; i++)
            {
                //поиск символа в алфавите
                for (int j = 0; j < alph.lang.Length; j++)
                {
                    //если символ найден
                    if (sourcetext[i] == alph.lang[j])
                    {
                        code.Append(alph.lang[(j + shift) % alph.lang.Length]);

                        break;
                    }
                    //если символ не найден
                    else if (j == alph.lang.Length - 1)
                    {
                        code.Append(sourcetext[i]);
                    }
                }
            }

            return code.ToString();
        }

        public string Decrypt(string sourcetext, int shift)
        {
            StringBuilder code = new StringBuilder();

            for (int i = 0; i < sourcetext.Length; i++)
            {
                //поиск символа в алфавите
                for (int j = 0; j < alph.lang.Length; j++)
                {
                    //если символ найден
                    if (sourcetext[i] == alph.lang[j])
                    {
                        code.Append(alph.lang[(j - shift + alph.lang.Length) % alph.lang.Length]);
                        break;
                    }
                    //если символ не найден
                    else if (j == alph.lang.Length - 1)
                    {
                        code.Append(sourcetext[i]);
                    }
                }
            }

            return code.ToString();
        }
    }
}