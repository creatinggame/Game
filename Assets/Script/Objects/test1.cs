using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
public class test1 : MonoBehaviour, ISelectable, IObservable, IInteractable
{
    //所有函数的信息可以在objectInfo类中找到，这里就不重复了
    //第一个变量是玩家的信息，用于和玩家互动
    [SerializeField] public GameObject player;
    [SerializeField] private Canvas canvas;
    [SerializeField] private bool selectable = true;
    [SerializeField] private bool observable = true;
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
    
    public void ISelecte(){
        SelectInterface.ISelectedActive(obj);
    }
    public void IDisSelecte(){
        SelectInterface.IDisSelecteActive(obj);
    }
    
    //重写observing接口行为
    public void IObservingAction(GameObject player,GameObject gameObject){
        Debug.Log("Test1的按钮被按下了一次");
        player.GetComponent<Basic>().state = "ending";
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
