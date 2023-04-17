using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSFXVolume : MonoBehaviour
{
    [SerializeField] Slider sfx_slider;
    [SerializeField] AudioClip test_sfx;

    // Start is called before the first frame update
    void Start()
    {
        sfx_slider = GetComponent<Slider>();
        //sfx_slider.onValueChanged.AddListener(delegate { SfxVolumeChanged(); });
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SfxVolumeChanged()
    {

        AudioSource.PlayClipAtPoint(test_sfx, Camera.main.transform.position, GetComponent<Slider>().value);

    }
}