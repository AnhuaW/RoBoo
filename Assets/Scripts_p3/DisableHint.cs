using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableHint : MonoBehaviour
{
    public GameObject hint;
    public int target_clicks = 1;
    [SerializeField]
    private int curr_clikcs = 0;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && GetComponent<Floatable>().is_floating)
        {
            curr_clikcs += 1;
        }

        if(curr_clikcs == target_clicks)
        {
            TurnOffHint();
        }
    }


    void TurnOffHint()
    {
         GameObject.Destroy(hint);
            // disable the blinking script of brick
            if (GetComponent<Blinking>() != null)
            {
                GetComponent<Blinking>().enabled = false;
                StartCoroutine(SpriteTurnWhite(2f));
                //Debug.Log(GetComponent<SpriteRenderer>().color);
            }
            this.enabled = false;
    }


    IEnumerator SpriteTurnWhite(float duration)
    {
        float initial_time = Time.time;

        while (Time.time - initial_time < duration)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            yield return null;
        }
    }

}
