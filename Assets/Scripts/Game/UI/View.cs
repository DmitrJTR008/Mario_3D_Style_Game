using System;
using UnityEngine;
using UnityEngine.UI;

public enum EndPanelType
{
    win,
    lose
}
public class View : MonoBehaviour
{
    public event Action OnRestarnSceneClick;
    public event Action OnNextSceneClick;

    [SerializeField] private Button restartBtn1, restartBtn2;
    [SerializeField] private Button nextBtn;

    [SerializeField] private EndGamePanel winPanel;
    [SerializeField] private EndGamePanel losePanel;

    [SerializeField] private Text healthText;
    [SerializeField] private Text timerText;

    private EndGamePanel _activePanel;

    private void OnEnable()
    {
        restartBtn1.onClick.AddListener(() => OnRestarnSceneClick?.Invoke());
        restartBtn2.onClick.AddListener(() => OnRestarnSceneClick?.Invoke());
        nextBtn.onClick.AddListener(() => OnNextSceneClick?.Invoke());
    }

    private void OnDisable()
    {
        restartBtn1.onClick.RemoveAllListeners();
        restartBtn2.onClick.RemoveAllListeners();
        nextBtn.onClick.RemoveAllListeners();
    }

    public void UpdateHealthCount(int count)
    {
        healthText.text = count.ToString();
    }

    public void ShowEndPanel(EndPanelType type)
    {
        if (_activePanel != null) return;

        _activePanel = type == EndPanelType.win ? winPanel : losePanel;
        _activePanel.HandleVisible();
    }

    public void UpdateTimer(TimerHandle timerHandle)
    {
        timerText.text = $"{timerHandle.maxTime:0}";
    }


}
