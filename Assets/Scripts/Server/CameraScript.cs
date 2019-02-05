using Assets.Scripts.Server;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private void OnApplicationQuit()
    { // disconnetti quando esci
        DataSender.SendGoodBye();
        ClientTcp.Disconnect();
    }
}
