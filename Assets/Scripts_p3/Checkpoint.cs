using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    // once player reaches a checkpoint, 
    // take a snapshot of current game stage
    // (record info about bricks, player, ammos)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            // record position of all bricks
            // require bricks to have tag "brick"
            GameObject[] bricks = GameObject.FindGameObjectsWithTag("brick");
            List<Vector3> bricks_pos = RecordPositions(bricks);
            List<bool> bricks_floating = RecordFloatingStatus(bricks);

            // if game reloads, player should respawn at checkpoint
            Vector3 player_pos = transform.position;
            bool player_floating = false;
            GameObject player = GameObject.Find("Player");
            if(player != null)
            {
                player_floating = player.GetComponent<PlayerFloat>().is_floating;
            }

            // record ammo status
            // require ammos to have tag "battery"
            GameObject[] ammos = GameObject.FindGameObjectsWithTag("battery");
            List<Vector3> ammos_pos = RecordPositions(ammos);

            // record ball positions
            GameObject[] balls = GameObject.FindGameObjectsWithTag("ball");
            List<Vector3> balls_pos = RecordPositions(balls);
            List<bool> balls_floating = RecordFloatingStatus(balls);

            // record breakable tiles status
            // require ammos to have tag "bar"
            GameObject[] breakables = GameObject.FindGameObjectsWithTag("bar");
            List<Vector3> breakables_pos = RecordPositions(breakables);
            List<Vector3> breakables_scale = RecordScale(breakables);
            List<Quaternion> breakbles_rotation = RecordRotation(breakables);

            // record inventory
            Inventory_tmp.instance.RecordInitialState();

            
            // inform GameStatus
            EventBus.Publish<Checked>(new Checked(bricks.ToList(), bricks_pos, bricks_floating,
                player_pos, player_floating, ammos_pos, balls.ToList(), balls_pos, balls_floating,
                breakables_pos, breakables_scale, breakbles_rotation));
        }
    }


    // record positions of given gameobjects
    List<Vector3> RecordPositions(GameObject[] objects)
    {
        List<Vector3> obj_pos = new List<Vector3>();
        foreach (GameObject obj in objects)
        {
            obj_pos.Add(obj.transform.position);
        }
        return obj_pos;
    }

    // record scales of given gameobjects
    List<Vector3> RecordScale(GameObject[] objects)
    {
        List<Vector3> obj_scale = new List<Vector3>();
        foreach (GameObject obj in objects)
        {
            obj_scale.Add(obj.transform.localScale);
        }
        return obj_scale;
    }

    // record rotations of given gameobjects
    List<Quaternion> RecordRotation(GameObject[] objects)
    {
        List<Quaternion> obj_rot = new List<Quaternion>();
        foreach (GameObject obj in objects)
        {
            obj_rot.Add(obj.transform.localRotation);
        }
        return obj_rot;
    }


    // record floating status of bricks and balls
    List<bool> RecordFloatingStatus(GameObject[] floatables)
    {
        List<bool> floating = new List<bool>();
        foreach (GameObject f in floatables)
        {
            if (f.GetComponent<Floatable>())
            {
                floating.Add(f.GetComponent<Floatable>().is_floating);
            }
        }
        return floating;
    }
}
