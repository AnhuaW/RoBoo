using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    public Vector3 player_pos;
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y < Camera.main.transform.position.y - 7)
        {
            StartCoroutine(ResetLevel());
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            StartCoroutine(ResetLevel());
        }
    }

    IEnumerator ResetLevel()
    {
        //TODO GameOver text
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}

