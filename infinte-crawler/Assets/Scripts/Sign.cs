using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Sign : MonoBehaviour
{
    public string text;
    public TextMeshProUGUI signText;

    public void changeSignText() {
        signText.text = text;
    }
}
