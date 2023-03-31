using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class merge_key : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefab;
    private GameObject door;
    private GameObject ui_control;
    private int required_key_num;
    GameObject door_key;
    public int current_key_num; // TODO: change to private later
    Vector3 target_position;
    public float speed = 0.5f;
    void Start()
    {
        door = GameObject.Find("Door");
        ui_control = GameObject.Find("key_uiControl");
        required_key_num = door.GetComponent<DoorToNextLevel>().required_key;
        current_key_num = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (current_key_num == required_key_num)
        {
            door_key = Instantiate(prefab);
            //door_key.transform.SetParent(this.transform, false);
            door_key.transform.position = target_position;
            current_key_num = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("key"))
        {
            ui_control.GetComponent<keyui_control>().activate_key(current_key_num);
            current_key_num += 1;
            if(current_key_num == required_key_num)
            {
                target_position = collision.gameObject.transform.position;
            }
        }
        else if (collision.gameObject.CompareTag("Portal"))
        {
            Destroy(door_key);
        }
    }
}
