using UnityEngine;

namespace Server
{
    public class CameraScript : MonoBehaviour
    {
        private void OnApplicationQuit()
        { // disconnetti quando esci
            DataSender.SendGoodBye();
            ClientTcp.Disconnect();
        }
    }
}
