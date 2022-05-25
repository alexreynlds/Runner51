using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacleCollectableSpace : MonoBehaviour
{
    public List<float> collectableLaneX;
    public List<float> collectableJumpsX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float getLane(){
        if(collectableLaneX == null || collectableLaneX.Count < 1){
            return -1f;
        }

        return collectableLaneX[Random.Range(0, collectableLaneX.Count)];
    }

    public float getJump(){
        if(collectableJumpsX == null || collectableJumpsX.Count < 1){
            return -1f;
        }

        return collectableJumpsX[Random.Range(0, collectableJumpsX.Count)];
    }
}
