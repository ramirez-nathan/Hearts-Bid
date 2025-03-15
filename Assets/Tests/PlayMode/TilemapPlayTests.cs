using NUnit.Framework;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.TestTools;
using System.Collections;

public class TilemapPlayModeTests
{
    private GameObject tilemapObject;
    private Tilemap tilemap;
    private TileBase testTile;

    [SetUp]
    public void SetUp()
    {
        tilemapObject = new GameObject("TestTilemap");
        var grid = tilemapObject.AddComponent<Grid>(); 
        tilemap = new GameObject("Tilemap").AddComponent<Tilemap>();
        tilemap.gameObject.AddComponent<TilemapRenderer>(); 

        tilemap.transform.SetParent(tilemapObject.transform);

        testTile = ScriptableObject.CreateInstance<Tile>();
    }

    //[TearDown]
    //public void TearDown()
    //{
        //Object.Destroy(tilemapObject);
        //Object.Destroy(tilemap.gameObject);
      //  Object.Destroy(testTile);
    //}

    [UnityTest]
    public IEnumerator Tile_PersistsAfterOneFrame()
    {
        Vector3Int position = new Vector3Int(2, 2, 0);
        tilemap.SetTile(position, testTile);
        yield return null; // Wait one frame

        Assert.AreEqual(testTile, tilemap.GetTile(position), "Tile should persist after one frame.");
    }

    [UnityTest]
    public IEnumerator RemovingTile_ReflectsNextFrame()
    {
        Vector3Int position = new Vector3Int(3, 3, 0);
        tilemap.SetTile(position, testTile);
        yield return null; // Wait a frame

        tilemap.SetTile(position, null);
        yield return null; // Wait a frame

        Assert.IsNull(tilemap.GetTile(position), "Tile should be removed and reflect in the next frame.");
    }
}
