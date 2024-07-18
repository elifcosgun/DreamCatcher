using DreamCatcherAssets.Code.Scripts.AI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace DreamCatcherAssets.Code.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;
        
        private CustomInputs _inputPlayer;
        
        private Vector3 _playerMovePosition;
        
        private LayerMask _layersToHit;
        
        private Animator _animator;

        private bool _canMagic;
        private bool _dreamCaught;

        private float _timeRemaining = 3.1f;

        private static readonly int IsWalking = Animator.StringToHash("IsWalking");
        private static readonly int IsDoMagic = Animator.StringToHash("IsDoMagic");

        public GameObject finishObject;
        public Canvas dreamCaughtCanvas;

        private void Update()
        {
            if (!_navMeshAgent.pathPending)
            {
                if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
                {
                    _animator.SetBool(IsWalking, false);
                    _navMeshAgent.ResetPath();
                }
                else
                {
                    _animator.SetBool(IsWalking, true);
                    
                    //play footstep sound here
 
                    Collider[] colliders = new Collider[50];
                    Physics.OverlapSphereNonAlloc(transform.position, 10, colliders);
                    
                    foreach (Collider enemy in colliders)
                    {
                        if (enemy != null && enemy.CompareTag($"Enemy"))
                        {
                            enemy.GetComponent<AISensor>().OnHear();
                        }
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.E) && _canMagic)
            {
                _animator.SetBool(IsDoMagic, true);
            }

            if (Input.GetKeyUp(KeyCode.E) || !_canMagic)
            {
                _animator.SetBool(IsDoMagic, false);
                _timeRemaining = 3.1f;
            }
            
            if (!_dreamCaught &&_animator.GetBool(IsDoMagic) && _timeRemaining > 0)
            {
                _timeRemaining -= Time.deltaTime;

                if (_timeRemaining <= 0)
                {
                    _dreamCaught = true;
                    dreamCaughtCanvas.gameObject.SetActive(true);
                    finishObject.SetActive(true);
                }
            }
        }

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _inputPlayer = new CustomInputs();
            _animator = GetComponent<Animator>();
        }

        private void OnMove(InputAction.CallbackContext value)
        {
            _playerMovePosition = Mouse.current.position.ReadValue();

            if (Camera.main != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(_playerMovePosition);
                if (Physics.Raycast(ray, out var hit, Mathf.Infinity, LayerMask.GetMask("Floor", "Object"), QueryTriggerInteraction.Collide))
                {
                    if (hit.collider.CompareTag("Floor"))
                    {
                        _navMeshAgent.isStopped = false;
                        _navMeshAgent.SetDestination(hit.point);
                    }
                }
            }
        }
        private void OnEnable()
        {
            _inputPlayer.PlayerInput.Enable();
            _inputPlayer.PlayerInput.LeftMouseClick.started += OnMove;

        }

        private void OnDisable()
        {
            _inputPlayer.PlayerInput.Disable();
            _inputPlayer.PlayerInput.LeftMouseClick.started -= OnMove;

        }

        public void SetCanDoMagic(bool canMagic)
        {
            _canMagic = canMagic;
        }
    }
}
