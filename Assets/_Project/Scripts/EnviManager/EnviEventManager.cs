// File: EnviEventManager.cs
using System.Collections.Generic;
using UnityEngine;

public class EnviEventManager : MonoBehaviour
{
    private static EnviEventManager _instance;
    public static EnviEventManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<EnviEventManager>();
                if (_instance == null)
                {
                    GameObject manager = new GameObject("EnviEventManager");
                    _instance = manager.AddComponent<EnviEventManager>();
                }
            }
            return _instance;
        }
    }

    private Dictionary<string, List<IEnviEventListener>> listeners = new Dictionary<string, List<IEnviEventListener>>();

    public void RegisterListener(string eventName, IEnviEventListener listener)
    {
        if (!listeners.ContainsKey(eventName))
        {
            listeners[eventName] = new List<IEnviEventListener>();
        }
        listeners[eventName].Add(listener);
    }

    public void UnregisterListener(string eventName, IEnviEventListener listener)
    {
        if (listeners.ContainsKey(eventName))
        {
            listeners[eventName].Remove(listener);
            if (listeners[eventName].Count == 0)
            {
                listeners.Remove(eventName);
            }
        }
    }

    public void RaiseEvent(string eventName)
    {
        if (listeners.ContainsKey(eventName))
        {
            foreach (var listener in listeners[eventName])
            {
                listener.OnEventRaised(eventName);
            }
        }
    }
}
