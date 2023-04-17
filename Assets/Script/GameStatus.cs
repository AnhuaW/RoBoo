using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// receives gameover signals and reload the last checkpoint
// receives checkpoint signals and record game state
public class GameStatus : MonoBehaviour
{

    public bool gameover = false;
    public bool has_checked = false;
    public bool manual_restart = false;

    bool restarting = false;

    public GameObject ammo_prefab = null;
    public GameObject breakable_tile_prefab = null;
    public GameObject death_panel = null;
    public GameObject bubble_prefab;

    List<GameObject> bricks = new List<GameObject>();
    List<Vector3> bricks_pos_record= new List<Vector3>();
    List<bool> bricks_floating_record = new List<bool>();
    Vector3 checkpoint_pos_record = Vector3.zero;
    public bool player_floating_record = false;
    List<Vector3> ammos_pos_record = new List<Vector3>();
    List<GameObject> balls = new List<GameObject>();
    List<Vector3> balls_pos_record = new List<Vector3>();
    List<bool> balls_floating_record = new List<bool>();
    List<Vector3> breakables_pos_record = new List<Vector3>();
    List<Vector3> breakables_scale_record = new List<Vector3>();
    List<Quaternion> breakables_rotation_record = new List<Quaternion>();

    Subscription<GameOver> gameover_subscription;
    Subscription<Checked> checkpoint_subscription;

    // Start is called before the first frame update
    void Start()
    {
        gameover_subscription = EventBus.Subscribe<GameOver>(_OnGameOver);
        checkpoint_subscription= EventBus.Subscribe<Checked>(_OnCheckPoint);
        GameObject ui = GameObject.Find("UI");
        death_panel = ui.transform.Find("DeathPanel").gameObject;
    }

    // Update is called once per frame
    
