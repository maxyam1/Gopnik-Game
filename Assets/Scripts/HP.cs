using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    public float hp;
    public CharacterStatus characterStatus;
    public Image hpImage;
    public PlayerController playerController;

    CharacterNavigationController characterNavigationController;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<CharacterNavigationController>()) {
            characterNavigationController = GetComponent<CharacterNavigationController>();
        }
        hp = 100;
        if (characterStatus)
        {
            characterStatus.isAlive = true;
        }
    }

    public void Change(float damage) {
        hp -= damage;
        if(hpImage)
            hpImage.fillAmount = hp / 100f;
        if (hp <= 0) {
            if (characterStatus)
            {
                characterStatus.isAlive = false;
            }
            if (playerController)
            {
                playerController.Die();
            }
            else if (characterNavigationController) {
                characterNavigationController.Die();
            }
        }
    }
  
}
