using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paint : MonoBehaviour
{

    [SerializeField] private Transform _target;
    private Material _material;

    void Start()
    {
        _material = GetComponent<MeshRenderer>().sharedMaterial;
    }

    void Update()
    {
        Ray ray = new Ray(_target.position + Vector3.up, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, 20))
        {
            _material.SetVector("_Coordinates", new Vector4(hit.textureCoord.x, hit.textureCoord.y, 0, 0));
        }
    }
}
