using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
public class BagItems:MonoBehaviour
{
    public int holdItemIndex = 0;
    public List<GameObject> items;
    public Canvas bagCanvas;
    public Image selectedItem;
    public Image frontItem;
    public Image nextItem;
    public TextMeshProUGUI selectedItemName;
    public InputController input;
    private int originalListLen = 0;
    [SerializeField] public GameObject player;
    //用来控制切换物品
    private bool isSwitching = false;
    //控制每次切换之间的间隔
    private float switchingTime = 0.8f;
    //这里详细介绍一下pressNumber的概念以及为什么我们需要它
    //首先我们有一个定时器，让切换当前物品的操作在switchingTime的间隔内只会触发一次
    //这样做的原因是玩家按下一个按钮，实际上会持续很多帧；如果不设置计时器，每帧都会触发一次切换。
    //但是这样有个问题，就是玩家如果在同一个switchingTime的时间内按下了很多次，那也只会切换一次，手感很差
    //所以我们需要让每次玩家手动松开按钮时重置按钮的状态，将isSwitching设置为false。这样玩家就可以按多少次切换多少次
    //可是这样又有一个新的问题：玩家按下一次按钮，实际上会在两个不同的时间段将isSwiching设置为false
    //流程为 玩家按下按钮->按钮不能按->计时器开始->玩家松开按钮->按钮可以按-> ..一段时间后.. -> 计时器到期 ->按钮可以按
    //不难发现，"松开按钮"和"计时器到期"之后，isSwitching都会被设置为false
    //结果就是，如果玩家非常密集地按下并松开按钮，按钮会切换地越来越快越来越快
    //所以我们需要pressedNumber这个int值，来统计玩家提前松开了几次；并将之后的计时器无效相同次数
    private int pressedNumber = 0;
    //输入目标位置和偏移量，分别返回当前列表中前偏移量个物体和后偏移量个物体的index
    int moveForward(int holdItemIndex,int offSet){
        return (items.Count+holdItemIndex-offSet%items.Count)%items.Count;
    }
    int moveNext(int holdItemIndex,int offSet){
        return (holdItemIndex+offSet)%items.Count;
    }
    void setImages(){
        if(items[holdItemIndex]!=null){
            //设置当前选中的物体以及它的名称
            selectedItem.sprite = items[holdItemIndex].GetComponent<Image>().sprite;
            selectedItemName.SetText(items[holdItemIndex].GetComponent<Item>().itemName);
            //设置前一个物体以及后一个物体--magic!
            frontItem.sprite = items[moveForward(holdItemIndex,1)].GetComponent<Image>().sprite;
            nextItem.sprite = items[moveNext(holdItemIndex,1)].GetComponent<Image>().sprite;
        }
    }
    //当list变化时自动更新选取的内容
    void updateChangedItem(){
        if(items.Count!=originalListLen){
            int offSet = items.Count-originalListLen;
            originalListLen = items.Count;
            if (offSet<0){
                holdItemIndex = moveForward(holdItemIndex,-offSet);
            }
            if (offSet>0){
                holdItemIndex = moveNext(holdItemIndex,offSet);
            }
        }
    }
    //当手动输入时更新选取的内容
    void updateChoosedItem(){
        updateChangedItem();
        float inputChoose = input.Player.ChooseItem.ReadValue<float>();
        //如果正在切换中则退出
        if (isSwitching){
            //如果玩家提前结束了按钮，重置状态
            if(inputChoose==0){
                isSwitching = false;
                switchingTime = 0.8f;
                pressedNumber += 1;
            }
            return;
        }
        //获取当前的输入
        if(inputChoose!=0){
            isSwitching = true;
            //计时器，这段时间内不会被重复触发
            StartCoroutine(switchTimer());
            if(inputChoose<0){
                holdItemIndex = moveForward(holdItemIndex,1);
            }
            else if(inputChoose>0){
                holdItemIndex = moveNext(holdItemIndex,1);
            }
            //如果玩家持续按下按钮，转换的速度会加快
            if(switchingTime>=0.2f){
                switchingTime*=0.65f;
            }
        }
        else{
            switchingTime = 0.8f;
        }
    }
    IEnumerator switchTimer(){
        //定时器
        yield return new WaitForSeconds(switchingTime);
        //如果需要被"提前松开按钮的次数"抵消的次数为0，设置为false
        if(pressedNumber==0){
            isSwitching = false;
        }
        else{
            //否则，需要被抵消的次数-1
            pressedNumber -= 1;
        }
    }
    void Awake(){
        input = player.GetComponent<Basic>().input;
    }
    void Update(){
        if(items.Count!=0){
            setImages();
            updateChoosedItem();
        }
    }

}
