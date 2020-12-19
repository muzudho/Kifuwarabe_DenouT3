﻿using Grayscale.P211WordShogi.L500Word;
using Grayscale.P218Starlight.I500Struct;
using Grayscale.P226Tree.I500Struct;
using Grayscale.P226Tree.L500Struct;
using Grayscale.P247KyokumenWra.L500Struct;
using Grayscale.P324KifuTree.I250Struct;
using Grayscale.P323Sennitite.L500Struct;
using Grayscale.P323Sennitite.I500Struct;

#if DEBUG
using System.Diagnostics;
#endif

namespace Grayscale.P324KifuTree.L250Struct
{
    public class KifuTreeImpl : TreeImpl<IMove, KyokumenWrapper>, KifuTree
    {
        /// <summary>
        /// 千日手カウンター。
        /// </summary>
        /// <returns></returns>
        public SennititeCounter GetSennititeCounter()
        {
            return this.sennititeCounter;
        }
        private SennititeCounter sennititeCounter;

        public KifuTreeImpl(Node<IMove, KyokumenWrapper> root)
            : base(root)
        {
            //----------------------------------------
            // 千日手カウンター
            //----------------------------------------
            this.sennititeCounter = new SennititeCounterImpl();
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// 棋譜を空っぽにします。
        /// ************************************************************************************************************************
        /// 
        /// ルートは残します。
        /// 
        /// </summary>
        public override void Clear()
        {
            base.Clear();

            //----------------------------------------
            // 千日手カウンター
            //----------------------------------------
            this.sennititeCounter.Clear();
        }


        /// <summary>
        /// これから追加する予定のノードの先後を診断します。
        /// </summary>
        /// <param name="node"></param>
        public void AssertChildPside(Playerside parentPside, Playerside childPside)
        {
#if DEBUG
            Debug.Assert(
                (parentPside==Playerside.P1 && childPside==Playerside.P2)
                ||
                (parentPside==Playerside.P2 && childPside==Playerside.P1)
                , "親子の先後に、異順序がありました。現手番[" + parentPside + "]　<> 次手番[" + childPside + "]");
#endif
        }


        ///// <summary>
        ///// この木の、全てのノードを、フォルダーとして作成します。
        ///// </summary>
        ///// <returns></returns>
        //public void CreateAllFolders(string folderpath, int limitDeep)
        //{
        //    int currentDeep = 0;

        //    if (null != this.GetRoot() && currentDeep <= limitDeep)
        //    {
        //        ((KifuNode)this.GetRoot()).CreateAllFolders(folderpath, currentDeep+1, limitDeep);
        //    }
        //}

    }


}