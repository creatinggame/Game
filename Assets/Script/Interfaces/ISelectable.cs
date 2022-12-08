using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//第一个接口：当被character选择时的行为
public interface ISelectable
{  
    //这些是面向玩家的接口：所有面向玩家的接口应该在game object中重写
    //这样做的好处是玩家脚本可以直接调用而不需要输入额外的参数（比如ISelectedActive里的那些）
    void ISelecte();
    void IDisSelecte();
    //这些是面向物体的接口：这些接口不需要重写，但是所有参数需要在物品的脚本里被初始化
    void ISelectedActive(objectInfo obj){
        //添加高光
        obj.outlineComponent.OutlineWidth = 2f;
        //显示canvas
        obj.canvas.enabled = true;
    }
     void IDisSelecteActive(objectInfo obj){
        //高光为0
        obj.outlineComponent.OutlineWidth = 0f;
        //隐藏canvas
        obj.canvas.enabled = false;
    }
    void IInitialSelect(objectInfo obj){
        //激活canvas
        obj.canvas.transform.gameObject.SetActive(true);
    }
    void IUpdateSelect(objectInfo obj, Camera mainCam){
        var rotation = mainCam.transform.rotation;
        //获取当前物体的最高点
        Vector3 tempPos = obj.gameObj.transform.position;
        //获得物体在y轴的最高点，加上偏移高度
        tempPos.y = obj.gameObj.GetComponent<Collider>().bounds.max.y+obj.canvasHight;
        obj.canvas.transform.position = tempPos;
        //使canvas始终面对着相机
        obj.canvas.transform.LookAt(tempPos + rotation * Vector3.forward,rotation*Vector3.up);
    }
}