using System.Collections;
using UnityEngine;

public class OpenShelfDoor : MonoBehaviour
{
    public float rotationSpeed = 2f; // Prêdkoœæ obracania drzwi
    private ObjectsManager objectsManager; // Referencja do ObjectsManager
    private Coroutine closeCoroutine; // Korutyna zamykania drzwi

    private void Start()
    {
        // Znajdujemy obiekt ObjectsManager w scenie (jeœli istnieje tylko jeden)
        objectsManager = FindObjectOfType<ObjectsManager>();

        if (objectsManager == null)
        {
            Debug.LogError("Brak obiektu ObjectsManager w scenie.");
        }
    }

    public void Open()
    {
        // Zatrzymujemy wczeœniejsze zamykanie drzwi, jeœli taka korutyna dzia³a³a
        if (closeCoroutine != null)
        {
            StopCoroutine(closeCoroutine);
        }

        // Rozpoczynamy animacjê otwierania drzwi
        StopAllCoroutines();
        StartCoroutine(RotateToAngle(90));

        // Uruchamiamy korutynê, która zamknie drzwi po okreœlonym czasie
        closeCoroutine = StartCoroutine(CloseAfterDelay(objectsManager.timeToClose));
    }

    public void Close()
    {
        StopAllCoroutines(); // Zatrzymujemy inne korutyny
        StartCoroutine(RotateToAngle(0)); // Rozpoczynamy animacjê zamykania drzwi
    }

    private IEnumerator RotateToAngle(float targetAngle)
    {
        Quaternion startRotation = transform.rotation; // Bie¿¹ca rotacja
        Quaternion endRotation = Quaternion.Euler(0, targetAngle, 0); // Docelowa rotacja
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * rotationSpeed;
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            yield return null; // Czekamy do nastêpnej klatki
        }

        transform.rotation = endRotation; // Ustawiamy dok³adnie docelow¹ rotacjê
    }

    private IEnumerator CloseAfterDelay(float delay)
    {
        // Czekaj przez okreœlon¹ iloœæ czasu
        yield return new WaitForSeconds(delay);

        // Zamykanie drzwi po czasie
        Close();
    }
}
