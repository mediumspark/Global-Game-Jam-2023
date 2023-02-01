using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Interactables;
using Managers; 

//Player State Machine for the overworld character
namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class Player : MonoBehaviour
    {
        private PlayerControls PlayerControls;
        private bool _impared; 
        public bool IsImpared { get => _impared; set => _impared = value; }
        private CharacterController Controller;
        private Vector3 MoveVector;

        [SerializeField]
        private LayerMask EnemyLayers;
        [SerializeField]
        private LayerMask InteractLayers;

        [Range(0.0f, 10f)]
        public float Speed; 

        private void Awake()
        {
            PlayerControls = new PlayerControls();

            PlayerControls.Controls.Movement.performed += Movement_performed;
            PlayerControls.Controls.Movement.canceled += ctx => MoveVector = new Vector3(0, MoveVector.y, 0); 
            PlayerControls.Controls.Pause.performed += Pause_performed;
            PlayerControls.Controls.Interact.performed += Interact_performed;

            Controller = GetComponent<CharacterController>(); 
        }

        private void Update()
        {
            //Gravity
            MoveVector.y = -2.0f; 
            Controller.Move(MoveVector * Speed * Time.deltaTime);
        }

        private void FixedUpdate()
        {
            if(Physics.CheckSphere(transform.position, 1.0f, EnemyLayers))
            {
                Debug.Log("Enemy, Battle Start");
                Collider[] col = Physics.OverlapSphere(transform.position, 1.0f, EnemyLayers);
                BattleManager BM = GameManager.Singleton.GetComponent<BattleManager>(); 
                OverWorldEnemy Enemy = col[0].GetComponent<OverWorldEnemy>();
                BM.StartBattle(Enemy); 
            }

        }

        private void Interact_performed(InputAction.CallbackContext obj)
        {
            
        }

        private void Pause_performed(InputAction.CallbackContext obj)
        {
        }

        private void Movement_performed(InputAction.CallbackContext obj)
        {
            MoveVector = new Vector3(obj.ReadValue<Vector2>().x, -2, obj.ReadValue<Vector2>().y);
            MoveVector = MoveVector.normalized;

        }
        private void OnEnable() => PlayerControls.Enable();
        private void OnDisable() => PlayerControls.Disable();

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, 1.0f); 
        }

#endif
    }
}
