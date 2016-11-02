using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private PooledObject prefab;

    public PooledObject GetObject()
    {
        PooledObject obj = Instantiate<PooledObject>(prefab);
        obj.transform.SetParent(transform, false);
        obj.Pool = this;
        return obj;
    }

    public void AddObject(PooledObject o)
    {
        Object.Destroy(o.gameObject);
    }
}