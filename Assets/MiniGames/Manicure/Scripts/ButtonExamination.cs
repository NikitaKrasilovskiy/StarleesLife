using UnityEngine;
using UnityEngine.UI;

public class ButtonExamination : MonoBehaviour
{
    private ButtonImage scriptButtonExamination;
    void Start()
    { scriptButtonExamination = FindObjectOfType<ButtonImage>(); }
    public void Examination()
    {
        if (scriptButtonExamination.manicure[0].sprite == GetComponent<Image>().sprite ||
            scriptButtonExamination.manicure[1].sprite == GetComponent<Image>().sprite ||
            scriptButtonExamination.manicure[2].sprite == GetComponent<Image>().sprite)
        { scriptButtonExamination.a++; GetComponent<Image>().sprite = scriptButtonExamination.errorOk[1]; }

        else { scriptButtonExamination.i--; GetComponent<Image>().sprite = scriptButtonExamination.errorOk[0]; }
    }
}
