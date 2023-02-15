using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collector : MonoBehaviour
{
    public bool battery_collected = false;
    public bool chip_collected = false;
    public Image chip_icon;
    public Image energy_icon;
    public Text chip_text;
    public Text energy_text;
    public Text energy_demo;
    public AudioClip recharge;
    public AudioClip chip_sound;
    bool typed = false;
    string content = "Now you can press [E] to control gravity";
    Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
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
            inventory.increase_energy(2);
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

        yield return new WaitForSeconds(3f);
        energy_demo.enabled = false;
    }
}
