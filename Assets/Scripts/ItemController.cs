using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour {
    float rotateAngle = 360f;

    void Update() {
        transform.Rotate(Vector3.up, rotateAngle * Time.deltaTime, Space.World);
    }
}
