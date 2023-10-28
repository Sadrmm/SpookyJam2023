using System;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class CaracolEnemyAI : BaseEnemyAI
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] [Range(1, 30)] private float _radius;

    [SerializeField] private Painter _painter;
    private Vector3 _destination;
    private bool _isWalkPointSet = false;

    private int _tries = 0;

    private void Start()
    {
        Init(transform);
    }

    protected override void Pathfinding()
    {
        if (!_isWalkPointSet)
        {
            SearchWalkPoint();
        }

        if (_isWalkPointSet)
        {
            _painter.Paint();
            _agent.SetDestination(_destination);
        }

        Vector3 distanceToWalkPoint = transform.position - _destination;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            _isWalkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomX = Random.Range(-_radius, _radius);
        float randomY = Random.Range(-_radius, _radius);
        Vector3 rayOrigin = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomY);
        Ray ray = new Ray(rayOrigin, Vector3.down);

        
        if (Physics.Raycast(ray, out RaycastHit hit, 20f, _groundLayer))
        {
            Paintable p = hit.collider.GetComponent<Paintable>();
            if (p != null)
            {
                _tries++;
                RenderTexture mask = p.getMask();

                Texture2D temp = new Texture2D(mask.width, mask.height, TextureFormat.RGBAFloat, false);
                var oldRT = RenderTexture.active;
                RenderTexture.active = mask;
                temp.ReadPixels(new Rect(0, 0, temp.width, temp.height), 0, 0, false);
                temp.Apply();
                RenderTexture.active = oldRT;
                Color pixelColor = temp.GetPixel((int) hit.textureCoord.x, (int) hit.textureCoord.y);

                Debug.Log(pixelColor);

                if (pixelColor.a > 0.35)
                {
                    Debug.Log("NORMAL");
                    _isWalkPointSet = true;
                    _destination = hit.point;
                }
                else if (_tries > 100)
                {
                    Debug.Log("TRY");
                    _isWalkPointSet = true;
                    _destination = hit.point;
                    _tries = 0;
                }
                Destroy(temp);   
            }          
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

}
