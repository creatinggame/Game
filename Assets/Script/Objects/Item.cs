using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using PixelCrushers.DialogueSystem;
public class Item : MonoBehaviour, ISelectable
{
    //所有函数的信息可以在objectInfo类中找到，这里就不重复了
    [SerializeField] public Canvas canvas;
    [SerializeField] public bool selectable;
    [SerializeField] public bool observable;
    [SerializeField] public bool interactable;
    [SerializeField] public float intervalRange = 3f;
    [SerializeField] public float canvasHight = 2f;
    public Outline outlineComponent;
    public Camera mainCam;
    public ISelectable SelectInterface; 
    public IObservable ObserveInterface;
    public IInteractable InteractInterface;
    public objectInfo obj;
    //进行一些初始化
    public void initialMain(){
        //放置canvas，如果可以select才会在Iinitialselect中激活
        canvas = Instantiate(canvas);
        canvas.transform.gameObject.SetActive(false);
        //和激活不同，这行代码是让canvas保持关闭：可以看成前者是断电，后者是关闭按钮
        canvas.enabled = false;
        //设置主相机位置，用来让canvas方向始终面向主相机
        mainCam = Camera.main;
        //获取outlineComponent的属性
        outlineComponent = gameObject.GetComponent<Outline>();
        //在最开始的时候关掉高光
        outlineComponent.OutlineWidth = 0f;
    }
    //初始化接口
    public void initialInterafce(){
        if(selectable){
            SelectInterface = gameObject.GetComponent<ISelectable>();
            SelectInterface.IInitialSelect(obj);
        }
        if(observable){
            ObserveInterface = gameObject.GetComponent<IObservable>();
            ObserveInterface.IInitialObserve(obj);
        }
        if(interactable){
            InteractInterface = gameObject.GetComponent<IInteractable>();
            InteractInterface.IInitialInteract(obj);
        }
    }
    //将函数的信息封装入objctInfo class,方便调用
    public void buildObjInfo(){
        obj = new objectInfo(gameObject, selectable, observable, interactable,
        canvas, intervalRange, canvasHight, outlineComponent);
    }
    
    //选择和取消选择接口，由player调用
    public void ISelecte(){
        SelectInterface.ISelectedActive(obj);
    }
    public void IDisSelecte(){
        SelectInterface.IDisSelecteActive(obj);
    }

    //初始化和更新class的通用函数，由gameObject调用
    public void initialClass(){
        initialMain();
        buildObjInfo();
        //注意接口初始化一定要在构建objectinfo class之后，因为它依赖于前者
        initialInterafce();
    }

    public void updateClass(){
        SelectInterface.IUpdateSelect(obj,mainCam);
    }

    //为什么下面两个函数不写成接口的形式呢？因为接口是player调用的，
    //没办法获取到observable和interactable这两个变量
    //如果需要重写观察接口的“IObservingAction”，需要先调用这个函数进行初始化
    //如果初始化成功返回true，初始化失败返回false
    public bool initialObservingAction(GameObject player,GameObject gameObject){
        if(!observable){
            player.GetComponent<Basic>().state = "observeEnding";
            return false;
        }
        player.GetComponent<Basic>().state = "observeEnding";
        return true;
    }

    //如果需要重写观察接口的“IInteractingAction”，需要先调用这个函数进行初始化
    //如果初始化成功返回true，初始化失败返回false
    public bool initialInteractingAction(GameObject player,GameObject gameObject){
        if(!interactable){
            player.GetComponent<Basic>().state = "interactEnding";
            return false;
        }
        player.GetComponent<Basic>().state = "interactEnding";
        return true;
    }

}
