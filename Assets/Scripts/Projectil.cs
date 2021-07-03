using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectil : MonoBehaviour
{
    [SerializeField] private float speed;
    private IEnumerator corrotine;
    [SerializeField] private float damageBullet;
    

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);    
    }

    public void disableProjectil(float lifetime)
    {
        if (corrotine != null)
        {
            StopCoroutine(corrotine);
        }

        corrotine = timer(lifetime);
        StartCoroutine(timer(lifetime));
    }

    private IEnumerator timer(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tags.Enemy.ToString()))
        {
            other.GetComponent<Enemy>().TakeDamage(damageBullet);
            gameObject.SetActive(false);    
        }
    }
}
