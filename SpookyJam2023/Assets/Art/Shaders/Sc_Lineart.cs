using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Lineart : MonoBehaviour
{
    [SerializeField] private Material lineartMaterial;
    [SerializeField] private float lineartWidht;
    [SerializeField] private Color lineartColor;
    private Renderer lineartRenderer;
    // Start is called before the first frame update
    void Start()
    {
        lineartRenderer = CreateLineart(lineartMaterial, lineartWidht, lineartColor);
        lineartRenderer.enabled = true;

    }

    Renderer CreateLineart( Material lineartMat, float lineartWidht, Color color)
    {
        GameObject lineartObject = Instantiate(this.gameObject, transform.position, transform.rotation, transform);
        Renderer rend = lineartObject.GetComponent<Renderer>();

        rend.material = lineartMat;
        rend.material.SetColor("_LineartColor", color);
        rend.material.SetFloat("_Scale", lineartWidht);
        rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        lineartObject.GetComponent<Sc_Lineart>().enabled = false;
        lineartObject.GetComponent<Collider>().enabled = false;

        rend.enabled = false;

        return rend;
    }
}
