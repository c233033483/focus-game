using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionRay : MonoBehaviour
{
    [SerializeField] private float rayLength = 10f;
    private PlayerInput playerInput;
    private InputAction pressAction;
    private InputAction dragAction;
    
    private IDraggable currentlyDragging;

    private bool dragging;
    
    [SerializeField] private Camera cam;
    
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        pressAction = playerInput.actions["Press"];
        dragAction = playerInput.actions["Pos"];
    }

    private void OnEnable()
    {
        pressAction.performed += OnInteract;
        pressAction.canceled += OnCancelled;
    }

    private void OnDisable()
    {
        pressAction.performed -= OnInteract;
        pressAction.canceled -= OnCancelled;
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

        Ray ray = cam.ScreenPointToRay(mousePos);
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
        Ray ray = cam.ScreenPointToRay(mousePos);
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
        Ray ray = cam.ScreenPointToRay(mousePos);
        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.blue);
        
        currentlyDragging.Drag(ray);
    }
    
    private void OnDragEnd()
    {
        print("STOPPING: " + currentlyDragging);
        currentlyDragging?.EndDrag(); //short for if != null
        currentlyDragging = null;
    }
}