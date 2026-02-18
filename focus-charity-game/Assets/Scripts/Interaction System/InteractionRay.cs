using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionRay : MonoBehaviour
{
    [SerializeField] private float rayLength = 10f;
    private PlayerInput playerInput;
    private InputAction pressAction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        pressAction = playerInput.actions["Interact"];
    }

    private void OnEnable()
    {
        pressAction.performed += OnInteract;
    }

    private void OnDisable()
    {
        pressAction.performed -= OnInteract;
    }
    
    public void OnInteract(InputAction.CallbackContext ctx)
    {
        if (Mouse.current == null)
            return;
        
        // Get mouse position (screen space)
        Vector2 mousePos = Mouse.current.position.ReadValue();

        // Create ray from camera through mouse
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        // Draw debug ray
        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);

        // Raycast
        if (Physics.Raycast(ray, out RaycastHit hit, rayLength))
        {
            InteractWithObject(hit.collider.gameObject);
        }
    }

    void InteractWithObject(GameObject objectToInteractWith)
    {
        if (objectToInteractWith.TryGetComponent(out IClickable clickableObject))
        {
            Debug.Log("Found object!");
            clickableObject.Interact();
        }
    }
}