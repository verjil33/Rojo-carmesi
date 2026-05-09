using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleDialogBox : MonoBehaviour
{
    [SerializeField] int letterPerSecond;
    [SerializeField] Text dialogText;
    [SerializeField] Color highligtedColor;


    [SerializeField] GameObject actionSelector;
    [SerializeField] GameObject moveSelector;
    [SerializeField] GameObject moveDetails;

    [SerializeField] List<Text> actionTexts;
    [SerializeField] List<Text> moveTexts;

    [SerializeField] Text ppText;
    [SerializeField] Text typeText;


    public void SetDialog(string dialog)
    {
        dialogText.text = dialog;
    }

    public IEnumerator TypeDialog(string dialog)
    {
        dialogText.text = "";
        foreach(var letter in dialog.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / letterPerSecond);
        }
        yield return new WaitForSeconds(1f);
    }

    public void EnableDialogText(bool enabled)
    {
        dialogText.enabled = enabled;
    }

    public void EnableActionSelector(bool enabled)
    {
        actionSelector.SetActive(enabled);
    }
    public void EnableMoveSelector(bool enabled)
    {
        moveSelector.SetActive(enabled);
        moveDetails.SetActive(enabled);

    }


    public void UpdateActionSelection(int selectedAction)
    {
        for (int i = 0; i < actionTexts.Count; i++)
        {
            if (i == selectedAction) actionTexts[i].color = highligtedColor;
            else actionTexts[i].color = Color.black;
        }
    }

    public void UpdateMoveSelection(int selectedMove, Attacks attack)
    {
        for (int i = 0; i < moveTexts.Count; i++)
        {
            if (i == selectedMove) moveTexts[i].color = highligtedColor;
            else moveTexts[i].color = Color.black;
        }

        ppText.text = $"PP: {attack.PP}/{attack.Base.PP}";
        typeText.text = attack.Base.Type.ToString();
    }



    public void SetMoveNames(List<Attacks>  attacks)
    {
        for (int i = 0; i < moveTexts.Count; i++)
        {
            if (i < attacks.Count) moveTexts[i].text = attacks[i].Base.Name;
            else moveTexts[i].text = "-";
        }
    }
}
