using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShotPlayer : NetworkBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    float nexFire = 0;
    public GameObject helth;
    public bool isInGame = false;
    public float firerate = 0.5f;
    [SyncVar] public int health = 100;
    TextMesh textHealt;
    public Animator anim;
    public Slider valor;
    // Start is called before the first frame update
    void Start()
    {
     //textHealt = helth.GetComponent<TextMesh>();

    }

    // Update is called once per frame
    [ClientRpc]
    void Update()
    {
        valor.value = health;
      //  textHealt.text = health.ToString();
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

    [ClientRpc]
    void RpcShoot()
    {
        anim.SetTrigger("shoot");
    }

    [TargetRpc]
    void TargetLoadGameOver()
    {
        SceneManager.LoadScene(0);
    }

    [Command]
    void Shoot()
    {
        GameObject projectile = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        NetworkServer.Spawn(projectile);
        RpcShoot();
    }

    [ServerCallback]
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            health-=10;
            if (health == 0)
            {
                TargetLoadGameOver();
                NetworkServer.Destroy(gameObject);

            }
        }
    }
}

