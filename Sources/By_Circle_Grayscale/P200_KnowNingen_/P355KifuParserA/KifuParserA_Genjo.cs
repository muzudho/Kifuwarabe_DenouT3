using Grayscale.P276SeizaStartp.L500Struct;

namespace Grayscale.P355_KifuParserA.I500Parser
{
    public interface KifuParserA_Genjo
    {

        /// <summary>
        /// パーサーを止めるフラグ。正常時を明示。
        /// </summary>
        void ToBreak_Normal();
        /// <summary>
        /// パーサーを止めるフラグ。異常時を明示。
        /// </summary>
        void ToBreak_Abnormal();
        /// <summary>
        /// パーサーを止めるか。
        /// </summary>
        /// <returns></returns>
        bool IsBreak();

        string InputLine { get; set; }

        /// <summary>
        /// 平手初期局面ではない指定があったときに使用。
        /// </summary>
        StartposImporter StartposImporter_OrNull { get; set; }

    }
}
