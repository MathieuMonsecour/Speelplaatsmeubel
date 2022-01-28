﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimonSays: MonoBehaviour
{
    public Controller controller;
    public Camera cam;
	public GameObject startButton;
    private bool listening = false;
    private float ballSize = -1;
    private float safetyWait = 0.3f;

    private float minimumSpeed = 0.1f;
    private float increaseSpeedFactor = 0.9f;

    private int nbOfItems = 1;
    private float itemSpeed = 0.75f;
    public int[] itemNumbers = new int[1];
    public GameObject[] balls = new GameObject[9];
    private int currentAnswer = 0;
    private bool safetyTimeCounting = false;
    private float lastAction;
    
    public GameObject highscore;
	public Text highscore_text;

    private int highestScore = 0;

    private bool isConnected = false;
    private float sizePercentage = 0.07f;
    private float localMenuPercentage = 0.15f;

    void Start(){
        float width = Camera.main.orthographicSize * 2.0f * Screen.width / Screen.height;
        float height = width/Screen.width*Screen.height;

        float percentageOfMenu = Controller.menuWidth/width;

        float maskSize = Screen.width*sizePercentage;
        float menuSize = Screen.width*localMenuPercentage;

        float absoluteConversion = width/Screen.width;
        float relativeConversion = Screen.width/width;

        // Display
		float display_width = menuSize;
		float display_height = menuSize/2.0f;

        // start game button
		float start_game_width = maskSize*1.5f;
		float start_game_height = maskSize;
        startButton.GetComponent<RectTransform>().sizeDelta = new Vector2(start_game_width, start_game_height);
        startButton.transform.position = new Vector3(Screen.width/2 + Controller.menuWidth/4*relativeConversion, Screen.height - start_game_height/2, startButton.transform.position.z);

        // highscore
        highscore.GetComponent<RectTransform>().sizeDelta = new Vector2(display_width, display_height);
        highscore.transform.position = new Vector3(Screen.width - display_width/2.0f, Screen.height-display_height/2.0f, highscore.transform.position.z);

        float absoluteDisplayHeight = display_height*absoluteConversion;
        ballSize =  (height - 2*absoluteDisplayHeight)/5;
        float xZero =  -2*ballSize + Controller.menuWidth/4;
        float startX = xZero;
        float startY = 2*ballSize - absoluteDisplayHeight/2f;

        // balls
        for(int i = 0; i < balls.Length; i++){
            balls[i].transform.localScale = new Vector3(ballSize, ballSize, ballSize);
            balls[i].transform.position = new Vector3(startX , startY, balls[i].transform.position.z);
            
            startX += 2*ballSize;
            if((i-1) % 3 == 1){
                startX = xZero;
                startY -= 2*ballSize;
            }
        }
        
        cam.backgroundColor = Color.black;
        
        startButton.SetActive(false);
    }

    public void delegateMessage(string msg){
        int answer = -1;
        if(listening && !safetyTimeCounting){
            for(int i = 0; i < balls.Length; i++){
                if(msg[i] == '1'){
                    answer = i;
                }
            }
        }
        if(answer > -1){
            StartCoroutine(safetyTime(safetyWait));
            if(answer == itemNumbers[currentAnswer]){
                cam.backgroundColor = Color.green;
                currentAnswer += 1;
                highestScore = Mathf.Max(highestScore, currentAnswer);
                highscore_text.text = "Highscore: " + highestScore.ToString();
                if(currentAnswer == itemNumbers.Length){
                    StartCoroutine(nextLevel(safetyWait));
                }
                else{
                    StartCoroutine(blackBackground(safetyWait));
                }
            }
            else{
                cam.backgroundColor = Color.red;
                currentAnswer = 0;
                listening = false;
                nbOfItems = 1;
                itemSpeed = 1f;
                StartCoroutine(blackBackground(0.5f));
                startButton.SetActive(true);
            }
        }
        
    }
    

    public void StartGame(){
        startButton.SetActive(false);
        cam.backgroundColor = Color.grey;
        StartCoroutine(nextExercise());
    }

    private void increaseDifficulty(){
        currentAnswer = 0;
        nbOfItems += 1;
        itemSpeed = Mathf.Max(itemSpeed*increaseSpeedFactor, minimumSpeed);
        listening = false;
        StartCoroutine(nextExercise());
    }

    public void setConnection(bool c){
        isConnected = c;
        startButton.SetActive(true||c);
    }

IEnumerator nextExercise()
{   
    /*itemNumbers = new int[nbOfItems];

    // Generate sequence
    for(int i = 0; i < nbOfItems; i++){
        itemNumbers[i] = Mathf.FloorToInt(Random.Range(0, balls.Length));
    }*/

    int[] itemNumbers_copy = itemNumbers;
    itemNumbers = new int[nbOfItems];
    // Extend sequence
    for(int i = 0; i < nbOfItems-1; i++){
        itemNumbers[i] = itemNumbers_copy[i];
    }
    itemNumbers[nbOfItems - 1] = Mathf.FloorToInt(Random.Range(0, balls.Length));

    yield return new WaitForSeconds(1);

    float sizeSteps = 10f;
    float endingSizeFactor = 2f;
    float sizeFactor = Mathf.Pow(endingSizeFactor, 1f/sizeSteps);

    // Display balls
    for(int i = 0; i < nbOfItems; i++){
        for(int j = 1; j <= sizeSteps; j++){
            float temp = ballSize * Mathf.Pow(sizeFactor, j);
            balls[itemNumbers[i]].transform.localScale = new Vector3(temp, temp, temp);
            yield return new WaitForSeconds((itemSpeed/3f)/sizeSteps);
        }
        yield return new WaitForSeconds(itemSpeed/3f);

        for(int j = 1; j <= sizeSteps; j++){
            float temp = endingSizeFactor*ballSize * Mathf.Pow(1/sizeFactor, j);
            balls[itemNumbers[i]].transform.localScale = new Vector3(temp, temp, temp);
            yield return new WaitForSeconds((itemSpeed/3f)/sizeSteps);
        }

        balls[itemNumbers[i]].transform.localScale = new Vector3(ballSize, ballSize, ballSize);
        yield return new WaitForSeconds(itemSpeed);
    }
    
    listening = true;
    cam.backgroundColor = Color.black;
    
}

IEnumerator blackBackground(float wait){
    yield return new WaitForSeconds(wait);
    cam.backgroundColor = Color.black;
}

IEnumerator nextLevel(float wait){
    yield return new WaitForSeconds(wait);
    cam.backgroundColor = Color.grey;
    increaseDifficulty();
}

IEnumerator safetyTime(float wait){
    safetyTimeCounting = true;
    yield return new WaitForSeconds(wait);
    safetyTimeCounting = false;
}

}
