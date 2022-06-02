using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class EnemyAıAdvance : MonoBehaviour
{
    [Header("Pathfinding")]
    public GameObject Target;
    [SerializeField] float ActivateDistance = 30f;
    [SerializeField] float pathUpdateSec = .5f;

    [Header("Sound system")]
    private AudioSource ZombieSource;
    [SerializeField] AudioClip ZombieAttackSound;



    [Header("Physics")]
    [SerializeField] float Speed = 100f;
    [SerializeField] float NextWaypointDistance = 3f;
    [SerializeField] float JumpNodeHeightRequirement = .8f;
    [SerializeField] float JumpModifier = .3f;
    [SerializeField] float JumpCheckOffset = .1f;

    [Header("Custom Behavior")]
    public bool FollowEnabled = true;
    public bool JumpEnabled = true;
    public bool DirectionLookEnabled = true;

    private Path path;
    private int CurrentWaypoint = 0;
    bool İsGrounded = false;
    Seeker seeker;
    Rigidbody2D rb;
    Animator animator;
    AdventurerHealth adventurerHealth;
    private bool IsAttack = false;
    [SerializeField] Transform AttackPoint;
    public bool isdead = false;
    public bool isTakenDamage = false;
    public Enemy enemy;

    private void Awake()
    {

        ZombieSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        if (gameObject.GetComponent<Enemy>())
        {
            enemy = GetComponent<Enemy>();
        }

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        InvokeRepeating("UpdatePath", 0f, pathUpdateSec);
        adventurerHealth = FindObjectOfType<AdventurerHealth>();

        Target = GameObject.Find("Hero");
    }


    private void FixedUpdate()
    {

        if (TarGetInDistence() && FollowEnabled)
        {

            PathFollow();

        }

    }
    private void UpdatePath()
    {

        if (FollowEnabled && TarGetInDistence() && seeker.IsDone() && !IsAttack && !isdead && !isTakenDamage)
        {
            animator.SetBool("Run", true);
            seeker.StartPath(rb.position, Target.transform.position, OnPathComplete);

        }
        else
        {
            animator.SetBool("Run", false);
        }


        if (Vector2.Distance(transform.position, Target.transform.position) < 3f && !isdead && !isTakenDamage)
        {
            IsAttack = true;


            animator.Play("Attack1");
            ZombieSource.PlayOneShot(ZombieAttackSound);

        }
        else
        {

            IsAttack = false;
        }
    }

    private void PathFollow()
    {

        if (path == null)
        {

            return;

        }

        //pathbiterse

        if (CurrentWaypoint >= path.vectorPath.Count)
        {

            return;
        }

        İsGrounded = Physics2D.Raycast(transform.position, -Vector3.up, GetComponent<Collider2D>().bounds.extents.y + JumpCheckOffset);


        Vector2 direction = ((Vector2)path.vectorPath[CurrentWaypoint] - rb.position).normalized;
        Vector2 force = direction * Speed * Time.deltaTime * 100;

        //Jump

        if (JumpEnabled && İsGrounded)
        {
            if (direction.y > JumpNodeHeightRequirement)
            {
                rb.AddForce(Vector2.up * Speed * JumpModifier);

            }

        }
        //movemnt
        rb.AddForce(force);

        //nextwaypoint
        float Distance = Vector2.Distance(rb.position, path.vectorPath[CurrentWaypoint]);
        if (Distance < NextWaypointDistance)
        {
            CurrentWaypoint++;
        }

        if (DirectionLookEnabled)
        {

            if (rb.velocity.x > .05f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

            }
            else if (rb.velocity.x < -.05f)
            {

                transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

            }

        }


    }


    private bool TarGetInDistence()
    {

        return Vector2.Distance(AttackPoint.position, Target.transform.position) < ActivateDistance;

    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            CurrentWaypoint = 0;

        }


    }


    public void Attack()
    {
        if (Vector2.Distance(transform.position, Target.transform.position) < 3f && !enemy.TakingDamage)
        {
            adventurerHealth.TakeDamage(15);
            //Playerhealthtan can yakıcaz
        }
    }
}
