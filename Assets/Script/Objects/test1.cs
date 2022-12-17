using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using PixelCrushers.DialogueSystem;
public class test1 : Item, ISelectable, IObservable, IInteractable
{
    //重写observing接口行为
    public void IObservingAction(GameObject player,GameObject gameObject){
        //初始化失败(比如物体被设为不可观察)
        if(!initialObservingAction(player, gameObject)){
            return;
        }
        DialogueManager.StartConversation("test1",player.transform,gameObject.transform);
    }

    //重写互动接口行为
    public void IInteractingAction(GameObject player,GameObject gameObject){
        //初始化失败(比如物体被设为不可互动)
        if(!initialInteractingAction(player, gameObject)){
            return;
        }
        //临时传送方案
        Vector3 tempPosition = player.transform.position;
        tempPosition.x += 1000;
        player.transform.position = tempPosition;
    }

    //用来测试的函数，能把test1变红
    public void TurnTest1ToRed(){
        gameObject.GetComponent<MeshRenderer>().sharedMaterial.color = Color.red;
    }
    //用来测试的函数，能把test1变蓝
    public void TurnTest1ToBlue(){
        gameObject.GetComponent<MeshRenderer>().sharedMaterial.color = Color.blue;
    }
    void Awake(){
        interactName = "Travel\nPress E";
        initialClass();
    }

    void LateUpdate(){
        updateClass();
    }
}

