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

    public GameObject ammo_prefab = null;
    public GameObject angle_prefab = null;
    public GameObject breakable_tile_prefab = null;
    public GameObject death_panel = null;

    List<GameObject> bricks = new List<GameObject>();
    List<Vector3> bricks_pos_record= new List<Vector3>();
    Vector3 checkpoint_pos_record = Vector3.zero;
    List<Vector3> ammos_pos_record = new List<Vector3>();
    List<GameObject> balls = new List<GameObject>();
    List<Vector3> balls_pos_record = new List<Vector3>();
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

        if (gameover)
        {
            // player dies
            if (!manual_restart)
            {
                Debug.Log("open death panel!");
                death_panel.SetActive(true);
                // disable player control
                SetPlayerControl(false);
            }
            // player wants to manually restart the game
            else
            {
                RestartGame();
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
                    SceneManager.LoadScene(0);
                }
                
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
            }
            GameObject.Find("Player").transform.position = checkpoint_pos_record;

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
            // TODO: edit this to improve compatibility
            // destory angel if restart or start at check point 
            GameObject[] angel = GameObject.FindGameObjectsWithTag("statue");
            foreach (GameObject ang in angel)
            {
                Destroy(ang);
                GameObject.Instantiate(angle_prefab, new Vector3(25.5f, 9.247638f, 0f), Quaternion.identity);
            }

            // balls states
            for (int i = 0; i < balls.Count; ++i)
            {
                balls[i].transform.position = balls_pos_record[i];
                balls[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                balls[i].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
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

        }

        // ensable player control
        SetPlayerControl(true);
        gameover = false;
        manual_restart = false;
    }


    // enable/disable player control and animator
    // change sprite color
    void SetPlayerControl(bool enable)
    {
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            GetComponent<ArrowKeyMovement>().player_control = enable;
            GetComponent<Animator>().enabled = enable;
            if (death_panel.activeSelf && !enable)
            {
                GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }



    // manual_restart directly reloads game without opening death panel
    /*
    void _OnGameOver(GameOver g)
    {
        gameover = true;
        manual_restart = g.manual_restart;
        for (int i = 0; i < death_panel.transform.childCount; ++i)
        {
            if (death_panel.transform.GetChild(i).name.Contains("DeathInfo"))
            {
                GameObject death_info= death_panel.transform.GetChild(i).gameObject;
                death_info.GetComponent<Text>().text = g.death_info.ToString();
            }
        }
    }*/

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
        checkpoint_pos_record = c.checkpoint_pos;
        ammos_pos_record = c.ammos_pos;
        balls = c.balls;
        balls_pos_record = c.balls_pos;
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
    public Vector3 checkpoint_pos = new Vector3();
    public List<Vector3> ammos_pos = new List<Vector3>();
    public List<GameObject> balls = new List<GameObject>();
    public List<Vector3> balls_pos = new List<Vector3>();
    public List<Vector3> breakables_pos = new List<Vector3>();
    public List<Vector3> breakables_scale = new List<Vector3>();
    public List<Quaternion> breakables_rotation = new List<Quaternion>();


    public Checked(List<GameObject> _bricks, List<Vector3> _bricks_pos,
        Vector3 _checkpoint_pos, List<Vector3> _ammos_pos,
        List<GameObject> _balls, List<Vector3> _balls_pos, 
        List<Vector3> _breakables_pos, List<Vector3> _breakables_scale, 
        List<Quaternion> _breakables_rotation)
    {
        bricks = _bricks;
        bricks_pos = _bricks_pos;
        checkpoint_pos = _checkpoint_pos;
        ammos_pos = _ammos_pos;
        balls = _balls;
        balls_pos = _balls_pos;
        breakables_pos = _breakables_pos;
        breakables_scale = _breakables_scale;
        breakables_rotation = _breakables_rotation;
    }
}