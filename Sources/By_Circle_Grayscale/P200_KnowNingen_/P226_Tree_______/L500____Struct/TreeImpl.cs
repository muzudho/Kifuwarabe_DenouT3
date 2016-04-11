using Grayscale.P226_Tree_______.L___500_Struct;
using System;
using System.Collections.Generic;

namespace Grayscale.P226_Tree_______.L500____Struct
{

    /// <summary>
    /// 棋譜。
    /// </summary>
    public class TreeImpl<
        T1,//ノードのキー
        T2//ノードの値
        > : Tree<
        T1,//ノードのキー
        T2//ノードの値
        >
    {
        #region プロパティ類

        /// <summary>
        /// ツリー構造になっている本譜の葉ノード。
        /// 根を「startpos」等の初期局面コマンドとし、次の節からは棋譜の符号「2g2f」等が連なっている。
        /// </summary>
        public Node<T1, T2> CurNode { get { return this.curNode; } }
        public void SetCurNode(Node<T1, T2> node) { this.curNode = node; }
        private Node<T1, T2> curNode;


        ///// <summary>
        ///// ------------------------------------------------------------------------------------------------------------------------
        ///// 一手目の手番
        ///// ------------------------------------------------------------------------------------------------------------------------
        ///// 
        /////     １手目を指す手番の、先後を指定してください。
        /////
        /////     ルート局面には　平手初期局面　が入っていると想定していますので、
        /////     ２個目のノードが　一手目の局面と想定しています。
        ///// 
        /////     TODO:プロパティに「pside=black」のように「文字列,object」で入れたい。
        ///// 
        ///// </summary>
        //public Object Pside_FirstSky{get;set;}

        /// <summary>
        /// 使い方自由。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetProperty(string key,object value)
        {
            if (this.properties.ContainsKey(key))
            {
                this.properties[key] = value;
            }
            else
            {
                this.properties.Add(key,value);
            }
        }

        /// <summary>
        /// 使い方自由。
        /// </summary>
        public object GetProperty(string key)
        {
            object result;

            if (this.properties.ContainsKey(key))
            {
                result = this.properties[key];
            }
            else
            {
                result = "Unknown kifu property [" + key + "]";
            }

            return result;
        }
        private Dictionary<string,object> properties;

        #endregion





        public TreeImpl(
            Node<T1, T2> root
            )
        {
            this.properties = new Dictionary<string,object>();
            this.SetCurNode( root);
        }



        #region カレント移動系

        public void Move_Previous()
        {
            this.SetCurNode( this.CurNode.GetParentNode());
        }

        #endregion


        /// <summary>
        /// ************************************************************************************************************************
        /// 現在の要素を切り取って返します。なければヌル。
        /// ************************************************************************************************************************
        /// 
        /// カレントは、１手前に戻ります。
        /// 
        /// </summary>
        /// <returns>ルートしかないリストの場合、ヌルを返します。</returns>
        public Node<T1, T2> PopCurrentNode()
        {
            Node<T1, T2> deleteeElement = null;

            if (this.CurNode.IsRoot())
            {
                // やってはいけない操作は、例外を返すようにします。
                string message = "ルート局面を削除しようとしました。";
                throw new Exception(message);
            }

            //>>>>> ラスト要素がルートでなかったら

            // 一手前の要素（必ずあるはずです）
            deleteeElement = this.CurNode;
            // 残されたリストの最後の要素の、次リンクを切ります。
            deleteeElement.GetParentNode().Clear_ChildNodes();

            // カレントを、１つ前の要素に替えます。
            this.Move_Previous();//this.CurNode = deleteeElement.GetParentNode();

            return deleteeElement;
        }



        /// <summary>
        /// 本譜だけ。
        /// </summary>
        /// <param name="endNode">葉側のノード。</param>
        /// <param name="delegate_Foreach"></param>
        public void ForeachHonpu(Node<T1, T2> endNode, DELEGATE_Foreach<T1, T2> delegate_Foreach)
        {
            bool toBreak = false;

            // 本譜（ノードのリスト）
            List<Node<T1, T2>> honpu = new List<Node<T1, T2>>();

            //
            // ツリー型なので、１本のリストに変換するために工夫します。
            //
            // カレントからルートまで遡り、それを逆順にすれば、本譜になります。
            //

            while (null!=endNode)//ルートを含むところまで遡ります。
            {
                honpu.Add(endNode); // リスト作成

                endNode = endNode.GetParentNode();
            }
            honpu.Reverse();

            //
            // 手済みを数えます。
            //
            int temezumi = 0;//初期局面が[0]

            foreach (Node<T1, T2> item in honpu)//正順になっています。
            {
                T2 sky = item.Value;

                delegate_Foreach(temezumi, sky, item, ref toBreak);
                if (toBreak)
                {
                    break;
                }

                temezumi++;
            }
        }
        /// <summary>
        /// 全て。
        /// </summary>
        /// <param name="endNode"></param>
        /// <param name="delegate_Foreach"></param>
        public void ForeachZenpuku(Node<T1, T2> startNode, DELEGATE_Foreach<T1, T2> delegate_Foreach)
        {

            List<Node<T1, T2>> list8 = new List<Node<T1, T2>>();

            //
            // ツリー型なので、１本のリストに変換するために工夫します。
            //
            // カレントからルートまで遡り、それを逆順にすれば、本譜になります。
            //

            int temezumi = 0;//※指定局面が0。
            bool toFinish_ZenpukuTansaku = false;

            this.Recursive_Node_NextNode(temezumi, startNode, delegate_Foreach, ref toFinish_ZenpukuTansaku);
            if (toFinish_ZenpukuTansaku)
            {
                goto gt_EndMetdhod;
            }

        gt_EndMetdhod:
            ;
        }

        private void Recursive_Node_NextNode(int temezumi1, Node<T1, T2> node1, DELEGATE_Foreach<T1, T2> delegate_Foreach1, ref bool toFinish_ZenpukuTansaku)
        {
            bool toBreak1 = false;

            // このノードを、まず報告。
            delegate_Foreach1(temezumi1, node1.Value, node1, ref toBreak1);
            if (toBreak1)
            {
                //この全幅探索を終わらせる指示が出ていた場合
                toFinish_ZenpukuTansaku = true;
                goto gt_EndMetdhod;
            }

            // 次のノード
            node1.Foreach_ChildNodes((string key2, Node<T1, T2> node2, ref bool toBreak2) =>
            {
                bool toFinish_ZenpukuTansaku2 = false;
                this.Recursive_Node_NextNode(temezumi1 + 1, node2, delegate_Foreach1, ref toFinish_ZenpukuTansaku2);
                if (toFinish_ZenpukuTansaku2)//この全幅探索を終わらせる指示が出ていた場合
                {
                    toBreak2 = true;
                    goto gt_EndBlock;
                }

            gt_EndBlock:
                ;
            });

        gt_EndMetdhod:
            ;
        }









        #region クリアー系

        /// <summary>
        /// ************************************************************************************************************************
        /// 棋譜を空っぽにします。
        /// ************************************************************************************************************************
        /// 
        /// ルートは残します。
        /// 
        /// </summary>
        public virtual void Clear()
        {
            // ルートまで遡ります。
            while (!this.CurNode.IsRoot())
            {
                this.Move_Previous();
            }

            // ルートの次の手を全クリアーします。
            this.CurNode.Clear_ChildNodes();
        }

        #endregion






        #region ランダムアクセッサ

        public Node<T1, T2> GetRoot()
        {
            return (Node<T1, T2>)this.NodeAt(0);
        }

        public Node<T1, T2> NodeAt(int sitei_temezumi)
        {
            Node<T1, T2> found = null;

            this.ForeachHonpu(this.CurNode, (int temezumi2, T2 sky, Node<T1, T2> node, ref bool toBreak) =>
            {
                if (sitei_temezumi == temezumi2) //新Verは 0 にも対応。
                {
                    found = node;
                    toBreak = true;
                }

            });

            if (null == found)
            {
                string message = "[" + sitei_temezumi + "]の局面ノード6はヌルでした。";
                //LarabeLogger.GetInstance().WriteLineError(LarabeLoggerList.ERROR, message);
                throw new Exception(message);
            }

            return found;
        }

        #endregion


        /// <summary>
        /// この木の、全てのノード数を数えます。
        /// </summary>
        /// <returns></returns>
        public int CountAllNodes()
        {
            int result = 0;

            if(null!=this.GetRoot())
            {
                result = this.GetRoot().CountAllNodes();
            }

            return result;
        }

    }


}
