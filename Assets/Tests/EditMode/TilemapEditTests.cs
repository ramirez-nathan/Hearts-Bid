using NUnit.Framework;
using UnityEngine;
using UnityEngine.Tilemaps;

[TestFixture]
public class TilemapTests
{
    private GameObject tilemapObject;
    private Tilemap tilemap;
    private TileBase testTile;

    [SetUp]
    public void SetUp()
    {
        // create test tilemap
        tilemapObject = new GameObject("TestTilemap"); 
        tilemap = tilemapObject.AddComponent<Tilemap>();
        tilemapObject.AddComponent<TilemapRenderer>(); 

   
        testTile = ScriptableObject.CreateInstance<Tile>();
    }

    //[TearDown]
    //public void TearDown()
    //{
        // Destroy objects to avoid memory leaks
        //Object.DestroyImmediate(tilemapObject);
      //  Object.DestroyImmediate(testTile);
    //}

    [Test]
    public void setTileTest()
    {
        Vector3Int position = new Vector3Int(0, 0, 0);
        tilemap.SetTile(position, testTile);

        TileBase retrievedTile = tilemap.GetTile(position);
        Assert.AreEqual(testTile, retrievedTile, "Tile at position (0,0,0) should match the assigned testTile.");
    }

    [Test]
    public void removeTileTest()
    {
        Vector3Int position = new Vector3Int(1, 1, 0);
        tilemap.SetTile(position, testTile);
        tilemap.SetTile(position, null);

        TileBase retrievedTile = tilemap.GetTile(position);
        Assert.IsNull(retrievedTile, "Tile should be removed from position (1,1,0).");
    }

    [Test]
    public void dimensionsTestAfterRemove()
    {
        Vector3Int position = new Vector3Int(5, 5, 0);
        tilemap.SetTile(position, testTile);

        Assert.IsTrue(tilemap.cellBounds.Contains(position), "Tilemap bounds should include the newly set tile.");
    }
}
