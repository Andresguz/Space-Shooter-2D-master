using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotPlayer : NetworkBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    float nexFire = 0;
    public GameObject helth;
    public bool isInGame = false;
    public float firerate = 0.5f;
    [SyncVar] public int health = 4;
    TextMesh textHealt;
    // Start is called before the first frame update
    void Start()
    {
        textHealt = helth.GetComponent<TextMesh>();

    }

    // Update is called once per frame
    void Update()
    {
        textHealt.text = health.ToString();
        if (isLocalPlayer)
        {
            if (isInGame)
            {
                if (Input.GetButtonDown("Fire1") && Time.time > nexFire)
                {
                    nexFire = Time.time + firerate;
                    Invoke("Shoot", 0.2f);
                }

            }
        }
    }
    [Command]
    void Shoot()
    {
        GameObject projectile = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        NetworkServer.Spawn(projectile);
    }

    [ServerCallback]
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<LaserShot>() != null)
        {
           /// --health;
            if (health == 0)
                NetworkServer.Destroy(gameObject);
        }
    }
}

