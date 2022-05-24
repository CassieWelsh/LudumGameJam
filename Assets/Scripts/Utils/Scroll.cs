using UnityEngine;

public class Scroll : MonoBehaviour
{
    // [Range(0f, 1f)]
    public float scrollSpeed = .5f;
    public float easing = .2f;
    private float currentSpeed;
    private float offset;
    private Material mat;
    
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        currentSpeed = scrollSpeed;
    }

    void Update()
    {
        currentSpeed = Mathf.Lerp(currentSpeed, scrollSpeed, easing); 
        offset += (Time.deltaTime * currentSpeed) / 10f;
        mat.SetTextureOffset("_MainTex", new Vector2(0,offset));
    }
}