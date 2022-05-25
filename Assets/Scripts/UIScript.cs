using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIScript : MonoBehaviour
{
    public int nextSceneLoad;

    private GameObject Player;
    private levelManager levelManager;
    private TextMeshProUGUI AtomsText;
    private TextMeshProUGUI TimeText;
    private TextMeshProUGUI livesText;

    public GameObject inGame;
    public GameObject deathScreen;
    public GameObject endScreen;
    public GameObject pauseScreen;

    int score = 0;
    bool isPaused;

    float currentTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        Player = GameObject.Find("Player");
        levelManager = GameObject.Find("levelManager").GetComponent<levelManager>();
        AtomsText = GameObject.Find("AtomsText").GetComponent<TextMeshProUGUI>();
        TimeText = GameObject.Find("TimeText").GetComponent<TextMeshProUGUI>();
        livesText = GameObject.Find("livesText").GetComponent<TextMeshProUGUI>();

        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;

        currentTime = levelManager.startingTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        score++;
        AtomsText.text = Player.GetComponent<playerController>().atoms.ToString();
        livesText.text = Player.GetComponent<playerController>().lives.ToString();
    }

    public void pauseGame(){
        if(isPaused){
            isPaused = false;
            pauseScreen.SetActive(false);
            inGame.SetActive(true);
            Time.timeScale = 1;
        } else{
            isPaused = true;
            pauseScreen.SetActive(true);
            inGame.SetActive(false);
            Time.timeScale = 0;
        }
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

    public void moveToNextLevel(){
        SceneManager.LoadScene(nextSceneLoad);
        PlayerPrefs.SetInt("levelAt", nextSceneLoad);
    }

    public void displayTime(float currentTime){
        if(currentTime < 0){
            currentTime = 0;
        } 
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        TimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void showDeathScreen(){
        inGame.SetActive(false);
        deathScreen.SetActive(true);

    }

    public void returnToMenu(){
        SceneManager.LoadScene(0);
    }

    public void noLivesReturnToMenu(){
        PlayerPrefs.SetInt("hasStarted", 0);
        PlayerPrefs.SetInt("levelAt", 1);
        SceneManager.LoadScene(0);
    }
}
