using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleSpeed : MonoBehaviour
{
    // Start is called before the first frame update
    ParticleSystem waterParticle;
    [SerializeField] float particle_speed;
    void Start()
    {
        waterParticle = GetComponent<ParticleSystem>();
        particle_speed = Mathf.Abs(GetComponent<WaterFlow>().added_speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (waterParticle.gameObject.activeSelf)
        {
            var main = waterParticle.main;
            main.simulationSpeed = particle_speed;
        }
    }
}
