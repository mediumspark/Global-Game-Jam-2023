using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Interactables;
using Managers;
using System.Linq; 

//Player State Machine for the character
namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class Player : MonoBehaviour
    {
        public Stats stats; 
        private PlayerControls PlayerControls;
        private bool _impared; 
        public bool IsImpared { get => _impared; set => _impared = value; }
        private CharacterController Controller;
        private Vector3 MoveVector;

        [SerializeField]
        private LayerMask EnemyLayers;
        [SerializeField]
        private LayerMask InteractLayers;

        [Range(10.0f, 25f)]
        public float Speed;

        public bool moveable;

        bool interactableInRange;
        IInteractable interactable; 

        private Animator Ani;

        private void Awake()
        {
            PlayerControls = new PlayerControls();

            PlayerControls.Controls.Movement.performed += Movement_performed;
            PlayerControls.Controls.Movement.canceled += Movement_canceled;
            PlayerControls.Controls.Pause.performed += Pause_performed;
            PlayerControls.Controls.Interact.performed += Interact_performed;

            Controller = GetComponent<CharacterController>();
            Ani = GetComponentInChildren<Animator>(); 
        }

        private void Movement_canceled(InputAction.CallbackContext obj)
        {
            MoveVector = new Vector3(0, MoveVector.y, 0);
            Ani.SetBool("Moving", false);
        }

        private void Update()
        {

            MoveVector.y = -2.0f;
            Controller.Move(MoveVector * Speed * Time.deltaTime);
        }

        private void FixedUpdate()
        {
            if(Physics.CheckSphere(transform.position, 1.0f, EnemyLayers))
            {
               // Debug.Log("Enemy, Battle Start");
                Collider[] col = Physics.OverlapSphere(transform.position, 1.0f, EnemyLayers);
                OverworldEnemy Enemy = col[0].GetComponent<OverworldEnemy>();
                GameManager.Singleton.OWEnemyID = FindObjectsOfType<OverworldEnemy>().ToList().IndexOf(Enemy);
                GameManager.Singleton.GetComponent<Party>().EnemyParty = Enemy.Party;
                GameManager.Singleton.LoadBattleLevel();
            }

            interactableInRange = Physics.CheckSphere(transform.position, 1.5f, InteractLayers);
            if (interactableInRange)
            {
                interactable = Physics.OverlapSphere(transform.position, 1.5f, InteractLayers)[0].GetComponent<IInteractable>(); 
            }
        }

        private void Interact_performed(InputAction.CallbackContext obj)
        {
            if (interactableInRange)
            {
                interactable.OnInteract();
            }
            else
            {
                Debug.Log("Nothing in range");
            }
        }

        private void Pause_performed(InputAction.CallbackContext obj)
        {
        }

        private void Movement_performed(InputAction.CallbackContext obj)
        {
            //Gravity
            if (moveable)
            {
                MoveVector = new Vector3(obj.ReadValue<Vector2>().x, 0, obj.ReadValue<Vector2>().y);
                Ani.SetBool("Moving", true);
                float Rotation = 0;
                if (MoveVector != Vector3.zero)
                {
                    if (MoveVector.z > 0)
                    {
                        Rotation = 0;
                    }
                    else if (MoveVector.z < 0)
                    {
                        Rotation = 180;
                    }
                    else if (MoveVector.x > 0)
                    {
                        Rotation = 90;
                    }
                    else if (MoveVector.x < 0)
                    {
                        Rotation = -90;
                    }
                    transform.GetChild(0).eulerAngles = new Vector3(0, Rotation);

                }
                MoveVector = MoveVector.normalized;
            }
        }

        private void OnEnable() => PlayerControls.Enable();
        private void OnDisable() => PlayerControls.Disable();

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, 1.0f); 
            Gizmos.DrawWireSphere(transform.position, 1.5f); 
        }

#endif
    }
}
