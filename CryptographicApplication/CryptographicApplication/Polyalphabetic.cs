using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptographicApplication
{
    class Polyalphabetic
    {
        Alphabet alph = new Alphabet();

        public string Encrypt(string sourcetext, string key)
        {
            StringBuilder code = new StringBuilder();
            int[] key_id = new int[key.Length];
            int t = 0;

            //поиск индексов букв ключа
            for (int i = 0; i < key.Length; i++)
            {
                for (int j = 0; j < alph.lang.Length; j++)
                {
                    if (key[i] == alph.lang[j])
                    {
                        key_id[i] = j;
                        break;
                    }
                }
            }

            for (int i = 0; i < sourcetext.Length; i++)
            {
                //поиск символа в алфавите
                for (int j = 0; j < alph.lang.Length; j++)
                {
                    //если символ найден
                    if (sourcetext[i] == alph.lang[j])
                    {
                            if (t > key.Length - 1)
                            {
                                t = 0;
                            }
                            code.Append(alph.lang[(j + key_id[t]) % alph.lang.Length]);
                            t++;
                            
                        break;
                    }
                    //если символ не найден
                    else if (j == alph.lang.Length - 1)
                    {
                        code.Append(sourcetext[i]);
                        t++;
                    }
                }
            }

            return code.ToString();
        }

        public string Decrypt(string sourcetext, string key)
        {
            StringBuilder code = new StringBuilder();
            int[] key_id = new int[key.Length];
            int t = 0;

            //поиск индексов букв ключа
            for (int i = 0; i < key.Length; i++)
            {
                for (int j = 0; j < alph.lang.Length; j++)
                {
                    if (key[i] == alph.lang[j])
                    {
                        key_id[i] = j;
                        break;
                    }
                }
            }

            for (int i = 0; i < sourcetext.Length; i++)
            {
                //поиск символа в алфавите
                for (int j = 0; j < alph.lang.Length; j++)
                {
                    //если символ найден
                    if (sourcetext[i] == alph.lang[j])
                    {
                        if (t > key.Length - 1)
                        {
                            t = 0;
                        }
                        code.Append(alph.lang[(j + alph.lang.Length - key_id[t]) % alph.lang.Length]);
                        t++;
                        break;
                    }
                    //если символ не найден
                    else if (j == alph.lang.Length - 1)
                    {
                        code.Append(sourcetext[i]);
                        t++;
                    }
                }
            }

            return code.ToString();
        }
    }
}