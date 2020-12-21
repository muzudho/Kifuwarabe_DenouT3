using Grayscale.Kifuwarakaku.Entities.Features;

namespace Grayscale.P353ConvSasuEx.L500Converter
{
    public abstract class Conv_SasuEntry
    {

        /// <summary>
        /// SasuEntry→KifuNode
        /// </summary>
        /// <param name="sasuEntry"></param>
        /// <param name="src_Sky"></param>
        /// <returns></returns>
        public static KifuNode ToKifuNode(
            SasuEntry sasuEntry,
            SkyConst src_Sky
            )
        {
            return new KifuNodeImpl(sasuEntry.NewMove, new KyokumenWrapper(
                Util_Sasu341.Sasu(
                    src_Sky,//指定局面
                    sasuEntry.Finger,//指す駒
                    sasuEntry.Masu,//移動先升
                    sasuEntry.Naru//成ります。
            )));
        }
    }
}
