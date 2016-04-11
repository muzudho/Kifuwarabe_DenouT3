using System;
using System.Collections.Generic;

namespace Grayscale.P055_Conv_Sy.L500____Converter
{
    public abstract class Conv_Sy
    {
        /// <summary>
        /// 集合論の要素の名前ディクショナリー。
        /// </summary>
        private static Dictionary<ulong, string> bitfieldWordDictionary;

        /// <summary>
        /// 集合論の要素の名前 → ビットフィールド ディクショナリー。
        /// </summary>
        private static Dictionary<string, ulong> wordBitfieldDictionary;


        static Conv_Sy()
        {
            Conv_Sy.bitfieldWordDictionary = new Dictionary<ulong, string>();
            Conv_Sy.wordBitfieldDictionary = new Dictionary<string, ulong>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bitfield"></param>
        /// <param name="word"></param>
        public static void Put_BitfieldWord(ulong bitfield, string word)
        {
            // bitfield→word
            if (Conv_Sy.bitfieldWordDictionary.ContainsKey(bitfield))
            {
                Conv_Sy.bitfieldWordDictionary[bitfield] = word;
            }
            else
            {
                Conv_Sy.bitfieldWordDictionary.Add(bitfield, word);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bitfield"></param>
        /// <param name="word"></param>
        public static void Put_WordBitfield(string word, ulong bitfield)
        {
            // word→bitfield
            if (Conv_Sy.wordBitfieldDictionary.ContainsKey(word))
            {
                Conv_Sy.wordBitfieldDictionary[word] = bitfield;
            }
            else
            {
                Conv_Sy.wordBitfieldDictionary.Add(word, bitfield);
            }
        }

        public static string Query_Word(ulong bitfield)
        {
            string result_word;

            if (Conv_Sy.bitfieldWordDictionary.ContainsKey(bitfield))
            {
                result_word =Conv_Sy.bitfieldWordDictionary[bitfield];
            }
            else
            {
                result_word = "(エラー:word登録なし)";
            }

            return result_word;
        }

        public static ulong Query_Bitfield(string word)
        {
            ulong result_bitfield;

            if (Conv_Sy.wordBitfieldDictionary.ContainsKey(word))
            {
                result_bitfield =Conv_Sy.wordBitfieldDictionary[word];
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("エラー：bitfield登録なし。word=[" + word + "]", "エラー");
                throw new Exception("エラー：bitfield登録なし。word=[" + word + "]");
            }

            return result_bitfield;
        }

    }
}
