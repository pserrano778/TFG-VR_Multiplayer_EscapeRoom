using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineObjectColliderPuzzleController : ObjectColliderPuzzleController
{ 
    //public string 
    //private GameObject
    public void SetNewObjectState(bool active, GameObject newObject)
    {

        ChangeObjectState(active);
    }

    protected override void Open()
    {
        base.Open();
    }
}
