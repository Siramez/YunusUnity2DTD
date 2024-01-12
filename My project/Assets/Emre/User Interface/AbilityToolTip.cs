using UnityEngine;
using UnityEngine.EventSystems;

public class AbilityTooltip : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject tooltipPanel;
    private static GameObject currentOpenTooltip;

    private void Start()
    {
        tooltipPanel.SetActive(false); // Hide tooltip initially
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (currentOpenTooltip != null && currentOpenTooltip != tooltipPanel)
        {
            currentOpenTooltip.SetActive(false); // Hide the currently open tooltip
        }

        if (tooltipPanel.activeSelf)
        {
            tooltipPanel.SetActive(false); // Hide tooltip if it's already visible
        }
        else
        {
            tooltipPanel.SetActive(true); // Show tooltip on click
            currentOpenTooltip = tooltipPanel; // Update the currently open tooltip
        }
    }
}
