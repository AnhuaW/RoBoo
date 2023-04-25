using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHint : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject hint;
    public bool collided;
    void Start()
    {
        hint.active = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        collided = true;
        Debug.Log("hint collision");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("displaying hint");
            hint.active = true;

            // freeze weaping angels
            StartCoroutine(FreezeAngels());

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        collided = false;
        if (other.gameObject.CompareTag("Player"))
        {
            hint.active = false;

            // activate weaping angels
            StopAllCoroutines();
            GameObject[] angels = GameObject.FindGameObjectsWithTag("statue");
            foreach (GameObject angel in angels)
            {
                angel.GetComponent<statue_attack>().enabled = true;
            }
        }
    }


    IEnumerator FreezeAngels()
    {
        while (true)
        {
            yield return null;
            GameObject[] angels = GameObject.FindGameObjectsWithTag("statue");
            foreach (GameObject angel in angels)
            {
                angel.GetComponent<statue_attack>().enabled = false;
            }
        }
    }
}
