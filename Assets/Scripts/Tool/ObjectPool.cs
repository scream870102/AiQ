using System.Collections;
using System.Collections.Generic;

using UnityEngine;
[System.Serializable]
public class ObjectPool {
    [Header ("ObjectPool")]
    /// <summary>which object to spawn</summary>
    public GameObject pooledObject;
    /// <summary>all poolObjects should belong to which transform</summary>
    public Transform poolParent;
    /// <summary>how many item can pool hold</summary>
    public int pooledAmount;
    /// <summary>if user ask an item and pool is empty now Can pool instaniate an new item</summary>
    public bool IsGrow;
    public bool IsExist { get { return poolObjects.Count > 0; } }
    public int Capacity { get { return poolObjects.Count; } }
    // To Store all item
    Queue<IObjectPoolItem> poolObjects = new Queue<IObjectPoolItem> ( );

    /// <summary>use this to set NECESSARY data</summary>
    /// <remarks>Not necessary to use constructor  you can also set data through inspector</remarks>
    public ObjectPool (GameObject pooledObject, Transform poolParent, int pooledAmount = 1, bool IsGrow = true) {
        this.pooledObject = pooledObject;
        this.poolParent = poolParent;
        this.pooledAmount = pooledAmount;
        this.IsGrow = IsGrow;
        Init ( );
    }

    /// <summary>Spawn all objects according to pooledAmount</summary>
    /// <remarks>MUST CALL this method if you set data with inspector not constructor</remarks>
    public void Init ( ) {
        poolObjects = new Queue<IObjectPoolItem> ( );
        for (int i = 0; i < pooledAmount; i++) {
            poolObjects.Enqueue (SpawnObject ( ));
        }
    }

    /// <summary>Return IObjectPoolItem</summary>
    /// <remarks>Need to convert to type which user need
    /// e.g. Bullets inherit from Mono,IObjectPoolItem
    /// if we ask for a bullet we need to convert type to bullet</remarks>
    /// <example><code>Bullet bullet = Bullets.GetPooledObject ( ) as Bullet;</code></example>
    public IObjectPoolItem GetPooledObject ( ) {
        if (poolObjects.Count != 0) {
            IObjectPoolItem item = poolObjects.Dequeue ( );
            item.Init ( );
            item.gameObject.SetActive (true);
            return item;
        }
        if (IsGrow) {
            IObjectPoolItem item = SpawnObject ( );
            item.Init ( );
            item.gameObject.SetActive (true);
            return item;
        }
        return null;
    }

    /// <summary>Recycle Object to Pooling again</summary>
    /// <param name="item">which item will be Recycle to ObjectPooling</param>
    public void RecycleObject (IObjectPoolItem item) {
        poolObjects.Enqueue (item);
        item.gameObject.SetActive (false);
    }

    // Spawn Object and set its pool change its parent to poolParent then disable it
    IObjectPoolItem SpawnObject ( ) {
        IObjectPoolItem item = GameObject.Instantiate (pooledObject, poolParent).GetComponent<IObjectPoolItem> ( );
        item.Pool = this;
        item.gameObject.SetActive (false);
        return item;
    }
}
