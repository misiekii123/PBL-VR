using System.Collections;
using UnityEngine;

public class OpenShelfDoor : MonoBehaviour
{
    public float rotationSpeed = 2f; // Pr�dko�� obracania drzwi
    private ObjectsManager objectsManager; // Referencja do ObjectsManager
    private Coroutine closeCoroutine; // Korutyna zamykania drzwi

    private void Start()
    {
        // Znajdujemy obiekt ObjectsManager w scenie (je�li istnieje tylko jeden)
        objectsManager = FindObjectOfType<ObjectsManager>();

        if (objectsManager == null)
        {
            Debug.LogError("Brak obiektu ObjectsManager w scenie.");
        }
    }

    public void Open()
    {
        // Zatrzymujemy wcze�niejsze zamykanie drzwi, je�li taka korutyna dzia�a�a
        if (closeCoroutine != null)
        {
            StopCoroutine(closeCoroutine);
        }

        // Rozpoczynamy animacj� otwierania drzwi
        StopAllCoroutines();
        StartCoroutine(RotateToAngle(90));

        // Uruchamiamy korutyn�, kt�ra zamknie drzwi po okre�lonym czasie
        closeCoroutine = StartCoroutine(CloseAfterDelay(objectsManager.timeToClose));
    }

    public void Close()
    {
        StopAllCoroutines(); // Zatrzymujemy inne korutyny
        StartCoroutine(RotateToAngle(0)); // Rozpoczynamy animacj� zamykania drzwi
    }

    private IEnumerator RotateToAngle(float targetAngle)
    {
        Quaternion startRotation = transform.rotation; // Bie��ca rotacja
        Quaternion endRotation = Quaternion.Euler(0, targetAngle, 0); // Docelowa rotacja
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * rotationSpeed;
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            yield return null; // Czekamy do nast�pnej klatki
        }

        transform.rotation = endRotation; // Ustawiamy dok�adnie docelow� rotacj�
    }

    private IEnumerator CloseAfterDelay(float delay)
    {
        // Czekaj przez okre�lon� ilo�� czasu
        yield return new WaitForSeconds(delay);

        // Zamykanie drzwi po czasie
        Close();
    }
}
