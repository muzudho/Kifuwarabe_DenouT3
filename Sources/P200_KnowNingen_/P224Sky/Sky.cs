using Grayscale.P211WordShogi.L500Word;
using Grayscale.P218Starlight.I500Struct;
using Grayscale.P223TedokuHisto.I250Struct;
using Grayscale.P224Sky.L500Struct;
using System.Runtime.CompilerServices;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //フィンガー番号

namespace Grayscale.P224Sky.I500Struct
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

        Starlight StarlightIndexOf(
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
