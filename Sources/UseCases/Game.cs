using Grayscale.Kifuwarakaku.Entities;
using Grayscale.Kifuwarakaku.Entities.Features;

namespace Grayscale.Kifuwarakaku.UseCases
{
    public class Game : IGame
    {
        public Game()
        {
            this.FirstPside = Playerside.P1;

            // FIXME:平手とは限らないが、平手という前提で作っておく。
            this.Kifu = new KifuTreeImpl(
                    new KifuNodeImpl(
                        Util_Sky258A.RootMove,
                        new KyokumenWrapper(SkyConst.NewInstance(
                                Util_SkyWriter.New_Hirate(this.FirstPside),
                                0 // 初期局面は 0手目済み
                            ))// きふわらべ起動時
                    )
            );
            this.Kifu.SetProperty(Word_KifuTree.PropName_Startpos, "startpos");// 平手 // FIXME:平手とは限らないが。
        }

        public Playerside FirstPside { get; private set; }

        /// <summary>
        /// 棋譜です。
        /// </summary>
        public KifuTree Kifu { get; private set; }

        public int GoBTime { get; private set; }
        public int GoWTime { get; private set; }
        public int GoByoYomi { get; private set; }
        public int GoBInc { get; private set; }
        public int GoWInc { get; private set; }

        /// <summary>
        /// 「go ponder」の属性です。
        /// </summary>
        public bool GoPonderNow { get; set; }
    }
}
