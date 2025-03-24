using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Tilemaps;
public class toolSpawnTile : MonoBehaviour
{
    public Tilemap tilemapFloor1;
    public List<Vector3Int> tilePositionsFloor1 = new List<Vector3Int>();
    public List<GameObject> lsPrefabs;
    [Button]
    private void Test()
    {
        GetTilePositions();
        SpawnObjects();
    }
    void GetTilePositions()
    {
        tilePositionsFloor1.Clear(); // Đảm bảo danh sách rỗng trước khi cập nhật

        BoundsInt bounds = tilemapFloor1.cellBounds; // Lấy giới hạn của Tilemap
        UnityEngine.Tilemaps.TileBase[] allTiles = tilemapFloor1.GetTilesBlock(bounds); // Lấy tất cả tile trong phạm vi

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int tilePos = new Vector3Int(x, y, 0);
                if (tilemapFloor1.HasTile(tilePos)) // Nếu vị trí có tile thì lưu lại
                {
                    tilePositionsFloor1.Add(tilePos);
                }
            }
        }
    }

    void SpawnObjects()
{
    int totalSpawnPoints = tilePositionsFloor1.Count;

    // Đảm bảo số lượng tile spawn được chia hết cho 3
    if (totalSpawnPoints % 3 != 0)
    {
        int excess = totalSpawnPoints % 3; // Số lượng dư
        tilePositionsFloor1.RemoveRange(0, excess); // Loại bỏ một số vị trí đầu tiên để đảm bảo chia hết cho 3
        totalSpawnPoints -= excess;
    }

    // Tính số lượng mỗi loại prefab cần spawn
    int prefabsPerType = totalSpawnPoints / lsPrefabs.Count; // Số lượng mỗi loại prefab
    Dictionary<GameObject, int> prefabCount = new Dictionary<GameObject, int>();

    foreach (GameObject prefab in lsPrefabs)
    {
        prefabCount[prefab] = prefabsPerType;
    }

    // Spawn object theo danh sách vị trí
    foreach (Vector3Int tilePos in tilePositionsFloor1)
    {
        Vector3 worldPos = tilemapFloor1.GetCellCenterWorld(tilePos);

        GameObject selectedPrefab = GetBalancedPrefab(prefabCount);
        GameObject temp = Instantiate(selectedPrefab, worldPos, Quaternion.identity);
        temp.layer = LayerMask.NameToLayer("floor1");
    }
}

// Chọn prefab sao cho số lượng sinh ra đồng đều
public GameObject GetBalancedPrefab(Dictionary<GameObject, int> prefabCount)
{
    foreach (var entry in prefabCount)
    {
        if (entry.Value > 0)
        {
            prefabCount[entry.Key]--; // Giảm số lượng còn lại của prefab này
            return entry.Key;
        }
    }
    return lsPrefabs[0]; // Tránh lỗi nếu không có prefab nào hợp lệ
}

}
