using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Requests;
using Sfs2X.Logging;
using UnityEngine.UI;

public class SmartChat : MonoBehaviour 
{

	//----------------------------------------------------------
	// Setup variables
	//----------------------------------------------------------
	public string serverName;	// Use Unity Inspector to change this value
	public int serverPort;			// Use Unity Inspector to change this value
	
	public Text serverStatus;
	public Text debugMsg;
	public Text chatText;
	public Text nickText;
	public Text inputMsg;
	
	// Internal / private variables
	private SmartFox smartFox;
	private string zone = "tothemoon";
	private string username = "";
	private string password = "";
//	private string loginErrorMessage = "";
	private string serverConnectionStatusMessage = "";
	private bool isLoggedIn;
	private bool isJoining = false;
	
//	private string newMessage = "";
	private ArrayList messages = new ArrayList();
	// Locker to use for messages collection to ensure its cross-thread safety
	private System.Object messagesLocker = new System.Object();
	
	private Vector2 chatScrollPosition, roomScrollPosition, userScrollPosition;
	
	private int roomSelection = 0;
	private string [] roomStrings;
	
	//----------------------------------------------------------
	// Called when program starts
	//----------------------------------------------------------
	void Start() {
		// In a webplayer (or editor in webplayer mode) we need to setup security policy negotiation with the server first
		if (Application.isWebPlayer || Application.isEditor) {
			if (!Security.PrefetchSocketPolicy(serverName, serverPort, 500)) {
				Debug.LogError("Security Exception. Policy file loading failed!");
			}
		}	
		
		// Lets connect
		smartFox = new SmartFox(true);
		
		// Register callback delegate
		smartFox.AddEventListener(SFSEvent.CONNECTION, OnConnection);
		smartFox.AddEventListener(SFSEvent.CONNECTION_LOST, OnConnectionLost);
		smartFox.AddEventListener(SFSEvent.LOGIN, OnLogin);
		smartFox.AddEventListener(SFSEvent.LOGIN_ERROR, OnLoginError);
		smartFox.AddEventListener(SFSEvent.LOGOUT, OnLogout);
		smartFox.AddEventListener(SFSEvent.ROOM_JOIN, OnJoinRoom);
		smartFox.AddEventListener(SFSEvent.PUBLIC_MESSAGE, OnPublicMessage);
		
		smartFox.AddLogListener(LogLevel.DEBUG, OnDebugMessage);
		
		smartFox.Connect(serverName, serverPort);
		
		//		var ipaddress = Network.player.ipAddress;
		//		Debug.Log (ipaddress);
		
	}
	
	//----------------------------------------------------------
	// As Unity is not thread safe, we process the queued up callbacks every physics tick
	//----------------------------------------------------------
	void FixedUpdate() {
		smartFox.ProcessEvents();
		serverStatus.text = serverConnectionStatusMessage;
	}
	
	//----------------------------------------------------------
	// Handle connection response from server
	//----------------------------------------------------------
	public void OnConnection(BaseEvent evt) {
		bool success = (bool)evt.Params["success"];
		string error = (string)evt.Params["errorMessage"];
		
		Debug.Log("On Connection callback got: " + success + " (error : <" + error + ">)");
		
		if (success) {
			serverConnectionStatusMessage = "Connection successful!";
		} else {
			serverConnectionStatusMessage = "Can't connect to server!";
		}
	}
	
	
	public void OnConnectionLost(BaseEvent evt) {
		// Reset all internal states so we kick back to login screen
		Debug.Log("OnConnectionLost");
		isLoggedIn = false;
		isJoining = false;
		
		serverConnectionStatusMessage = "Connection lost; reason: " + (string)evt.Params["reason"];
	}
	
	public void OnLogin(BaseEvent evt) {
		// Make sure we got in and then populate the room list string array
		isLoggedIn = true;
		Debug.Log("Logged in successfully");
		ReadRoomListAndJoin();
	}
	
	public void OnLoginError(BaseEvent evt) {
		Debug.Log("Login error: "+(string)evt.Params["errorMessage"]);
	}
	
	void OnLogout(BaseEvent evt) {
		Debug.Log("OnLogout");
		isLoggedIn = false;
		isJoining = false;
	}
	
	void OnJoinRoom(BaseEvent evt) {
		Room room = (Room)evt.Params["room"];
		Debug.Log("Room " + room.Name + " joined successfully");
		
		lock (messagesLocker) {
			messages.Clear();
		}
		isJoining = false;
	}
	
