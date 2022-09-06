
using UnityEngine;

public class Dot : MonoBehaviour
{
    public float moveSpeed;
    public Vector2 minMaxXValue;
    Vector2 pos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (transform.position.x < minMaxXValue.x)
        {
            pos = transform.position;
            pos.y = transform.position.y;
            pos.x = minMaxXValue.y;
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
