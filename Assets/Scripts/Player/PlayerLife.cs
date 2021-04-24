using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{

    public static PlayerLife Instance;
    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }

    }

    public int maxHealth;
    public int health;

    void Start()
    {
        health = maxHealth;
        
    }

    public void TakeDamage()
    {
        health -= 1;
        PlayerAnimation.Instance.FlashDamage();
        if (health == 0)
        {
            // TODO: PlayerAnimation.Instance.Death();
            GameManager.Instance.GameOver();
        }
    }

    private void Reset()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