	void OnPublicMessage(BaseEvent evt) 
	{
		string message = (string)evt.Params["message"];
		User sender = (User)evt.Params["sender"];
		
		// We use lock here to ensure cross-thread safety on the messages collection 
		lock (messagesLocker) 
		{
			messages.Add(string.Concat (sender.Name, " : ", message, "\n"));
		}
		
		chatText.text =""; // clear ChatMsg
		
		lock (messagesLocker) 
		{
			foreach (string imessage in messages)
			{
				chatText.text += imessage;
			}
		}
		
		nickText.text = "";
		foreach (User user in smartFox.LastJoinedRoom.UserList)
		{
			nickText.text += string.Concat (user.Name, "\n");
		}
		//	chatScrollPosition.y = Mathf.Infinity;
		//	Debug.Log("User " + sender.Name + " said: " + message);
	}
	
	
	public void OnDebugMessage(BaseEvent evt) 
	{
		string message = (string)evt.Params["message"];
		Debug.Log("[SFS DEBUG] " + message);
	}
	
	
	//----------------------------------------------------------
	// Private helper methods
	//----------------------------------------------------------
	
	private void ReadRoomListAndJoin() {
		// We use the list of rooms to make a selection grid as part of the GUI. So lets read the room list and put it into a string[]
		Debug.Log("Room list: ");
		
		List<Room> roomList = smartFox.RoomManager.GetRoomList();
		List<string> roomNames = new List<string>();
		foreach (Room room in roomList) {
			if (room.IsHidden || room.IsPasswordProtected) {
				continue;
			}	
			
			roomNames.Add(room.Name);
			Debug.Log("Room id: " + room.Id + " has name: " + room.Name);
			
		}
		
		roomStrings = roomNames.ToArray();
		
		if (smartFox.LastJoinedRoom==null && smartFox.RoomList.Count > 0) {
			JoinRoom(smartFox.RoomList[0].Name);
		}
	}
	
	void JoinRoom(string roomName) {
		if (isJoining) return;
		
		isJoining = true;
		Debug.Log("Joining room: "+roomName);
		
		// Need to leave current room, if we are joined one
		if (smartFox.LastJoinedRoom==null)
			smartFox.Send(new JoinRoomRequest(roomName));
		else	
			smartFox.Send(new JoinRoomRequest(roomName, "", smartFox.LastJoinedRoom.Id));
	}
	
	
	//----------------------------------------------------------
	// Unity engine callbacks
	//----------------------------------------------------------
	
	void DrawMessagePanel(string _msg)
	{
		debugMsg.text = _msg;
	}
	
	
	void Update() {
		if (smartFox == null) return;
		
		// Determine which state we are in and show the GUI accordingly
		if (!smartFox.IsConnected) {
			DrawMessagePanel("Not connected");
		}
		else if (!isLoggedIn) {
			//DrawLoginGUI();
			smartFox.Send(new LoginRequest(username, password, zone));
		}
		else if (isJoining) {
			DrawMessagePanel("Joining...");
		}
		else if (smartFox.LastJoinedRoom != null) {
			DrawLobby();
		}
	}
	
	
	void DrawLobby() 
	{
		// We use lock here to ensure cross-thread safety on the messages collection 
		//		chatText.text =""; // clear ChatMsg
		
		//		lock (messagesLocker) {
		//			foreach (string message in messages) {
		//
		//				chatText.text += message;
		//
		//			}
		//		}
		
		
		if (roomStrings[roomSelection] != smartFox.LastJoinedRoom.Name) {
			JoinRoom(roomStrings[roomSelection]); 
			return;
		}
		
		//		debugMsg.text += smartFox.LastJoinedRoom.Name;
		
		//		nickText.text = "";
		//		foreach (User user in smartFox.LastJoinedRoom.UserList) {
		////			GUILayout.Label(user.Name);
		//			nickText.text += string.Concat (user.Name, "\n");
		//		}
		
		//		debugMsg.text += smartFox.MySelf.Name;
		
		//		if (GUILayout.Button("Logout")) {
		//			smartFox.Send( new LogoutRequest() );
		//		}
	}
	
	void OnDisable()
	{
		//smartFox.Send(new LogoutRequest());
	}
	
	void OnEnable()
	{
		if (smartFox == null) return;
		
		if (!smartFox.IsConnected) {
			DrawMessagePanel("Not connected");
		}
		else if (!isLoggedIn) {
			smartFox.Send(new LoginRequest(username, password, zone));
		}
	}
	
	public void SendPublicMessage()
	{
		smartFox.Send( new PublicMessageRequest(inputMsg.text) );
		inputMsg.text = "";
	}
	
}