using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum G_State
{
    Idle,
    Patrolling,
    Chasing,
    Null
}


public class Undergroundie : MonoBehaviour
{

    public Transform patrolLoc;
    public GameObject target;
    public Vector3 startPos;
    public Vector3 moveDest;
    public bool patrolSwitch;
    public G_State state;
    private G_State newState;

    public float moveSpeed;

    // Movement variables
    private readonly float reachDist = 0.4f;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    private Vector3 m_Velocity = Vector3.zero;
    bool m_FacingRight = true;

    // Idle state variables
    public float idleDone;

    // Attack related variables
    public float attackRange;
    public float lastAttack;
    public float attackTime;


    Animator anim;


    private Rigidbody2D m_Rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        state = G_State.Patrolling;
        newState = G_State.Null;
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if(target != null)
        {
            state = G_State.Chasing;
            moveDest = target.transform.position;
        }
        else if (patrolLoc != null)
        {
            moveDest = patrolLoc.position;
        }
    }

    void _handlePatrolState()
    {
        if(Vector2.Distance(transform.position, moveDest) < reachDist)
        {
            //Transition to idle
            idleDone = Time.time + Random.Range(1.5f, 3.0f);
            state = G_State.Idle;
            anim.SetFloat("move", 0);
            anim.SetTrigger("idle");
        }
    }

    void _handleIdleState()
    {
        if(Time.time > idleDone)
        {
            //Transition back to patrolling
            if (patrolSwitch)
            {
                moveDest = patrolLoc.position;
            }
            else
            {
                moveDest = startPos;
            }
            patrolSwitch = !patrolSwitch;
            state = G_State.Patrolling;
        }
    }

    void _handleChaseState()
    {
        if(target !=null)
        {
            moveDest = target.transform.position;
        }
        else
        {
            moveDest = PlayerMovement.Instance.transform.position;
        }
        if (Vector2.Distance(PlayerMovement.Instance.transform.position, transform.position) < attackRange)
        {
            if(Time.time > lastAttack + attackTime)
            {
                Attack();
            }
        }
    }

    public void SetNewState(G_State n)
    {
        newState = n;
    }


    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case G_State.Patrolling:
                Move();
                _handlePatrolState();
                break;
            case G_State.Chasing:
                Move();
                _handleChaseState();
                break;
            case G_State.Idle:
                _handleIdleState();
                break;
        }
        if(newState != G_State.Null && target == null)
        {
            state = newState;
            newState = G_State.Null;
        }
    }

    void Move()
    {
        // Move the character by finding the target velocity
        float dX = moveDest.x - transform.position.x > 0 ? moveSpeed : -1 * moveSpeed;
        Vector3 targetVelocity = new Vector2(dX, m_Rigidbody2D.velocity.y);
        anim.SetFloat("move", Mathf.Abs(dX));
        // And then smoothing it out and applying it to the character
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

        if (dX > 0 && m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (dX < 0 && !m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(moveDest, 0.2f);
    }

    public void TestOnSense(int t)
    {
        Debug.Log("Received sense: " + t);
    }


    
    public void Attack()
    {
        PlayerLife.Instance.TakeDamage();
        lastAttack = Time.time;
    }


}
