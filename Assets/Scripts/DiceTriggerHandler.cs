using UnityEngine;

public class DiceTriggerHandler : MonoBehaviour
{
    private GameObject[] walls;
    void Start()
    {
        walls = GameObject.FindGameObjectsWithTag("Wall");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trigger"))
        {
            foreach (GameObject wall in walls)
            {
                MeshCollider meshCollider = wall.GetComponent<MeshCollider>();
                if (meshCollider != null)
                {
                    meshCollider.enabled = true;
                }
            }
            Debug.Log($"{gameObject.name} interacted with a trigger!");
        }
    }
}
