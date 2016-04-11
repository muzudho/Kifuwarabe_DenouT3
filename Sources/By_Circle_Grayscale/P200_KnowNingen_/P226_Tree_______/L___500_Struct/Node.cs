using Grayscale.P226_Tree_______.L500____Struct;
using System.Collections.Generic;

namespace Grayscale.P226_Tree_______.L___500_Struct
{

    /// <summary>
    /// ツリー構造です。
    /// 
    /// 局面を入れるのに利用します。
    /// 根ノードに平手局面、最初の子ノードに１手目の局面、を入れるような使い方を想定しています。
    /// </summary>
    public interface Node<T1,T2>
    {

        /// <summary>
        /// このノードのキー。インスタンスを作った後では変更できません。
        /// </summary>
        T1 Key { get; }

        /// <summary>
        /// このノードの値。
        /// </summary>
        T2 Value { get; set; }

        /// <summary>
        /// 親ノード。変更可能。
        /// </summary>
        Node<T1, T2> GetParentNode();
        void SetParentNode(Node<T1, T2> parent);

        /// <summary>
        /// ルート・ノードなら真。
        /// </summary>
        /// <returns></returns>
        bool IsRoot();

        bool HasChildNode(string key);
        /// <summary>
        /// sfenがキーなのは暫定。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Node<T1, T2> GetChildNode(string key);

        int Count_ChildNodes{get;}
        void Clear_ChildNodes();
        bool ContainsKey_ChildNodes(string key);
        void PutAdd_ChildNode(string key, Node<T1, T2> newNode);
        void PutSet_ChildNodes(Dictionary< string, Node<T1, T2>> newNextNodes);

        void Foreach_ChildNodes(NodeImpl<T1, T2>.DELEGATE_NextNodes d);

        /// <summary>
        /// 子ノードの削除。
        /// 
        /// キー：SFEN ※この仕様は暫定
        /// 値：ノード
        /// </summary>
        /// <param name="key"></param>
        bool RemoveChild(string key_sfen);

        /// <summary>
        /// この木の、全てのノード数を数えます。
        /// </summary>
        /// <returns></returns>
        int CountAllNodes();

    }
}
