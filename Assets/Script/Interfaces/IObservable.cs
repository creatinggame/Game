using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//第二个接口：当被character观察时的行为
public interface IObservable
{
    //这个接口会在project初始化的时候由project自身调用
    void IInitialObserve(objectInfo obj){
        //0是observe button
        Transform observebutton = obj.canvas.transform.GetChild(0);
        //激活button
        observebutton.gameObject.SetActive(true);
        //如果同时要显示interact界面，将其向左移,空出位置给interact
        if (obj.interactable){
            Vector3 observePos = obj.canvas.transform.position;
            observePos.x-=obj.intervalRange/2;
            observebutton.transform.position = observePos;
        }
    }
    //三种状态 free->observing->ending->free
    //观察开始
    void IObserveStart(GameObject player,GameObject gameObject){
        if(player.GetComponent<Basic>().state=="free"){
            player.GetComponent<Basic>().state ="observing";
            IObserveActing(player,gameObject);
        }
    }
    //观察结束
    void IObserveEnd(GameObject player,GameObject gameObject){
        if (player.GetComponent<Basic>().state == "observeEnding"){
                player.GetComponent<Basic>().state="free";
        }
    } 

    //观察进行
    void IObserveActing(GameObject player,GameObject gameObject){
        if(player.GetComponent<Basic>().state=="observing"){
            IObservingAction(player,gameObject);
        }
    }

    //观察行为：一般来说，这个是需要在不同的gameobject中重定义的部分
    //动作结束的时候，别忘了更新player的状态：player.GetComponent<Basic>().state = "ending";
    //不然，动作只会触发一次，之后没有办法再次触发
    void IObservingAction(GameObject player,GameObject gameObject){
        Debug.Log("观察了一次");
        player.GetComponent<Basic>().state = "observeEnding";
    }
}
