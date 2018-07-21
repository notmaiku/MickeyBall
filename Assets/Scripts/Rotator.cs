using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour
{

    void Update()
    {
        switch (gameObject.tag)
        {
            case "Pick Up":
                transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
                break;
            case "Speed Up":
                transform.Rotate(new Vector3(0, 145, 0) * Time.deltaTime);
                break;
        }
    }
}