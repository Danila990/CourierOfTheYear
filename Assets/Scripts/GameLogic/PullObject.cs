using System.Collections.Generic;
using UnityEngine;

public class PullObject : MonoBehaviour
{
    public List<GameObject> GeneratePullObject(int countPullObject, GameObject[] objectPrafabs)
    {
        Transform parent = new GameObject("PullObject " + gameObject.name).transform;
        List<GameObject> _objectList = new List<GameObject>(countPullObject * objectPrafabs.Length);
        for (int i = 0; i < objectPrafabs.Length; i++)
        {
            for (int j = 0; j < countPullObject; j++)
            {
                GameObject spawnedObject = Instantiate(objectPrafabs[i], parent);
                spawnedObject.SetActive(false);
                _objectList.Add(spawnedObject);
            }
        }
        return _objectList;
    }
}
