using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class CheckObjects : MonoBehaviour
{
    public string targetTag;
    private XRBaseController controller;
    public static CheckObjects instance;
    public string currentTag;

    private void Start()
    {
        targetTag = GameManager.instance.selectedTag;
        controller = FindObjectOfType<XRBaseController>();

        if (instance == null)
            instance = this;
    }
    private void OnTriggerEnter(Collider other)
    {
        currentTag = other.tag;

        if (other.tag == targetTag)
        {
            ObjectsManager.instance.CheckDespawnTime();
            GameManager.instance.setPoints(GameManager.instance.points += 1);
            currentTag = null;
        }
        else if(other.tag != targetTag)
        {
            ObjectsManager.instance.CheckDespawnTime();
            controller.SendHapticImpulse(1.0f, 1.0f);
            currentTag = null;
        }
    }
}
