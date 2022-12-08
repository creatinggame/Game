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
}
