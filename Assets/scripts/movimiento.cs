using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimiento : MonoBehaviour
{ 
    // Velocidad de movimiento del personaje
    [SerializeField] private float velocidadMovimiento;

    // Velocidad de rotación del personaje
    [SerializeField] private float velocidadRotacion;

    // Componentes necesarios para el movimiento del personaje
    [SerializeField] private CharacterController characterController; // Componente CharacterController para mover al personaje
    [SerializeField] private Transform transformPersonaje; // Transform del objeto del personaje para aplicar rotaciones
    [SerializeField] private Transform camaraTransform; // Transform de la cámara para aplicar rotaciones verticales

    // Variables para almacenar la dirección de movimiento y rotación en el eje X
    private Vector3 movimiento_personaje;
    private float rotacionX;

    // Velocidad de movimiento cuando el personaje está corriendo
    [SerializeField] private float velocidadCorrer;

    // Velocidad gradual de transición entre la velocidad normal y la velocidad de correr
    [SerializeField] private float velocidadTransicion = 1f;
    public float velocidadActual; // Velocidad actual del personaje
    public bool corriendo = false;

    // Función llamada en cada frame para actualizar el movimiento y la rotación
    private void Update() {
        movimientoPersonaje();   // Actualiza el movimiento del personaje
        movimientoCamara();   // Actualiza la rotación de la cámara
    }

     void FixedUpdate() {
          if(velocidadActual <= (velocidadMovimiento * 3) && corriendo == true ){
            velocidadActual+=10*Time.deltaTime;
            Debug.Log(velocidadActual); 
          }else if (velocidadActual > velocidadMovimiento){
            velocidadActual-=10*Time.deltaTime;
            corriendo = false;
          }
    }

    // Función para controlar el movimiento del personaje
    void movimientoPersonaje(){
        // Obtener la entrada del eje horizontal (izquierda/derecha) y vertical (adelante/atrás)
        float movX = Input.GetAxis("Horizontal");
        float movZ = Input.GetAxis("Vertical");

        // Calcular el vector de movimiento basado en las entradas de los ejes X y Z
        movimiento_personaje = (transform.right * movX + transform.forward * movZ).normalized; // Normalizar el vector para evitar velocidades aumentadas en diagonales
        
        // Calcular la velocidad actual del personaje
        if(Input.GetKey(KeyCode.LeftShift)) {
            corriendo = true;
            
        }else{
            corriendo = false;
            
        }
        // Aplicar movimiento al CharacterController para desplazar al personaje
        characterController.SimpleMove(movimiento_personaje * velocidadActual);
    }

    // Función para controlar la rotación de la cámara
    void movimientoCamara(){
        // Obtener la entrada del ratón en el eje X e Y para rotar la cámara
        float ratonX = Input.GetAxis("Mouse X") * velocidadRotacion; 
        float ratonY = Input.GetAxis("Mouse Y") * velocidadRotacion;

        
        rotacionX -= ratonY; // Actualizar la rotación en el eje X (horizontal) basada en la entrada del ratón 
        rotacionX = Mathf.Clamp(rotacionX, -45f, 45); // Limitar la rotación en el eje Y para evitar que la cámara haga giros completos
        transformPersonaje.Rotate(Vector3.up * ratonX); // Rotar el objeto del personaje (no la cámara) en el eje Y según la entrada del ratón
        camaraTransform.localRotation = Quaternion.Euler(rotacionX, 0f, 0f); // Rotar la cámara en el eje X (vertical) basada en la entrada del ratón
    }
}
