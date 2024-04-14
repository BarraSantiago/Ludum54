using System;
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
    [SerializeField] Transform[] limits = null;

    [Header("UI")]
    [SerializeField] private Button btnBuy = null;
    [SerializeField] private TextMeshProUGUI txtName = null;
    [SerializeField] private TextMeshProUGUI txtCost = null;
    [SerializeField] private Image imgCooldown = null;
    [SerializeField] private Image imgEnabled = null;
    [SerializeField] private TextMeshProUGUI txtCooldown = null;
    #endregion

    #region PRIVTAE_FIELDS
    private float maxRayDist = Mathf.Infinity;
    private GameObject ghostInstance;
    private Vector3 halfFloorHeight;
    private bool isDraggin;
    private bool isCancellingInvocation;

    private float cooldown = 0;
    private bool canBuy = false;
    private bool cooldownActive = false;
    #endregion

    #region ACTIONS
    public Action<int> onBuyItem = null;
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

        if (cooldownActive)
        {
            UpdateCooldown();
        }
    }
    #endregion

    #region PUBLIC_METHODS
    public void UpdateStateByEnergyLeft(int energyLeft)
    {
        canBuy = unitToSpawn.cost <= energyLeft;

        if (canBuy)
        {
            imgEnabled.gameObject.SetActive(false);

            if (!cooldownActive)
            {
                btnBuy.interactable = true;
            }
        }
        else
        {
            if (!cooldownActive)
            {
                imgEnabled.gameObject.SetActive(true);
                btnBuy.interactable = false;
            }
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
            imgCooldown.fillAmount = (cooldown / UnitsDataSO.spawnCooldown);
        }
    }

    private void ToggleCooldown(bool status)
    {
        cooldownActive = status;

        imgCooldown.gameObject.SetActive(status);

        btnBuy.interactable = !status;

        if (status)
        {
            cooldown = UnitsDataSO.spawnCooldown;            
        }
        else
        {
            if (!canBuy)
            {
                imgEnabled.gameObject.SetActive(true);
                btnBuy.interactable = false;
            }
        }
    }
    #endregion

    #region DRAG_BEHAVIOUR
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (cooldownActive || !canBuy)
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
        if (cooldownActive || !canBuy)
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
        if (cooldownActive || !canBuy)
        {
            return;
        }

        if (eventData.button != PointerEventData.InputButton.Left || isCancellingInvocation)
        {
            isCancellingInvocation = false;
            return;
        }

        isDraggin = false;
        Destroy(ghostInstance);

        if (ghostInstance.transform.position.z < limits[0].position.z && ghostInstance.transform.position.z > limits[1].position.z &&
            ghostInstance.transform.position.x > limits[0].position.x && ghostInstance.transform.position.x < limits[1].position.x)
        {
            GameObject unit = Instantiate(unitToSpawn.unitPrefab, ghostInstance.transform.position, unitToSpawn.unitPrefab.transform.rotation);
            
            unit.GetComponent<BasicUnit>().SetUnitData(unitToSpawn, Team.BlueTeam);

            ToggleCooldown(true);

            onBuyItem.Invoke(unitToSpawn.cost);
        }       
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
        return new Vector3((int)snappedX, position.y, (int)snappedZ);
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
    #endregion
}