using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class Enemy : NetworkBehaviour
{
    [SerializeField]
    private float _enemySpeed = 4.0f;
    private Player _player;
//    private Player2 _player2;
    public Animator _animator;
    private AudioSource _explSrc;
    private bool _isHit = false;

    public override void OnStartServer()
    {
        base.OnStartServer();
        _explSrc = GetComponent<AudioSource>();
        transform.position = new Vector3(Random.Range(-8.0f, 8.0f), 7.5f, 0);
        _player = GameObject.Find("Player").GetComponent<Player>();        
        _animator = GetComponent<Animator>();
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

    [TargetRpc]
    public void DeadExplosion()
    {
        _animator.SetTrigger("OnEnemyDeath"); //trigger anim 
    }
   // [ServerCallback]
    private void OnTriggerEnter2D(Collider2D other)
    {
       // if (_isHit == false)
        {
            _isHit = true;

           // Player player = other.transform.GetComponent<Player>();
           
            if (other.tag == "Player")
            {
                DeadExplosion();

                // _animator.SetTrigger("OnEnemyDeath"); //trigger anim 
                _enemySpeed = 0;
              //  _explSrc.Play();
                Destroy(this.gameObject, 2.5f);
            }


            if (other.tag == "Laser")
            {
                DeadExplosion();
                Destroy(other.gameObject);
                if (_player != null)
                {
                    //_player.AddScore(5);
                }
          
                //  _animator.SetTrigger("OnEnemyDeath"); //trigger anim
                _enemySpeed = 0;
                // _explSrc.Play();
                Destroy(this.gameObject, 2.5f);
            }

        }

    }
}
