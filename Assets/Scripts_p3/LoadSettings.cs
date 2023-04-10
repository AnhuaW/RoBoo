using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSettings : MonoBehaviour
{
    public int current_level_index = 0;

    // Start is called before the first frame update
    void Start()
    {
        current_level_index = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        if(current_level_index != SceneManager.GetActiveScene().buildIndex)
        {
            StartCoroutine(ReFetchRefs());
            current_level_index = SceneManager.GetActiveScene().buildIndex;
        }
    }

    public IEnumerator ReFetchRefs()
    {
        Debug.Log("re-fetching sfx refs");
        SettingsMenu.instance.gameObject.SetActive(true);
        SettingsMenu.instance.gameObject.GetComponent<Canvas>().enabled = false;
        yield return new WaitForSeconds(1f);
        //SettingsMenu.instance.gameObject.GetComponent<Canvas>().enabled = true;
        SettingsMenu.instance.gameObject.SetActive(false);
    }

}
