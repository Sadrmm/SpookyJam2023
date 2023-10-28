using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Burst.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Painter : MonoBehaviour
{

    [SerializeField] private bool isPlayer;
    [SerializeField] private LayerMask _paintMask;
    [SerializeField] private float _radius = 0.25f;
    [SerializeField] private float _strength = 1;
    [SerializeField] private float _hardness = 1;

    public static Painter Instance;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Paint();
        }
    }

    public void Paint()
    {
        ThreadStart threadStart = delegate
        {
            Ray ray = new Ray(transform.position + Vector3.up, Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100.0f, _paintMask))
            {
                transform.position = hit.point;
                Paintable p = hit.collider.GetComponent<Paintable>();
                if (p != null)
                {
                    PaintManager.instance.paint(p, hit.point, _radius, _hardness, _strength, isPlayer ? Color.black : Color.green);
                }
            }
        };
        threadStart.Invoke();   
    }
}
