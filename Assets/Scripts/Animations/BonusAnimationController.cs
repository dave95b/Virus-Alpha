using UnityEngine;
using UnityEngine.UI;

public class BonusAnimationController : MonoBehaviour {

    [SerializeField]
    private ElementActionEvent addEvent;
    public bool hack;
    
    private Text bonusText;
    private Animator animator;
    private int addAnimationId;
    private CanvasGroup canvasGroup;

    private void OnEnable()
    {
        addEvent.AddListener(PlayAdd);   
    }

    private void OnDisable()
    {
        addEvent.RemoveListener(PlayAdd);
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        addAnimationId = Animator.StringToHash("Add");
        canvasGroup = GetComponent<CanvasGroup>();
        bonusText = GetComponentInChildren<Text>();
    }

    private void PlayAdd(SystemElementController systemElementController, VirusValue virus)
    {
        if(hack)
            bonusText.text = "+" + systemElementController.SystemElement.HackReward;
        else
            bonusText.text = "+" + systemElementController.SystemElement.DestroyReward;
        animator.SetTrigger(addAnimationId);
    }

    public void Hide()
    {
        canvasGroup.alpha = 0f;
    }
}
