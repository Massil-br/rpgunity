using System.Collections.Generic;
using UnityEngine;

public class Hell : MonoBehaviour
{
    public bool hell = false;

    private readonly List<KeyCode> hellCode = new List<KeyCode> {
        KeyCode.H, KeyCode.E, KeyCode.L, KeyCode.L
    };

    private List<KeyCode> inputBuffer = new List<KeyCode>();

    void Update()
    {
        // Vérifie si une touche a été pressée
        if (Input.anyKeyDown && hell == false && GetComponent<Cheat>().konami == false && GetComponent<Player>().Level == 1)
        {
            // Parcourt toutes les touches du code Konami
            foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(key))
                {
                    inputBuffer.Add(key);

                    // Garde la taille du buffer égale à celle du code
                    if (inputBuffer.Count > hellCode.Count)
                        inputBuffer.RemoveAt(0);

                    // Vérifie si le buffer correspond au code
                    if (IsHellCodeEntered())
                    {
                        Debug.Log("Hell Mode Activé !");
                        TriggerHellAction();
                        inputBuffer.Clear(); // Reset
                    }

                    break;
                }
            }
        }
    }

    private bool IsHellCodeEntered()
    {
        if (inputBuffer.Count != hellCode.Count) return false;

        for (int i = 0; i < hellCode.Count; i++)
        {
            if (inputBuffer[i] != hellCode[i])
                return false;
        }
        return true;
    }

    private void TriggerHellAction()
    {
        GetComponent<Player>().MaxHealthPointCap = 1;
        GetComponent<Player>().MaxHealthPoint = 1;
        GetComponent<Player>().CurrentHealthPoint = 1;
        hell = true;
    }
}