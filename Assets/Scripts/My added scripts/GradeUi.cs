using TMPro;
using UnityEngine;

public class GradeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gradeLabel;
    [SerializeField] private string prefix = "Grade: ";

    private void Awake()
    {
        if (!gradeLabel)
            gradeLabel = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        var gm = GameManager.Instance;
        if (gm == null || gradeLabel == null)
            return;

        string grade = gm.CurrentGrade;
        if (string.IsNullOrEmpty(grade))
            grade = "D";

        gradeLabel.text = prefix + grade;
    }
}