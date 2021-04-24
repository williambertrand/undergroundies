using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    #region Singleton
    public static PlayerAnimation Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    #endregion
    Animator anim;
    public float lastMove;
    public int numIdle;

    float randIdleTime;
    bool isidle;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        randIdleTime = Random.Range(1.5f, 3.7f);
        numIdle = 2; // Set to number of different idle animations
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInput.Instance.crouch)
        {
            anim.SetBool("crouch", true);
        }
        else
        {
            anim.SetFloat("move", Mathf.Abs(PlayerInput.Instance.horizontalMove));
            anim.SetBool("crouch", false);
        }
        if(PlayerInput.Instance.horizontalMove == 0 && !isidle)
        {
            lastMove = Time.time;
            isidle = true;
        }

        if (isidle && Time.time - lastMove > randIdleTime)
        {
            //set random idle anim
            int r = Random.Range(1, numIdle + 1);
            anim.SetInteger("idle", r);
            randIdleTime = Random.Range(2.5f, 4.7f);
            lastMove = Time.time;
        }
        else
        {
            anim.SetInteger("idle", 0);
        }
    }

    public void FlashDamage()
    {
        anim.SetTrigger("TakeDamage");
    }
}
