using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClone : MonoBehaviour
{
    [SerializeField]
    private GameObject originPlayer;
    private GameObject copyPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)){
            copyPlayer = Instantiate(originPlayer, new Vector3(0,0,0), Quaternion.identity);
        }
    }
}
