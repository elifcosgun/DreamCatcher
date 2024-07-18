using System;
using System.Collections;
using Dreamteck.Splines;
using UnityEngine;

namespace DreamCatcherAssets.Code.Scripts.AI
{ 
    public class AISensor : MonoBehaviour
    {
        public float distance = 10;
        public float angle = 30;
        public float height = 1.0f;
        public int scanFrequency = 30;
        public LayerMask layers;
        public LayerMask occlusionLayers;
        public GameObject foundObject;
        public Canvas gameOverCanvas;

        private readonly Collider[] _colliders = new Collider[50];
        private int _count;
        private float _scanInterval;
        private float _scanTimer;
        private bool _isHear;
        private Animator _animator;
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");
        
        //Uncomment for editor test
        /*public Color meshColor = Color.red;
        private Mesh _mesh;*/

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            
            _animator.SetBool(IsWalking, true);
            _scanInterval = 1.0f / scanFrequency;
        }

        private void Update()
        {
            _scanTimer -= Time.deltaTime;
            if (_scanTimer < 0)
            {
                _scanTimer += _scanInterval;
                Scan();
            }
        }

        private void Scan()
        {
            _count = Physics.OverlapSphereNonAlloc(transform.position, distance, _colliders, layers,
                QueryTriggerInteraction.Collide);

             if (_count <= 0)
             {
                 foundObject = null;
             }
             else
             {
                 for (int i = 0; i < _count; ++i)
                 {
                     GameObject item = _colliders[i].gameObject;
                     foundObject = item;
                     if (foundObject && IsInSight(foundObject))
                     {
                         gameOverCanvas.gameObject.SetActive(true);
                     }
                 }
             }
        }

        private bool IsInSight(GameObject obj)
        {
            if (!obj) return false;
            
            Vector3 position = transform.position;
            position.y -= 1.06f;
            Vector3 origin = position;
            Vector3 dest = obj.transform.position;
            Vector3 direction = dest - origin;
            
            if (direction.y < 0 || direction.y > height)
            {
                return false;
            }

            direction.y = 0;
            float deltaAngle = Vector3.Angle(direction, transform.forward);
            if (deltaAngle > angle)
            {
                return false;
            }

            origin.y += height / 2;
            dest.y = origin.y;
            
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            Physics.Raycast(transform.position, fwd, out var objectHit, 50);
            
            if (objectHit.transform.CompareTag("Wall") || objectHit.transform.CompareTag("Untagged"))
            {
                return false;
            }
            
            return true;
        }
        
        private void OnValidate()
        {
            //Uncomment for editor test
            //CreateWedgeMesh();
            
            _scanInterval = 1.0f / scanFrequency;
        }

        public void OnHear()
        {
            if (!_isHear)
            {
                _isHear = true;
                Debug.Log("Hear Something");
                StartCoroutine(Rotate(5));
                GetComponent<SplineFollower>().followSpeed = 0;
                _animator.SetBool(IsWalking, false);
                StartCoroutine(StartPatrolling(5));
            }
        }
        
        IEnumerator StartPatrolling(int secs)
        {
            yield return new WaitForSeconds(secs);
            _isHear = false;
            GetComponent<SplineFollower>().followSpeed = 5;
            _animator.SetBool(IsWalking, true);
            Debug.Log("Anyway, continue");
            foundObject = null;
        }
        
        IEnumerator Rotate(float duration)
        {
            float startRotation = transform.eulerAngles.y;
            float endRotation = startRotation + 360.0f;
            float t = 0.0f;
            while ( t  < duration )
            {
                t += Time.deltaTime;
                float yRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
                yield return null;
            }
        }

        //Uncomment for editor test
        /* Mesh CreateWedgeMesh()
        {
            Mesh mesh = new Mesh();

            int segments = 10;
            int numTriangles = (segments * 4) + 2 +2;
            int numVertices = numTriangles * 3;

            Vector3[] vertices = new Vector3[numVertices];
            int[] triangles = new int[numVertices];

            Vector3 bottomCenter = Vector3.zero;
            Vector3 bottomLeft = Quaternion.Euler(0, -angle, 0) * Vector3.forward * distance;
            Vector3 bottomRight = Quaternion.Euler(0, angle, 0) * Vector3.forward * distance;

            Vector3 topCenter = bottomCenter + Vector3.up * height;
            Vector3 topRight = bottomRight + Vector3.up * height;
            Vector3 topLeft = bottomLeft + Vector3.up * height;

            int vert = 0;
            
            //left side
            vertices[vert++] = bottomCenter;
            vertices[vert++] = bottomLeft;
            vertices[vert++] = topLeft;
            
            vertices[vert++] = topLeft;
            vertices[vert++] = topCenter;
            vertices[vert++] = bottomCenter;
            
            //right side
            vertices[vert++] = bottomCenter;
            vertices[vert++] = topCenter;
            vertices[vert++] = topRight;
            
            vertices[vert++] = topRight;
            vertices[vert++] = bottomRight;
            vertices[vert++] = bottomCenter;

            float currentAngle = -angle;
            float deltaAngle = (angle * 2) / segments;
            for (int i = 0; i < segments; ++i)
            {
                bottomLeft = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * distance;
                bottomRight = Quaternion.Euler(0, currentAngle + deltaAngle, 0) * Vector3.forward * distance;
                
                topRight = bottomRight + Vector3.up * height;
                topLeft = bottomLeft + Vector3.up * height;
                
                //far side
                vertices[vert++] = bottomLeft;
                vertices[vert++] = bottomRight;
                vertices[vert++] = topRight;
            
                vertices[vert++] = topRight;
                vertices[vert++] = topLeft;
                vertices[vert++] = bottomLeft;
            
                //top
                vertices[vert++] = topCenter;
                vertices[vert++] = topLeft;
                vertices[vert++] = topRight;
            
                //bottom
                vertices[vert++] = bottomCenter;
                vertices[vert++] = bottomRight;
                vertices[vert++] = bottomLeft;
                
                currentAngle += deltaAngle;
            }

            for (int i = 0; i < numVertices; ++i)
            {
                triangles[i] = i;
            }

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();

            return mesh;
        }

        private void OnDrawGizmos()
        {
            if (_mesh)
            {
                Gizmos.color = meshColor;
                Vector3 position = transform.position;
                position.y -= 1f;
                Gizmos.DrawMesh(_mesh, position, transform.rotation);
            }
            
            Gizmos.DrawWireSphere(transform.position, distance);
            for (int i = 0; i < _count; ++i)
            {
                Gizmos.DrawSphere(_colliders[i].transform.position, 0.2f);
            }

            Gizmos.color = Color.green;
            if(foundObject != null)
            {
                Gizmos.DrawSphere(foundObject.transform.position, 0.2f);
            }
        }*/
    }
}
