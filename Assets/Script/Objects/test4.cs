using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
public class test4 : Item, ISelectable, IObservable, IInteractable
{
    //重写互动接口行为
    public void IInteractingAction(GameObject player,GameObject gameObject){
        //临时传送方案
        Vector3 tempPosition = player.transform.position;
        tempPosition.x -= 1000;
        player.transform.position = tempPosition;
        player.GetComponent<Basic>().state = "interactEnding";
    }
    void Awake(){
        initialClass();
    }

    void LateUpdate(){
        updateClass();
    }
}
