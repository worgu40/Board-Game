using UnityEngine;
using System.Collections.Generic;

public class DiceRolling : MonoBehaviour
{
    private GameObject[] walls;
    private List<GameObject> dice = new List<GameObject>();
    public GameObject die;
    private GameObject mainCamera;
    public int diceRolls;

    void Start()
    {
        walls = GameObject.FindGameObjectsWithTag("Wall");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            for (int i = 0; i < diceRolls; i++) {
                GameObject newDie = Instantiate(die, mainCamera.transform.position, Quaternion.identity);
                dice.Add(newDie);
            }
            foreach (GameObject die in dice) {
                Rigidbody dicerb = die.GetComponent<Rigidbody>();
                dicerb.isKinematic = false;
                dicerb.AddForce(new Vector3(-Random.Range(5, 20), 0, 0), ForceMode.Impulse);
            }
        }
    }
}
