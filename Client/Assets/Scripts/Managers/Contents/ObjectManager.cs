using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager
{
    List<GameObject> _objects = new List<GameObject>();

    public void Add(GameObject go)
    {
        _objects.Add(go);
    }

    public void Remove(GameObject go)
    {
        _objects.Remove(go);
    }

    public GameObject Find()
    {
        foreach (GameObject obj in _objects)
        {
            CreatureController cc = obj.GetComponent<CreatureController>();
            if (cc == null)
                continue;
            //TODO
        }
        return null;
    }

    public GameObject Find(Func<GameObject, bool> condition)
    {
        foreach (GameObject obj in _objects)
        {
            if (condition.Invoke(obj))
                return obj;
        }

        return null;
    }

    public void Clear()
    {
        _objects.Clear();
    }
}
