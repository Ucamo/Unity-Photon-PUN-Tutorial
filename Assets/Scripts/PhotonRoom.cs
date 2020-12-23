using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PhotonRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
   //Room info
   public static PhotonRoom room;

   public int currentScene;
   public int multiplayScene;

    public Text txtInfo;

    int numConectados;

   private void Awake(){
       //set up singleton
       if(PhotonRoom.room == null){
           PhotonRoom.room=this;
       }
       else{
           if(PhotonRoom.room!=this){
               Destroy(PhotonRoom.room.gameObject);
               PhotonRoom.room=this;
           }
       }
       DontDestroyOnLoad(this.gameObject);
   }

   public override void OnEnable(){
       //subscribe to functions
       base.OnEnable();
       PhotonNetwork.AddCallbackTarget(this);
       SceneManager.sceneLoaded += OnSceneFinishedLoading;
   }

      public override void OnDisable(){
       //subscribe to functions
       base.OnDisable();
       PhotonNetwork.RemoveCallbackTarget(this);
       SceneManager.sceneLoaded -= OnSceneFinishedLoading;
   }

   public override void OnJoinedRoom(){
       //sets player data when we join the room
       base.OnJoinedRoom();
       Debug.Log("We are in a room");
       numConectados= PhotonNetwork.PlayerList.Length;
       string mensajeJoinedRoom="Connected to a Room.  Room Name: "+PhotonNetwork.CurrentRoom.Name+ " Number of Players connected: "+numConectados;
       Debug.Log(mensajeJoinedRoom);
       txtInfo.text=string.Format(mensajeJoinedRoom, PhotonNetwork.CurrentRoom.Name,numConectados);
       Debug.Log(string.Format(mensajeJoinedRoom, PhotonNetwork.CurrentRoom.Name,numConectados));

       StartGame();
   }


   void StartGame(){
       //loads the multiplayer scene for all players
       string mensajeMatchFound="Match found, starting a game (VS)";
       txtInfo.text=mensajeMatchFound;
       Debug.Log(mensajeMatchFound);
       PhotonNetwork.LoadLevel(multiplayScene);
    }
   

   void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode){
       //called when multiplayer scene is loaded
       currentScene= scene.buildIndex;
       if(currentScene==multiplayScene){
           CreatePlayer();
       }
   }

   private void CreatePlayer(){
       //creates player network controller but not player character
       PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs","PhotonNetworkPlayer"), transform.position,Quaternion.identity,0);
   }
}

