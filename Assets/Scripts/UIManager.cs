using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartLevelText;
    private GameManager _gameManager;


    //\\\\\\\
    [SerializeField]
    private Image _livesImg2;
    [SerializeField]
    private Text _score2Text;
    [SerializeField]
    private Sprite[] _liveSprites2;

    public Player player;
    public Player2 player2;
    public Text Winner;
    
    void Start()
    {
        if (_gameManager == null)
        {
            //Debug.LogError("Game Manager is null!");
        }
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _scoreText.text = "Score: " + 0;
        //_score2Text.text = "Score: " + 0;

        _gameOverText.gameObject.SetActive(false);
}

    void Update()
    {

        //if (_gameManager._isGameOver==true)
        //{
        //    if (player._score > player2._score2)
        //    {
        //        Winner.text = "PLAYER 1" + " ".ToString();
        //        //aux = player._score;
        //    }
        //    else
        //    {
        //        Winner.text = "PLAYER 2" + " " .ToString();
        //    }
        //}
       
     
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore;
    }

    public void UpdateScore2(int playerScore)
    {
        _score2Text.text = "Score: " + playerScore;
    }
    public void UpdateLives(int currentLives)
    {
        _livesImg.sprite = _liveSprites[currentLives];
        if (currentLives == 0)
        {
            GameOverSequence();
        }
    }
    public void UpdateLives2(int currentLives)
    {
        _livesImg2.sprite = _liveSprites2[currentLives];
        if (currentLives == 0)
        {
            GameOverSequence();
        }
    }

    void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameOverText.gameObject.SetActive(true);
        _restartLevelText.gameObject.SetActive(true);
        StartCoroutine(FlickerGameOver());
    }

    IEnumerator FlickerGameOver()
    {
        while (true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.75f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.75f);
        }
    }

}
