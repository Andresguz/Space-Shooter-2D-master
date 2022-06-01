using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class Asteroid : NetworkBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 19.0f;
    [SerializeField]
    private GameObject _explosionPrefab;
    private SpawnManager _spawnManager;


    public override void OnStartServer()
    {
        base.OnStartServer();
        //_spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        //_spawnManager.StartSpawning();
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
          
            Destroy(this.gameObject, 0.25f);
        }
        //if (other.tag == "Laser2")
        //{
        //    Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        //    Destroy(other.gameObject);
        //  //  _spawnManager.StartSpawning();
        //    Destroy(this.gameObject, 0.25f);
        //}

    }

}
