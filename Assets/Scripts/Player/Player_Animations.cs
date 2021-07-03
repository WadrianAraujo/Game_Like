using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animations : Player
{
    private Animator m_animator;

    private enum Facing
    {
        up,
        down,
        left,
        right
    }

    private Facing m_facing;

    private string m_CurrentAnimation;
    
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        m_animator = GetComponent<Animator>();
        player_rigidbody = GetComponent<Rigidbody2D>();
        m_facing = Facing.down;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeAnim();
    }

    private void ChangeAnim()
    {
        if (player_rigidbody.velocity == Vector2.zero)
        {
            if (m_facing == Facing.down) PlayAnimation("Idle_front");
            else if(m_facing == Facing.up) PlayAnimation("Idle_top");
            else if(m_facing == Facing.left) PlayAnimation("Idle_left");
            else if(m_facing == Facing.right) PlayAnimation("Idle_right");
        }
        else
        {
            if (player_rigidbody.velocity.x > 0.1f)
            {
                PlayAnimation("Right_run");
                m_facing = Facing.right;
            }
            else if (player_rigidbody.velocity.x < -0.1f)
            {
                PlayAnimation("Left_run");
                m_facing = Facing.left;
            }
            else if (player_rigidbody.velocity.y > 0.1f)
            {
                PlayAnimation("Top_run");
                m_facing = Facing.up;
            }else if (player_rigidbody.velocity.y < -0.1f)
            {
                PlayAnimation("Front_run");
                m_facing = Facing.down;
            }
        }
    }

    private void PlayAnimation(string animationName)
    {
        if (m_CurrentAnimation == animationName) return;

        m_CurrentAnimation = animationName;
        m_animator.Play(animationName);

    }


}
