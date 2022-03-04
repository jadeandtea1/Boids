using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSize : MonoBehaviour
{
    void Start(){
        transform.localScale = new Vector3(Camera.main.aspect * Camera.main.orthographicSize * 2, Camera.main.orthographicSize * 2, 1);
    }
}
