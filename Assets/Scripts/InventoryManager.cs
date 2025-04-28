using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    private List<GameObject> inventory = new List<GameObject>();
    private const int maxInventorySize = 3;
    private GameObject currentItem;
    private GameObject currentViewItem;
    public Transform itemViewpoint; // Reference to the itemViewpoint object
    public float rotationSpeed;
    public float viewItemScale;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("Items");
        foreach (GameObject item in items)
        {
            if (inventory.Count < maxInventorySize)
            {
                inventory.Add(item);
                item.SetActive(false); // Make the item non-visible
                Debug.Log($"Item {item.name} added to inventory.");
            }
            else
            {
                Debug.Log($"Cannot add {item.name}. Inventory is full!");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1)) {
            ChangeItem(0);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2)) {
            ChangeItem(1);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3)) {
            ChangeItem(2);
        }

        // Rotate the currentViewItem on the X-axis
        if (currentViewItem != null)
        {
            currentViewItem.transform.Rotate(Vector3.right * Time.deltaTime * rotationSpeed); // Adjust speed as needed
        }
    }

    public bool AddItem(GameObject item)
    {
        if (inventory.Count >= maxInventorySize)
        {
            Debug.Log("Inventory is full!");
            return false;
        }

        inventory.Add(item);
        item.SetActive(false); // Make the item non-visible
        Debug.Log($"Item {item.name} added to inventory.");
        return true;
    }

    public void RemoveItem(GameObject item)
    {
        if (inventory.Contains(item))
        {
            inventory.Remove(item);
            item.SetActive(true); // Optionally make the item visible again
            Debug.Log($"Item {item.name} removed from inventory.");
        }
        else
        {
            Debug.Log("Item not found in inventory.");
        }
    }
    public void ChangeItem(int index) {
        if (currentItem != null) {
            currentItem.SetActive(false);
        }
        inventory[index].SetActive(true);
        currentItem = inventory[index];
        Debug.Log("Current item: " + currentItem.name);

        // Handle the itemViewpoint logic
        if (currentViewItem != null)
        {
            Destroy(currentViewItem); // Remove the last object moved to itemViewpoint
        }

        // Duplicate the selected item
        currentViewItem = Instantiate(inventory[index]);
        currentViewItem.transform.SetParent(itemViewpoint); // Move to itemViewpoint
        currentViewItem.transform.localPosition = Vector3.zero; // Center it at itemViewpoint
        currentViewItem.transform.localScale = Vector3.one * viewItemScale; // Scale to viewItemScale along all axes
        currentViewItem.transform.localRotation = Quaternion.Euler(45, 0, 45); // Set rotation to (45, 0, 45)
    }
}
