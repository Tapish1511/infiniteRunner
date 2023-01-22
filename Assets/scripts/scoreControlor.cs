using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scoreControlor : MonoBehaviour
{
    // Start is called before the first frame update
    public Text score;
    public Text coin;
    public Text highScore;

    public Text finalScore;
    public Text finalCoins;
    public Text finalHighScore;
    public Text wish;

    public Text pausedScore;
    public Text pausedCoins;
    public Text pausedHighScore;

    public static int _score = 0;
    public static int _coin = 0;
    public static int _highScore = 0;
    public static int _prevScore = 0;
    public static bool isAlive = true;


    [SerializeField]
    Animator[] Child;

    void Start()
    {
        Time.timeScale = 1;
        _prevScore = _highScore;
        _score = 0;
        _coin = 0;
        isAlive = true;
        highScore.text = _highScore.ToString();
        Child = gameObject.GetComponentsInChildren<Animator>();
        /*Child[2].SetTrigger("close");*/
      
    }

    // Update is called once per frame
    void Update()
    {
       
        if(_score > _highScore)
        {
            _highScore = _score;
            highScore.text = _highScore.ToString();
        }
        score.text = _score.ToString();
        coin.text = _coin.ToString();

        pausedScore.text = _score.ToString();
        pausedCoins.text = _coin.ToString();
        pausedHighScore.text = highScore.text;

        if (!isAlive)
        {
            diedActions();
        }

        
    }

    public void diedActions()
    {
        StartCoroutine(diedPannel());
        finalCoins.text = _coin.ToString();
        finalScore.text = _score.ToString();
        finalHighScore.text = _highScore.ToString();

        if(_score > _prevScore)
        {
            wish.text = "Congratulations!\nyou created a new High Score";
            wish.color = Color.green;
        }
        else
        {
            wish.text = "Better luck for the next time!";
            wish.color = Color.red;
        }

    }

    public void retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void goHome()
    {
        SceneManager.LoadScene(0);
       
    }

    public void pause()
    {
        Time.timeScale = 0;
    }

    public void resume()
    {
        Time.timeScale = 1;
    }


    IEnumerator diedPannel()
    {
        yield return new WaitForSeconds(.5f);
        Child[0].SetBool("died", true);       
    }
}
