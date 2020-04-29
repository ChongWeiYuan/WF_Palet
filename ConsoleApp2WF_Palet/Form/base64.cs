using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2WF_Palet
{
    class Base64
    {
        private Encoding CharacterEncoding;//文字コード

        #region コンストラクタ
        public Base64(string encStr)
        {
            CharacterEncoding = Encoding.GetEncoding(encStr);
        }
        #endregion

        public string Encode(string str)
        {
            return Convert.ToBase64String(CharacterEncoding.GetBytes(str));
        }

        public string Decode(string str)
        {
            return CharacterEncoding.GetString(Convert.FromBase64String(str));
        }
    }
}
