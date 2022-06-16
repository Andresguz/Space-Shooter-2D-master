using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
[RequireComponent(typeof(Animator))]
public class Enemy : NetworkBehaviour
{
    [SerializeField]
    private float _enemySpeed = 4.0f;
//  private Player _manager;
//    private Player2 _player2;
    public Animator _animator;
    public GameObject hitPrefab;
    public Transform hitTransform;
    //  private AudioSource _explSrc;
    // private bool _isHit = false;

     void Start()
    {

      
        transform.position = new Vector3(Random.Range(-8.0f, 8.0f), 7.5f, 0);
      // _manager = GameObject.Find("Player").GetComponent<Player>();        
  
    }

    void Update()
    {
        if (isServer)
        {
            Vector3 enemyMovement = Vector3.down * _enemySpeed * Time.deltaTime;
            transform.Translate(enemyMovement);
            Vector3 respawnTop = new Vector3(Random.Range(-8.0f, 8.0f), 7.5f, 0);
            if (transform.position.y <= -5.5f)
            {
                // transform.position = respawnTop;
                NetworkServer.Destroy(gameObject);
            }
        }
      
    }

   [ClientRpc]
     void DeadExplosion()
    {

       // _animator.SetBool("f",true); //trigger anim 
        GameObject hitD = Instantiate(hitPrefab , hitTransform);
        NetworkServer.Spawn(hitD);
    }

    [ServerCallback]
    private void OnTriggerEnter2D(Collider2D other)
    {

           
            if (other.tag == "Player")
            {
            _animator.SetBool("f", true);
              DeadExplosion();
                _enemySpeed = 0;
            NetworkServer.Destroy(gameObject);
            }


            if (other.tag == "Laser")
            {

      
      //   GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().addScore();
              DeadExplosion();
            NetworkServer.Destroy(other.gameObject);
             
          
                //  _animator.SetTrigger("OnEnemyDeath"); //trigger anim
                _enemySpeed = 0;
                Destroy(this.gameObject, 2.5f);
            }

        

    }
}