    void Update()
    {

        if (gameover && !restarting)
        {
            restarting = true;

            // player dies, show death panel
            if (!manual_restart)
            {
                Debug.Log("open death panel!");
                death_panel.SetActive(true);
                // disable player control
                SetPlayerControl(false);
            }
            // player wants to manually restart the game
            // don't show death panel
            else
            {
                RestartGame();
            }
        }

        // if already showing death panel
        // wait for player's operation
        if (death_panel.activeSelf)
        {
            Debug.Log("death panel already open");
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartGame();
                death_panel.SetActive(false);
            }
            // press T to restart from beginning
            else if (Input.GetKeyDown(KeyCode.T))
            {
                has_checked = false;
                RestartGame();
                death_panel.SetActive(false);
            }
            // press Esc to go back to menu
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                SetPlayerControl(true);
                SceneManager.LoadScene(0);
            }

        }

    }
    


    // restart the level or load checkpoint
    void RestartGame()
    {
        if (has_checked)
        {
            // retrieve last checkpoint state
            for (int i = 0; i < bricks.Count; ++i)
            {
                bricks[i].transform.position = bricks_pos_record[i];
                bricks[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                RetrieveBubbles(bricks[i], bricks_floating_record[i]);
            }
            
            // player
            transform.position = checkpoint_pos_record;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            // destroy bubble
            // and re-instantiate one if necessary (now disabled)
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform temp_child = transform.GetChild(i);
                if (temp_child.gameObject.name.Contains("Bubble"))
                {
                    GetComponent<PlayerFloat>().ApplyGravity();
                }
            }
            /*
            if (player_floating_record)
            {
                GetComponent<PlayerFloat>().RemoveGravity();
            }
            */

            // replace all ammos
            GameObject[] ammos_left = GameObject.FindGameObjectsWithTag("battery");
            foreach (GameObject old_ammo in ammos_left)
            {
                Destroy(old_ammo);
            }
            foreach (Vector3 ammo_pos in ammos_pos_record)
            {
                Instantiate(ammo_prefab, ammo_pos, Quaternion.identity);
            }

            // angels
            // destory angel if restart or start at check
            if (GetComponent<recrod_angle_position>() != null)
            {
                Vector3 angle_pos = GetComponent<recrod_angle_position>().getPosition();
                if (angle_pos != Vector3.zero)
                {
                    GameObject[] angles = GameObject.FindGameObjectsWithTag("statue");
                    foreach (GameObject angle in angles)
                    {
                        angle.transform.position = angle_pos;
                    }
                }
            }



            // balls states
            for (int i = 0; i < balls.Count; ++i)
            {
                balls[i].transform.position = balls_pos_record[i];
                balls[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                balls[i].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                RetrieveBubbles(balls[i], balls_floating_record[i]);
            }

            // replace all breakables
            GameObject[] tiles_left = GameObject.FindGameObjectsWithTag("bar");
            foreach (GameObject old_tile in tiles_left)
            {
                Destroy(old_tile);
            }
            for (int i = 0; i < breakables_pos_record.Count; ++i)
            {
                Vector3 tile_pos = breakables_pos_record[i];
                Vector3 tile_scale = breakables_scale_record[i];
                Quaternion tile_rot = breakables_rotation_record[i];
                GameObject new_tile = Instantiate(breakable_tile_prefab, tile_pos, tile_rot);
                new_tile.transform.localScale = tile_scale;
            }

            // retrieve inventory
            Inventory_tmp.instance.RetrieveInitialState();
        }
        else
        {

            // reload scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            // re-fetch sfx references
            GameObject BGM = GameObject.Find("BGM");
            if (BGM)
            {
                LoadSettings l = BGM.GetComponent<LoadSettings>();
                if (l)
                {
                    l.current_level_index--;
                }
            }
        }

        // ensable player control
        SetPlayerControl(true);
        /*
        gameover = false;
        manual_restart = false;
        restarting = false;
        */
        StartCoroutine(Invincible(0.5f)); // invincible after death

        // notify SFX settings
        EventBus.Publish<SFXRefChange>(new SFXRefChange());
    }


    // enable/disable player control, animator, and movement
    // change sprite color
    void SetPlayerControl(bool enable)
    {
        // freeze time-based processes
        Time.timeScale = enable ? 1f : 0f;
        CollectibleMovement(enable);

        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            GetComponent<ArrowKeyMovement>().player_control = enable;
            GetComponent<BackToMenu>().enabled = enable;

            // sprite color
            if (death_panel.activeSelf && !enable)
            {
                GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                GetComponent<SpriteRenderer>().color = Color.white;
            }

            // close restart hint
            GameObject hint_panel = GameObject.Find("HintPanel");
            if (hint_panel != null)
            {
                if(hint_panel.GetComponent<RestartHintGuide2>() != null)
                {
                    hint_panel.GetComponent<RestartHintGuide2>().ResetPanel();
                }

                if (hint_panel.GetComponent<RestartHintGuide1>() != null)
                {
                    hint_panel.GetComponent<RestartHintGuide1>().ResetPanel();
                }
            }
        }
    }


    // disable/enable collectible movement
    void CollectibleMovement(bool enable)
    {
        // ammos
        GameObject[] ammos = GameObject.FindGameObjectsWithTag("battery");
        foreach (GameObject ammo in ammos)
        {
            CollectibleMovement cm = ammo.GetComponent<CollectibleMovement>();
            if (cm)
            {
                cm.enabled = enable;
            }
        }
        // keys
        GameObject[] keys = GameObject.FindGameObjectsWithTag("key");
        foreach (GameObject key in keys)
        {
            CollectibleMovement cm = key.GetComponent<CollectibleMovement>();
            if (cm)
            {
                cm.enabled = enable;
            }
        }
    }


    // delay setting gameover to true to avoid
    // opening death panel (aka executing _OnGameOver)
    // again just as player restarts
    // (e.g. when player died near laser, or respawn out of camera view)
    IEnumerator Invincible(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameover = false;
        manual_restart = false;
        restarting = false;
    }


    // re-generate bubbles if the object was floating when player checked
    void RetrieveBubbles(GameObject floatable, bool was_floating)
    {
        // destroy any bubble on the object
        for (int i = 0; i < floatable.transform.childCount; i++)
        {
            Transform temp_child = floatable.transform.GetChild(i);
            if (temp_child.gameObject.name.Contains("Bubble"))
            {
                floatable.GetComponent<Floatable>().ApplyGravity();
            }
        }
        // re-generate one
        if (was_floating)
        {
            floatable.GetComponent<Floatable>().RemoveGravity();
        }
    }


    void _OnGameOver(GameOver g)
    {
        if (!gameover)
        {
            gameover = true;
            manual_restart = g.manual_restart;
            for (int i = 0; i < death_panel.transform.childCount; ++i)
            {
                if (death_panel.transform.GetChild(i).name.Contains("DeathInfo"))
                {
                    GameObject death_info = death_panel.transform.GetChild(i).gameObject;
                    death_info.GetComponent<Text>().text = g.death_info.ToString();
                }
            }

        }

    }



    void _OnCheckPoint(Checked c)
    {
        has_checked = true;
        bricks = c.bricks;
        bricks_pos_record = c.bricks_pos;
        bricks_floating_record = c.bricks_floating;
        checkpoint_pos_record = c.checkpoint_pos;
        player_floating_record = c.player_floaing;
        ammos_pos_record = c.ammos_pos;
        balls = c.balls;
        balls_pos_record = c.balls_pos;
        balls_floating_record = c.balls_floating;
        breakables_pos_record = c.breakables_pos;
        breakables_scale_record = c.breakables_scale;
        breakables_rotation_record = c.breakables_rotation;

    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe(gameover_subscription);
        EventBus.Unsubscribe(checkpoint_subscription);
    }

}


