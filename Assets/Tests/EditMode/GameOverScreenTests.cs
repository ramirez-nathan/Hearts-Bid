using NUnit.Framework;
using UnityEngine;

public class GameOverScreenTests
{
    private GameOverScreen gameOverScreen;

    [SetUp]
    public void Setup()
    {
        // Create a GameObject and add GameOverScreen component
        var gameObject = new GameObject();
        gameOverScreen = gameObject.AddComponent<GameOverScreen>();

        // Ensure it starts disabled
        gameObject.SetActive(false);
    }

    [Test]
    public void Setup_ActivatesGameObject()
    {
        // Call Setup()
        gameOverScreen.Setup();

        // Check if the GameObject is now active
        Assert.IsTrue(gameOverScreen.gameObject.activeSelf);
    }
}