using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarTests
{
    private HealthBar healthBar;
    private Slider slider;
    private Image fill;
    private Gradient gradient;

    [SetUp]
    public void Setup()
    {
        // create GameObject and add necessary components
        var gameObject = new GameObject();
        healthBar = gameObject.AddComponent<HealthBar>();
        slider = gameObject.AddComponent<Slider>();
        fill = gameObject.AddComponent<Image>();

        // assign components to the HealthBar
        healthBar.slider = slider;
        healthBar.fill = fill;

        // create a simple gradient for testing
        gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.red, 0f), new GradientColorKey(Color.green, 1f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1f, 0f), new GradientAlphaKey(1f, 1f) }
        );
        healthBar.gradient = gradient;
    }

    [Test]
    public void SetMaxHealth_SetsSliderValuesCorrectly()
    {
        healthBar.SetMaxHealth(100);

        Assert.AreEqual(100, slider.maxValue);
        Assert.AreEqual(100, slider.value);
        Assert.AreEqual(Color.green, fill.color); // since gradient(1f) should be green
    }

    [Test]
    public void SetHealth_UpdatesSliderValueCorrectly()
    {
        healthBar.SetMaxHealth(100); // set max health

        healthBar.SetHealth(50);

        Assert.AreEqual(50, slider.value);
        Assert.AreEqual(gradient.Evaluate(0.5f), fill.color); // middle of the gradient
    }

    [Test]
    public void SetHealth_UpdatesToZeroCorrectly()
    {
        healthBar.SetMaxHealth(100);
        healthBar.SetHealth(0);

        Assert.AreEqual(0, slider.value);
        Assert.AreEqual(Color.red, fill.color); // gradient(0f) should be red
    }
}