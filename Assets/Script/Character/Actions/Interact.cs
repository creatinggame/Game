using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
public class Interact : MonoBehaviour
{
    //Basic脚本
    public Basic basic; 
    //声明接口
    public IInteractable InteractdInterface;
    public float pressInteract;
    void initialMain(){
        //把Basic脚本用类型推断的方式转化成简略的变量名
        basic = gameObject.GetComponent<Basic>();
    }
    void updateMain(){
    }
    void updateInteract(){
        //没有任何选择的物体时，直接返回
        if (basic.selectObject==null){
            return;
        }        
        //如果选择的物体不可hu
        InteractdInterface = basic.selectObject.GetComponent<IInteractable>();
        if (InteractdInterface==null){
            return;
        }
        pressInteract = basic.input.Player.Interact.ReadValue<float>();
        if (pressInteract==1){
            //按下按钮，调用接口的IInteractStart方法，观察开始
            InteractdInterface.IInteractStart(gameObject,basic.selectObject);
        }
        if (pressInteract==0){
            //松开按钮，状态占用结束
            InteractdInterface.IInteractEnd(gameObject,basic.selectObject);
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
        updateInteract();
    }
}
