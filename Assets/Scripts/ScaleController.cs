using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScaleController : MonoBehaviour
{
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject block;
    private int isMinus = 0;

    private void Update()
    {
        isMinus = block.transform.rotation.x * 100 > 70.3f ? -1 : 1;
        arrow.transform.eulerAngles = new Vector3((block.transform.eulerAngles.x - 90) * 3 * isMinus, 0, 0);
    }

}
