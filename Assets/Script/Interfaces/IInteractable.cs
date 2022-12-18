using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//第三个接口：当被character选择后的互动选项
public interface IInteractable
{
    void IInitialInteract(objectInfo obj){
        //1 是interact button
        Transform interactbutton = obj.canvas.transform.GetChild(1);
        //激活canvas
        interactbutton.gameObject.SetActive(true);
        //如果同时要显示observe界面，将其向右移,空出位置给interact
        if (obj.observable){
            Vector3 observePos = obj.canvas.transform.position;
            observePos.x+=obj.intervalRange/2;
            interactbutton.transform.position = observePos;
        }
        //修改物体传送的方式名
        interactbutton.gameObject.GetComponentInChildren<TMP_Text>().text = obj.interactName;
    }

    //用于被拾取时的动作
    void IPick(objectInfo obj){
        //获取背包系统中的bagItems class; 其中包含变量items，是一个储存了gameObject的列表
        BagItems bagItems = GameObject.Find("BagSystem").GetComponent<BagItems>();
        //将被拾取的物体添加进列表
        bagItems.items.Add(obj.gameObj);
        //在地图上把物体设为不激活
        obj.gameObj.SetActive(false);
        
    }
    //用于删除背包中的某个物品
    void IRemove(objectInfo obj,int num){
        //获取背包系统中的bagItems class; 其中包含变量items，是一个储存了gameObject的列表
        BagItems bagItems = GameObject.Find("BagSystem").GetComponent<BagItems>();
        //按序列将一个物体移除出背包
        bagItems.items.RemoveAt(num);
        
    }

    //三种状态 free->observing->ending->free
    //互动开始
    void IInteractStart(GameObject player,GameObject gameObject){
        if(player.GetComponent<Basic>().state=="free"){
            player.GetComponent<Basic>().state ="interacting";
            IInteractActing(player,gameObject);
        }
    }
    //互动结束
    void IInteractEnd(GameObject player,GameObject gameObject){
        if (player.GetComponent<Basic>().state == "interactEnding"){
            player.GetComponent<Basic>().state="free";
        }
    } 

    //互动进行
    void IInteractActing(GameObject player,GameObject gameObject){
        if(player.GetComponent<Basic>().state=="interacting"){
            IInteractingAction(player,gameObject);
        }
    }

    //互动行为：一般来说，这个是需要在不同的gameobject中重定义的部分
    //动作结束的时候，别忘了更新player的状态：player.GetComponent<Basic>().state = "ending";
    //不然，动作只会触发一次，之后没有办法再次触发
    void IInteractingAction(GameObject player,GameObject gameObject){
        Debug.Log("互动了一次");
        player.GetComponent<Basic>().state = "interactEnding";
    }
}
