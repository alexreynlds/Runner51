using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScript : MonoBehaviour
{
    private GameObject Player;
    private TextMeshProUGUI AtomsText;
    int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        AtomsText = GameObject.Find("AtomsText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        score++;
        AtomsText.text = Player.GetComponent<PlayerController>().Atoms.ToString();
    }
}
