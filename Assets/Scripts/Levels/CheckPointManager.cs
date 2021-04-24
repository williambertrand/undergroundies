using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{

    public static CheckPointManager Instance;
    Checkpoint last;

    private void Awake()
    {
        if (Instance == null) Instance = this;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateTo(Checkpoint cp)
    {
        this.last = cp;
    }

    public void Reset()
    {
        // TODO: Re-Set all enemies to start?
        PlayerMovement.Instance.gameObject.transform.position = last.spawnPos.position;
        Camera.main.transform.position = last.spawnPos.position;
    }
}
