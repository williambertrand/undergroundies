using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (PlayerMovement.Instance.isCrouched)
        {
            PlayerHiding.Instance.isHidden = true;
        }
        else
        {
            PlayerHiding.Instance.isHidden = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerHiding.Instance.isHidden = false;
    }
}
