using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


using Mirror;
public class GameManager : NetworkBehaviour
{
    public static GameManager instance;
  //  public int score;
  //  public Text scoreText;
    //[SerializeField]
    //public bool _isGameOver;

    private void Start()
    {
      //  scoreText.text=score.ToString(); 
    }
    void Update()
    {
       // scoreText.text = score.ToString();
        //    if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        //    {
        //        SceneManager.LoadScene(1); //Current Game Scene
        //    }
    }

    //public void GameOver()
    //{
    //    _isGameOver = true;
    //}

}
