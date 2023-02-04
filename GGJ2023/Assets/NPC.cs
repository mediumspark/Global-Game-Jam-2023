using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using Interactables;
using Managers; 

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField]
    string Block;

    public void OnInteract()
    {
        PlayBlock(); 
    }

    private void PlayBlock()
    {
        GameManager.Singleton.Flow.ExecuteBlock(Block);
    }

}
