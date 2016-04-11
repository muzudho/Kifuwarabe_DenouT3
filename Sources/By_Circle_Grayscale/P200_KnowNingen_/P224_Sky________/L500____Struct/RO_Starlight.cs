using Grayscale.P218_Starlight__.L___500_Struct;

namespace Grayscale.P224_Sky________.L500____Struct
{

    /// <summary>
    /// リードオンリー駒位置
    /// 
    /// 動かない星の光。
    /// </summary>
    public class RO_Starlight : Starlight
    {
        #region プロパティー類

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 先後、升、配役
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public Starlightable Now { get { return this.now; } }
        protected Starlightable now;

        #endregion



        /// <summary>
        /// ************************************************************************************************************************
        /// 駒用。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="masu"></param>
        /// <param name="syurui"></param>
        public RO_Starlight(Starlightable nowStar)
        {
            this.now = nowStar;
        }

    }
}
