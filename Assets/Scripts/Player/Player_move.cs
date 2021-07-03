using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_move : Player
{
    private InputMap m_input;

    private Vector2 m_direction;

    [SerializeField] private float move_speed;
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        m_input = new InputMap();
        m_input.Player.Movement.performed += ctx => m_direction = ctx.ReadValue<Vector2>();
        m_input.Player.Movement.canceled += ctx => m_direction = Vector2.zero;
    }

    private void OnEnable()
    {
        m_input.Enable();
    }

    private void OnDisable()
    {
        m_input.Disable();
    }

    private void FixedUpdate()
    {
        player_rigidbody.velocity = m_direction * move_speed;
    }
}
