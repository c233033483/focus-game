using System;
using UnityEngine;
using UnityEngine.UIElements;

public class DraggableIngredient : MonoBehaviour, IDraggable
{
    [SerializeField] private string ingredientName;
    private Plane draggingPlane;
    private Vector3 offset;

    public bool isInPlacingZone;
    
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
        if (isInPlacingZone)
        {            
            IngredientPlacingEventChannel.PlacingEvent(ingredientName);
            Destroy(gameObject);
        }
    }

    
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("PlaceZone"))
        {
            isInPlacingZone = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("PlaceZone"))
            isInPlacingZone = false;
    }
}
