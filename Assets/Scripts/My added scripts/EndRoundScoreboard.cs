using TMPro;
using UnityEngine;

public class EndRoundScoreboard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyLabel;
    [SerializeField] private TextMeshProUGUI gradeLabel;

    private void OnEnable()
    {
        var gm = GameManager.Instance;
        if (gm == null) return;

        if (moneyLabel)
            moneyLabel.text = $"The Take: £{gm.Score}";

        if (gradeLabel)
        {
            string grade = string.IsNullOrEmpty(gm.CurrentGrade) ? "D" : gm.CurrentGrade;
            gradeLabel.text = grade;
        }
    }
}