using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIScript : MonoBehaviour
{
    private GameObject Player;
    private levelManager levelManager;
    private TextMeshProUGUI AtomsText;
    private TextMeshProUGUI TimeText;
    public GameObject inGame;
    public GameObject endScreen;
    int score = 0;

    float currentTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        levelManager = GameObject.Find("levelManager").GetComponent<levelManager>();
        AtomsText = GameObject.Find("AtomsText").GetComponent<TextMeshProUGUI>();
        TimeText = GameObject.Find("TimeText").GetComponent<TextMeshProUGUI>();

        // inGame = GameObject.Find("inGame");
        // endScreen = GameObject.Find("endScreen");

        currentTime = levelManager.startingTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        score++;
        AtomsText.text = Player.GetComponent<playerController>().atoms.ToString();
    }

    public void showEndScreen(){
        Debug.Log(currentTime);
        Time.timeScale = 0;
        levelManager.calcScore();
        inGame.SetActive(false);
        endScreen.SetActive(true);
        GameObject.Find("atomsEndText").GetComponent<TextMeshProUGUI>().text=levelManager.atomScore.ToString();
        GameObject.Find("timeEndText").GetComponent<TextMeshProUGUI>().text=levelManager.timeScore.ToString();
        GameObject.Find("totalEndText").GetComponent<TextMeshProUGUI>().text=levelManager.levelScore.ToString();
    }

    public void displayTime(float currentTime){
        if(currentTime < 0){
            currentTime = 0;
        } 
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        TimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void returnToMenu(){
        SceneManager.LoadScene(0);
    }
}
