
using UnityEngine;

public class ColorChooser : MonoBehaviour
{
    public GameObject[] imagesGO;
    public ColorCombinatoin[] chooseColor;
    // Start is called before the first frame update
    void Awake()
    {
        int randomNo = Random.Range(0, chooseColor.Length );
        imagesGO[0].GetComponent<SpriteRenderer>().color = chooseColor[randomNo].ColorToChoose[0];
        imagesGO[1].GetComponent<SpriteRenderer>().color = chooseColor[randomNo].ColorToChoose[1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
