using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Weapon : MonoBehaviour
{
    private InputMap m_inputActions;

    [SerializeField] private GameObject shoot;

    [SerializeField] private Transform spanwPoint;

    [SerializeField] private int poolSize;

    [SerializeField] private float lifetime;
    
    [SerializeField] private float fireRate;

    private float m_fireRateTimer;
    private bool m_shotPerformed;
    
    private Queue<GameObject> projectilPool;
    
   
    
    // Start is called before the first frame update
    void Awake()
    {
        m_inputActions = new InputMap();
        m_inputActions.Player.Shot.performed += ctx => shootPerformed(ctx);
        m_inputActions.Player.Shot.canceled += ctx => shootPerformed(ctx);
        projectilPool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject newShoot = Instantiate(shoot);
            projectilPool.Enqueue(newShoot);
            newShoot.SetActive(false);
        }
    }

    private void OnEnable() => m_inputActions.Enable();

    private void OnDisable() => m_inputActions.Disable();
    


    // Update is called once per frame
    void Update()
    {
        AjustRotation();
        //timer
        m_fireRateTimer += Time.deltaTime;
        if (m_fireRateTimer > fireRate && m_shotPerformed)
        {
            ShootProjectil();
            m_fireRateTimer = 0;
        }
    }

    private void AjustRotation()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        transform.rotation = rotation;
    }

    private void shootPerformed(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) m_shotPerformed = true;
        else m_shotPerformed = false;
    }
    
    private void ShootProjectil()
    {
        GameObject bullet = projectilPool.Dequeue();
        bullet.transform.position = spanwPoint.position;
        bullet.transform.rotation = transform.rotation;
        bullet.SetActive(true);
        bullet.GetComponent<Projectil>().disableProjectil(lifetime);
        projectilPool.Enqueue(bullet);
    }
}
