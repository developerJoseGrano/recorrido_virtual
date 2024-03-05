using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class funciones : MonoBehaviour
{
    [SerializeField] private float transitionTime = 1f;
    private Animator transitionAnimator;

    // Start is called before the first frame update
    void Start()
    {
        transitionAnimator = GetComponentInChildren<Animator>();
    }

    // Elimina el método Update si no lo necesitas para otra cosa

    public void cambio(string escena)
    {
        // Obtener el índice de la escena a cargar
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;

        // Cargar la nueva escena
        SceneManager.LoadScene(escena);

        // Iniciar la animación de transición
        StartCoroutine(SceneLoad(nextSceneIndex));
    }

    public IEnumerator SceneLoad(int sceneIndex)
    {
        // Verificar si transitionAnimator es null
        if (transitionAnimator == null)
        {
            Debug.LogError("Animator no está asignado en " + gameObject.name);
            yield break; // Salir del método si transitionAnimator es null
        }

        // Disparar el trigger para reproducir animacion FadeIn
        transitionAnimator.SetTrigger("StartTransition");
        
        // Esperar un segundo
        yield return new WaitForSeconds(transitionTime);

        // Cargar la escena
        SceneManager.LoadScene(sceneIndex);
    }
}
