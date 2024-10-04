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

        if (instance == null )
            instance = this;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == targetTag)
        {
            currentTag = other.tag;
            GameManager.instance.setPoints(GameManager.instance.points += 1);
            ObjectsManager.instance.CheckDespawnTime();
        }
        else if(other.tag != targetTag)
        {
            currentTag = other.tag;
            controller.SendHapticImpulse(1.0f, 5.0f);
            ObjectsManager.instance.CheckDespawnTime();
        }
    }
}
