using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Item
{
    Nothing,
    Wood
}
public class Inventory : MonoBehaviour
{
    public string[] Lines;
    public GameObject UsePanel;
    public Text UseText;
    public Item CurrentItem;

    // Start is called before the first frame update
    void Start()
    {
        UsePanel.SetActive(false);
    }
    private void OnTriggerStay(Collider col)
    {
        switch (col.tag)
        {
            case "Wood":
                UseText.text = Lines[0];
                UsePanel.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    UsePanel.SetActive(false);
                    Destroy(col.transform.parent.gameObject);
                    AddItem(Item.Wood);
                }
                break;
            default:
                Debug.Log("Empty");
                break;
        }
    }
    public void RemoveItem(Item id)
    {
        CurrentItem = Item.Nothing;
        this.transform.parent.Find(id.ToString()).gameObject.SetActive(false);
    }
    public void AddItem(Item id)
    {
        CurrentItem = id;
        this.transform.parent.Find(id.ToString()).gameObject.SetActive(true);
    }
}
