using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class HealthBarManager2 : MonoBehaviour
{
    public HealthBar healthBar; // Référence à la barre de santé

    private float currentHealth;
    [SerializeField]
    private float maxHealth = 100f;
    [SerializeField]  
    private TextMeshProUGUI healthText;
    bool isDead;
    private Vector3 lastPosition;
    private float moveTimer = 0f;
    [SerializeField] private ClaireController _player;
    [SerializeField] private GameObject _gameOverPanel;



    void Start()
    {
        Debug.Log("Démarrage du script HealthBarManager"); // Debug pour vérifier que Start est bien appelé
        currentHealth = maxHealth;
        lastPosition = transform.position;
        healthBar.SetMaxHealth(maxHealth); // Initialiser la barre de santé avec la valeur maximale
        StartCoroutine(Energy());
    }

    void Update()
    {
        healthText.text = currentHealth.ToString();

        // Appuyer sur la flèche haut pour infliger des dégâts
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Flèche haut appuyée, inflige 10 points de dégâts");
            TakeDamage(10);
        }


        if (currentHealth <= 0 && !isDead)
        {
     
            _player.ClaireDead();
            StartCoroutine(ShowGameOverPanel());
            
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Battery"))
        {
            Debug.Log("Collision avec un ennemi, +5 HP");
            AddingDamage(5); // Infliger des dégâts
        }
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Limiter la santé entre 0 et la santé max
        Debug.Log("Santé actuelle après dégâts : " + currentHealth); // Vérifier que la santé est correctement calculée
        healthBar.SetHealth(currentHealth); // Appeler la mise à jour de la barre de santé
    }

    public void AddingDamage(float damageCount)
    {
        currentHealth += damageCount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Limiter la santé entre 0 et la santé max
        Debug.Log("Santé actuelle après dégâts : " + currentHealth); // Vérifier que la santé est correctement calculée
        healthBar.SetHealth(currentHealth); // Appeler la mise à jour de la barre de santé
    }


    IEnumerator ShowGameOverPanel()
    {
        _gameOverPanel.SetActive(true); 
        Debug.Log("Game over");

        yield return new WaitForSeconds(5f); 
            
        SceneManager.LoadScene(0); 
    }


    IEnumerator Energy()
    {
        while (currentHealth > 0)
        {
            if (transform.position != lastPosition)
            {
                moveTimer += Time.deltaTime;

                if (moveTimer >= 3f)
                {
                    TakeDamage(3); 
                    moveTimer = 0f; 
                }
            }
            else
            {
                moveTimer = 0f; 
            }

            lastPosition = transform.position;
            yield return null;
        }
        
    }


}
