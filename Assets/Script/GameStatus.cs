using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// receives gameover signals and reload the last checkpoint
// receives checkpoint signals and record game state
public class GameStatus : MonoBehaviour
{

    public bool gameover = false;
    public bool has_checked = false;

    public GameObject ammo_prefab = null;
    public GameObject angle_prefab = null;
    public GameObject breakable_tile_prefab = null;

    List<GameObject> bricks = new List<GameObject>();
    List<Vector3> bricks_pos_record= new List<Vector3>();
    Vector3 checkpoint_pos_record = Vector3.zero;
    List<Vector3> ammos_pos_record = new List<Vector3>();
    List<GameObject> balls = new List<GameObject>();
    List<Vector3> balls_pos_record = new List<Vector3>();
    List<Vector3> breakables_pos_record = new List<Vector3>();

    Subscription<GameOver> gameover_subscription;
    Subscription<Checked> checkpoint_subscription;

    // Start is called before the first frame update
    void Start()
    {
        gameover_subscription = EventBus.Subscribe<GameOver>(_OnGameOver);
        checkpoint_subscription= EventBus.Subscribe<Checked>(_OnCheckPoint);
    }

    // Update is called once per frame
    void Update()
    {
        // restart the level
        GameObject[] angel = GameObject.FindGameObjectsWithTag("statue");
        if (gameover)
        {
            if (has_checked)
            {
                // retrieve last checkpoint state
                for(int i = 0; i < bricks.Count; ++i)
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
                foreach (GameObject ang in angel)
                {
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
                foreach (Vector3 tile_pos in breakables_pos_record)
                {
                    Instantiate(breakable_tile_prefab, tile_pos, Quaternion.identity);
                }

                // retrieve inventory
                Inventory_tmp.instance.RetrieveInitialState();
            }
            else
            {
                // reload scene
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            }
            //destory angel if restart or start at check point
            
            foreach (GameObject ang in angel)
            {
                Destroy(ang);
            }

            gameover = false;
        }
    }

    void _OnGameOver(GameOver g)
    {
        gameover = true;
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
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe(gameover_subscription);
        EventBus.Unsubscribe(checkpoint_subscription);
    }

}


public class GameOver
{
    public GameOver() { }
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

    public Checked(List<GameObject> _bricks, List<Vector3> _bricks_pos,
        Vector3 _checkpoint_pos, List<Vector3> _ammos_pos,
        List<GameObject> _balls, List<Vector3> _balls_pos, 
        List<Vector3> _breakables_pos)
    {
        bricks = _bricks;
        bricks_pos = _bricks_pos;
        checkpoint_pos = _checkpoint_pos;
        ammos_pos = _ammos_pos;
        balls = _balls;
        balls_pos = _balls_pos;
        breakables_pos = _breakables_pos;
    }
}