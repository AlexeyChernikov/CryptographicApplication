using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace CryptographicApplication
{
    class RandomKeyGeneration
    {
        Alphabet alph = new Alphabet();
        RSA RSA_obj = new RSA();

        Random rnd = new Random();

        public string Rand_Key_Generation()
        {
            int value = rnd.Next(1, alph.lang.Length);
            return Convert.ToString(value);
        }

        public string Rand_Key_Generation(int sizesourcetext)
        {
            StringBuilder code = new StringBuilder();

            int value;

            for (int i = 0; i < sizesourcetext; i++)
            {
                value = rnd.Next(0, alph.lang.Length);
                code.Append(alph.lang[value]);
            }

            return code.ToString();
        }

        public string Rand_Key_Generation_Transposition(int sizesourcetext)
        {
            StringBuilder code = new StringBuilder();
            int[] arr = new int[sizesourcetext];
            int value1, value2, temp;

            for (int i = 0; i < sizesourcetext; i++)
            {
                arr[i] = i + 1;
            }

            for (int i = 0; i < sizesourcetext; i++) //перемешиваем элементы
            {
                value1 = rnd.Next(0, sizesourcetext);
                value2 = rnd.Next(0, sizesourcetext);

                temp = arr[value1];
                arr[value1] = arr[value2];
                arr[value2] = temp;
            }

            for (int i = 0; i < sizesourcetext; i++)
            {
                if (i == sizesourcetext - 1)
                {
                    code.Append(arr[i]);
                }
                else
                {
                    code.Append(arr[i] + " ");
                }
            }

            return code.ToString();
        }
    }
}