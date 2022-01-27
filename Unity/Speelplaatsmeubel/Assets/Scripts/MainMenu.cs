using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using ArduinoSerialAPI;
using UnityEngine.SceneManagement;

public class MainMenu: MonoBehaviour
{
    public GameObject[] games = new GameObject[2];

     void Start()
    {	
        float width = Camera.main.orthographicSize * 2.0f * Screen.width / Screen.height;
        float height = width/Screen.width*Screen.height;
             
        // Display
		float display_height = Screen.height/2.0f;

        float sizeDivided = Screen.width / (games.Length*1.0f + 1f);
        
        for(int i = 0; i < games.Length; i++){
            games[i].GetComponent<RectTransform>().sizeDelta = new Vector2(sizeDivided*0.75f, sizeDivided*0.25f);
            games[i].transform.position = new Vector3((i+1)*sizeDivided, Screen.height/2.0f, games[i].transform.position.z);
        }
    }

    public void openSimonSays(){
        SceneManager.LoadScene (sceneName:"SimonSays");
    }

    public void openQuiz(){
        SceneManager.LoadScene (sceneName:"QuizQuestions");
    }

}
