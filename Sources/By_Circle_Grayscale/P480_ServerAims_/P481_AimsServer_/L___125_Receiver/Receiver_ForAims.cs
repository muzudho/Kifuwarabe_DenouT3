using Grayscale.P461_Server_____.L___125_Receiver;
using Grayscale.P481_AimsServer_.L___070_ServerBase;

namespace Grayscale.P481_AimsServer_.L___125_Receiver
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
