using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : NetworkBehaviour
{
    private float _speed;
    [SerializeField]
 
    
    private int _lives = 3;
   
   //
   // private UIManager _uiManager;


    void Start()
    {
 
       // transform.position = new Vector3(0, 0, 0);
        //_uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
     
        //if (_uiManager == null)
        //{
        //    Debug.LogError("UI Manager is null!");
        //}
       
        
    }

    void Update()
    {
        ShipMovement();
        Boundaries();
   

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
