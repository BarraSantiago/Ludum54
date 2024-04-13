using UnityEngine;
using UnityEngine.EventSystems;

public class DragCards : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [SerializeField] UnitsDataSO unitToSpawn;
    [SerializeField] float tileSize = 500f; // Tamaño de la tile
    [SerializeField] LayerMask rayMask;
    private float maxRayDist = Mathf.Infinity;

    private GameObject ghostInstance;

    Vector3 halfFloorHeight;
    bool isDraggin;
    bool isCancellingInvocation;


    private void Update()
    {
        CheckForCancelInvocation();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }
        isDraggin = true;

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
        if (eventData.button != PointerEventData.InputButton.Left || isCancellingInvocation)
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

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left || isCancellingInvocation)
        {
            isCancellingInvocation = false;
            return;
        }

        isDraggin = false;

        Instantiate(unitToSpawn.unitPrefab, ghostInstance.transform.position, unitToSpawn.unitPrefab.transform.rotation);
        Destroy(ghostInstance);

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

    private void CheckForCancelInvocation()
    {
        if (!isDraggin)
        {
            return;
        }

        if (Input.GetMouseButtonDown(1))
        {
            isCancellingInvocation = true;
            Destroy(ghostInstance);
        }

    }
}