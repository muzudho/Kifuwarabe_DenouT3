using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P218_Starlight__.L___500_Struct;
using Grayscale.P226_Tree_______.L___500_Struct;
using Grayscale.P226_Tree_______.L500____Struct;
using Grayscale.P247_KyokumenWra.L500____Struct;
using Grayscale.P307_UtilSky____.L500____Util;
using Grayscale.P321_KyokumHyoka.L___250_Struct;
using Grayscale.P321_KyokumHyoka.L250____Struct;
using Grayscale.P324_KifuTree___.L___250_Struct;
using Grayscale.P339_ConvKyokume.L500____Converter;
using System.Text;

namespace Grayscale.P324_KifuTree___.L250____Struct
{
    public class KifuNodeImpl : NodeImpl<Starbeamable, KyokumenWrapper>, KifuNode
    {
        #region プロパティー

        /// <summary>
        /// スコア
        /// </summary>
        public float Score { get { return this.score; } }
        public void AddScore(float offset) { this.score += offset; }
        public void SetScore(float score) { this.score = score; }
        private float score;

        /// <summary>
        /// 局面評価明細。Mutable なので、SkyConst には入れられない。
        /// </summary>
        public KyHyokaSheet KyHyokaSheet_Mutable { get { return this.kyHyokaSheet; } }
        /// <summary>
        /// 枝専用。
        /// </summary>
        /// <param name="branchKyHyoka"></param>
        public void SetBranchKyHyokaSheet(KyHyokaSheet branchKyHyoka)
        {
            this.kyHyokaSheet = branchKyHyoka;
        }
        private KyHyokaSheet kyHyokaSheet;

        #endregion

        /// <summary>
        /// TODO:この局面データを、読み込めないようにします。（使っていないことを確認するため用）
        /// </summary>
        public void Lock_Kyokumendata()
        {
            this.Value = null;
        }

        public bool IsLeaf { get { return 0 == this.Count_ChildNodes; } }



        /// <summary>
        /// コンストラクター。
        /// </summary>
        /// <param name="shootingStarlightable"></param>
        /// <param name="kyokumenWrapper"></param>
        public KifuNodeImpl(Starbeamable shootingStarlightable, KyokumenWrapper kyokumenWrapper)
            : base(shootingStarlightable, kyokumenWrapper)
        {
            this.kyHyokaSheet = new KyHyokaSheetImpl();
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="hubNode">追加したいノードの一覧を入れたリスト。</param>
        //public void PutAppdendNextNodes(
        //    Node<Starbeamable, KyokumenWrapper> hubNode
        //    )
        //{

        //    hubNode.Foreach_ChildNodes((string key, Node<Starbeamable, KyokumenWrapper> node, ref bool toBreak) =>
        //    {
        //        if (!this.ContainsKey_ChildNodes(key))
        //        {
        //            this.PutAdd_ChildNode(key, node);
        //        }
        //    });
        //}

        public bool HasTuginoitte(
            string sasiteStr
            )
        {
            return this.ContainsKey_ChildNodes(sasiteStr);
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// 棋譜に　次の一手　を追加します。
        /// ************************************************************************************************************************
        /// 
        /// KifuIO を通して使ってください。
        /// 
        /// ①コマ送り用。
        /// ②「成り」フラグの更新用。
        /// ③マウス操作用
        /// 
        /// カレントノードは変更しません。
        /// </summary>
        public void PutTuginoitte_New(
            Node<Starbeamable, KyokumenWrapper> newNode
            )
        {
            // 同じ指し手があれば追加してはいけない？
#if DEBUG
            string sasiteStr = Conv_SasiteStr_Sfen.ToSasiteStr_Sfen(newNode.Key);

            System.Diagnostics.Debug.Assert(
                !this.HasTuginoitte(sasiteStr),
                "指し手[" + sasiteStr + "]は既に指されていました。"
                );
#endif
            // SFENをキーに、次ノードを増やします。
            this.PutAdd_ChildNode(Conv_SasiteStr_Sfen.ToSasiteStr_Sfen(newNode.Key), newNode);
            //手番はここでは変更できない。

            newNode.SetParentNode( this);
        }
        /// <summary>
        /// 既存の子要素を上書きします。
        /// </summary>
        /// <param name="existsNode"></param>
        public void PutTuginoitte_Override(
            Node<Starbeamable, KyokumenWrapper> existsNode
            )
        {
            // SFENをキーに、次ノードを増やします。
            this.NextNodes[Conv_SasiteStr_Sfen.ToSasiteStr_Sfen(existsNode.Key)] = existsNode;
            existsNode.SetParentNode( this);
        }

        public string Json_NextNodes_MultiSky(
            string memo,
            string hint,
            int temezumi_yomiGenTeban_forLog,//読み進めている現在の手目済
            KwErrorHandler errH)
        {
            StringBuilder sb = new StringBuilder();

            this.Foreach_ChildNodes((string key, Node<Starbeamable, KyokumenWrapper> node, ref bool toBreak) =>
            {
                sb.AppendLine(Util_Sky307.Json_1Sky(
                    node.Value.KyokumenConst,
                    memo + "：" + key,
                    hint + "_SF解1",
                    temezumi_yomiGenTeban_forLog
                    ));// 局面をテキストで作成
            });

            return sb.ToString();
        }


        ///// <summary>
        ///// この木の、全てのノードを、フォルダーとして作成します。
        ///// </summary>
        ///// <returns></returns>
        //public void CreateAllFolders(string folderpath, int currentDeep, int limitDeep)
        //{
        //    try
        //    {
        //        if (null == this.GetParentNode())
        //        {
        //            // ルートノードはスルー。
        //        }
        //        else
        //        {
        //            folderpath = folderpath + "/" + Conv_SasiteStr_Sfen.ToSasiteStr_Sfen_ForFilename(this.Key);
        //            Directory.CreateDirectory(folderpath);
        //        }

        //        if (currentDeep <= limitDeep)
        //        {
        //            this.Foreach_ChildNodes((string key, Node<Starbeamable, KyokumenWrapper> node, ref bool toBreak) =>
        //            {
        //                ((KifuNode)node).CreateAllFolders(
        //                    folderpath,
        //                    currentDeep + 1,
        //                    limitDeep);
        //            });
        //        }
        //    }
        //    catch(PathTooLongException ex)
        //    {
        //        System.Console.WriteLine("パスが長すぎた☆無視して続行☆：" + ex.Message + "\n folderpath=[" + folderpath + "]");
        //    }
        //}

    }
}
