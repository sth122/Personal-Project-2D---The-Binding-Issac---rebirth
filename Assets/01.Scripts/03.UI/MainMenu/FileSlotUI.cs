using UnityEngine;
using UnityEngine.UI;
public class FileSlotUI : MonoBehaviour
{
    
    [SerializeField]private Image targetImage;
    [SerializeField]private UISpriteAnimation spriteAnimation;

    private Color normalColor = new Color(0.4f, 0.4f, 0.4f);
    private Color focusColor = Color.white;

    public void SetFocus(bool isFocoused)
    {
        if(targetImage != null)
        {
            targetImage.color = isFocoused ? focusColor : normalColor;
        }

        if(spriteAnimation != null)
        {
            spriteAnimation.enabled = isFocoused;
        }
    }

}
