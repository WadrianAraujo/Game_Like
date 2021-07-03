using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealf : Entity
{
    [SerializeField] private float healf;
    // Start is called before the first frame update
    void Awake()
    {
        m_CurrentHealf = healf;
    }
    
}
