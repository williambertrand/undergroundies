using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class OnSenseEvent : UnityEvent<int>
{
}

[RequireComponent(typeof(BoxCollider2D))]
public class SensePlayer : MonoBehaviour
{
    Undergroundie behavior;
    public OnSenseEvent onSense; 

    // Start is called before the first frame update
    void Start()
    {
        behavior = GetComponentInParent<Undergroundie>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //TODO: add some kind of OnPlayerSensed funtion that will then put the
    // undergroundie in whatever state it should be in based on its type
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            if(PlayerHiding.Instance.isHidden)
            {
                Debug.Log("Player hidden, not sensing...");
                return;
            }
            onSense.Invoke(1);
            behavior.SetNewState(G_State.Chasing);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (behavior.state == G_State.Chasing) return;

        if (collision.gameObject.CompareTag("Player"))
        {

            if (PlayerHiding.Instance.isHidden)
            {
                return;
            }
            onSense.Invoke(1);
            behavior.SetNewState(G_State.Chasing);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(behavior.state == G_State.Chasing)
            {
                // Todo: onLostSense.Invoke(1);
                behavior.SetNewState(G_State.Idle);
            }
        }
    }
}
