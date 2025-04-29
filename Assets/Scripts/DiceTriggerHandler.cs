using UnityEngine;
using System.Collections;

public class DiceTriggerHandler : MonoBehaviour
{
    private GameObject[] walls;
    private Rigidbody rb;

    void Start()
    {
        walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (GameObject wall in walls) {
            wall.layer = LayerMask.NameToLayer("Wall");
        }
        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trigger"))
        {
            foreach (GameObject wall in walls) {
                wall.GetComponent<BoxCollider>().enabled = true;
            }
            StartCoroutine(ChangeLayerAfterDelay());
        }
    }

    private IEnumerator ChangeLayerAfterDelay()
    {
        yield return new WaitForSeconds(.5f);
        gameObject.layer = LayerMask.NameToLayer("Dice");
    }
    void Update()
    {
        if (rb.IsSleeping())
        {
            Vector3 localUpVector = Quaternion.Inverse(transform.rotation) * Vector3.up;    

            if (localUpVector.z > 0.9f) {
                print(1);
            } else if (localUpVector.y > .9f) {
                print(2);
            } else if (localUpVector.x < -.9f) {
                print(3);
            } else if (localUpVector.x > .9f) {
                print(4);
            } else if (localUpVector.y < -.9f) {
                print(5);
            } else if (localUpVector.z < -.9f) {
                print(6);
            }
            // Maybe make dice explode when the number is returned
        }
    }
}
