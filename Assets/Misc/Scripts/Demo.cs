using UnityEngine;

public class DemoScene : MonoBehaviour
{
    private float inactivityTimer = 20f;
    private Vector3 lastMousePosition;

    void Start()
    {
        lastMousePosition = Input.mousePosition;
    }

    void Update()
    {
        inactivityTimer -= Time.deltaTime;

        if (inactivityTimer <= 0)
        {
            SceneLoader.Instance.LoadSceneByName("DemoScene");
        }

        if (Input.anyKey || HasMouseMoved())
        {
            inactivityTimer = 20f;
        }

        lastMousePosition = Input.mousePosition;
    }

    private bool HasMouseMoved()
    {
        return lastMousePosition != Input.mousePosition;
    }
}
