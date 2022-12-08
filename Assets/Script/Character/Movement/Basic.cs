using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//包含基础的移动以及选中的物体
public class Basic : MonoBehaviour
{
    public InputController input;
    private float targetAngle;
    private Quaternion targetAngleQuater;
    [SerializeField] private float moveSpeed = 10f; 
    [SerializeField] private float rotateSpeed = 5f; 
    //选择物体的范围
    [SerializeField] private float selectRange = 5f;
    //当前角色选中的物品
    public GameObject selectObject;
    //用来标注角色的状态：free，observing，interacting
    public string state;
    public ISelectable selectedInterface;
    private void Awake()
    {
        state = "free";
        input = new InputController();
        input.Enable();
        selectObject = CheckRaycast();
    }

    // Update is called once per frame
    void Update()
    {
        updateMove();
        updateSelect();
    }
    void updateSelect(){
        selectObject = CheckRaycast();
        if (selectObject != null)
        {
            ISelectable newselectedInterface = selectObject.GetComponent<ISelectable>();
            //如果观察到的物体没有selected的属性，返回
            if (newselectedInterface==null){
                return;
            }
            // 如果观察到了新的物体
            if (newselectedInterface!=selectedInterface){
                //取消原来物体的选中并选中新的物体
                if (selectedInterface!=null){
                    selectedInterface.IDisSelecte();
                }
                selectedInterface = newselectedInterface;
                selectedInterface.ISelecte();
            }
        }
        else{
            //如果没有选中任何物体，取消原来物体的选中
            if (selectedInterface!=null){
                selectedInterface.IDisSelecte();
                selectedInterface = null;
            }
        }
    }
    void updateMove(){
        //获取input system的信息
        Vector2 move = input.Player.Move.ReadValue<Vector2>();
        Vector3 changedSpeed= new Vector3(move.x,0,move.y)*moveSpeed*Time.deltaTime;
        //如果角度发生改变，获取目标角度并将其转化为Quaternion
        if(move != new Vector2(0,0)){
            targetAngle = Mathf.Atan2(move.x,move.y)*180/Mathf.PI;
            targetAngleQuater = Quaternion.Euler(0,targetAngle,0);
        }
        //获取当前玩家的角度
        float tempAngle = transform.localEulerAngles.y;
        if (tempAngle!=targetAngle){
            //用插值的方式进行平滑旋转
            transform.rotation = Quaternion.Slerp(transform.rotation, targetAngleQuater, rotateSpeed * Time.deltaTime);
        }
        //移动物体位置
        gameObject.transform.position+=changedSpeed;
    }
    // 定义一个CheckRaycast函数，用于检测最近的距离玩家一定范围内的gameobject
    // 如果有符合要求的物体，则返回它；否则，返回NULL
    public GameObject CheckRaycast()
    {
        
        //被选择的物体
        GameObject selectedObject = null;
        // 圆形区域的半径
        float radius = selectRange;
        // 圆形区域的中心点
        Vector3 center = transform.position;
        // 查找在圆形区域内的所有物体
        Collider[] colliders = Physics.OverlapSphere(center, radius, 1);
        // 扇形的中心角度，表示扇形的宽度
        float angle = 60;
        // 扇形的方向
        Vector3 direction = transform.forward;
        // 用来存储符合条件的物体
        List<GameObject> objects = new List<GameObject>();
        foreach (Collider collider in colliders)
        {
            // 计算物体和圆心的夹角
            float angleBetween = Vector3.Angle(direction, collider.transform.position - center);
            if (angleBetween >= 0 && angleBetween <= angle)
            {
                // 存储符合条件的物体
                objects.Add(collider.gameObject);
            }
        }
        // 如果存在符合条件的物体
        if (objects.Count > 0)
        {
            // 初始化离玩家最近的物体
            selectedObject = objects[0];
            // 记录最小的距离
            float minDistance = Vector3.Distance(transform.position, selectedObject.transform.position);
            foreach (GameObject obj in objects)
            {
                // 计算物体和玩家的距离
                float distance = Vector3.Distance(transform.position, obj.transform.position);
                // 如果距离更近，则更新离玩家最近的物体
                if (distance < minDistance)
                {
                    minDistance = distance;
                    selectedObject = obj;
                }
            }
            return selectedObject;
        }
        else{
            return null;
        }
    }

}


