using System;
using UnityEngine;

public class DraggableIngredient : MonoBehaviour, IDraggable
{
    [SerializeField] private Ingredients ingredientName;
    private Plane draggingPlane;
    private Vector3 offset;
    private bool isInPlacingZone;

    public event Action OnIngredientDestroyed;
    
    private const string PLACE_ZONE_TAG = "PlaceZone";
    
    public void BeginDrag(Ray ray)
    {
        draggingPlane = new Plane(Vector3.up, transform.position);
        
        if (draggingPlane.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            offset = transform.position - hitPoint;
        }
    }
    
    public void Drag(Ray ray)
    {
        if (draggingPlane.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            transform.position = hitPoint + offset;
        }
    }

    public void EndDrag()
    {
        if (!isInPlacingZone) return;
        
        OnIngredientDestroyed?.Invoke();
        IngredientPlacingEventChannel.PlacingEvent(ingredientName);
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(PLACE_ZONE_TAG))
        {
            isInPlacingZone = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag(PLACE_ZONE_TAG))
            isInPlacingZone = false;
    }
}
