using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager
{
    private static EventManager instance;

    public static EventManager Instance
    {
        get
        {
            if (instance == null) instance = new EventManager();
            return instance;
        }
    }

    public Dictionary<string, UnityAction> actionDic= new Dictionary<string, UnityAction>();

    //添加事件
    public void AddEventListener(string name,UnityAction action)
    {
        //如果有这个事件
        if (actionDic.ContainsKey(name))
        {
            actionDic[name] += action;
        }
        else
        {
            actionDic.Add(name, action);
        }
    }
    //移除事件
    public void RemoveEventListener(string name, UnityAction action)
    {
        //如果有这个事件
        if (actionDic.ContainsKey(name))
        {
            actionDic[name] -= action;
        }
    }
    //触发事件
    public void TriggerEventListener(string name)
    {
        //如果有这个事件
        if (actionDic.ContainsKey(name))
        {
            actionDic[name]?.Invoke();
        }
    }
    //清空事件
    public void Clean()
    {
        actionDic.Clear();
    }
}
