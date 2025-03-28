using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Tilemaps;
public class toolSpawnTile : MonoBehaviour
{
    [Header("Tilemap")]
    public Tilemap tilemapFloor1;
    public Tilemap tilemapFloor2;
    public Tilemap tilemapFloor3;
    public Tilemap tilemapFloor4;
    public Tilemap tilemapFloor5;

    [Header("TilePos")]
    public List<Vector3Int> tilePositionsFloor1 = new List<Vector3Int>();
    public List<Vector3Int> tilePositionsFloor2 = new List<Vector3Int>();
    public List<Vector3Int> tilePositionsFloor3 = new List<Vector3Int>();
    public List<Vector3Int> tilePositionsFloor4 = new List<Vector3Int>();
    public List<Vector3Int> tilePositionsFloor5 = new List<Vector3Int>();

    public List<GameObject> lsPrefabs;
 
    public toolSpawnParentContainer parent;
    [Button]
    private void SpawnParent()
    {
        parent = GetComponent<toolSpawnParentContainer>();
        parent.SpawnContainer();
    }
    [Button]
    private void Btn_GetTilePositions()
    {
        GetTilePositions(tilemapFloor1, tilePositionsFloor1);
        GetTilePositions(tilemapFloor2, tilePositionsFloor2);
        GetTilePositions(tilemapFloor3, tilePositionsFloor3);
        GetTilePositions(tilemapFloor4, tilePositionsFloor4);
        GetTilePositions(tilemapFloor5, tilePositionsFloor5);

    }
    [Button]
    private void Btn_SpawnTile()
    {
        if (parent == null)
        {
            Debug.LogError("Parent container is null. Did you forget to call SpawnParent()?");
            return;
        }
        int spawnIndex = 0;
        SpawnObjects(tilePositionsFloor1, "floor1", 3, 0, new Vector3(0, 0, 0), parent.containerFloor1Instance, spawnIndex);
        SpawnObjects(tilePositionsFloor2, "floor2", 2, 1, new Vector3(0f, -0.5f, 0), parent.containerFloor2Instance, spawnIndex);
        SpawnObjects(tilePositionsFloor3, "floor3", 1, 2, new Vector3(0f, -1f, 0), parent.containerFloor3Instance, spawnIndex);
        SpawnObjects(tilePositionsFloor4, "floor4", 0, 3, new Vector3(0f, -2.5f, 0), parent.containerFloor4Instance, spawnIndex);
        SpawnObjects(tilePositionsFloor5, "floor5", -1, 4, new Vector3(0.5f, 0.5f, 0), parent.containerFloor5Instance, spawnIndex);
    }
    void GetTilePositions(Tilemap tilemap, List<Vector3Int> tilePositions)
    {
        tilePositions.Clear();
        BoundsInt bounds = tilemap.cellBounds;
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int tilePos = new Vector3Int(x, y, 0);
                if (tilemap.HasTile(tilePos))
                {
                    tilePositions.Add(tilePos);
                }
            }
        }
    }

    void SpawnObjects(List<Vector3Int> tilePositions, string layerName, float positionZ, int sortingOrder, Vector3 offset, GameObject container, int spawnIndex)
    {
        if (container == null)
        {
            Debug.LogError($"Container for {layerName} is null. Check if it was instantiated.");
            return;
        }
        int totalSpawnPoints = tilePositions.Count;

        if (totalSpawnPoints % 3 != 0)
        {
            int excess = totalSpawnPoints % 3;
            tilePositions.RemoveRange(0, excess);
            totalSpawnPoints -= excess;
        }

        int prefabsPerType = totalSpawnPoints / lsPrefabs.Count;
        Dictionary<GameObject, int> prefabCount = new Dictionary<GameObject, int>();
        foreach (GameObject prefab in lsPrefabs)
        {
            prefabCount[prefab] = prefabsPerType;
        }

        Shuffle(tilePositions);

        foreach (Vector3Int tilePos in tilePositions)
        {
            Vector3 worldPos = tilemapFloor1.GetCellCenterWorld(tilePos) + offset;
            worldPos.z = positionZ;

            GameObject selectedPrefab = GetBalancedPrefab(prefabCount);
            GameObject temp = Instantiate(selectedPrefab, worldPos, Quaternion.identity);
            temp.layer = LayerMask.NameToLayer(layerName);
            temp.name = selectedPrefab.name + "_"+ layerName +"_"+ spawnIndex++;
            temp.transform.parent = container.transform;
            SpriteRenderer sr = temp.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sortingOrder = sortingOrder;
            }
        }
    }

    public GameObject GetBalancedPrefab(Dictionary<GameObject, int> prefabCount)
    {
        foreach (var entry in prefabCount)
        {
            if (entry.Value > 0)
            {
                prefabCount[entry.Key]--;
                return entry.Key;
            }
        }
        return lsPrefabs[0];
    }

    void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}

