using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonComponent : MonoBehaviour
{
    public GameObject button;
    public GameObject buttonText;

    private bool active;

    private GameController gameController;

    public void SetGameControllerReference(GameController controller)
    {
        gameController = controller;
        active = true;
    }

    public bool IsActive()
    {
        return active;
    }

    public void SetSpace()
    {
        if (!active) return;
        string current = gameController.GetPlayerSide();
        buttonText.GetComponent<TextMesh>().text = current;
        StopInteraction();
        if (current == "X") gameController.EndTurn();
    }

    public void StopInteraction()
    {
        active = false;
        button.GetComponent<EventTrigger>().enabled = false;
    }

    public void ResetSpace()
    {
        active = true;
        button.GetComponent<EventTrigger>().enabled = true;
        buttonText.GetComponent<TextMesh>().text = "";
    }

    

}
