using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigationTests
{
    private class TestMenuNavigation : MenuNavigation
    {
        public string loadedScene = "";
        public bool quitCalled = false;

        protected override void LoadScene(string sceneName)
        {
            loadedScene = sceneName; // fake scene loading
        }

        protected override void QuitApplication()
        {
            quitCalled = true; // fake quitting
        }
    }

    private TestMenuNavigation menu;

    [SetUp]
    public void Setup()
    {
        var gameObject = new GameObject();
        menu = gameObject.AddComponent<TestMenuNavigation>();
    }

    [Test]
    public void ReturnToMenu_LoadsMenuScene()
    {
        menu.ReturnToMenu();
        Assert.AreEqual("Menu", menu.loadedScene);
    }


    [Test]
    public void QuitGame_CallsApplicationQuit()
    {
        menu.QuitGame();
        Assert.IsTrue(menu.quitCalled);
    }
}
