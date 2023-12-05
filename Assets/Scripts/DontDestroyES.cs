using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyES : MonoBehaviour {
    static DontDestroyES instance;
    void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
