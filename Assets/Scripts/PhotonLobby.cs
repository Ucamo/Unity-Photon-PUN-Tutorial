using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
   public static PhotonLobby lobby;
   RoomInfo[] rooms;
   public GameObject battleButton;
   public GameObject cancelButton;
    public Text txtInfo;

     public Text txtNumJugadores;

   private void Awake(){
       lobby=this;//Create the singleton, lives withing the scene
   }

   void Start(){
       PhotonNetwork.ConnectUsingSettings(); // connects to master photon server
   }

   void Update(){
       int numJugadores=PhotonNetwork.CountOfPlayers;
       txtNumJugadores.text="Number of Players: "+numJugadores.ToString()+"/20";
   }

   public override void OnConnectedToMaster(){
       string mensajeConnectedMaster="Connected to master";
       Debug.Log(mensajeConnectedMaster);
       txtInfo.text=mensajeConnectedMaster;       
       PhotonNetwork.AutomaticallySyncScene=true;
       battleButton.SetActive(true);
   }

   public void OnBattleButtonClicked(){
       Debug.Log("Battle button was click");
       battleButton.SetActive(false);
       cancelButton.SetActive(true);
       PhotonNetwork.JoinRandomRoom();
   }

   public override void OnJoinRandomFailed(short returnCode, string message){
       string mes="Failed to join the room";
       Debug.Log(mes);
       txtInfo.text=mes; 
       CreateRoom();
   }

   void CreateRoom(){
       string mes="Trying to create a room";
       Debug.Log(mes);
       txtInfo.text=mes; 
       int randomRoomName = Random.Range(0,10000);
       RoomOptions roomOps = new RoomOptions(){IsVisible=true,IsOpen=true,MaxPlayers=20};
       PhotonNetwork.CreateRoom("Room"+randomRoomName,roomOps);
   }

   public override void OnCreateRoomFailed(short returnCode, string message){
       string mes="Create Room Failed";
       Debug.Log(mes);
       txtInfo.text=mes; 
       CreateRoom();
   }

   public void OnCancelButtonClick(){
       cancelButton.SetActive(false);
       battleButton.SetActive(true);
       PhotonNetwork.LeaveRoom();
   }

}
