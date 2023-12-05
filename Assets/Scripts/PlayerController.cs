using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    Rigidbody rb;
    float speed = 5f;
    float jumpSpeed = 15f;
    bool jumped = false;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && !jumped) {
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            jumped = true;
        }

        float xDir = Input.GetAxis("Horizontal");
        float zDir = Input.GetAxis("Vertical");

        transform.position += new Vector3(xDir * speed * Time.deltaTime, 0f, zDir * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Platform") {
            jumped = false;
        }    
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Item") {
            GameManager.instance.IncreaseScore();
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.tag == "DeadZone") {
            GameManager.instance.GameOver = true;
            GameObject.Find("Canvas").transform.Find("Gameover").gameObject.SetActive(true);
        }

        if (other.gameObject.tag == "Finish") {
            GameManager.instance.FinishPoint();
        }
    }
}
