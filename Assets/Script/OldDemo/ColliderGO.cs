
using UnityEngine;

public class ColliderGO : MonoBehaviour
{
    public Transform PlayerTransform;
    public Transform RingTransform;

    public void CallPlayerMethod()
    {
        Debug.Log(PlayerTransform.name);
        PlayerTransform.GetComponent<Player>().CallThisMethod();
    }
    
    public void CallRingMethod()
    {
        Debug.Log(RingTransform.name);
        RingTransform.GetComponent<Ring>().CallThisMethod();
    }
}
