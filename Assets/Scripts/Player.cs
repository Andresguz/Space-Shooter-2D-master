using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : NetworkBehaviour
{
    private float _speed;
   [SyncVar] public int ScoreP=0;
    public int numPlayer;
    private SpriteRenderer m_SpriteRenderer;
    public void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        //Set the GameObject's Color quickly to a set Color (blue)
      
    }

    
    [ClientRpc]
  public void scoreSearch(int i)
    {
        GameObject sc = GameObject.FindGameObjectWithTag("score"+numPlayer);
        sc.GetComponent<Text>().text = ScoreP.ToString();
    }
    [Server]
    public void addScore()
    {
        ScoreP++;
        scoreSearch(ScoreP);
    }
    void Update()
    {
        ShipMovement();
        Boundaries();
        if (numPlayer == 0)
        {
            m_SpriteRenderer.color = Color.yellow;
        }
        if (numPlayer == 1)
        {
            m_SpriteRenderer.color = Color.red;
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

}
