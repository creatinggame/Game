using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
public class test2 : Item, IObservable
{
    void Awake(){
        initialClass();
    }

    void LateUpdate(){
        updateClass();
    }
}
