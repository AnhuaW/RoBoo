using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// when player is out of ammo continuously for 
// <wait_seconds> seconds, prompt restart hint
public class RestartHint : MonoBehaviour
{
    Inventory_tmp inventory;
    RectTransform hint_panel;
    bool toasting = false;

    [SerializeField] Vector3 hidden_pos, visible_pos;
    [SerializeField] float wait_seconds = 30f;
    [SerializeField] float rest_seconds = 10f;
    [SerializeField] float ease_duration = 0.5f;
    [SerializeField] float show_duration = 2f;
    [SerializeField] AnimationCurve ease;
    [SerializeField] AnimationCurve ease_out;

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory_tmp.instance;
        hint_panel = GetComponent<RectTransform>();
        hidden_pos = new Vector3(-330, -150, 0);
        visible_pos = new Vector3(-330, 250 - 150, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (inventory.GetBubbleAmmo() <= 0 && !toasting)
        {
            toasting = true;
            StartCoroutine(WaitAndGiveHint());
        }
    }


    IEnumerator WaitAndGiveHint()
    {
        // check for <wait_seconds> if player is out of ammo
        float initial_time = Time.time;
        while (Time.time - initial_time < wait_seconds)
        {
            if (inventory.GetBubbleAmmo() > 0)
            {
                toasting = false;
                yield break;
            }
            yield return null;
        }
        yield return StartCoroutine(GiveRestartHint(ease_duration, show_duration));

    }

    IEnumerator GiveRestartHint(float duration_ease_sec, float duration_show_sec)
    {

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

        // done toasting
        toasting = false;
    }

}
