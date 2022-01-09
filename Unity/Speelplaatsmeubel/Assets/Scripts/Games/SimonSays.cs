using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimonSays: MonoBehaviour
{
    public GameObject whiteBall;
    public GameObject greenBall;
    public GameObject redBall;
    public GameObject blueBall;
    public Controller controller;
    public Camera cam;
	public GameObject startButton;
    private bool listening = false;
    float absoluteSize = -1;

    void Start(){
        float width = Camera.main.orthographicSize * 2.0f * Screen.width / Screen.height;
        float height = width/Screen.width*Screen.height;
        
        float sizePercentage = 0.1f;
        absoluteSize = width*sizePercentage;
        float maskSize = Screen.width*sizePercentage;

        // White ball
        whiteBall.transform.localScale = new Vector3(absoluteSize, absoluteSize, absoluteSize);
        whiteBall.transform.position = new Vector3(-width/2f*0.6f, 0f, whiteBall.transform.position.z);
        // Green ball
        greenBall.transform.localScale = new Vector3(absoluteSize, absoluteSize, absoluteSize);
        greenBall.transform.position = new Vector3(-width/2f*0.2f, 0f, greenBall.transform.position.z);
        // Red ball
        redBall.transform.localScale = new Vector3(absoluteSize, absoluteSize, absoluteSize);
        redBall.transform.position = new Vector3(width/2f*0.2f, 0f, redBall.transform.position.z);
        // Blue ball
        blueBall.transform.localScale = new Vector3(absoluteSize, absoluteSize, absoluteSize);
        blueBall.transform.position = new Vector3(width/2f*0.6f, 0f, blueBall.transform.position.z);

        // start game button
		float start_game_width = maskSize*1.5f;
		float start_game_height = maskSize/2.0f;
        startButton.GetComponent<RectTransform>().sizeDelta = new Vector2(start_game_width, start_game_height);
        startButton.transform.position = new Vector3(Screen.width/2, Screen.height - start_game_height/2, startButton.transform.position.z);
        
        cam.backgroundColor = Color.black;
    }

    private float lastAction;
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
            StartCoroutine(safetyTime(0.3f));
            if(answer == itemNumbers[currentAnswer]){
                cam.backgroundColor = Color.green;
                currentAnswer += 1;
                if(currentAnswer == itemNumbers.Length){
                    currentAnswer = 0;
                    nbOfItems += 1;
                    itemSpeed = itemSpeed*0.9f;
                    listening = false;
                    StartCoroutine(nextExercise());
                }
                StartCoroutine(blackBackground(0.5f));
            }
            else{
                cam.backgroundColor = Color.red;
                currentAnswer = 0;
                listening = false;
                nbOfItems = 2;
                itemSpeed = 1f;
                StartCoroutine(blackBackground(0.5f));
                startButton.SetActive(true);
            }
        }
        
    }

    public void StartGame(){
        startButton.SetActive(false);
        StartCoroutine(nextExercise());
    }

private int nbOfItems = 1;
private float itemSpeed = 0.75f;
public int[] itemNumbers = new int[1];
public GameObject[] balls = new GameObject[4];
private int currentAnswer = 0;
private bool safetyTimeCounting = false;

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

    // Display balls
    for(int i = 0; i < nbOfItems; i++){
        balls[itemNumbers[i]].transform.localScale = new Vector3(absoluteSize*2, absoluteSize*2, absoluteSize*2);
        yield return new WaitForSeconds(itemSpeed);
        balls[itemNumbers[i]].transform.localScale = new Vector3(absoluteSize, absoluteSize, absoluteSize);
        yield return new WaitForSeconds(itemSpeed);
    }
    
    listening = true;
}

IEnumerator blackBackground(float wait){
    yield return new WaitForSeconds(wait);
    cam.backgroundColor = Color.black;
}

IEnumerator safetyTime(float wait){
    safetyTimeCounting = true;
    yield return new WaitForSeconds(wait);
    safetyTimeCounting = false;
}

}
