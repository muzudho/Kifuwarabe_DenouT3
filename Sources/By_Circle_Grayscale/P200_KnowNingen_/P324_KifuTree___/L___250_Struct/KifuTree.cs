using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P218_Starlight__.L___500_Struct;
using Grayscale.P226_Tree_______.L___500_Struct;
using Grayscale.P247_KyokumenWra.L500____Struct;
using Grayscale.P323_Sennitite__.L___500_Struct;


namespace Grayscale.P324_KifuTree___.L___250_Struct
{
    public interface KifuTree : Tree<Starbeamable, KyokumenWrapper>
    {
        /// <summary>
        /// 千日手カウンター。
        /// </summary>
        /// <returns></returns>
        SennititeCounter GetSennititeCounter();

        //void AssertPside(Node<Starbeamable, KyokumenWrapper> node, string hint, KwErrorHandler errH);
        /// <summary>
        /// これから追加する予定のノードの先後を診断します。
        /// </summary>
        /// <param name="node"></param>
        void AssertChildPside(Playerside parentPside, Playerside childPside);
        //Playerside CountPside(Node<Starbeamable, KyokumenWrapper> node, KwErrorHandler errH);


        
        ///// <summary>
        ///// ************************************************************************************************************************
        ///// [ここから採譜]機能
        ///// ************************************************************************************************************************
        ///// </summary>
        //void SetStartpos_KokokaraSaifu(Playerside pside, KwErrorHandler errH);

        ///// <summary>
        ///// この木の、全てのノードを、フォルダーとして作成します。
        ///// </summary>
        ///// <returns></returns>
        //void CreateAllFolders(string folderpath, int limitDeep);

    }
}
