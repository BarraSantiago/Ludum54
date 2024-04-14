using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] Image healthBar;
    [SerializeField] float timeToTurnOffHealthBar = 5f; // Time in seconds to reset the timer

    [SerializeField] AttackableObject attackableObject;

    Quaternion cameraRotation;
     float elapsedTime = 0f;


    private void Start()
    {
        cameraRotation = Camera.main.transform.rotation;

        gameObject.SetActive(false);
    }

    public void SetAttackableObject(AttackableObject attackable)
    {
        attackableObject = attackable;
    }

    public void OnHealthChange()
    {
        gameObject.SetActive(true);
        healthBar.fillAmount = attackableObject.Health / attackableObject.MaxHealth;

        elapsedTime = 0;
    }

    void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            transform.rotation = cameraRotation;

            elapsedTime += Time.deltaTime;

            if (elapsedTime >= timeToTurnOffHealthBar)
            {
                gameObject.SetActive(false);
                elapsedTime = 0f;
            }
        }
    }
}
