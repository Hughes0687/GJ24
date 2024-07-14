using UnityEngine;
using UnityEngine.UIElements;

public class FuelUpdater : MonoBehaviour
{
    public UIDocument uiDocument;
    private VisualElement root;
    private ProgressBar fuelProgressBar;

    void Start()
    {
        root = uiDocument.rootVisualElement;
        fuelProgressBar = root.Q<ProgressBar>("FuelBar");
        if (fuelProgressBar != null)
        {
            fuelProgressBar.title = "Fuel";
        }
    }

    void Update()
    {
        if (fuelProgressBar != null && Player.instance != null)
        {
            fuelProgressBar.value = Player.instance.fuel;
        }
    }
}