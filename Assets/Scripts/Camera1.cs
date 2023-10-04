using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Camera1 : MonoBehaviour
{
    void Update()
    {
        UpdateCamera();
    }

    [ContextMenu("Camera size refresh")]
    void UpdateCamera()
    {
        Vector2 screenSize = ScreenResizer();
        float midScreenHeight = Mathf.Max(5f, (1777 * screenSize.y / screenSize.x) / 200);
        GetComponent<Camera>().orthographicSize = midScreenHeight;
    }
    Vector2 ScreenResizer()
    {
#if UNITY_EDITOR
        System.Type T = System.Type.GetType("UnityEditor.GameView, UnityEditor");
        System.Reflection.MethodInfo GetSizeOfMainGameView = T.GetMethod("GetSizeOfMainGameView", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        System.Object Res = GetSizeOfMainGameView.Invoke(null, null);
        return (Vector2)Res;
#else
        return new Vector2(Screen.width, Screen.height);
#endif
    }
}