using UnityEngine;
using TMPro;

public class GradeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gradeLabel;
    [SerializeField] private string prefix = "Grade: ";

    void Awake()
    {
        if (!gradeLabel)
            gradeLabel = GetComponent<TextMeshProUGUI>();
    }

    void Update()
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
