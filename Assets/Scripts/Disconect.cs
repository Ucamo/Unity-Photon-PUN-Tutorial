using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Disconect : MonoBehaviour
{
    public void DisconectButton(){
        PhotonNetwork.LeaveRoom();
    }
}
