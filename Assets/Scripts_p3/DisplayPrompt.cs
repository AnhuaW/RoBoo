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
    string ammoPrompt = "Dear Diary," +
        "\n\n" +"  It has been two days since I lost contact with my teammates. Last time, the signal they sent said they"
        +" COULD NOT swim in that Lost Sea Ruins." +"\n" +"  Their oxygen is about to running out. I have to go find them" +"\n"
        +"  Wish me good look!";
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        load = GameObject.Find("levelLoader");
        StartCoroutine(typeWritter(ammoPrompt,sceneName));
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
        yield return new WaitForSeconds(10f);
        prompt.text = "";
        if(sceneName == "Background_story")
        {
            Debug.Log(prompt);
            SceneManager.LoadScene("p3_guide_1");
            //StartCoroutine(load.GetComponent<levelLoder>().LoadLevel(1));
        }
    }
}
