using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    Subscription<StageStatus> stage_status_subscription;

    // Start is called before the first frame update
    void Start()
    {
        stage_status_subscription = EventBus.Subscribe<StageStatus>(_OnStageChanges);
    }

    // Update is called once per frame
    void Update()
    {

    }


    void _OnStageChanges(StageStatus e)
    {
        if (SceneManager.GetActiveScene().buildIndex != e.level_index)
        {
            SceneManager.LoadScene(e.level_index);
        }
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe(stage_status_subscription);
    }
}

public class StageStatus
{
    public int level_index;
    public StageStatus(int _level_index) { level_index = _level_index; }
}
