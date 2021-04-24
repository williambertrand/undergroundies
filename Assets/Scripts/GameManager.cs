using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public bool isGameOver = false;
    public bool isPaused = false; //p2

    #region Singleton
    public static GameManager Instance;
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }


    #endregion Singleton

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
