using System.Collections;
using UnityEngine;

public class OpenShelfDoor : MonoBehaviour
{
    public float rotationSpeed = 2f; 
    private ObjectsManager objectsManager; 

    private void Start()
    {
        objectsManager = FindObjectOfType<ObjectsManager>();

        if (objectsManager == null)
        {
            Debug.LogError("Brak obiektu ObjectsManager w scenie.");
        }
    }

    public void Open()
    { 
        StopAllCoroutines();
        StartCoroutine(RotateToAngle(90));
    }

    public void Close()
    {
        StopAllCoroutines(); 
        StartCoroutine(RotateToAngle(0)); 
    }

    private IEnumerator RotateToAngle(float targetAngle)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(0, targetAngle, 0); 
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * rotationSpeed;
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            yield return null; 
        }

        transform.rotation = endRotation; 
    }
}
