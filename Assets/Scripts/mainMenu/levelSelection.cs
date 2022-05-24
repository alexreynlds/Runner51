using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class levelSelection : MonoBehaviour
{
    public Button[] levelButtons;
    public int levelAt;
    // Start is called before the first frame update
    void Start()
    {
        levelAt = PlayerPrefs.GetInt("levelAt", 1);

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
    }

    public void loadNextLevel(int level){
        SceneManager.LoadScene(level);
    }
}
