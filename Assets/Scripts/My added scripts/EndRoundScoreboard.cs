using UnityEngine;
using TMPro;

public class EndRoundScoreboard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyLabel;
    [SerializeField] private TextMeshProUGUI gradeLabel;

    void OnEnable()
    {
        var gm = GameManager.Instance;
        if (gm == null)
        {
            if (moneyLabel) moneyLabel.text = "Final Take: £0";
            if (gradeLabel) gradeLabel.text = "D";
            return;
        }

        int money = gm.Score;
        string grade = gm.CurrentGrade;

        if (string.IsNullOrEmpty(grade))
            grade = "D";

        if (moneyLabel) moneyLabel.text = $"Final Take: £{money}";
        if (gradeLabel) gradeLabel.text = grade;
    }
}
