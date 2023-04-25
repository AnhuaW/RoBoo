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

        foreach (Transform child in transform)
        {
            // turn off line and ray
            if (child.gameObject.name == "Line")
            {
                child.gameObject.SetActive(turn_on);
            }
        }
    }
}
