using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
public class test3 : Item, ISelectable, IInteractable
{
    void Awake(){
        observable = false;
        initialClass();
    }

    void LateUpdate(){
        updateClass();
    }
}
