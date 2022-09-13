
using UnityEngine;

public class Spike : MonoBehaviour
{
    public float moveSpeed;
    public Vector2 minMaxYValue;
    Vector2 pos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   //Vector3.down = new Vector3(0,-1,0)
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;

        if (transform.position.y < minMaxYValue.y)
        {
            pos = transform.position;
            pos.x = transform.position.x;
            pos.y = minMaxYValue.x;
            transform.position = pos;
            ShowOrHide();
        }
    }
    void ShowOrHide()
    {
        if (Random.value > 0.5f)
        {
            Debug.Log("Disabling");
            transform.GetChild(0).gameObject.SetActive(false);
            return;
        }

        transform.GetChild(0).gameObject.SetActive(true);

    }
}
