using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragCards : MonoBehaviour, IDragHandler, IEndDragHandler,IBeginDragHandler
{
    [SerializeField] UnitsDataSO unitToSpawn;

    [SerializeField] LayerMask rayMask;
    private float maxRayDist = Mathf.Infinity;

    private GameObject ghostInstance;
    private GameObject ghostModel;

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
            ghostInstance = Instantiate(unitToSpawn.unitPrefab, hit.point, unitToSpawn.unitPrefab.transform.rotation);
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
            float offset = hit.transform.localScale.y; //Es lo que le sumamos en y al ghost, que es la mitad del tamaño del tile en y

            SetGhostPositionWithMousePos(hit.point);
        }

        lastMousePosition = eventData.position;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }

        //Aca habria que hacer la comprobacion de si te alcanza el mana
    }

    private void SetGhostPositionWithMousePos(Vector3 position)
    {
        if (ghostInstance)
        {
            ghostInstance.transform.position = position;
            ghostInstance.transform.SetAsLastSibling();
        }
    }
}
