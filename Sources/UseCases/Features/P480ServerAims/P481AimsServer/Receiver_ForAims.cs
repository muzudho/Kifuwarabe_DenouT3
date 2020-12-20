using Grayscale.P461Server.I125Receiver;
using Grayscale.P481AimsServer.I070ServerBase;

namespace Grayscale.P481AimsServer.I125Receiver
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
