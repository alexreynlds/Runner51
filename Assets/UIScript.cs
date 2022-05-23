using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScript : MonoBehaviour
{
    private GameObject Player;
    private TextMeshProUGUI AtomsText;
    private TextMeshProUGUI TimeText;
    int score = 0;

    float currentTime = 0f;
    float startingTime = 300;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        AtomsText = GameObject.Find("AtomsText").GetComponent<TextMeshProUGUI>();
        TimeText = GameObject.Find("TimeText").GetComponent<TextMeshProUGUI>();
        currentTime = startingTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        score++;
        AtomsText.text = Player.GetComponent<playerController>().Atoms.ToString();
        currentTime -= 1 *Time.deltaTime;
        displayTime(currentTime);
        // TimeText.text = currentTime.ToString();
    }

    void displayTime(float currentTime){
        if(currentTime < 0){
            currentTime = 0;
        } 
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        TimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
