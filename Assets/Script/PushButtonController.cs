using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButtonController : MonoBehaviour
{
    public Sprite pressed_sprite;
    public Sprite eased_sprite;
    public bool pressed = false;
    public string controlled_obj_name; // name of object(s) being controlled by this button

    SpriteRenderer sprite_renderer;
    BoxCollider2D this_collider;

    // Start is called before the first frame update
    void Start()
    {
        sprite_renderer = GetComponent<SpriteRenderer>();
        this_collider = GetComponent<BoxCollider2D>();

        EventBus.Publish<PushButtonEvent>(new PushButtonEvent(pressed, controlled_obj_name));
    }

    // Update is called once per frame
    void Update()
    {
        if (pressed)
        {
            sprite_renderer.sprite = pressed_sprite;
            //this_collider.offset = new Vector2(0, 0.025f);
            //this_collider.size = new Vector2(0.12f, 0.05f);
        }
        else
        {
            sprite_renderer.sprite = eased_sprite;
            //this_collider.offset = new Vector2(0, 0.035f);
            //this_collider.size = new Vector2(0.12f, 0.07f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.name.Contains("Floor") && !pressed
            && Mathf.Abs(transform.position.x - collision.gameObject.transform.position.x)<0.38)
        {
            pressed = true;
            EventBus.Publish<PushButtonEvent>(new PushButtonEvent(pressed, controlled_obj_name));
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        pressed = false;
        EventBus.Publish<PushButtonEvent>(new PushButtonEvent(pressed, controlled_obj_name));
    }
}

public class PushButtonEvent
{
    public bool pressed;
    public string name; // control specific set of objects
    public PushButtonEvent(bool _pressed, string _name)
    {
        pressed = _pressed;
        name = _name;
    }
}