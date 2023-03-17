using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPrompt : MonoBehaviour
{
    // Start is called before the first frame update
    public Text prompt;
    string ammoPrompt = "Congratulations on collecting the bubble ammo! Now select with cursor and make the target float.";
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("firstAmmo"))
        {
            Debug.Log("displaying prompt");
            StartCoroutine(typeWritter(ammoPrompt));
        }
    }

    IEnumerator typeWritter(string input)
    {
        string[] letters = input.Split(' ');
        prompt.text = letters[0];
        for (int i = 1; i < letters.Length; i++)
        {
            yield return new WaitForSeconds(0.1f);
            prompt.text += " " + letters[i];
        }
        yield return new WaitForSeconds(4f);
        prompt.text = "";
    }
}
