using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Collector : MonoBehaviour
{
    public bool battery_collected = false;
    public bool chip_collected = false;
    public Image chip_icon;
    public Image energy_icon;
    public Text chip_text;
    public Text energy_text;
    public Text energy_demo;
    public Text level;
    public Text gravity;
    public AudioClip recharge;
    public AudioClip chip_sound;
    bool typed = false;
    bool displayed = false;
    string content = "Now you can press [E] to turn gravity on and off";
    string hint = "Rise as high as you can to break through tiles";
    string endMessage = "Congratulations on reaching the control room!";
    Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<GravityControl>().enable_gravity)
        {
            gravity.text = "Gravity: ON";
        }
        else
        {
            gravity.text = "Gravity: OFF";
        }
        if (battery_collected)
        {
            energy_icon.enabled = true;
            energy_text.enabled = true;
            if (!typed)
            {
                StartCoroutine(TypeSentence());
            }
        }

        if (chip_collected)
        {
            chip_icon.enabled = true;
            chip_text.enabled = true;
        }

        change_text();
    }

    private void OnTriggerStay2D (Collider2D other)
    {
        if (other.CompareTag("battery"))
        {
            AudioSource.PlayClipAtPoint(recharge, Camera.main.transform.position);
            battery_collected = true;
            inventory.increase_energy(3);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("chip"))
        {
            AudioSource.PlayClipAtPoint(chip_sound, Camera.main.transform.position);
            chip_collected = true;
            inventory.add_chip(1);
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("control"))
        {
            StartCoroutine(finalMessage());
        }
    }


    void change_text()
    {
        chip_text.text = "X " + inventory.get_chip_count();
        energy_text.text = "X " + inventory.get_energy_level();
    }

    IEnumerator TypeSentence()
    {
        typed = true;
        string[] letters = content.Split(' ');
        energy_demo.text = letters[0];
        for (int i = 1; i < letters.Length; i++)
        {
            yield return new WaitForSeconds(0.1f);
            energy_demo.text += " " + letters[i];
        }

        yield return new WaitForSeconds(1.5f);
        energy_demo.text = "";
        letters = hint.Split(' ');
        energy_demo.text = letters[0];
        for (int i = 1; i < letters.Length; i++)
        {
            yield return new WaitForSeconds(0.1f);
            energy_demo.text += " " + letters[i];
        }

        yield return new WaitForSeconds(2f);
        energy_demo.text = "";
    }

    IEnumerator finalMessage()
    {
        displayed = true;
        string[] letters = endMessage.Split(' ');
        energy_demo.text = letters[0];
        for (int i = 1; i < letters.Length; i++)
        {
            yield return new WaitForSeconds(0.1f);
            energy_demo.text += " " + letters[i];
        }
        yield return new WaitForSeconds(2f);
        energy_demo.text = "";
        endMessage = "Press 1 to restart.";
        letters = endMessage.Split(' ');
        energy_demo.text = letters[0];
        for (int i = 1; i < letters.Length; i++)
        {
            yield return new WaitForSeconds(0.1f);
            energy_demo.text += " " + letters[i];
        }
    }
}
