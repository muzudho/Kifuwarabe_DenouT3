namespace Grayscale.Kifuwarakaku.Entities.Features
{

    /// <summary>
    /// リードオンリー駒位置
    /// 
    /// 動かない星の光。
    /// </summary>
    public class RO_Starlight : IMoveHalf
    {
        #region プロパティー類

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 先後、升、配役
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public IMoveSource Now { get { return this.now; } }
        protected IMoveSource now;

        #endregion



        /// <summary>
        /// ************************************************************************************************************************
        /// 駒用。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="masu"></param>
        /// <param name="syurui"></param>
        public RO_Starlight(IMoveSource nowStar)
        {
            this.now = nowStar;
        }

    }
}
