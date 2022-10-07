using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    public static ObjectPool SharedInstance;
    public List<GameObject> pooledObjects;
    // public Queue<GameObject> usedObjects;
    public GameObject objectToPool;
    public int amountToPool;

    void Awake()
    {
        SharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        pooledObjects = new List<GameObject>();
        // usedObjects = new Queue<GameObject>();
        GameObject tmp;
        for(int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    // void FixedUpdate(){
    //     GameObject temp;
    //     while(usedObjects.Count > 0 && usedObjects.Peek().activeInHierarchy && usedObjects.Peek().transform.position.x <= -10){
    //         Debug.Log("Used: " + usedObjects.Count);
    //         temp = usedObjects.Dequeue();
    //         Debug.Log("after dequeue: " + usedObjects.Count);
    //         temp.SetActive(false);
    //         pooledObjects.Enqueue(temp);
    //     }
    // }

    public GameObject GetPooledObject()
    {
        for(int i = 0; i < amountToPool; i++)
        {
            if(!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        // if(pooledObjects.Count > 0)
        // {
        //     GameObject tmp = pooledObjects.Dequeue();
        //     tmp.SetActive(true);
        //     usedObjects.Enqueue(tmp);
        //     return tmp;
        // }
        Debug.Log("out of notes");
        return null;
    }
    
}
