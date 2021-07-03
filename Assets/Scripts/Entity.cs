using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected float m_CurrentHealf;
    
    public virtual void TakeDamage(float damageRecive)
    {
        m_CurrentHealf -= damageRecive;
        if (m_CurrentHealf <= 0)
        {
            StartDeath();
        }
    }
    
    protected virtual void StartDeath()
    {
        Destroy(gameObject);
    }
}
