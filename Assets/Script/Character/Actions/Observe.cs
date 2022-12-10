using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
public class Observe : MonoBehaviour
{
    //Basic脚本
    public Basic basic; 
    //声明接口
    public IObservable observedInterface;
    public float pressObserve;
    void initialMain(){
        //把Basic脚本用类型推断的方式转化成简略的变量名
        basic = gameObject.GetComponent<Basic>();
    }
    void updateMain(){
    }
    void updateObserve(){
        if(Time.timeScale == 0){
            return;
        }
        //没有任何选择的物体时，直接返回
        if (basic.selectObject==null){
            return;
        }        
        //如果选择的物体不可观察
        observedInterface = basic.selectObject.GetComponent<IObservable>();
        if (observedInterface==null){
            return;
        }
        pressObserve = basic.input.Player.Observe.ReadValue<float>();
        if (pressObserve==1){
            //按下按钮，调用接口的IObserveStart方法，观察开始
            observedInterface.IObserveStart(gameObject,basic.selectObject);
        }
        if (pressObserve==0){
            //松开按钮，状态占用结束
            observedInterface.IObserveEnd(gameObject,basic.selectObject);
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        initialMain();
    }

    // Update is called once per frame
    void Update()
    {
        updateMain();
        updateObserve();
    }
}
