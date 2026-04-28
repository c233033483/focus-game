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

    private Camera mainCamera;
    public SwitchCamera cameraController;
    
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        pressAction = playerInput.actions["Press"];
        dragAction = playerInput.actions["Pos"];
        switchCamAction = playerInput.actions["SwitchCam"];
        
        mainCamera = Camera.main;
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
    
    public void SetInteractionEnabled(bool interactionEnabled)
    {
        if (interactionEnabled)
            pressAction.Enable();
        else
            pressAction.Disable();
    }
    
    
    private void OnInteract(InputAction.CallbackContext ctx)
    {
        Vector2 mousePos = dragAction.ReadValue<Vector2>();
        Ray ray = mainCamera.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hit, rayLength))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject.TryGetComponent(out IngredientSelector selector))
            {
                DraggableIngredient spawned = selector.SpawnIngredient(hit.point);
                if (spawned != null)
                {
                    currentlyDragging = spawned;
                    currentlyDragging.BeginDrag(ray);
                }
            }
            else if (hitObject.TryGetComponent(out IDraggable draggable))
            {
                currentlyDragging = draggable;
                currentlyDragging.BeginDrag(ray);
            }
            else if (hitObject.TryGetComponent(out IClickable clickable))
            {
                clickable.Interact();
            }
        }

        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);
    }

    private void OnCancelled(InputAction.CallbackContext ctx)
    {
        OnDragEnd();
    }

    /*
    private void ClickObject(GameObject objectToInteractWith)
    {
        if (objectToInteractWith.TryGetComponent(out IClickable clickableObject))
        {
            #if UNITY_EDITOR
            Debug.Log("Found clickable object!");
            #endif
            clickableObject.Interact();
        }
        else if (objectToInteractWith.TryGetComponent(out IDraggable draggableObject))
        {
            #if UNITY_EDITOR
            Debug.Log("Found draggable object!");
            #endif
            OnDragStart();
        }
    }
    
    private void OnDragStart()
    {
        var mousePos = dragAction.ReadValue<Vector2>();
        var ray = mainCamera.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, rayLength))
        {
            if (hit.collider.TryGetComponent(out IDraggable draggable))
            {
                currentlyDragging = draggable;
                currentlyDragging.BeginDrag(ray);
            }
        }
    }
    */
    
    private void Update()
    {
        if (currentlyDragging == null)
            return;

        Vector2 mousePos = dragAction.ReadValue<Vector2>();
        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.blue);
        
        currentlyDragging.Drag(ray);
    }
    
    private void OnDragEnd()
    {
        currentlyDragging?.EndDrag(); //short for if != null
        currentlyDragging = null;
    }               
    
    private void SwitchActiveCamera(InputAction.CallbackContext ctx)
    {
        cameraController.SwitchActiveCamera(1);
    }
}