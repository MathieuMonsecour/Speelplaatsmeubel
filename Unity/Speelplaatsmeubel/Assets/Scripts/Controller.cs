﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using ArduinoSerialAPI;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
	public SimonSays game;

    SerialHelper helper;
    public GameObject display;
	public Text control_display;

	public GameObject connect;
	public GameObject disconnect;

	public GameObject homeButton;

    void Start()
    {	
        float width = Camera.main.orthographicSize * 2.0f * Screen.width / Screen.height;
        float height = width/Screen.width*Screen.height;
        
        float sizePercentage = 0.1f;
        float absoluteSize = width*sizePercentage;
        float maskSize = Screen.width*sizePercentage;        

        // Display
		float display_width = maskSize*1.5f;
		float display_height = maskSize/2.0f;
        display.GetComponent<RectTransform>().sizeDelta = new Vector2(display_width, display_height);
        display.transform.position = new Vector3(display_width/2.0f, Screen.height-display_height/2.0f, display.transform.position.z);

		// homeButton
		homeButton.GetComponent<RectTransform>().sizeDelta = new Vector2(display_width, display_height);
        homeButton.transform.position = new Vector3(display_width/2.0f, Screen.height-display_height*2f, homeButton.transform.position.z);

        // (dis)connect buttons
        connect.GetComponent<RectTransform>().sizeDelta = new Vector2(display_width, display_height);
        disconnect.GetComponent<RectTransform>().sizeDelta = new Vector2(display_width, display_height);
        connect.transform.position = new Vector3(display_width/2.0f, Screen.height-display_height*3.5f, homeButton.transform.position.z);
        disconnect.transform.position = new Vector3(display_width/2.0f, Screen.height-display_height*3.5f, homeButton.transform.position.z);

		try{
			if(Application.platform.ToString().ToLower().Contains("droid")){
				helper = SerialHelper.CreateInstance("");
			}
			else if(Application.platform.ToString().ToLower().Contains("windows")){
				helper = SerialHelper.CreateInstance("COM15"); 
			}
			helper.setTerminatorBasedStream("\n");
			// helper.setLengthBasedStream();

			helper.OnConnected +=  () => {
				Debug.Log("Connected ");
				control_display.text = "Connected";
			};

			helper.OnConnectionFailed += () => {
				//Debug.Log("Failed");
				control_display.text = "Connection failed";
			};

			helper.OnDataReceived += () => {
                String temp = helper.Read();
                Debug.Log(temp);
                game.delegateMessage(temp);
			};

			helper.Connect();
			
		}catch(Exception ex){
			control_display.text = ex.Message;
		}        
    }

    void OnGUI()
	{
		connect.SetActive(false);
		disconnect.SetActive(false);

		if(!helper.isConnected()){
		    connect.SetActive(true);
			game.setConnection(false);
        }
		else if(helper.isConnected()){
		    disconnect.SetActive(true);
			game.setConnection(true);
		}
	}

	public void openMainMenu(){
		SceneManager.LoadScene (sceneName:"MainMenu");
	}

	public void connectButton(){
		helper.Connect ();
	}
	public void disconnectButton(){
		helper.Disconnect ();
		control_display.text = "Disconnected";
	}
	void OnDestroy()
	{
		if(helper!=null)
		    helper.Disconnect ();
	}

    public void sendString(String s){
        helper.SendData(s);
    }
}
