using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;

public class EmeyGFX : MonoBehaviour{


    public AIPath aIPath;
    
    void Update(){
        if (aIPath.desiredVelocity.x >= 0.01f){
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (aIPath.desiredVelocity.x <= -0.01f) {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
