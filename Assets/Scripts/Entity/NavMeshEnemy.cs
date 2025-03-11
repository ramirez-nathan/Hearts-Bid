using Scripts.Hand;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshEnemy : Entity
{
    Animator animator;
    protected NavMeshAgent agent;
    protected Transform playerTarget;
    [SerializeField] int maxHealth = 10;
    [SerializeField] GameObject canvas;
    public override void Die()
    {
        base.Die();
        canvas.transform.SetParent(null, true);
        canvas.GetComponent<Canvas>().enabled = true;
        canvas.GetComponentInChildren<HandDisplay>().gameObject.SetActive(false);
        canvas.GetComponentInChildren<HealthBar>().gameObject.SetActive(false);
        Destroy(canvas, 3);

        //TODO: play animations here
    }

    public override void TakeHit(int damage)
    {
        base.TakeHit(damage);

        //TODO: play animations here
        StartCoroutine(HurtRoutine(0.5f));
    }

    IEnumerator HurtRoutine(float duration)
    {
        GetComponent<Animator>().SetBool("Hurt", true);
        yield return new WaitForSeconds(duration);
        GetComponent<Animator>().SetBool("Hurt", false);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerTarget = GameObject.Find("Player").transform;
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false; //this may be needed for looking towards player
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTarget != null)
        {
           agent.SetDestination(playerTarget.position);
        }

        Animator animator = GetComponent<Animator>();
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();

        animator.SetFloat("x", agent.velocity.x);
        animator.SetFloat("z", agent.velocity.y);
    }

}
