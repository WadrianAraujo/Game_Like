using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusEnemy : Enemy
{
    [SerializeField] private float minDistance2Player;
    [SerializeField] private float attackSpeed;
    private bool isAttacking;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (isAttacking || Player_pos == null)
        {
            rb2d.velocity = Vector2.zero;
            return;
        }
    
        if ((transform.position - Player_pos.position).sqrMagnitude >= Mathf.Pow(minDistance2Player, 2))
        {
            Vector2 direction = (Player_pos.position - transform.position).normalized * speed;
            rb2d.velocity = direction;
        }
        else
        {
            if (Time.time > AttackDelayTimer)
            {
                AttackDelayTimer = Time.time + AttackDelay;
                StartCoroutine(ChargeAttack());
                isAttacking = true;
            }
            else
            {
                rb2d.velocity = Vector2.zero;
            }
        }
    
    }

    IEnumerator ChargeAttack()
    {
        
        isAttacking = true;
        Vector2 originalPos = transform.position;
        Vector2 targetPos = Player_pos.position;

        float percent = 0;
        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float form = (-Mathf.Pow(percent, 2) + percent) * 4;
            rb2d.position = Vector2.Lerp(originalPos, targetPos, form);
            yield return null;
        }

        isAttacking = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(tags.Player.ToString()))
        {
            other.gameObject.GetComponent<Entity>().TakeDamage(damage);
        }
    }
}



