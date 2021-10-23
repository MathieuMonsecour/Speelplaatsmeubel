using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using ArduinoSerialAPI;

public class Controller : MonoBehaviour
{
    public YesNo YesNo;

    SerialHelper helper;
    public GameObject display;
	public Text display_text;

	public GameObject connect;
	public GameObject disconnect;

    void Start()
    {	
        float width = Camera.main.orthographicSize * 2.0f * Screen.width / Screen.height;
        float height = width/Screen.width*Screen.height;
        
        float sizePercentage = 0.1f;
        float absoluteSize = width*sizePercentage;
        float maskSize = Screen.width*sizePercentage;        

        // Display
        display.GetComponent<RectTransform>().sizeDelta = new Vector2(maskSize*5, maskSize*2);
        display.transform.position = new Vector3(Screen.width/2f, Screen.height/2f, display.transform.position.z);

        // buttons
        connect.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, maskSize);
        disconnect.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, maskSize);
        connect.transform.position = new Vector3(Screen.width/2, maskSize/2, disconnect.transform.position.z);
        disconnect.transform.position = new Vector3(Screen.width/2, maskSize/2, disconnect.transform.position.z);

		try{
			helper = SerialHelper.CreateInstance("COM10");
			helper.setTerminatorBasedStream("\n");
			// helper.setLengthBasedStream();

			helper.OnConnected +=  () => {
				Debug.Log("Connected");
				//display_text.text = "Connected";
			};

			helper.OnConnectionFailed += () => {
				//Debug.Log("Failed");
				//display_text.text = "failed";
			};

			helper.OnDataReceived += () => {
                String temp = helper.Read();
                Debug.Log(temp);
                YesNo.delegateMessage(temp);
			};

			helper.Connect();
			
		}catch(Exception ex){
			display_text.text = ex.Message;
		}        
    }

    void OnGUI()
	{
		connect.SetActive(false);
		disconnect.SetActive(false);

		if(!helper.isConnected()){
		    connect.SetActive(true);
        }
		else if(helper.isConnected()){
		    disconnect.SetActive(true);
		}
	}

	public void connectButton(){
		helper.Connect ();
	}
	public void disconnectButton(){
		helper.Disconnect ();
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
