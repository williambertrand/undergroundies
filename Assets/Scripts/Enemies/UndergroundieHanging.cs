using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndergroundieHanging : Undergroundie
{

    Rigidbody2D rb;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnSense(int t)
    {
        rb.gravityScale = 1.5f;
        anim.SetTrigger("fall");
    }

    private void OnPlayerCollide()
    {
        // TODO animate attacking player
        StartCoroutine(SelfDestory());
    }

    private IEnumerator SelfDestory()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerLife.Instance.TakeDamage();
            //Destroy self
            OnPlayerCollide();
        }
        else
        {
            // Animate hitting ground
            StartCoroutine(SelfDestory());
            anim.SetTrigger("splat");
        }
        
    }
}
