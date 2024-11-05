//using UnityEngine;

//public class HealthBarManager : MonoBehaviour

//{

//    public HealthBar healthBar; // Référence à la barre de santé (assurez-vous qu'elle est assignée dans l'inspecteur)

//    private float currentHealth;

//    private float maxHealth = 100f;

//    void Start()

//    {

//        currentHealth = maxHealth;

//        healthBar.SetMaxHealth(maxHealth); // Initialiser la barre de santé avec la valeur maximale

//        Debug.Log("Santé initialisée à : " + currentHealth); // Vérifiez que la santé est bien initialisée

//    }

//    void Update()

//    {

//        foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))

//        {

//            if (Input.GetKey(vKey))

//            {

//                Debug.Log("Touche détectée : " + vKey);
//                TakeDamage(10);

//            }

//        }

//    }

//    // Fonction pour infliger des dégâts au joueur

//    public void TakeDamage(float damageAmount)

//    {

//        currentHealth -= damageAmount;

//        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Limiter la santé entre 0 et la santé max

//        Debug.Log("Dégâts reçus. Santé actuelle : " + currentHealth); // Vérifiez la santé après avoir pris des dégâts

//        healthBar.SetHealth(currentHealth); // Mettre à jour l'interface utilisateur de la barre de santé

//    }

//}


using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class HealthBarManager : MonoBehaviour
{
    public HealthBar healthBar; // Référence à la barre de santé

    private float currentHealth;
    [SerializeField]
    private float maxHealth = 100f;
    [SerializeField]  
    private TextMeshProUGUI healthText;
    bool isDead;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private ClaireController _player;
    private Coroutine damageCoroutine;


    void Start()
    {
        Debug.Log("Démarrage du script HealthBarManager"); // Debug pour vérifier que Start est bien appelé
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth); // Initialiser la barre de santé avec la valeur maximale
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
        if (other.CompareTag("Insect"))
        {
            if (damageCoroutine == null) 
            {
                damageCoroutine = StartCoroutine(DamageOverTime()); 
                Debug.Log("Collision avec un ennemi, -5 HP");
              
            }
            
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Insect"))
        {
            Debug.Log("Sortie de la collision avec l'ennemi, arrêt des dégâts");
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine); // Arrêter la coroutine
                damageCoroutine = null; // Réinitialiser la coroutine
            }
        }
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
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

    IEnumerator DamageOverTime()
    {
        while (currentHealth > 0)
        {
            TakeDamage(5);  
            yield return new WaitForSeconds(2f); 
        } 
    }



}
