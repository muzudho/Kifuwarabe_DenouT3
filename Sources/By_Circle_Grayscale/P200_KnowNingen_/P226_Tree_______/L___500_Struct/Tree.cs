
namespace Grayscale.P226_Tree_______.L___500_Struct
{

    /// <summary>
    /// 記録係が利用します。
    /// </summary>
    /// <param name="temezumi">手目済</param>
    /// <param name="nodeRef">ノードのかたまりのまま。</param>
    /// <param name="toBreak"></param>
    public delegate void DELEGATE_Foreach<
        T1,//ノードのキー
        T2//ノードの値
        >(int temezumi, T2 sky, Node<T1, T2> nodeRef, ref bool toBreak);


    public interface Tree<
        T1,//ノードのキー
        T2//ノードの値
        >
    {
        /// <summary>
        /// ツリー構造になっている本譜の葉ノード。
        /// 根を「startpos」等の初期局面コマンドとし、次の節からは棋譜の符号「2g2f」等が連なっている。
        /// </summary>
        Node<T1, T2> CurNode { get; }
        void SetCurNode(Node<T1, T2> node);

        Node<T1, T2> NodeAt(int temezumi1);

        /// <summary>
        /// 使い方自由。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void SetProperty(string key, object value);

        
        /// <summary>
        /// ************************************************************************************************************************
        /// 棋譜を空っぽにします。
        /// ************************************************************************************************************************
        /// 
        /// ルートは残します。
        /// 
        /// </summary>
        void Clear();


        
        /// <summary>
        /// ************************************************************************************************************************
        /// 現在の要素を切り取って返します。なければヌル。
        /// ************************************************************************************************************************
        /// 
        /// カレントは、１手前に戻ります。
        /// 
        /// </summary>
        /// <returns>ルートしかないリストの場合、ヌルを返します。</returns>
        Node<T1, T2> PopCurrentNode();


        Node<T1, T2> GetRoot();

        
        /// <summary>
        /// 使い方自由。
        /// </summary>
        object GetProperty(string key);


        
        /// <summary>
        /// 本譜だけ。
        /// </summary>
        /// <param name="endNode"></param>
        /// <param name="delegate_Foreach"></param>
        void ForeachHonpu(Node<T1, T2> endNode, DELEGATE_Foreach<T1, T2> delegate_Foreach);

                
        /// <summary>
        /// 全て。
        /// </summary>
        /// <param name="endNode"></param>
        /// <param name="delegate_Foreach"></param>
        void ForeachZenpuku(Node<T1, T2> startNode, DELEGATE_Foreach<T1, T2> delegate_Foreach);

        /// <summary>
        /// この木の、全てのノード数を数えます。
        /// </summary>
        /// <returns></returns>
        int CountAllNodes();
    }
}
