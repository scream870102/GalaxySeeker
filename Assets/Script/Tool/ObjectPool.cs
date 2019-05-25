using System.Collections;
using System.Collections.Generic;

using UnityEngine;
[System.Serializable]
public class ObjectPool {
    [Header ("ObjectPool")]
    public GameObject pooledObject;
    public Transform poolParent;
    public int pooledAmount;
    public bool IsGrow;

    Queue<ObjectPoolItem> poolObjects;
    public void Init ( ) {
        poolObjects = new Queue<ObjectPoolItem> ( );
        for (int i = 0; i < pooledAmount; i++) {
            poolObjects.Enqueue (SpawnObject ( ));
        }
    }

    public ObjectPoolItem GetPooledObject ( ) {
        if (poolObjects.Count != 0) {
            ObjectPoolItem item = poolObjects.Dequeue ( );
            item.Init ( );
            item.gameObject.SetActive (true);
            return item;
        }
        if (IsGrow) {
            ObjectPoolItem item = SpawnObject ( );
            item.Init ( );
            item.gameObject.SetActive (true);
            return item;
        }
        return null;
    }

    public void RecycleObject (ObjectPoolItem item) {
        poolObjects.Enqueue (item);
        item.gameObject.SetActive (false);
    }

    ObjectPoolItem SpawnObject ( ) {
        ObjectPoolItem item = GameObject.Instantiate (pooledObject).GetComponent<ObjectPoolItem> ( );
        item.transform.parent = poolParent;
        item.pool = this;
        item.gameObject.SetActive (false);
        return item;
    }
}
