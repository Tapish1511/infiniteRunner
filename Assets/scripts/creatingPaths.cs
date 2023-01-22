using UnityEngine;

public class creatingPaths : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] paths;
    public float zPos = 245;
    void Start()
    {
       /* trns = path.transform;*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("half"))
        {
            int index = Random.Range(0, paths.Length);
            zPos += 500;
            Instantiate(paths[index], new Vector3(0, 0, zPos), new Quaternion());
        }
    }

   
}
