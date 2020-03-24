using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildOrderUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.SetSiblingIndex(1);
    }
}
