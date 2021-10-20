using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YesNo : MonoBehaviour
{
    public string[] questions = new string[3];
    public bool[] question_answers = new bool[3];

    public GameObject display_text;
    public GameObject display;
    public GameObject redBall;
    public GameObject greenBall;

    void Start(){
        display_text.GetComponent<UnityEngine.UI.Text>().text = "123";
        foreach(string i in questions){
            Debug.Log(i);
        }
    }

    void Update(){
        float width = Camera.main.orthographicSize * 2.0f * Screen.width / Screen.height;
        float height = width/Screen.width*Screen.height;
        
        float sizePercentage = 0.1f;
        float absoluteSize = width*sizePercentage;
        float maskSize = Screen.width*sizePercentage;

        // Red ball
        redBall.transform.localScale = new Vector3(absoluteSize, absoluteSize, absoluteSize);
        redBall.transform.position = new Vector3(-absoluteSize*2, 0f, redBall.transform.position.z);
        
        // Green ball
        greenBall.transform.localScale = new Vector3(absoluteSize, absoluteSize, absoluteSize);
        greenBall.transform.position = new Vector3(absoluteSize*2, 0f, greenBall.transform.position.z);

        // Display
        display.transform.position = new Vector3(Screen.width/2f, Screen.height/2f, display.transform.position.z);
        display.GetComponent<RectTransform>().sizeDelta = new Vector2(maskSize, maskSize);
    }
}
