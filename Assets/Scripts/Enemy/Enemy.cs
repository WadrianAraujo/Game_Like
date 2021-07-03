using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] protected float speed;

    [SerializeField] protected float maxHealf;

    [SerializeField] protected float damage;

    protected Transform Player_pos;

    protected Rigidbody2D rb2d;
    protected Animator m_animator;
    [SerializeField]
    protected float AttackDelay;
    protected float AttackDelayTimer;
    
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        Player_pos = GameObject.FindGameObjectWithTag(tags.Player.ToString()).transform;
        m_CurrentHealf = maxHealf;
        m_Anim = AnimList.walk_down;
    }
    
    private enum AnimList
    {
        walk_up,
        walk_down,
        walk_left,
        walk_right
    }

    private AnimList m_Anim;
    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        AnimateEnemy();
    }

    private void AnimateEnemy()
    {
        if (Mathf.Abs(rb2d.velocity.x) > Mathf.Abs(rb2d.velocity.y))
        {
            if (rb2d.velocity.x > 0.1f)
            {
                ChangeAnimation(AnimList.walk_right);
            }
            else if (rb2d.velocity.x < -0.1f)
            {
                ChangeAnimation(AnimList.walk_left);
            }
        }
        else
        {
            if (rb2d.velocity.y > 0.1f)
            {
                ChangeAnimation(AnimList.walk_up);
            }
            else if (rb2d.velocity.y < -0.1f)
            {
                ChangeAnimation(AnimList.walk_down);
            }
        }
    }

    private void ChangeAnimation(AnimList newAnim)
    {
        
        if (m_Anim == newAnim)
        {
            return;
        }
        m_Anim = newAnim;
        m_animator.Play(newAnim.ToString());
    }
}
