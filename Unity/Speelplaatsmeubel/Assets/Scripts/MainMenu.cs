using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using ArduinoSerialAPI;
using UnityEngine.SceneManagement;

public class MainMenu: MonoBehaviour
{
    void Start()
    {	
          SceneManager.LoadScene (sceneName:"SimonSays");
    }

}
