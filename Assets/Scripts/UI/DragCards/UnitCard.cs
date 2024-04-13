using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitCard : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    #region EXPOSED_FIELDS
    [SerializeField] UnitsDataSO unitToSpawn;
    [SerializeField] float tileSize; // Tama�o de la tile
    [SerializeField] LayerMask rayMask;

    [Header("UI")]
    [SerializeField] private Button btnBuy = null;
    [SerializeField] private TextMeshProUGUI txtName = null;
    [SerializeField] private TextMeshProUGUI txtCost = null;
    [SerializeField] private Image imgCooldown = null;
    [SerializeField] private TextMeshProUGUI txtCooldown = null;
    #endregion

    #region PRIVTAE_FIELDS
    private float maxRayDist = Mathf.Infinity;
    private GameObject ghostInstance;
    private Vector3 halfFloorHeight;
    private bool isDraggin;
    private bool isCancellingInvocation;

    private float cooldown = 0;
    private bool canBuy = true;
    #endregion

    #region UNITY_CALLS
    private void Start()
    {
        txtCost.text = unitToSpawn.cost.ToString();
        txtName.text = unitToSpawn.name;
    }

    private void Update()
    {
        CheckForCancelInvocation();

        if (!canBuy)
        {
            UpdateCooldown();
        }
    }
    #endregion

    #region PRIVATE_METHODS
    private void UpdateCooldown()
    {
        cooldown -= Time.deltaTime;

        if (cooldown <= 0)
        {
            ToggleCooldown(false);
        }
        else
        {
            txtCooldown.text = ((int)cooldown).ToString();
            imgCooldown.fillAmount = 1 - (cooldown / UnitsDataSO.spawnCooldown);
        }
    }

    private void ToggleCooldown(bool status)
    {
        canBuy = !status;
        imgCooldown.gameObject.SetActive(status);
        btnBuy.interactable = !status;

        if (status)
        {            
            cooldown = UnitsDataSO.spawnCooldown;            
        }
    }
    #endregion

    #region DRAG_BEHAVIOUR
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!canBuy)
        {
            return;
        }

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
        if (!canBuy)
        {
            return;
        }

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
        if (!canBuy)
        {
            return;
        }

        if (eventData.button != PointerEventData.InputButton.Left || isCancellingInvocation)
        {
            isCancellingInvocation = false;
            return;
        }

        isDraggin = false;

        Instantiate(unitToSpawn.unitPrefab, ghostInstance.transform.position, unitToSpawn.unitPrefab.transform.rotation);
        Destroy(ghostInstance);

        ToggleCooldown(true);
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
        if (!isDraggin || canBuy)
        {
            return;
        }

        if (Input.GetMouseButtonDown(1))
        {
            isCancellingInvocation = true;
            Destroy(ghostInstance);
        }

    }
    #endregion
}