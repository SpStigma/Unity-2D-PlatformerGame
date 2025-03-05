using UnityEngine;

public class CursorEffect : MonoBehaviour
{
    public Texture2D cursorTexture1;
    public Texture2D cursorTexture2;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    private bool isInMenu = true;

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Cursor.SetCursor(cursorTexture1, hotSpot, cursorMode);
        Cursor.visible = true;
    }

    void Update()
    {
        if (!isInMenu)
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            AudioManager.instance.PlaySFX(0);
            Cursor.SetCursor(cursorTexture2, hotSpot, cursorMode);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(cursorTexture1, hotSpot, cursorMode);
        }
    }

    public void SetMenuState(bool isMenu)
    {
        isInMenu = isMenu;
    }
}
