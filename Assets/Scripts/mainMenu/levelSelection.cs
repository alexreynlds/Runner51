using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class levelSelection : MonoBehaviour
{
    public Button[] levelButtons;
    public int levelAt;
    public int hasStarted;
    public TextMeshProUGUI scoreText1, scoreText2, scoreText3, scoreText4, scoreText5, scoreText6;
    public int score1, score2, score3, score4, score5, score6;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 6; i++){
            string score = "score" + i;

        }
        levelAt = PlayerPrefs.GetInt("levelAt", 1);

        hasStarted = PlayerPrefs.GetInt("hasStarted", 0);
        for (int i = 0; i < levelButtons.Length; i++){
            if ( i + 1 > levelAt){
                levelButtons[i].interactable = false;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        levelAt = PlayerPrefs.GetInt("levelAt", 1);
        for (int i = 0; i < levelButtons.Length; i++){
            if ( i + 1 > levelAt){
                levelButtons[i].interactable = false;
            }
        }
        //Getting the high scores and then changing the on-screen text to match.
        score1 = PlayerPrefs.GetInt("score1", 0);
        score2 = PlayerPrefs.GetInt("score2", 0);
        score3 = PlayerPrefs.GetInt("score3", 0);
        score4 = PlayerPrefs.GetInt("score4", 0);
        score5 = PlayerPrefs.GetInt("score5", 0);
        score6 = PlayerPrefs.GetInt("score6", 0);
        scoreText1.text = score1.ToString();
        scoreText2.text = score2.ToString();
        scoreText3.text = score3.ToString();
        scoreText4.text = score4.ToString();
        scoreText5.text = score5.ToString();
        scoreText6.text = score6.ToString();
    }

    public void loadNextLevel(int level){
        if(hasStarted == 0){
            PlayerPrefs.SetInt("hasStarted", 1);
            PlayerPrefs.SetInt("playerLives", 10);
            PlayerPrefs.Save();
            SceneManager.LoadScene(level); 
        }
        else{
        // PlayerPrefs.SetInt("playerLives", 10);
            PlayerPrefs.Save();
            SceneManager.LoadScene(level);            
        }
    }
}
