using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragCards : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [SerializeField] UnitsDataSO unitToSpawn;
    [SerializeField] float tileSize = 500f; // Tamaño de la tile
    [SerializeField] LayerMask rayMask;
    private float maxRayDist = Mathf.Infinity;

    private GameObject ghostInstance;
    private GameObject ghostModel;

    Vector3 halfFloorHeight;
    Vector2 lastMousePosition;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxRayDist, rayMask))
        {
            Vector3 hitPoint = hit.point;
            hitPoint = SnapToGrid(hitPoint);
            ghostInstance = Instantiate(unitToSpawn.ghostPrefab, hitPoint, unitToSpawn.ghostPrefab.transform.rotation);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxRayDist, rayMask))
        {
            float offset = hit.transform.localScale.y;
            halfFloorHeight = new Vector3(0, offset / 2, 0);

            Vector3 hitPoint = hit.point;
            SetGhostPositionWithMousePos(SnapToGrid(hitPoint));
        }

        lastMousePosition = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }

        // Aquí podrías hacer comprobaciones adicionales, como si el movimiento es válido, si se puede colocar en esa posición, etc.
    }

    private void SetGhostPositionWithMousePos(Vector3 position)
    {
        if (ghostInstance)
        {
            ghostInstance.transform.position = position + halfFloorHeight;
            ghostInstance.transform.SetAsLastSibling();
        }
    }

    private Vector3 SnapToGrid(Vector3 position)
    {
        float snappedX = Mathf.Round(position.x / tileSize) * tileSize;
        float snappedZ = Mathf.Round(position.z / tileSize) * tileSize;
        return new Vector3(snappedX, position.y, snappedZ);
    }
}