
using UnityEngine;

public class ColliderGO : MonoBehaviour
{
    public Transform PlayerTransform;

    public void CallPlayerMethod()
    {
        Debug.Log(PlayerTransform.name);
        PlayerTransform.GetComponent<Player>().CallThisMethod();
    }
}
