using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// turn on/off pipes using push buttons
public class SwitchOnOff : MonoBehaviour
{
    public string this_name; // this_name should match button's controlled_obj_name
    Subscription<PushButtonEvent> button_event_subscription;

    public bool launch_if_button_pressed = false;

    private void Awake()
    {
        button_event_subscription = EventBus.Subscribe<PushButtonEvent>(_OnButtonChange);
    }
    // Start is called before the first frame update
    void Start()
    {
        
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
                //if(e.name == "pipes"){
                 //   transform.GetChild(0).gameObject.SetActive(launch_if_button_pressed);
                  //  transform.GetChild(1).gameObject.SetActive(launch_if_button_pressed);
                //}else{
                    Debug.Log("pressed");
                    transform.GetChild(0).gameObject.SetActive(launch_if_button_pressed);
               // }
            }
            else
            {
                //if(e.name == "pipes"){
                 //   transform.GetChild(0).gameObject.SetActive(!launch_if_button_pressed);
                  //  transform.GetChild(1).gameObject.SetActive(!launch_if_button_pressed);
                //}else{
                    transform.GetChild(0).gameObject.SetActive(!launch_if_button_pressed);
                //}
            }

        }
    }
    private void OnDestroy()
    {
        EventBus.Unsubscribe(button_event_subscription);
    }
}
