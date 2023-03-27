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
        // record position of all bricks
        // require bricks to have tag "brick"
        GameObject[] bricks = GameObject.FindGameObjectsWithTag("brick");
        /*
        List<Vector3> bricks_pos = new List<Vector3>();
        foreach (GameObject brick in bricks)
        {
            bricks_pos.Add(brick.transform.position);
        } 
        */
        List<Vector3> bricks_pos = RecordPositions(bricks);

        // if game reloads, player should respawn at checkpoint
        Vector3 player_pos = transform.position;

        // record ammo status
        // require ammos to have tag "battery"
        GameObject[] ammos = GameObject.FindGameObjectsWithTag("battery");
        List<Vector3> ammos_pos = RecordPositions(ammos);

        // record ball positions
        GameObject[] balls = GameObject.FindGameObjectsWithTag("ball");
        List<Vector3> balls_pos = RecordPositions(balls);

        // record breakable tiles status
        // require ammos to have tag "bar"
        GameObject[] breakables = GameObject.FindGameObjectsWithTag("bar");
        List<Vector3> breakables_pos = RecordPositions(breakables);

        // record inventory
        Inventory_tmp.instance.RecordInitialState();

        // inform GameStatus
        EventBus.Publish<Checked>(new Checked(bricks.ToList(), bricks_pos, player_pos, ammos_pos, balls.ToList(), balls_pos, breakables_pos));
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
}
