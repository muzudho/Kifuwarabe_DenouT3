namespace Grayscale.Kifuwarakaku.Entities.Features
{
    public class MotiItemImpl : MotiItem
    {

        /// <summary>
        /// 駒の種類。
        /// </summary>
        public PieceType Komasyurui
        {
            get
            {
                return this.komasyurui;
            }
        }
        private PieceType komasyurui;

        /// <summary>
        /// 持っている枚数。
        /// </summary>
        public int Maisu
        {
            get
            {
                return this.maisu;
            }
        }
        private int maisu;

        /// <summary>
        /// プレイヤーサイド。
        /// </summary>
        public Playerside Playerside
        {
            get
            {
                return this.playerside;
            }
        }
        private Playerside playerside;

        /// <summary>
        /// コンストラクター。
        /// </summary>
        public MotiItemImpl(PieceType komasyurui, int maisu, Playerside playerside)
        {
            this.komasyurui = komasyurui;
            this.maisu = maisu;
            this.playerside = playerside;
        }

    }
}
