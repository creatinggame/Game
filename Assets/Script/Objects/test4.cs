using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
public class test4 : Item, ISelectable, IObservable, IInteractable
{
    //重写互动接口行为
    public void IInteractingAction(GameObject player,GameObject gameObject){
        //初始化失败(比如物体被设为不可互动)
        if(!initialInteractingAction(player, gameObject)){
            return;
        }
        //临时传送方案
        Vector3 tempPosition = player.transform.position;
        tempPosition.x -= 1000;
        player.transform.position = tempPosition;
    }
    void Awake(){
        initialClass();
    }

    void LateUpdate(){
        updateClass();
    }
}
