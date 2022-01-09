using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quiz: MonoBehaviour
{
    public string[] questions = new string[4];
    public bool[] question_answers = new bool[4];
    private int questionCount = 0;

    public Text display_text;
    public GameObject redBall;
    public GameObject greenBall;
    public Controller controller;
    public Camera cam;

    void Start(){
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

        display_text.text = questions[questionCount];

        lastAction = Time.time;

        greenBall.SetActive(false);
        redBall.SetActive(false);
    }

    private float lastAction;
    private float pressingRate = 1f;
    public void delegateMessage(string msg){
        if(Time.time - lastAction >= pressingRate){
            if(msg[0] == '1' && msg[1] == '0'){
                if(question_answers[questionCount] == false){
                    questionCount = Mathf.Min(questionCount + 1, questions.Length-1);
                    cam.backgroundColor = Color.green;
                }
                else{
                    controller.sendString("F");
                    cam.backgroundColor = Color.red;
                }
                lastAction = Time.time;
                
            }
            else if(msg[0] == '0' && msg[1] == '1'){
                if(question_answers[questionCount] == true){
                    questionCount = Mathf.Min(questionCount + 1, questions.Length-1);
                    cam.backgroundColor = Color.green;
                }
                else{
                    controller.sendString("F");
                    cam.backgroundColor = Color.red;
                }
                lastAction = Time.time;
            }
        }
    }

    void Update(){
        if(Time.time - lastAction >= pressingRate){
            cam.backgroundColor = Color.black;
            display_text.text = questions[questionCount];
        }
    }
}
