using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSFX : MonoBehaviour
{
    public void ButtonSFX()
    {
        AudioManager.Instance.PlaySFX("Button");
    }
}
