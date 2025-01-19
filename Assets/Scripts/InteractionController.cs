using UnityEngine;
using TMPro;

public class InteractionController : MonoBehaviour
{
    public TextMeshProUGUI interactionText;
    public GameObject objectToSpawn;
    public Transform spawnLocation;

    private bool isNearObject = false;
    private GameObject spawnedObject;  // lights off object

    void Update()
    {
 
        if (isNearObject && Input.GetKeyDown(KeyCode.Q))
        {
            if (spawnedObject == null)
            {
                
                SpawnObject();
            }
            else
            {
                
                DestroyObject();
            }
        }

       
        if (isNearObject)
        {
            interactionText.text = "Press here / key Q to interact";
        }
        else
        {
            interactionText.text = "";
        }
    }

    public void SpawnObject()
    {
        
        spawnedObject = Instantiate(objectToSpawn, spawnLocation.position, Quaternion.identity);
    }

    public void DestroyObject()
    {
       
        Destroy(spawnedObject);
        spawnedObject = null;  // clear reference
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNearObject = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNearObject = false;
        }
    }
}