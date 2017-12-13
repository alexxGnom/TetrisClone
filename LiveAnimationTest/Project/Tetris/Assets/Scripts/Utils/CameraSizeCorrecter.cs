using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSizeCorrecter : MonoBehaviour
{
    [SerializeField]
    private float _needWorldWidth = 11.2f;

    void Start () 
    {
        GetComponent<Camera>().orthographicSize = (_needWorldWidth/ 2f) * Screen.height / Screen.width;
    }

}
