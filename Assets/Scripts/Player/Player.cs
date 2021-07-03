using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    protected Rigidbody2D player_rigidbody;
    // Start is called before the first frame update
    protected virtual  void Awake()
    {
        player_rigidbody = GetComponent<Rigidbody2D>();
    }
}
