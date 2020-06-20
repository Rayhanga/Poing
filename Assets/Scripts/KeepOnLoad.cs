using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepOnLoad : MonoBehaviour{
    void Awake(){
        DontDestroyOnLoad(this.gameObject);
    }
}
