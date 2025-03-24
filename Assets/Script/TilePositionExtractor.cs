using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class TilePositionExtractor : MonoBehaviour
{
    public Tilemap gameTilemap;  // Tilemap chứa các Tile đã vẽ
    public List<Vector3Int> tilePositions = new List<Vector3Int>();  // Danh sách vị trí Tile

    //private void Awake()
    //{
    //    ExtractTilePositions();
    //    SaveTilePositions();
    //}

    //void ExtractTilePositions()
    //{
    //    tilePositions.Clear();  // Xóa danh sách cũ nếu có

    //    BoundsInt bounds = gameTilemap.cellBounds;
    //    TileBase[] allTiles = gameTilemap.GetTilesBlock(bounds);

    //    for (int x = bounds.xMin; x < bounds.xMax; x++)
    //    {
    //        for (int y = bounds.yMin; y < bounds.yMax; y++)
    //        {
    //            Vector3Int tilePos = new Vector3Int(x, y, 0);
    //            if (gameTilemap.HasTile(tilePos)) // Kiểm tra xem có tile ở vị trí này không
    //            {
    //                tilePositions.Add(tilePos);
    //            }
    //        }
    //    }

    //    Debug.Log("Tổng số vị trí hợp lệ: " + tilePositions.Count);
    //}
    //void SaveTilePositions()
    //{
    //    string json = JsonHelper.ToJson(tilePositions);
    //    PlayerPrefs.SetString("TilePositions", json);
    //    PlayerPrefs.Save();
    //    Debug.Log("Lưu danh sách vị trí tile vào PlayerPrefs.");        
    //}
}
