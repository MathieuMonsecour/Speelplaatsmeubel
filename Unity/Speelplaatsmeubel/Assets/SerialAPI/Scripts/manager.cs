using System;
using System.IO.Ports;
using UnityEngine;
using UnityEngine.UI;
using ArduinoSerialAPI;
using UnityEngine.SceneManagement;

public class manager : MonoBehaviour
{ 
    SerialHelper helper;

	public Text text;

	public GameObject connect;
	public GameObject disconnect;
	public GameObject sendLetterA;

    void Start()
    {	
		string[] ports = SerialPort.GetPortNames();

        // Display each port name to the console.
        foreach(string port in ports)
        {
			Debug.Log(port);
		try{
			helper = SerialHelper.CreateInstance(port);
			helper.setTerminatorBasedStream("\n");
			// helper.setLengthBasedStream();

			helper.OnConnected +=  () => {
				Debug.Log("Connected");
				text.text = "Connected";
			};

			helper.OnConnectionFailed += () => {
				//Debug.Log("Failed");
				//text.text = "failed";

			};

			helper.OnDataReceived += () => {
				text.text = helper.Read();
			};

			helper.Connect();
			
		}catch(Exception ex){
			text.text = ex.Message;
		}
		}
        
    }

    void OnGUI()
	{
		connect.SetActive(false);
		disconnect.SetActive(false);
		sendLetterA.SetActive(false);

		if(helper!=null)
			helper.DrawGUI();
		else 
		    return;

		if(!helper.isConnected()){
		connect.SetActive(true);
		if(GUI.Button(new Rect(0, Screen.height - Screen.height/10, Screen.width/2, Screen.height/10), "Connect"))
		{
				helper.Connect (); 
		}}

		if(helper.isConnected()){
		disconnect.SetActive(true);
		if(GUI.Button(new Rect(0, Screen.height - Screen.height/10, Screen.width/2, Screen.height/10), "Disconnect"))
		{
			helper.Disconnect ();
		}}

		if(helper.isConnected()){
		sendLetterA.SetActive(true);
		if(GUI.Button(new Rect(Screen.width/2, Screen.height - Screen.height/10, Screen.width/2, Screen.height/10), "Send 'A' text"))
		{
			helper.SendData("A");
		}}
	}

	public void connectButton(){
		helper.Connect ();
	}
	public void disconnectButton(){
		helper.Disconnect ();
	}
	public void sendAButton(){
		helper.SendData("A");
	}

	void OnDestroy()
	{
		if(helper!=null)
		    helper.Disconnect ();
	}
}
