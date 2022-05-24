using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==================================================
//  Level Manager
//  Stores all things related to the specific level
//  - Time Limit
//  - Scores
//  - High Scores
//  Also handles displaying the time on the UI and
//  calculating the final score
//==================================================

public class levelManager : MonoBehaviour
{
    private GameObject player;
    public float startingTime;
    public int levelScore = 0;
    public int levelHighScore;

    float currentTime = 0f;

    //Variables to create score
    public int atomScore;
    public int timeScore;

    //When level is loaded set the scores to 0 and set the current time (displayed) to the starting time for the level
    void Start(){
        player = GameObject.Find("Player");
        currentTime = startingTime;
        atomScore = 0;
        timeScore = 0;
    }
    //Decrease current time every second and also update the display of it
    void FixedUpdate(){
        currentTime -= 1 *Time.deltaTime;
        GameObject.Find("UICanvas").GetComponent<UIScript>().displayTime(currentTime);
    }
    //Calculate the total score from the atom score and the time score.
    public void calcScore(){
        atomScore = player.GetComponent<playerController>().atoms * 1000;
        timeScore = (int)currentTime * 1000;
        levelScore = atomScore + timeScore;
    }
}
