using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickChecker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            if (hit)
            {   
                if (hit.transform.name == "wholescreen")
                {
                    hit.collider.GetComponent<ColliderGO>().CallPlayerMethod();
                    hit.collider.GetComponent<ColliderGO>().CallRingMethod();
                }
                /*
                if (hit.transform.name == "LeftSide")
                {
                    hit.collider.GetComponent<ColliderGO>().CallPlayerMethod();
                }
                else if (hit.transform.name == "RightSide")
                {
                   
                    hit.collider.GetComponent<ColliderGO>().CallPlayerMethod();
                }
                */
                else { Debug.Log("Unknown hit from click Checker"); return; }
            }
        }
    }
}
