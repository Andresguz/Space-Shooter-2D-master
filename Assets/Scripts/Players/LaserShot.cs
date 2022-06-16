using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LaserShot : NetworkBehaviour
{
    public float speed = 20f;
    public float bulletLife = 3;
    public int damage = 40;
    public Rigidbody2D rb;
    public GameObject impactEffect;
    public GameObject adds;
    //  private GameManager game;

    public override void OnStartServer()
    {
        base.OnStartServer();
        
        rb.velocity = transform.up * speed;

    }

   
    [Server]
    void DestroySelf()
    {
        NetworkServer.Destroy(gameObject);
    }
   
    [ClientRpc]
    void DeadExplosion()
    {

        // _animator.SetBool("f",true); //trigger anim 
        GameObject hitD = Instantiate(impactEffect, gameObject.transform);
        NetworkServer.Spawn(hitD);
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
            DeadExplosion();
            NetworkServer.Destroy(hitInfo.gameObject);
            //GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().addScore();
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().addScore();
       // adds.GetComponent<Player>().addScore();
            Debug.Log("ASDASDSA");
        }
       // NetworkServer.Destroy(gameObject);
      
    }
    [ServerCallback]
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //    game.score++;
            // GameManager.instance.score++;
            //GameObject hitD=   Instantiate(impactEffect, collision.gameObject.transform);
            //    NetworkServer.Spawn(hitD);
           
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
