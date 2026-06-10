using UnityEngine;
using System.Collections.Generic;

public class InfiniteTreeSpawner : MonoBehaviour
{
    public GameObject[] treePrefabs; // Drag your different tree sprites/prefabs here
    public int treeCount = 10;       // How many trees to keep on screen
    public float minXBuffer = 5f;    // Min distance between trees
    public float maxXBuffer = 15f;   // Max distance between trees
    public float groundY = -2.5f;    // The Y position where trees sit
    public Transform cam;            // Reference to Main Camera

    private List<GameObject> activeTrees = new List<GameObject>();
    private float lastSpawnX;
    private float screenWidth;

    void Start()
    {
        if (cam == null) cam = Camera.main.transform;
        
        // Calculate screen width in world units
        screenWidth = Camera.main.orthographicSize * Camera.main.aspect * 2;
        
        // Start spawning from the camera's current position
        lastSpawnX = cam.position.x - (screenWidth / 2);

        for (int i = 0; i < treeCount; i++)
        {
            SpawnTree(true);
        }
    }

    void Update()
    {
        foreach (GameObject tree in activeTrees)
        {
            // If a tree is too far behind the camera
            if (tree.transform.position.x < cam.position.x - (screenWidth))
            {
                RepositionTree(tree);
            }
        }
    }

    void SpawnTree(bool initial)
    {
        GameObject prefab = treePrefabs[Random.Range(0, treePrefabs.Length)];
        float spawnX = initial ? lastSpawnX + Random.Range(minXBuffer, maxXBuffer) : lastSpawnX;
        
        GameObject newTree = Instantiate(prefab, new Vector3(spawnX, groundY, 0), Quaternion.identity);
        newTree.transform.parent = this.transform;
        activeTrees.Add(newTree);
        
        lastSpawnX = spawnX + Random.Range(minXBuffer, maxXBuffer);
    }

    void RepositionTree(GameObject tree)
    {
        // Move the tree that went off-screen to the far right
        float newX = GetFarthestTreeX() + Random.Range(minXBuffer, maxXBuffer);
        tree.transform.position = new Vector3(newX, groundY, 0);
        
        // Randomize the visual a bit so it doesn't look like the same tree
        tree.transform.localScale = Vector3.one * Random.Range(0.8f, 1.2f);
    }

    float GetFarthestTreeX()
    {
        float max = cam.position.x;
        foreach (var tree in activeTrees)
        {
            if (tree.transform.position.x > max) max = tree.transform.position.x;
        }
        return max;
    }
}