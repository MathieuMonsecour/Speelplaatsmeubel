using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizQuestions : MonoBehaviour
{

    public GameObject buttonPrefab;
    public GameObject quiz_panel;
    public GameObject quiz_scroll;
    public GameObject quiz_background;
    public GameObject newQuizButton;
    public GameObject question_panel;
    public GameObject question_scroll;
    public GameObject question_background;
    public GameObject newQuestionButton;
    public GameObject answersBackground;
    public int spacing;

    // Start is called before the first frame update
    void Start()
    {
        float width = Camera.main.orthographicSize * 2.0f * Screen.width / Screen.height;
        float height = width/Screen.width*Screen.height;

        // Quiz - left
        quiz_background.transform.localScale = new Vector3(width/4*0.97f, height*0.95f, 0f);
        quiz_background.transform.position = new Vector3(-3*width/8, 0f, 0f);
        newQuizButton.transform.position = new Vector2(Screen.width/8, Screen.height*0.92f);
        quiz_scroll.transform.position = new Vector2(Screen.width/8, Screen.height/2);
        quiz_scroll.GetComponent<RectTransform>().sizeDelta = new Vector2 (Screen.width/4*0.9f, Screen.height*0.75f);

        // Questions - middle
        question_background.transform.localScale = new Vector3(width/2*0.97f, height*0.95f, 0f);
        question_background.transform.position = new Vector3(0f, 0f, 0f);
        newQuestionButton.transform.position = new Vector2(Screen.width/2, Screen.height*0.92f);
        question_scroll.transform.position = new Vector2(Screen.width/2, Screen.height/2);
        question_scroll.GetComponent<RectTransform>().sizeDelta = new Vector2 (Screen.width/2*0.9f, Screen.height*0.75f);

        // Answers - right
        answersBackground.transform.localScale = new Vector3(width/4*0.97f, height*0.95f, 0f);
        answersBackground.transform.position = new Vector3(3*width/8, 0f, 0f);

    }

    public void newQuiz(){
        GameObject obj = Instantiate(buttonPrefab);
        obj.transform.SetParent(quiz_panel.transform,false);

        RectTransform rt = quiz_panel.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2 (rt.sizeDelta.x, rt.sizeDelta.y + 30 + spacing);
    }

    public void newQuestion(){
        GameObject obj = Instantiate(buttonPrefab);
        obj.transform.SetParent(question_panel.transform,false);

        RectTransform rt = question_panel.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2 (rt.sizeDelta.x, rt.sizeDelta.y + 30 + spacing);
    }
}
