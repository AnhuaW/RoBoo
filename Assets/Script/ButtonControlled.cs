using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControlled : MonoBehaviour
{
    public string this_name;
    public bool launch_when_button_pressed = false;

    Subscription<PushButtonEvent> button_event_subscription;

    void Awake()
    {
        button_event_subscription = EventBus.Subscribe<PushButtonEvent>(_OnButtonChange);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void _OnButtonChange(PushButtonEvent e)
    {
        Debug.Log("buttonchange");
        if (e.name == this_name)
        {
            if (e.pressed)
            {
                ControlRays(launch_when_button_pressed);
            }
            else
            {
                ControlRays(!launch_when_button_pressed);
            }
        }
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe(button_event_subscription);
    }

    void ControlRays(bool turn_on)
    {

        //Debug.Log(gameObject.name + turn_on);
        foreach (Transform child in transform)
        {
            // enable/disable sprite renderer and collider
            //child.gameObject.GetComponent<SpriteRenderer>().enabled = turn_on;
            child.gameObject.SetActive(turn_on);


            // turn on/off ray
            DamagingRay child_ray = child.gameObject.GetComponent<DamagingRay>();
            if (child_ray != null)
            {
                child_ray.enabled = turn_on;
            }
            LineRenderer child_line = child.gameObject.GetComponent<LineRenderer>();
            if (child_line != null)
            {
                child_line.enabled = turn_on;
                //child_line.sortingLayerName = "Foreground";
            }
        }
    }
}
