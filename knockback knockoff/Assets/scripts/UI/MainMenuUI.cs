using FMODUnity;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private StudioEventEmitter hover;
    [SerializeField] private StudioEventEmitter click;
    [SerializeField] private StudioEventEmitter gameNameAnouncer;

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        hover.Play();
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void playClickSound()
    {
        click.Play();
    }

    public void singlePlayerChosen()
    {
        //load scene
        Invoke(nameof(loadSinglePlayer), 1.5f);
        //play audio saying "knockback knockof" kinda like resident evil
        gameNameAnouncer.Play();
    }

    public void loadSinglePlayer()
    {
        SceneManager.LoadScene(1);
    }

    public void loadMultiplayer()
    {

    }

    public void quit()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
