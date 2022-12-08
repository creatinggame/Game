using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//定义了一个class，封装了一个object应该有的信息
//这样某个物体在使用接口的时候就不用传入很多离散的变量
public class objectInfo
{
    //物体的transform信息
    public GameObject gameObj;
    //是否可以被选择，被观测和被互动
    public bool selectable;
    public bool observable;
    public bool interactable;
    //每个object会绑定一个canvas，用来弹出互动按钮
    public Canvas canvas;
    //显示互动按钮之间的距离
    public float intervalRange;
    //canvas在物体之上多高
    public float canvasHight;
    //用来给物体加高光的组件信息
    public Outline outlineComponent;
    //构造函数:按照上面定义的顺序传入
    public objectInfo(GameObject go,bool s,bool o,bool i,Canvas c, float ir, float ch, Outline oc)
    {
        gameObj = go;
        selectable = s;
        observable = o;
        interactable = i;
        canvas = c;
        intervalRange = ir;
        canvasHight = ch;
        outlineComponent = oc;

    }
}
