using UnityEngine;

public class DiceRolling : MonoBehaviour
{
    private GameObject[] walls;
    private GameObject[] dice;

    void Start()
    {
        walls = GameObject.FindGameObjectsWithTag("Wall");
        dice = GameObject.FindGameObjectsWithTag("Dice");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            foreach (GameObject die in dice) {
                Rigidbody dicerb = die.GetComponent<Rigidbody>();
                dicerb.isKinematic = false; // Enable physics for the die
                dicerb.AddForce(new Vector3(Random.Range(5, 20), 0, 0), ForceMode.Impulse);
            }
        }
    }
}
