using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryptor
{
    public class Crypt
    {
        string key;
        string codes = "aábcčdďeéěfghiíjklmnoópqrřsštťuúůvwxyýzž ";
        string[] keys;
        public Crypt(string _key)
        {
            key = _key;
            codes = codes + codes.ToUpper();
            createKeys();
        }
        public string decrypt(string str)
        {
            string output = "";
            int tmpi = 0;
            int itrkey = 0;
            for (int p = 0; p < str.Length; p++)
            {
                if (str[p] == '\r' && str[p + 1] == '\n')
                {
                    output += "\r\n";
                    p++;
                    continue;
                }
                tmpi = Array.IndexOf(this.keys[itrkey].ToCharArray(), str[p]);
                output += this.codes[tmpi];
                itrkey = itrkey + 1 < key.Length ? itrkey + 1 : 0;

            }
            return output;
        }
        public string encrypt(string str)
        {
            string output = "";
            int tmpi = 0;
            int itrkey = 0;
            for (int p = 0; p < str.Length; p++)
            {
                if (str[p] == '\r' && str[p+1] == '\n')
                {
                    output += "\r\n";
                    p++;
                    continue;
                }

                if (str[p] == ',' || str[p] == '.')
                    continue;

                tmpi = Array.IndexOf(this.codes.ToCharArray(), str[p]);
                output += keys[itrkey][tmpi];
                itrkey = itrkey+1 < key.Length ? itrkey+1 : 0;

            }
            return output;
        }

        private void createKeys()
        {
            string waplhabet = this.codes + this.codes;

            int startpos = 0;
            int length = this.codes.Length;
            this.keys = new string[this.key.Length];
            for (int i = 0; i < key.Length; i++)
            {
                if(key[i] == '\r' && key[i + 1] == '\n')
                {
                    i++;
                    continue;
                }
                if (key[i] == ',' || key[i] == '.')
                    continue;

                startpos = Array.IndexOf(waplhabet.ToCharArray(), key[i]);
                this.keys[i] = String.Copy(waplhabet.Substring(startpos, length));
            }
        }

    }
}