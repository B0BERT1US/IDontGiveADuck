

using UnityEngine;

public class BackgroundSwitch : MonoBehaviour
{
    public SpriteRenderer Hud_bg;

    public Sprite BG1;
    public Sprite BG2;
    public Sprite BG3;
    public Sprite BG4;

    public GameManager gameManager;

    public void ChangeBackground()
    {
        if (gameManager == null || Hud_bg == null)
        {
            return;
        }

        switch (gameManager.CurrentLevelId)
        {
            case 1:
                Hud_bg.sprite = BG1;
                break;
            case 4:
                Hud_bg.sprite = BG2;
                break;
            case 7:
                Hud_bg.sprite = BG3;
                break;
            case 10:
                Hud_bg.sprite = BG4;
                break;
            default:
                break;
        }

        FitSpriteToScreen(Hud_bg);
    }

    private void FitSpriteToScreen(SpriteRenderer sr)
    {
        if (sr == null || sr.sprite == null)
        {
            return;
        }

        Camera cam = Camera.main;
        if (cam == null)
        {
            return;
        }

        float worldHeight = cam.orthographicSize * 2f;
        float worldWidth = worldHeight * cam.aspect;

        Vector2 spriteWorldSize = sr.sprite.bounds.size;

        if (spriteWorldSize.x <= 0f || spriteWorldSize.y <= 0f)
        {
            return;
        }

        float scaleX = worldWidth / spriteWorldSize.x;
        float scaleY = worldHeight / spriteWorldSize.y;

        Vector3 newLocalScale = new Vector3(scaleX, scaleY, sr.transform.localScale.z);
        sr.transform.localScale = newLocalScale;
    }
}