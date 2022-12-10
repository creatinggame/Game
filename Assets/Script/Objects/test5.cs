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
        Debug.Log("test5观察");
        player.GetComponent<Basic>().state = "observeEnding";
    }

    //重写互动接口行为
    public void IInteractingAction(GameObject player,GameObject gameObject){
        Debug.Log("test5互动");
        player.GetComponent<Basic>().state = "interactEnding";
    }
    void Awake(){
        initialClass();
    }

    void LateUpdate(){
        updateClass();
    }
}