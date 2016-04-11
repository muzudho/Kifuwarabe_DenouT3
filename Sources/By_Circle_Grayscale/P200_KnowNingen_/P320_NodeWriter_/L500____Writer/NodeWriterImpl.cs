using Grayscale.P206_Json_______.L___500_Struct;
using Grayscale.P206_Json_______.L500____Struct;
using Grayscale.P226_Tree_______.L500____Struct;
using Grayscale.P247_KyokumenWra.L500____Struct;
using Grayscale.P307_UtilSky____.L500____Util;
using System.Diagnostics;

namespace Grayscale.P320_NodeWriter_.L500____Writer
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
