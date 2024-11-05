using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public float smoothSpeed = 10f;

    // Fonction pour initialiser la barre de santé à la valeur maximale
    public void SetMaxHealth(float maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }

    // Utilise une Coroutine pour mettre à jour progressivement la barre de santé
    public void SetHealth(float health)
    {
        StartCoroutine(SmoothHealthTransition(health));
    }

    // Coroutine pour animer la transition de la barre de santé
    private IEnumerator SmoothHealthTransition(float targetHealth)
    {
        while (Mathf.Abs(healthSlider.value - targetHealth) > 0.5f)
        {
            healthSlider.value = Mathf.Lerp(healthSlider.value, targetHealth, Time.deltaTime * smoothSpeed);
            yield return null; // Attendre le frame suivant avant de continuer la boucle
        }
        healthSlider.value = targetHealth;
        Canvas.ForceUpdateCanvases(); // Assurez-vous que le Canvas est à jour
    }



}



