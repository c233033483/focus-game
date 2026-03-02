using System;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionRay : MonoBehaviour
{
    [SerializeField] private float rayLength = 10f;
    private PlayerInput playerInput;
    private InputAction pressAction;
    private InputAction dragAction;
    private InputAction switchCamAction;
    
    private IDraggable currentlyDragging;

    private bool dragging;

    public SwitchCamera cameraController;
    
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        pressAction = playerInput.actions["Press"];
        dragAction = playerInput.actions["Pos"];
        switchCamAction = playerInput.actions["SwitchCam"];
    }

    private void OnEnable()
    {
        pressAction.performed += OnInteract;
        pressAction.canceled += OnCancelled;
        switchCamAction.performed += SwitchActiveCamera;
    }

    private void OnDisable()
    {
        pressAction.performed -= OnInteract;
        pressAction.canceled -= OnCancelled;
        switchCamAction.performed -= SwitchActiveCamera;
    }
    
    /// <summary>
    /// Called when the pressAction event is detected.
    /// Looks for a clickable object with IClickable
    /// </summary>
    /// <param name="ctx"></param>
    private void OnInteract(InputAction.CallbackContext ctx)
    {
        if (pressAction == null)
            return;

        Vector2 mousePos = dragAction.ReadValue<Vector2>();

        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, rayLength))
        {
            ClickObject(hit.collider.gameObject);
        }
        
        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);
    }

    private void OnCancelled(InputAction.CallbackContext ctx)
    {
        OnDragEnd();
    }

    private void ClickObject(GameObject objectToInteractWith)
    {
        if (objectToInteractWith.TryGetComponent(out IClickable clickableObject))
        {
            Debug.Log("Found clickable object!");
            clickableObject.Interact();
        }

        if (objectToInteractWith.TryGetComponent(out IDraggable draggableObject))
        {
            Debug.Log("Found draggable object!");
            OnDragStart();
        }
    }

    /// <summary>
    /// Called when the dragAction event is detected.
    /// Looks for a draggable object with IDraggable
    /// </summary>
    /// <param name="ctx"></param>
    public void OnDragStart()
    {
        Vector2 mousePos = dragAction.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, rayLength))
        {
            if (hit.collider.TryGetComponent(out IDraggable draggable))
            {
                currentlyDragging = draggable;
                currentlyDragging.BeginDrag(ray);
            }
        }
    }
    
    private void Update()
    {
        if (currentlyDragging == null)
            return;

        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.blue);
        
        currentlyDragging.Drag(ray);
    }
    
    private void OnDragEnd()
    {
        print("STOPPING: " + currentlyDragging);
        currentlyDragging?.EndDrag(); //short for if != null
        currentlyDragging = null;
    }               
    
    private void SwitchActiveCamera(InputAction.CallbackContext ctx)
    {
        cameraController.SwitchActiveCamera(1);
    }
}