using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TwitchLib.Client.Models;
using TwitchLib.Unity;

public class ManagerConnections : MonoBehaviour
{
	private Client _client;
    public static ManagerConnections Singleton;
    const float TimeReset = 2f;
    public float currentTime = 0;
    string MessageToSend = "";
    [SerializeField]
    bool ConcatMessage =  true;
    [SerializeField]
    bool StartConnect =  true;
    string ConcatSeparator = " ";
	public UnityEvent<string> OnConnect;
	public UnityEvent<string> OnError;
    private void Start() {
        DontDestroyOnLoad(this.gameObject);
        Singleton = this;
        if (StartConnect) connect();
    }


    [ContextMenu("Connect")]
    // Start is called before the first frame update
    public void connect()
    {
        // To keep the Unity application active in the background, you can enable "Run In Background" in the player settings:
		// Unity Editor --> Edit --> Project Settings --> Player --> Resolution and Presentation --> Resolution --> Run In Background
		// This option seems to be enabled by default in more recent versions of Unity. An aditional, less recommended option is to set it in code:
		// Application.runInBackground = true;

		//Create Credentials instance
		ConnectionCredentials credentials = new ConnectionCredentials(Secrets.UserName, Secrets.AccessToken);
		// ConnectionCredentials credentials = new ConnectionCredentials("MIvenBot", Secrets.bot_access_token);

		// Create new instance of Chat Client
		_client = new Client();

		// Initialize the client with the credentials instance, and setting a default channel to connect to.
		_client.Initialize(credentials, Secrets.Channel);

		// Bind callbacks to events
		_client.OnConnected += OnConnected;
		_client.OnJoinedChannel += OnJoinedChannel;
		_client.OnMessageReceived += OnMessageReceived;
		_client.OnChatCommandReceived += OnChatCommandReceived;
		_client.OnConnectionError += OnErrorConnect;
		// Connect
		_client.Connect();
    }
    private void OnConnected(object sender, TwitchLib.Client.Events.OnConnectedArgs e)
	{
		Debug.Log($"The bot {e.BotUsername} succesfully connected to Twitch.");
		OnConnect.Invoke($"{e.BotUsername} succesfully connected to Twitch.");
		if (!string.IsNullOrWhiteSpace(e.AutoJoinChannel))
			Debug.Log($"The bot will now attempt to automatically join the channel provided when the Initialize method was called: {e.AutoJoinChannel}");
	}

	private void OnJoinedChannel(object sender, TwitchLib.Client.Events.OnJoinedChannelArgs e)
	{
		Debug.Log($"The bot {e.BotUsername} just joined the channel: {e.Channel}");
		// _client.SendMessage(e.Channel, "Miv Control Twitch Connect...");
	}
	private void OnErrorConnect(object sender, TwitchLib.Client.Events.OnConnectionErrorArgs e)
	{
		OnError.Invoke($"{e.BotUsername} Error connected to Twitch.");
	}
	private void OnMessageReceived(object sender, TwitchLib.Client.Events.OnMessageReceivedArgs e)
	{
		Debug.Log($"Message received from {e.ChatMessage.Username}: {e.ChatMessage.Message}");
	}

	private void OnChatCommandReceived(object sender, TwitchLib.Client.Events.OnChatCommandReceivedArgs e)
	{
		switch (e.Command.CommandText)
		{
			case "hello":
				_client.SendMessage(e.Command.ChatMessage.Channel, $"Hello {e.Command.ChatMessage.DisplayName}!");
				break;
			case "about":
				_client.SendMessage(e.Command.ChatMessage.Channel, "I am a Twitch bot running on TwitchLib!");
				break;
			default:
				_client.SendMessage(e.Command.ChatMessage.Channel, $"Unknown chat command: {e.Command.CommandIdentifier}{e.Command.CommandText}");
				break;
		}
	}
    public void SendMessageChat(string data){

		Debug.Log("Sending "+  data);
        if (ConcatMessage) {
            MessageToSend += ConcatSeparator + data;
        }
        if (currentTime > 0) return;

        if (ConcatMessage){
            _client.SendMessage(Secrets.Channel, MessageToSend);
            MessageToSend = "";
        }else{
            _client.SendMessage(Secrets.Channel, data);
        }

        currentTime = TimeReset;
        
    }
	private void Update()
	{
		// Don't call the client send message on every Update,
		// // this is sample on how to call the client,
		// // not an example on how to code.
		// if (Input.GetKeyDown(KeyCode.Space))
		// {
		// 	SendMessageChat( "join");
		// }
		// if (Input.GetKeyDown(KeyCode.W))
		// {
		// 	SendMessageChat("w");
		// }
		// if (Input.GetKeyDown(KeyCode.S))
		// {
		// 	SendMessageChat("s");
		// }
		// if (Input.GetKeyDown(KeyCode.D))
		// {
        //     SendMessageChat("d");
		// }
		// if (Input.GetKeyDown(KeyCode.A))
		// {
        //     SendMessageChat("a");
		// }
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        // else if (MessageToSend != "")
        // {
        //     SendMessageChat("");
        // }
	}
}
