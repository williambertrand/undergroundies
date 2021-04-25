using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHelper : MonoBehaviour
{
    
    public List<Undergroundie> spawnGroundies;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            foreach (Undergroundie g in spawnGroundies)
            {
                g.gameObject.SetActive(true);
            }
        }
    }
}
