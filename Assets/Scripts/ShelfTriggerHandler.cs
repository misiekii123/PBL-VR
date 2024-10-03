using UnityEngine;
using System.Collections.Generic;

public class ShelfTriggerHandler : MonoBehaviour
{
    public OpenShelfDoor Doors;
    private List<GameObject> objectsInTrigger = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (!objectsInTrigger.Contains(other.gameObject))
        {
            objectsInTrigger.Add(other.gameObject);
        }

        if (Doors != null)
        {
            Doors.Open();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (objectsInTrigger.Contains(other.gameObject))
        {
            objectsInTrigger.Remove(other.gameObject);
        }

        if (objectsInTrigger.Count == 0 && Doors != null)
        {
            Doors.Close();
        }
    }

    private void Update()
    {
        for (int i = objectsInTrigger.Count - 1; i >= 0; i--)
        {
            if (objectsInTrigger[i] == null) 
            {
                objectsInTrigger.RemoveAt(i);
            }
        }

        if (objectsInTrigger.Count == 0 && Doors != null)
        {
            Doors.Close();
        }
    }
}
