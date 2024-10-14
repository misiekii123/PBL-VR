using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectsManager : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private Level[] difficulties = { Level.Easy, Level.Medium, Level.Hard };
    [SerializeField] private Level difficulty;

    private GameObject prefab;
    public static ObjectsManager instance;

    [SerializeField] private float[] xValues = { 1.5f, 0.96f, 0.45f, -0.06f };
    [SerializeField] private float[] yValues = { 2.68f, 2.25f, 1.85f, 1.425f };
    private float currentX;
    private float currentY;
    private float currentZ = 16.3f;
    private float spawnTime;
    private float despawnTime;
    public float reactionTime;
    private Vector3 targetPosition;

    private List<GameObject> currentObjects = new List<GameObject>();
    private Coroutine currentCoroutine;

    private DataSaver DataSaver;

    void Start()
    {
        if (MenuManager.instance.selectedLevel >= 0 && MenuManager.instance.selectedLevel <= difficulties.Length) 
            difficulty = difficulties[MenuManager.instance.selectedLevel];

        if (instance == null)
            instance = this;

        for (int i = 0; i < difficulty.amount; i++) 
        {
            CreateObject();
        }

        if (DataSaver == null)
            DataSaver = new DataSaver();

        string startGameTimestamp = DataSaver.GetTimestamp(DateTime.Now);
        DataSaver.startTimestamp = startGameTimestamp;
        DataSaver.correctTag = GameManager.instance.selectedTag;
    }

    private void CreateObject()
    {
        prefab = prefabs[UnityEngine.Random.Range(0, prefabs.Length)];
        GameObject obj = Instantiate(prefab, transform.localPosition, Quaternion.identity, transform);
        currentObjects.Add(obj);

        GenerateObjectPosition(obj);

        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(SpawnAndDespawn(obj));
    }

    private void GenerateObjectPosition(GameObject obj)
    {
        currentX = xValues[UnityEngine.Random.Range(0, xValues.Length)];
        currentY = yValues[UnityEngine.Random.Range(0, yValues.Length)];
        targetPosition = new Vector3(currentX, currentY, currentZ);
        obj.transform.localPosition = targetPosition;
    }

    private IEnumerator SpawnAndDespawn(GameObject obj)
    {
        spawnTime = Time.time;

        yield return new WaitForSeconds(difficulty.duration - 1f);

        CheckDespawnTime();
    }

    public void CheckDespawnTime()
    {
        despawnTime = Time.time;
        reactionTime = despawnTime - spawnTime;

        DataSaver.selectedTag = CheckObjects.instance.currentTag;
        DataSaver.reactionTime = reactionTime;
        DataSaver.SaveToFile();

        if (currentObjects.Count > 0)
        {
            Destroy(currentObjects[0]);
            currentObjects.RemoveAt(0);
        }

        CreateObject();
    }
}
