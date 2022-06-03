using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class Enemy : NetworkBehaviour
{
    [SerializeField]
    private float _enemySpeed = 4.0f;
    //private Player _player;
//    private Player2 _player2;
    public Animator _animator;
    //  private AudioSource _explSrc;
    // private bool _isHit = false;

    public override void OnStartServer()
    {
        base.OnStartServer();
        //  _explSrc = GetComponent<AudioSource>();
        transform.position = new Vector3(Random.Range(-8.0f, 8.0f), 7.5f, 0);
       // _player = GameObject.Find("Player").GetComponent<Player>();        
     //   _animator = GetComponent<Animator>();
    }
    void Update()
    {
        Vector3 enemyMovement = Vector3.down * _enemySpeed * Time.deltaTime;
        transform.Translate(enemyMovement);
        Vector3 respawnTop = new Vector3(Random.Range(-8.0f, 8.0f), 7.5f, 0);
        if (transform.position.y <= -5.5f)
        {
            transform.position = respawnTop;
        }
    }

    [ClientRpc]
     void DeadExplosion()
    {
        Debug.Log("eer");
        _animator.SetBool("f",true); //trigger anim 
    }
    [ServerCallback]
    private void OnTriggerEnter2D(Collider2D other)
    {
       

           // Player player = other.transform.GetComponent<Player>();
           
            if (other.tag == "Player")
            {
            _animator.SetBool("f", true);
          //  DeadExplosion();
            
                // _animator.SetTrigger("OnEnemyDeath"); //trigger anim 
                _enemySpeed = 0;
            //  _explSrc.Play();
            NetworkServer.Destroy(gameObject);
            }


            if (other.tag == "Laser")
            {
            // Debug.Log("eer");
            //_animator.SetBool("f", true);
            //    DeadExplosion();
            NetworkServer.Destroy(other.gameObject);
             
          
                //  _animator.SetTrigger("OnEnemyDeath"); //trigger anim
                _enemySpeed = 0;
                // _explSrc.Play();
                Destroy(this.gameObject, 2.5f);
            }

        

    }
}