public class GameOver
{
    public bool manual_restart = false;
    public string death_info = "";
    public GameOver(bool _manual_restart, string _death_info)
    {
        manual_restart = _manual_restart;
        death_info = _death_info;
    }

    public GameOver(bool _manual_restart)
    {
        manual_restart = _manual_restart;
        death_info = "YOU DIED!";
    }

    public GameOver()
    {
        manual_restart = false;
        death_info = "YOU DIED!";
    }
}

public class Checked
{
    public List<GameObject> bricks = new List<GameObject>();
    public List<Vector3> bricks_pos = new List<Vector3>();
    public List<bool> bricks_floating = new List<bool>();
    public Vector3 checkpoint_pos = new Vector3();
    public bool player_floaing = false;
    public List<Vector3> ammos_pos = new List<Vector3>();
    public List<GameObject> balls = new List<GameObject>();
    public List<Vector3> balls_pos = new List<Vector3>();
    public List<bool> balls_floating = new List<bool>();
    public List<Vector3> breakables_pos = new List<Vector3>();
    public List<Vector3> breakables_scale = new List<Vector3>();
    public List<Quaternion> breakables_rotation = new List<Quaternion>();


    public Checked(List<GameObject> _bricks, List<Vector3> _bricks_pos, List<bool> _bricks_floating,
        Vector3 _checkpoint_pos, bool _player_floating, List<Vector3> _ammos_pos,
        List<GameObject> _balls, List<Vector3> _balls_pos, List<bool> _balls_floating,
        List<Vector3> _breakables_pos, List<Vector3> _breakables_scale, 
        List<Quaternion> _breakables_rotation)
    {
        bricks = _bricks;
        bricks_pos = _bricks_pos;
        bricks_floating = _bricks_floating;
        checkpoint_pos = _checkpoint_pos;
        player_floaing = _player_floating;
        ammos_pos = _ammos_pos;
        balls = _balls;
        balls_pos = _balls_pos;
        balls_floating = _balls_floating;
        breakables_pos = _breakables_pos;
        breakables_scale = _breakables_scale;
        breakables_rotation = _breakables_rotation;
    }
}