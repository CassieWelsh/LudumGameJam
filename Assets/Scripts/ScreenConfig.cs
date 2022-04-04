using UnityEngine;

public class ScreenConfig : MonoBehaviour
{
    [Tooltip("Aspect Ratio to use for game.  If Vector2.zero, the default aspect ratio will be used.")]
    [SerializeField]
    private Vector2 aspectRatio = Vector2.zero;

    [Tooltip("Whether or not full screen will be used")]
    [SerializeField]
    private bool fullScreen = false;

    private void Awake()
    {
        if(aspectRatio != Vector2.zero)
        {
            float x = Screen.height * (aspectRatio.x / aspectRatio.y);
            float y = Screen.height;
            Screen.SetResolution((int) x, (int) y, fullScreen);
        }
    }
}