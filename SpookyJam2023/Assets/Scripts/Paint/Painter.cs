using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Burst.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Painter : MonoBehaviour
{
    [SerializeField] private bool calculate = false;
    [SerializeField] private Color _paintColor;
    [SerializeField] private float _radius = 1;
    [SerializeField] private float _strength = 1;
    [SerializeField] private float _hardness = 1;

    public static Painter Instance;

    public void Paint()
    {
        Ray ray = new Ray(transform.position + Vector3.up, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            Debug.DrawRay(ray.origin, hit.point - ray.origin, Color.red);
            transform.position = hit.point;
            Paintable p = hit.collider.GetComponent<Paintable>();
            if (p != null)
            {
                PaintManager.instance.paint(p, hit.point, _radius, _hardness, _strength, _paintColor);
            }
        }
    }
}
