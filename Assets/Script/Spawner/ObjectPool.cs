using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    public static ObjectPool SharedInstance;
    // public List<GameObject> pooledObjects;
    public Queue<GameObject> pooledObjects;
    public Queue<GameObject> usedObjects;
    public GameObject objectToPool;
    public int amountToPool;

    void Awake()
    {
        SharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // pooledObjects = new List<GameObject>();
        pooledObjects = new Queue<GameObject>();
        usedObjects = new Queue<GameObject>();
        GameObject tmp;
        for(int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            // pooledObjects.Add(tmp);
            pooledObjects.Enqueue(tmp);
        }
    }

    

    public GameObject GetPooledObject()
    {
        // for(int i = 0; i < amountToPool; i++)
        // {
        //     if(!pooledObjects[i].activeInHierarchy)
        //     {
        //         return pooledObjects[i];
        //     }
        // }
        Debug.Log("Pooled: " + pooledObjects.Count);
        if(pooledObjects.Count > 0)
        {
            GameObject tmp = pooledObjects.Dequeue();
            return tmp;
        }
        Debug.LogWarning("No object in pool");
        return null;
    }

    public GameObject DequeueUsedObject()
    {
        if(usedObjects.Count > 0)
        {
            GameObject tmp = usedObjects.Dequeue();
            return tmp;
        }
        return null;
    }

    public GameObject PeekUsedObject()
    {
        if(usedObjects.Count > 0)
        {
            GameObject tmp = usedObjects.Peek();
            return tmp;
        }
        return null;
    }

    public void EnqueueUsedObject(GameObject obj)
    {
        usedObjects.Enqueue(obj);
    }

    public void EnqueuePooledObject(GameObject obj)
    {
        pooledObjects.Enqueue(obj);
    }
    
    public int GetUnusedCount()
    {
        return pooledObjects.Count;
    }

    public int GetUsedCount()
    {
        return usedObjects.Count;
    }
}
