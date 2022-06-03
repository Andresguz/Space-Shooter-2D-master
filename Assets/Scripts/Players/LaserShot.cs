using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LaserShot : NetworkBehaviour
{
    public float speed = 20f;
    public float bulletLife = 3;
    public int damage = 40;
    public Rigidbody2D rb;
    public GameObject impactEffect;

    void Start()
    {
        rb.velocity = transform.up * speed;

    }

   
    [Server]
    void DestroySelf()
    {
        NetworkServer.Destroy(gameObject);
    }
    public override void OnStartServer()
    {
        //Invoke(nameof(DestroySelf), bulletLife);
    }
    private void Update()
    {
        if (transform.position.y > 7.5f)
        {
            DestroySelf();
        }
    }
    [ServerCallback]
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.CompareTag("Enemy"))
        {
            NetworkServer.Destroy(hitInfo.gameObject);
        }
       // NetworkServer.Destroy(gameObject);
      
    }
    [ServerCallback]
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            NetworkServer.Destroy(collision.gameObject);
            DestroySelf();
        }
        //NetworkServer.Destroy(gameObject);
        /*  if (collision.transform.tag == "wall")
          {
              Instantiate(impactEffect, transform.position, transform.rotation);
              Destroy(gameObject);
          }*/
    }
}
