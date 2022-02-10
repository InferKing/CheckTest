using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IsRightAnswer
{
    Empty,
    True,
    False
}

public class CheckAnswer : MonoBehaviour
{
    public static IsRightAnswer IsRight;

    private void Start()
    {
        IsRight = IsRightAnswer.Empty;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ball")
        {
            if (other.gameObject.GetComponent<Rigidbody>().mass > 1)
            {
                IsRight = IsRightAnswer.True;
            }
            else
            {
                IsRight = IsRightAnswer.False;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ball")
        {
            IsRight = IsRightAnswer.Empty;
        }
    }
}
