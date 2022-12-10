using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//第三个接口：当被character选择后的互动选项
public interface IInteractable
{
    void IInitialInteract(objectInfo obj){
        //1 是interact button
        Transform observebutton = obj.canvas.transform.GetChild(1);
        //激活canvas
        observebutton.gameObject.SetActive(true);
        //如果同时要显示observe界面，将其向右移,空出位置给interact
        if (obj.observable){
            Vector3 observePos = obj.canvas.transform.position;
            observePos.x+=obj.intervalRange/2;
            observebutton.transform.position = observePos;
        }
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
