using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private Dictionary<string, List<GameObject>> _listPool = new Dictionary<string, List<GameObject>>();
    
    public GameObject CheckPool(GameObject prefab)
    {
        if (_listPool.Count < 0 || !_listPool.ContainsKey(prefab.name))
        {
            List<GameObject> tmpList = new List<GameObject>();

            _listPool.Add(prefab.name, tmpList);

            return GetFromPool(tmpList, prefab);
        }
        else
        {
            _listPool.TryGetValue(prefab.name, out var existList);

            return GetFromPool(existList, prefab);
        }
    }

    private GameObject GetFromPool(List<GameObject> objPool, GameObject instance)
    {
        if (objPool.Count != 0)
        {
            foreach (var pref in objPool)
            {
                if (!pref.activeInHierarchy)
                    return pref;
            }
        }
        
        var newObject = Instantiate(instance);
        objPool.Add(newObject);


        return newObject;
    }

    public void Reset()
    {
        _listPool = new Dictionary<string, List<GameObject>>();
    }
}