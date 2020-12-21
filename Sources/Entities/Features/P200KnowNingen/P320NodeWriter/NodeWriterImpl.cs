using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.P307UtilSky.L500Util;
using System.Diagnostics;

namespace Grayscale.P320NodeWriter.L500Writer
{
    public class NodeWriterImpl<T1, T2>
    {

        public Json_Val ToJsonVal(NodeImpl<T1, T2> node)
        {
            Json_Obj obj = new Json_Obj();

            KyokumenWrapper kWrap = node.Value as KyokumenWrapper;
            if (null != kWrap)
            {
                // TODO: ログが大きくなるので、１行で出力したあとに改行にします。

                Json_Prop prop = new Json_Prop("kyokumen", Util_Sky307.ToJsonVal(kWrap.KyokumenConst));
                obj.Add(prop);
            }
            else
            {
                Debug.Fail("this.Value as KyokumenWrapper じゃなかった。");
            }

            return obj;
        }

    }
}
