using System.Runtime.CompilerServices;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //フィンガー番号

namespace Grayscale.Kifuwarakaku.Entities.Features
{
    public interface Sky
    {
        /// <summary>
        /// 手得ヒストリー。
        /// </summary>
        TedokuHistory TedokuHistory { get; }

        /// <summary>
        /// これから指す側。
        /// </summary>
        Playerside KaisiPside { get; }

        /// <summary>
        /// 何手目済みか。初期局面を 0 とする。
        /// </summary>
        int Temezumi { get; }

        IMoveHalf StarlightIndexOf(
            Finger finger,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0
        );

        void Foreach_Starlights(SkyBuffer.DELEGATE_Sky_Foreach delegate_Sky_Foreach);

        int Count
        {
            get;
        }

        Sky Clone();

        Fingers Fingers_All();
    }
}
