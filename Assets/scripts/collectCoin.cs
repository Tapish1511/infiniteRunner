using UnityEngine;

public class collectCoin : MonoBehaviour
{
    // Start is called before the first frame update
    Animator coinAnime;
    string collect = "collect";
    
    private void Start()
    {
        coinAnime = gameObject.GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag.Equals("Player"))
        {
            scoreControlor._coin += 1;   
            coinAnime.SetBool(collect, true);
        }
    }
    public void finish()
    {
        Destroy(gameObject);
    }
}
