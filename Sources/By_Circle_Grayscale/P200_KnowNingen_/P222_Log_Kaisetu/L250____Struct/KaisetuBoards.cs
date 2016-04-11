using System.Collections.Generic;

namespace Grayscale.P222_Log_Kaisetu.L250____Struct
{

    /// <summary>
    /// 複数の盤をもつログ・ファイルです。
    /// </summary>
    public class KaisetuBoards
    {

        public List<KaisetuBoard> boards { get; set; }

        public KaisetuBoards()
        {
            this.boards = new List<KaisetuBoard>();
        }

    }
}
