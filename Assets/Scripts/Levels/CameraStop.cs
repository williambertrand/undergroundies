using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStop : MonoBehaviour
{

    // Disable camera following when player enters trigger zone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Stopping camera");
            Debug.Log(Camera.main);
            Camera.main.GetComponent<CameraFollow>().target = null;
        }
    }

}
