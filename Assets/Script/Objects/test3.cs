using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
public class test3 : MonoBehaviour, ISelectable, IInteractable
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private bool selectable = true;
    [SerializeField] private bool observable = false;
    [SerializeField] private bool interactable = true;
    [SerializeField] private float intervalRange = 3f;
    [SerializeField] private float canvasHight = 5f;
    private Outline outlineComponent;
    private Camera mainCam;
    private ISelectable SelectInterface; 
    private IObservable ObserveInterface;
    private IInteractable InteractInterface;
    public objectInfo obj;
    //进行一些初始化
    void initialMain(){
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
    void initialInterafce(){
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
    void buildObjInfo(){
        obj = new objectInfo(gameObject, selectable, observable, interactable,
        canvas, intervalRange, canvasHight, outlineComponent);
    }
    
    //下面两行用于将“需要输入的接口”转换成“不需要输入的接口”，方便在其他地方调用
    public void ISelecte(){
        SelectInterface.ISelectedActive(obj);
    }
    public void IDisSelecte(){
        SelectInterface.IDisSelecteActive(obj);
    }

    void Awake(){
        initialMain();
        buildObjInfo();
        //注意接口初始化一定要在构建objectinfo class之后，因为它依赖于前者
        initialInterafce();
    }

    void LateUpdate(){
        SelectInterface.IUpdateSelect(obj,mainCam);
    }
}
