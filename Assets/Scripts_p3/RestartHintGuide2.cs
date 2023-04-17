using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartHintGuide2 : MonoBehaviour
{
    public int total_ammo_count;
    public int remaining_ammo_count;
    GameObject player;
    GameStatus game_status;

    Inventory_tmp inventory;
    RectTransform hint_panel;
    public bool toasting = false;

    [SerializeField] Vector3 hidden_pos, visible_pos;
    [SerializeField] float ease_duration = 0.5f;
    [SerializeField] float show_duration = 2f;
    [SerializeField] AnimationCurve ease;
    [SerializeField] AnimationCurve ease_out;

    [SerializeField] GameObject third_key;
    public bool check_checkpoint_started = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        inventory = Inventory_tmp.instance;
        game_status = GameObject.Find("Player").GetComponent<GameStatus>();

        hint_panel = GetComponent<RectTransform>();
        hidden_pos = new Vector3(-330, -150, 0);
        visible_pos = new Vector3(-330, 250 - 150, 0);

        CountRemainingAmmos();
        total_ammo_count = remaining_ammo_count;
    }

    // Update is called once per frame
    void Update()
    {
        CountRemainingAmmos();

        // bubbles are used up
        if (inventory.GetBubbleAmmo() <= 0 && !BubbleExist() && !toasting)
        {
            Debug.Log("toast!");
            if (remaining_ammo_count == total_ammo_count - 2)
            {
                if (player.transform.position.x <= 12f)
                {
                    toasting = true;
                    StartCoroutine(GiveRestartHint(ease_duration, show_duration));
                }
            }
            else if (remaining_ammo_count == total_ammo_count - 3)
            {
                if (player.transform.position.x < 27.64f || (player.transform.position.x > 27.65f && player.transform.position.y > -1.9f))
                {
                    if (!check_checkpoint_started)
                    {
                        check_checkpoint_started = true;
                        StartCoroutine(CheckCheckpoint());
                    }
                }

            }
            else if (remaining_ammo_count == total_ammo_count - 6)
            {
                if (player.transform.position.x < 53.5f)
                {
                    toasting = true;
                    StartCoroutine(GiveRestartHint(ease_duration, show_duration));
                }

            }

            // reset this component when game is over (and restarted)
            if (game_status.gameover)
            {
                //ResetPanel();
            }
            
        }
    }


    void CountRemainingAmmos()
    {
        GameObject[] ammos = GameObject.FindGameObjectsWithTag("battery");
        remaining_ammo_count = ammos.Length;
    }


    bool BubbleExist()
    {
        GameObject[] allGameObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allGameObjects)
        {
            Floatable floatable = obj.GetComponent<Floatable>();
            if (floatable && (floatable.is_floating || floatable.is_falling))
            {
                Debug.Log("bubble exists" + floatable.name);
                return true;
            }
        }
        return false;
    }


    IEnumerator GiveRestartHint(float duration_ease_sec, float duration_show_sec)
    {
        yield return new WaitForSeconds(0.5f);

        // Ease In the UI panel
        float initial_time = Time.time;
        float progress = (Time.time - initial_time) / duration_ease_sec;

        while (progress < 1.0f)
        {
            progress = (Time.time - initial_time) / duration_ease_sec;
            float eased_progress = ease.Evaluate(progress);
            hint_panel.anchoredPosition = Vector3.LerpUnclamped(hidden_pos, visible_pos, eased_progress);

            yield return null;
        }

        // Show the UI Panel for "duration_show_sec" seconds.
        yield return new WaitForSeconds(duration_show_sec);

        // Ease Out the UI panel
        initial_time = Time.time;
        progress = 0.0f;
        while (progress < 1.0f)
        {
            progress = (Time.time - initial_time) / duration_ease_sec;
            float eased_progress = ease_out.Evaluate(progress);
            hint_panel.anchoredPosition = Vector3.LerpUnclamped(hidden_pos, visible_pos, 1.0f - eased_progress);

            yield return null;
        }

        yield return new WaitForSeconds(2f);

        // done toasting
        toasting = false;
    }


    // check after 1 second whether the second key has been collected
    // if not, activate restart hint
    IEnumerator CheckCheckpoint()
    {
        yield return new WaitForSeconds(1f);
        if (third_key) {
            toasting = true;
            Debug.Log("neow");
            yield return StartCoroutine(GiveRestartHint(ease_duration, show_duration));
        }
        check_checkpoint_started = false;
    }


    public void ResetPanel()
    {
        toasting = false;
        check_checkpoint_started = false;
        hint_panel.anchoredPosition = hidden_pos;
        StopAllCoroutines();
    }

}
