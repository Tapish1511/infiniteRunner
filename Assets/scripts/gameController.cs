using UnityEngine;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject plateForm;

    public static int playerIndex = 0;
    [SerializeField]
    GameObject[] players;

   
    void Start()
    {
        Time.timeScale = 1;
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        Instantiate(players[playerIndex], plateForm.transform.position, new Quaternion(0, 135, 0, 45));      
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        DontDestroyOnLoad(gameObject);
        
        SceneManager.LoadScene(1);

    }

    public void SelectPlayer()
    {
        SceneManager.LoadScene(2);
    }

}
