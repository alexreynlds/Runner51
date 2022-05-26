using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUpSpin : MonoBehaviour
{
    public AudioSource soundEffect;

    public float amplitude = 0.25f;
    public float frequency = 1f;
    private GameObject pickUp;

    Vector3 posOffset = new Vector3 ();
    Vector3 tempPos = new Vector3 ();

    // Update is called once per frame
    void Start(){
        pickUp = this.gameObject;
        posOffset = transform.position;
    }
    void Update()
    {
        pickUp.transform.Rotate(0.0f, 0.2f, 0.0f);
        tempPos = posOffset;
        tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;
 
        transform.position = tempPos;
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag=="Player"){
            if(pickUp.gameObject.tag == "Atom"){
                other.gameObject.GetComponent<playerController>().atoms++;
                soundEffect.Play();
            }
            if(pickUp.gameObject.tag == "Life"){
                GameObject.Find("Player").GetComponent<playerController>().lives++;
                soundEffect.Play();
            }
            if (pickUp.gameObject.tag == "2X Speed")
            {
                GameObject.Find("Player").GetComponent<playerController>().speed += 12;
                soundEffect.Play();
            }
            if (pickUp.gameObject.tag == "Point Multiplier")
            {
                int multiplier = GameObject.Find("Player").GetComponent<playerController>().atoms;
                GameObject.Find("Player").GetComponent<playerController>().atoms += multiplier;
                soundEffect.Play();
            }
            if (pickUp.gameObject.tag == "Shield")
            {
                GameObject.Find("Player").GetComponent<playerController>().shield += 1;
                soundEffect.Play();
            }
            if (pickUp.gameObject.tag == "Time-cut")
            {
                GameObject.Find("levelManager").GetComponent<levelManager>().currentTime -= 30;
                soundEffect.Play();
            }
            Destroy(soundEffect.gameObject, 1f);
            Destroy(pickUp, 0.05f);
        }
    }
}
