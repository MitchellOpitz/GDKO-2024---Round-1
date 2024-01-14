using UnityEngine;

public class DemoReturn : MonoBehaviour
{
    private Vector3 lastMousePosition;

    void Start()
    {
        lastMousePosition = Input.mousePosition;
    }

    void Update()
    {
        if (Input.anyKey || HasMouseMoved())
        {
            SceneLoader.Instance.LoadSceneByName("Title");
        }

        lastMousePosition = Input.mousePosition;
    }

    private bool HasMouseMoved()
    {
        return lastMousePosition != Input.mousePosition;
    }
}
