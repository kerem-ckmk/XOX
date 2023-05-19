using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI scoreText;
    public Button rebuildButton;
    public TMP_InputField sizeInputField;
    public GameplayController gameplayController;

    private void Awake()
    {
        scoreText.text = "Score: 0";
        rebuildButton.onClick.AddListener(() => gameplayController.GridRebuild(sizeInputField.text));
    }

    private void Start()
    {
        gameplayController.Initialize();
        gameplayController.OnChangedInputText += GameplayController_OnChangedInputText;
        gameplayController.OnScoreUpdate += GameplayController_OnMatchScoreUpdate;
    }

    private void GameplayController_OnMatchScoreUpdate(int matchScore)
    {
        scoreText.text = string.Format("Score: {0}", matchScore);
    }

    private void GameplayController_OnChangedInputText(int gridSize)
    {
        sizeInputField.text = gridSize.ToString();
    }
}
