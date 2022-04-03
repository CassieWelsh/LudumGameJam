using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenConfig : MonoBehaviour
{
    private float _lastWidth;
    private float _lastHeight;    

    void Start()
    {
        // Screen.SetResolution(1080, 1920, true);
        _lastWidth = Screen.width;
        _lastHeight = Screen.height;
    }

    void Update()
    {
        if(_lastWidth != Screen.width)
        {
            Screen.SetResolution(Screen.width, (int) (Screen.width * 4f / 3f), true);
        }
        else if(_lastHeight != Screen.height)
        {
            Screen.SetResolution((int) (Screen.height * (3f / 4f)), Screen.height, true);
        }

        _lastWidth = Screen.width;
        _lastHeight = Screen.height;
    }
}
