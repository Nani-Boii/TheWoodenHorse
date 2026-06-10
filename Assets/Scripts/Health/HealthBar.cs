using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHP;
    [SerializeField] private Image totalHPbar;
    [SerializeField] private Image currentHPbar;

    private void Start(){
        totalHPbar.fillAmount = playerHP.currentHealth / 10;
    }
    
    private void Update(){
        currentHPbar.fillAmount = playerHP.currentHealth / 10;
    }
}