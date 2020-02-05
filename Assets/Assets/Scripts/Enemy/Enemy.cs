using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public GameObject coinPrefab;
    [SerializeField]
    protected int health;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected int coins;
    [SerializeField]
    protected Transform pointA, pointB;

    protected Vector3 currentTarget;
    protected Animator anim;
    protected SpriteRenderer sprite;
    protected bool isDead = false;

    protected bool isHit = false;

    protected Player player;

    //Combat Mode
    [SerializeField]
    protected float playerDistance;

    public virtual void Init()
    {
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        try
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
        catch (Exception err)
        {
            // leave blank
        }
    }

    private void Start()
    {
        Init();
    }

    public virtual void Update()
    {
        try
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && !anim.GetBool("InCombat"))
            {
                return;
            }

            if (!isDead)
            {
                //keep calculating the distance between player and enemy
                playerDistance = Vector3.Distance(this.transform.position, player.transform.position);

                if (PlayerPrefs.GetInt("IS_DIED") == 0)
                {
                    if (playerDistance > 2)
                    {
                        anim.SetBool("InCombat", false);

                        // if the animation that is currently playing isn't Walk, do nothing
                        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
                        {
                            return;
                        }

                        Movement();
                    }
                    else if (playerDistance <= 2)
                    {
                        CombatMode();
                    }
                }
                else
                {
                    anim.SetBool("InCombat", false);

                    // if the animation that is currently playing isn't Walk, do nothing
                    if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
                    {
                        return;
                    }

                    Movement();
                }
            }
        }
        catch (Exception err)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            // leave blank
        }
    }

    public virtual void Movement()
    {
        if (currentTarget == pointA.position)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }

        if (transform.position == pointA.position)
        {
            currentTarget = pointB.position;
            anim.SetTrigger("Idle");
        }
        else if (transform.position == pointB.position)
        {
            currentTarget = pointA.position;
            anim.SetTrigger("Idle");
        }

        if (!isHit)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
        }

        float distance = Vector3.Distance(transform.localPosition, player.transform.localPosition);
        if (distance > 4.0f)
        {
            isHit = false;
            anim.SetBool("InCombat", false);
        }

        Vector3 direction = player.transform.localPosition - transform.localPosition;

        if (anim.GetBool("InCombat"))
        {
            if (direction.x > 0)
            {
                sprite.flipX = false;
            }
            else if (direction.x < 0)
            {
                sprite.flipX = true;
            }
        }
    }

    //Activate the Combat Mode
    public void CombatMode()
    {
        anim.SetBool("InCombat", true);

        //Flip the sprite to the player direction
        if (this.transform.position.x > player.transform.position.x)
            sprite.flipX = true;
        else if (this.transform.position.x < player.transform.position.x)
            sprite.flipX = false;

    }
}