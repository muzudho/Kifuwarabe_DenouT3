using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P218_Starlight__.L___500_Struct;
using Grayscale.P226_Tree_______.L___500_Struct;
using Grayscale.P247_KyokumenWra.L500____Struct;
using Grayscale.P321_KyokumHyoka.L___250_Struct;


namespace Grayscale.P324_KifuTree___.L___250_Struct
{

    /// <summary>
    /// 棋譜ノード。
    /// </summary>
    public interface KifuNode : Node<Starbeamable, KyokumenWrapper>
    {

        #region プロパティー

        /// <summary>
        /// スコア
        /// </summary>
        float Score { get; }
        void AddScore(float offset);
        void SetScore(float score);

        /// <summary>
        /// 局面評価明細。Mutable なので、SkyConst には入れられない。
        /// </summary>
        KyHyokaSheet KyHyokaSheet_Mutable { get; }
        /// <summary>
        /// 枝専用。
        /// </summary>
        /// <param name="branchKyHyokaSheet"></param>
        void SetBranchKyHyokaSheet(KyHyokaSheet branchKyHyokaSheet);

        #endregion

        /// <summary>
        /// この局面データを、読み込めないようにします。（移行用）
        /// </summary>
        void Lock_Kyokumendata();


        //void PutAppdendNextNodes(
        //    Node<Starbeamable, KyokumenWrapper> hubNode
        //    );


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
        void PutTuginoitte_New(Node<Starbeamable, KyokumenWrapper> newNode);
        /// <summary>
        /// 既存の子要素を上書きします。
        /// </summary>
        /// <param name="existsNode"></param>
        void PutTuginoitte_Override(Node<Starbeamable, KyokumenWrapper> existsNode);
        bool HasTuginoitte(string sasiteStr);

        ///// <summary>
        ///// 王手がかかった局面は取り除きます。
        ///// </summary>
        //Maps_OneAndMulti<Finger, Starbeamable> SplitSasite_ByStar(Node<Starbeamable, KyokumenWrapper> hubNode, KwErrorHandler errH);

        string Json_NextNodes_MultiSky(
            string memo,
            string hint,
            int temezumi_yomiGenTeban_forLog,//読み進めている現在の手目済
            KwErrorHandler errH
            );

        bool IsLeaf { get; }


        ///// <summary>
        ///// この木の、全てのノードを、フォルダーとして作成します。
        ///// </summary>
        ///// <returns></returns>
        //void CreateAllFolders(string folderpath, int currentDeep, int limitDeep);

    }
}
