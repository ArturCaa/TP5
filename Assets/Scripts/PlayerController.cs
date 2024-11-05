using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 3.0f;    // Vitesse de déplacement vers l'avant/arrière
    private float turnSpeed = 60.0f;  // Vitesse de rotation
    public float strafeSpeed = 0f;  // Vitesse de déplacement latéral (gauche/droite)
    
    private float horizontalInput;
    private float forwardInput;

    private Rigidbody rb;  // Référence au Rigidbody

    void Start()
    {
        rb = GetComponent<Rigidbody>();  // Obtenir le Rigidbody du joueur
    }

    // Utiliser FixedUpdate pour les interactions physiques
    private void FixedUpdate() 
    {
        // Obtenir les entrées utilisateur
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        // Déplacer l'objet en utilisant le Rigidbody pour respecter la physique
        Vector3 move = transform.forward * forwardInput * speed * Time.fixedDeltaTime;  // Mouvement vers l'avant/en arrière
        Vector3 strafe = transform.right * horizontalInput * strafeSpeed * Time.fixedDeltaTime;  // Mouvement latéral (gauche/droite)

        // Appliquer le mouvement
        // Rigidbody.MovePosition est utilisé pour déplacer un objet tout en respectant la physique d'Unity.
        rb.MovePosition(rb.position + move + strafe);

        // Rotation directe du joueur sur l'axe Y (rotation en fonction de l'entrée horizontale)
        Vector3 rotation = new Vector3(0f, horizontalInput * turnSpeed * Time.fixedDeltaTime, 0f);

        // Appliquer la rotation directement à l'axe Y
        rb.transform.Rotate(rotation);
    }
}





// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PlayerController : MonoBehaviour
// {
//     private float speed = 3.0f;    // Vitesse de déplacement vers l'avant/arrière
//     private float turnSpeed = 60.0f;  // Vitesse de rotation
//     public float strafeSpeed = 5.0f;  // Vitesse de déplacement latéral (gauche/droite)
    
//     private float horizontalInput;
//     private float forwardInput;

//     private Rigidbody rb;  // Référence au Rigidbody

//     void Start()
//     {
//         rb = GetComponent<Rigidbody>();  // Obtenir le Rigidbody du joueur
//     }

//     // Utiliser FixedUpdate pour les interactions physiques
//     private void FixedUpdate() 
//     {
//         // Obtenir les entrées utilisateur
//         horizontalInput = Input.GetAxis("Horizontal");
//         forwardInput = Input.GetAxis("Vertical");

//         // Déplacer l'objet en utilisant le Rigidbody pour respecter la physique
//         Vector3 move = transform.forward * forwardInput * speed * Time.fixedDeltaTime;  // Mouvement vers l'avant/en arrière
//         Vector3 strafe = transform.right * horizontalInput * strafeSpeed * Time.fixedDeltaTime;  // Mouvement latéral (gauche/droite)

//         // Appliquer le mouvement
//         rb.MovePosition(rb.position + move + strafe);

//         // Rotation du joueur
//         float turn = horizontalInput * turnSpeed * Time.fixedDeltaTime;
//         rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, turn, 0f));
//     }
// }
