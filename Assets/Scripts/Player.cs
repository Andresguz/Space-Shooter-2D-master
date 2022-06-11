using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : NetworkBehaviour
{
    private float _speed;
   [SyncVar] public int ScoreP;
    public int numPlayer;


    void Start()
    {
 
       
        
    }
    [ClientRpc]
  public void scoreSearch()
    {
        GameObject sc = GameObject.FindGameObjectWithTag("score0");
        sc.GetComponent<Text>().text = ScoreP.ToString();
    }
    [ServerCallback]
    public void addScore()
    {
        ScoreP++;
    }
    void Update()
    {
        ShipMovement();
        Boundaries();
        scoreSearch();
        if (Input.GetButtonDown("Fire1") )
        {
      //   addScore();
        }
  

    }

    void Boundaries()
    {
        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -3.4f)
        {
            transform.position = new Vector3(transform.position.x, -3.4f, 0);
        }
        //optimized way of the code above
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.4f, 0), 0);
        //cannot be clamped we need to move left and right to wrap
        if (transform.position.x >= 11.25)
        {
            transform.position = new Vector3(-11.25f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.25)
        {
            transform.position = new Vector3(11.25f, transform.position.y, 0);
        }
    }


    void ShipMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 dirMovement = new Vector3(horizontalInput, verticalInput, 0);
        //if speedboost enabled move at 8.5f speed duration 5s
        _speed = 5.0f;
      
        transform.Translate(dirMovement * _speed * Time.deltaTime);


    }


    
    //public void AddScore(int points)
    //{
    //    _score += points;
    //    _uiManager.UpdateScore(_score);
    //}

}
