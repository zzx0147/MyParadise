using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance = null;
    public static T Instance
    {
        get
        {
            if(!instance)
            {
                instance = FindObjectOfType(typeof(T)) as T;
                if (!instance)
                {
                    instance = new GameObject("@" + typeof(T).ToString(), typeof(T)).AddComponent<T>();
                }

            }
            return instance;
        }
    }
}