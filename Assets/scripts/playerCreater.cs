using UnityEngine;

public class playerCreater : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject[] players;
   
    void Awake()
    {
        players[gameController.playerIndex].SetActive(true); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
