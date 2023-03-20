using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DisplayPrompt : MonoBehaviour
{
    // Start is called before the first frame update
    public Text prompt;
    string sceneName;
    GameObject load;
    string waterguide = "watch out currents! ↓";
    string ammoPrompt = "Dear Diary," +
        "\n\n" +"  It has been two days since I lost contact with my teammates. Last time, the signal they sent said they"
        +" COULD NOT swim in that Lost Sea Ruins." +"\n" +"  Their oxygen is about to running out. I have to go find them." +"\n"
        +"  Wish me good look!";
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        load = GameObject.Find("levelLoader");
        if (sceneName == "Background_story")
        {
            StartCoroutine(typeWritter(ammoPrompt, sceneName));
        }else if(sceneName == "p3_guide_waterflow")
        {
            StartCoroutine(typeWritter(waterguide, sceneName));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator typeWritter(string input,string sceneName)
    {
        string[] letters = input.Split(' ');
        prompt.text = letters[0];
        for (int i = 1; i < letters.Length; i++)
        {
            yield return new WaitForSeconds(0.1f);
            prompt.text += " " + letters[i];
        }
        if(sceneName == "Background_story")
        {
            yield return new WaitForSeconds(9f);
            prompt.text = "";
            Debug.Log(prompt);
            SceneManager.LoadScene("p3_guide_1");
            //StartCoroutine(load.GetComponent<levelLoder>().LoadLevel(1));
        }
        else
        {
            yield return new WaitForSeconds(4f);
            prompt.text = "";
        }
    }
}
