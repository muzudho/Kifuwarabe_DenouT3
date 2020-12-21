namespace Grayscale.Kifuwarakaku.UseCases.Features
{
    public interface Receiver_ForAims : Receiver
    {

        /// <summary>
        /// オーナー・サーバー。
        /// </summary>
        AimsServerBase Owner_AimsServer { get; }
        void SetOwner_AimsServer(AimsServerBase owner_AimsServer);

    }
}
