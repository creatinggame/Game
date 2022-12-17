using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
public class test3 : Item, ISelectable, IInteractable
{
    public void IInteractingAction(GameObject player,GameObject gameObject){
        //初始化失败(比如物体被设为不可互动)
        if(!initialInteractingAction(player, gameObject)){
            return;
        }
        if(pickable){
            gameObject.SetActive(false);
        }
    }
    void Awake(){
        interactName = "Pick\nPress E";
        pickable = true;
        initialClass();
    }

    void LateUpdate(){
        updateClass();
    }
}
