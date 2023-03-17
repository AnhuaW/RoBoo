using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControlled : MonoBehaviour
{
    public string this_name;
    public Vector3 offset = Vector3.zero;
    public bool launch_when_button_pressed = false;

    Subscription<PushButtonEvent> button_event_subscription;
    Vector3 initial_pos, target_pos;

    // Start is called before the first frame update
    void Start()
    {
        button_event_subscription = EventBus.Subscribe<PushButtonEvent>(_OnButtonChange);

        //float new_y = transform.position.y + offset;
        initial_pos = transform.position;
        target_pos = transform.position + offset;
        //target_pos = new Vector3(transform.position.x, new_y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void _OnButtonChange(PushButtonEvent e)
    {
        if (e.name == this_name)
        {
            if (e.pressed)
            {
                transform.position = target_pos;
                ControlRays(launch_when_button_pressed);
            }
            else
            {
                transform.position = initial_pos;
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
            DamagingRay child_ray = child.gameObject.GetComponent<DamagingRay>();
            if (child_ray != null)
            {
                child_ray.enabled = turn_on;
            }
            LineRenderer child_line = child.gameObject.GetComponent<LineRenderer>();
            if (child_line != null)
            {
                child_line.enabled = turn_on;
            }
        }
    }
}
