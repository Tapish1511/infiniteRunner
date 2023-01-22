using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class selectChar : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] characters;
    public Rigidbody[] bodies;
    bool[] unlocked;
    

    [SerializeField]
    Button leftBtn, rightBtn, selectBtn;

    [SerializeField]
    Sprite yellowSp, greenSp;

    Text txt;
    int selected = 0;
    int current = 0;
    void Start()
    {
        selected = gameController.playerIndex;
        characters = new GameObject[transform.childCount];
        bodies = new Rigidbody[transform.childCount];
        for(int i=0; i<transform.childCount; i++)
        {
            characters[i] = transform.GetChild(i).gameObject;
            characters[i].SetActive(i == selected);
            bodies[i] = characters[i].GetComponent<Rigidbody>();

        }

        current = selected;
        txt = selectBtn.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (current == selected)
        {
            selectBtn.image.sprite = greenSp;
            selectBtn.image.color = Color.green;
            txt.text = "Selected";
            txt.color = new Color(0.4313726f, 0.8666667f, 0);
                
            txt.GetComponent<Outline>().effectColor = new Color(0.05405472f, 0.6886792f, 0.01559272f);

        }
        else
        {
            selectBtn.image.sprite = yellowSp;
            selectBtn.image.color = Color.yellow;
            txt.text = "Select";
            txt.color = new Color(1, 0.6588235f, 0);
            txt.GetComponent<Outline>().effectColor = new Color(1, 0.4627451f, 0);
        }
    }

    public void SelectChar()
    {
        selected = current;
        gameController.playerIndex = selected;       
    }

    public void exitSelection()
    {
        SceneManager.LoadScene(0);
    }

    public void next()
    {

        if (current < characters.Length - 1)
        {
            leftBtn.interactable = true;
            current++;
            characters[current].SetActive(true);
            Vector3 position_curr = bodies[current].position;
           
            position_curr = new Vector3(0, position_curr.y, position_curr.z);

            Vector3 position_prev = new Vector3(-5, position_curr.y, position_curr.z);
            Vector3 position_next = new Vector3(5, position_curr.y, position_curr.z);
            bodies[current].position = position_next;

            StartCoroutine(swapLeft(current, position_curr, position_prev));
            characters[current - 1].SetActive(false);
            StopCoroutine(swapLeft(current, position_curr, position_prev));
           
        }
        if (current == characters.Length - 1)
        {
            rightBtn.interactable = false;
        }
        else
        {
            rightBtn.interactable = true;
        }

    }

    public void previous()
    {
        if(current >= 1)
        {
            rightBtn.interactable = true;
            current--;
            characters[current].SetActive(true);
            Vector3 position_curr = bodies[current].position;
            position_curr = new Vector3(0, position_curr.y, position_curr.z);
            Vector3 position_prev = new Vector3(5, position_curr.y, position_curr.z);
            Vector3 position_next = new Vector3(-5, position_curr.y, position_curr.z);
            bodies[current].position = position_next;

            StartCoroutine(swapRight(current, position_curr, position_prev));
            characters[current + 1].SetActive(false);
            StopCoroutine(swapLeft(current, position_curr, position_prev));
        }

        if(current == 0)
        {
            leftBtn.interactable = false;
        }
        else
        {
            leftBtn.interactable = true;
        }
       
        
    }

   

    IEnumerator swapLeft(int index, Vector3 position_curr,  Vector3 position_prev)
    {
        
        while (!bodies[index].position.x.Equals(0))
        {
            
            Vector3 position_next = bodies[index].position;
            Vector3 position_prev1 = bodies[index - 1].position;
            
            bodies[index].position = Vector3.Lerp(position_next, position_curr, 5 * Time.deltaTime);
            bodies[index-1].position = Vector3.Lerp(position_prev1, position_prev, 5 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator swapRight(int index, Vector3 position_curr, Vector3 position_prev)
    {

        while (!bodies[index].position.x.Equals(0))
        {

            Vector3 position_next = bodies[index].position;
            Vector3 position_prev1 = bodies[index + 1].position;
          
            bodies[index].position = Vector3.Lerp(position_next, position_curr, 5 * Time.deltaTime);
            bodies[index + 1].position = Vector3.Lerp(position_prev1, position_prev, 5 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }


}
