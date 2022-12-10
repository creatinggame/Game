using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using PixelCrushers.DialogueSystem;

public class test5: Item, IObservable, IInteractable
{
    //重写observing接口行为
    public void IObservingAction(GameObject player,GameObject gameObject){
        //初始化失败(比如物体被设为不可观察)
        if(!initialObservingAction(player, gameObject)){
            return;
        }
        Debug.Log("test5观察");
    }

    //重写互动接口行为
    public void IInteractingAction(GameObject player,GameObject gameObject){
        //初始化失败(比如物体被设为互动)
        if(!initialInteractingAction(player, gameObject)){
            return;
        }
        Debug.Log("test5互动");
    }
    void Awake(){
        initialClass();
    }

    void LateUpdate(){
        updateClass();
    }
}