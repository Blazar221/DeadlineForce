using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class GoalPanel : MonoBehaviour
{
    public static GoalPanel instance;
    [SerializeField] private Sprite[] items;
    [SerializeField] private Image firstGem;
    [SerializeField] private Image secondGem;
    [SerializeField] private Image thirdGem;
    [SerializeField] private Image upgradeItem;
    
    private Color blue = new Color(0.01568625f, 0.1250286f, 0.9921569f, 1.0f);
    private Color green = new Color(0.4305087f, 0.8207547f, 0.2516465f, 1.0f);
    private Color cyan = new Color(1.0f, 1.0f, 1.0f, 1.0f);

    private int[] formulas = {0,1,2};

    // 0: blue*3 = knife
    // 1: green*3 = shield
    // 2: cyan*3 = mine
    // 3: cyan*2 + green*1 = sword

    
    private int itemIndex = 0;
    private int gemIndex = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        setGemsAndItem();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void setGemsAndItem()
    {
        switch (formulas[itemIndex])
        {
            case 0:
            // 0: blue*3 = knife
                firstGem.GetComponent<Image>().color = blue;
                secondGem.GetComponent<Image>().color = blue;
                thirdGem.GetComponent<Image>().color = blue;
                upgradeItem.GetComponent<Image>().sprite = items[0];
                break;
            case 1:
                // 1: green*3 = shield
                firstGem.GetComponent<Image>().color = green;
                secondGem.GetComponent<Image>().color = green;
                thirdGem.GetComponent<Image>().color = green;
                upgradeItem.GetComponent<Image>().sprite = items[1];
                break;
            case 2:
                // 2: cyan*3 = mine
                firstGem.GetComponent<Image>().color = cyan;
                secondGem.GetComponent<Image>().color = cyan;
                thirdGem.GetComponent<Image>().color = cyan;
                upgradeItem.GetComponent<Image>().sprite = items[2];
                break;
            case 3:
                // 3: cyan*2 + green*1 = sword
                firstGem.GetComponent<Image>().color = cyan;
                secondGem.GetComponent<Image>().color = cyan;
                thirdGem.GetComponent<Image>().color = green;
                upgradeItem.GetComponent<Image>().sprite = items[3];
                break;
            default:
                break;
        }

        itemIndex++;
    }
}
