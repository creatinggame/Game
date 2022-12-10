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
        //trigger因为是挂载在test1上的，所有player和gameObject的信息是必须的
        //不然只会弹出一段对话，而不会进行test1中的trigger中的一些默认操作
        //因为那种情况DialogueManager不知道是谁发起的对话
        DialogueManager.StartConversation("test1",player.transform,gameObject.transform);
        player.GetComponent<Basic>().state = "observeEnding";
    }

    //重写互动接口行为
    public void IInteractingAction(GameObject player,GameObject gameObject){
        //临时传送方案
        Vector3 tempPosition = player.transform.position;
        tempPosition.x += 1000;
        player.transform.position = tempPosition;
        player.GetComponent<Basic>().state = "interactEnding";
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
        initialClass();
    }

    void LateUpdate(){
        updateClass();
    }
}

