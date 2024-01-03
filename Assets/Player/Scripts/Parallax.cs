using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public Sprite sprite;
    public int horizontalCount = 3;
    public int verticalCount = 2;
    public float scrollSpeed = 0.5f;

    private float spriteHeight;
    private float spriteWidth;
    private Camera mainCamera;
    private GameObject[,] grid;

    void Start()
    {
        if (sprite == null)
        {
            Debug.LogError("Sprite is not assigned!");
            return;
        }

        spriteHeight = sprite.bounds.size.y;
        spriteWidth = sprite.bounds.size.x;
        mainCamera = Camera.main;
        CreateGrid();
    }

    void Update()
    {
        ScrollBackground();
    }

    void CreateGrid()
    {
        grid = new GameObject[horizontalCount, verticalCount];

        Vector2 cameraCenter = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, mainCamera.nearClipPlane));
        Vector2 startPosition = new Vector2(cameraCenter.x - spriteWidth * (horizontalCount - 1) / 2, cameraCenter.y);

        for (int i = 0; i < horizontalCount; i++)
        {
            for (int j = 0; j < verticalCount; j++)
            {
                Vector2 position = new Vector2(i * spriteWidth, j * spriteHeight) + startPosition;
                GameObject spriteObj = new GameObject("ParallaxSprite");
                SpriteRenderer spriteRenderer = spriteObj.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = sprite;
                spriteRenderer.sortingOrder = -1; // Set the order in layer to -1
                spriteObj.transform.position = position;
                spriteObj.transform.SetParent(transform, false);
                grid[i, j] = spriteObj;
            }
        }
    }

    void ScrollBackground()
    {
        Vector2 cameraBottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));

        for (int i = 0; i < horizontalCount; i++)
        {
            for (int j = 0; j < verticalCount; j++)
            {
                GameObject currentSpriteObj = grid[i, j];
                currentSpriteObj.transform.Translate(Vector2.down * scrollSpeed * Time.deltaTime);

                if (currentSpriteObj.transform.position.y + spriteHeight < cameraBottomLeft.y)
                {
                    Vector2 newPosition = new Vector2(currentSpriteObj.transform.position.x, (verticalCount - 1) * spriteHeight + cameraBottomLeft.y);
                    currentSpriteObj.transform.position = newPosition;
                }
            }
        }
    }
}
