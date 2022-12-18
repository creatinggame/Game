using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
public class test3Black : Item, ISelectable, IInteractable
{
    public void IInteractingAction(GameObject player,GameObject gameObject){
        //初始化失败(比如物体被设为不可互动)
        if(!initialInteractingAction(player, gameObject)){
            return;
        }
        BagItems bagItems = GameObject.Find("BagSystem").GetComponent<BagItems>();
        if(bagItems.items.Count!=0){
            InteractInterface.IRemove(obj,bagItems.holdItemIndex);
        }
    }
    void Awake(){
        interactName = "按E\n消除最后的物品";
        pickable = false;
        initialClass();
    }

    void LateUpdate(){
        updateClass();
    }
}
