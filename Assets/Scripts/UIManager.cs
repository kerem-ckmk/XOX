using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI matchScoreTMP;
    public Button rebuildButton;
    public TMP_InputField sizeInputField;
    public GameplayController gameplayController;

    private void Awake()
    {
        rebuildButton.onClick.AddListener(gameplayController.GridRebuild);
    }

    private void Start()
    {
        gameplayController.Initialize();
    }
}
