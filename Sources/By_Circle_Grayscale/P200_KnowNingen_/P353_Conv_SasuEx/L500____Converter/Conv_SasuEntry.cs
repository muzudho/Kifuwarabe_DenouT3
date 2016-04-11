using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P224_Sky________.L500____Struct;
using Grayscale.P247_KyokumenWra.L500____Struct;
using Grayscale.P324_KifuTree___.L___250_Struct;
using Grayscale.P324_KifuTree___.L250____Struct;
using Grayscale.P341_Ittesasu___.L510____OperationB;

namespace Grayscale.P353_Conv_SasuEx.L500____Converter
{
    public abstract class Conv_SasuEntry
    {

        /// <summary>
        /// SasuEntry→KifuNode
        /// </summary>
        /// <param name="sasuEntry"></param>
        /// <param name="src_Sky"></param>
        /// <param name="errH"></param>
        /// <returns></returns>
        public static KifuNode ToKifuNode(
            SasuEntry sasuEntry,
            SkyConst src_Sky,
            KwErrorHandler errH
            )
        {
            return new KifuNodeImpl(sasuEntry.NewSasite, new KyokumenWrapper(
                Util_Sasu341.Sasu(
                    src_Sky,//指定局面
                    sasuEntry.Finger,//指す駒
                    sasuEntry.Masu,//移動先升
                    sasuEntry.Naru,//成ります。
                    errH
            )));
        }
    }
}
