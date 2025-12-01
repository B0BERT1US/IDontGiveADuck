using UnityEngine;
using TMPro;

public class FinalScoreboard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI finalGradeLabel;

    void Start()
    {
        string grade = "-";

        if (GameManager.Instance != null)
            grade = GameManager.Instance.CurrentGrade;

        if (string.IsNullOrEmpty(grade))
            grade = "D";

        if (finalGradeLabel)
            finalGradeLabel.text = grade;   // <-- NO prefix here
    }
}
