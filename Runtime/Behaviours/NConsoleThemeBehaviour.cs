using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace Nazio_LT.Tools.Console
{
    /// <summary>
    /// This class change console elements theme.
    /// </summary>
    public class NConsoleThemeBehaviour : MonoBehaviour
    {
        private enum TextType
        {
            Terminal,
            Placeholder,
            UI
        }

        private enum ImageType
        {
            Terminal,
            UI,
            UIVariant
        }

        [SerializeField] private TextMeshProUGUI[] m_text;
        [SerializeField] private TextType m_textType;

        [Space]
        [SerializeField] private Image[] m_image;
        [SerializeField] private ImageType m_imageType;

        public void ApplyTheme(NConsoleTheme theme)
        {
            Color color = GetTextColor(theme);
            foreach (var text in m_text)
            {
                text.color = color;
            }

            color = GetImageColor(theme);
            foreach (var image in m_image)
            {
                image.color = color;
            }
        }

        private Color GetImageColor(NConsoleTheme theme)
        {
            switch (m_imageType)
            {
                case ImageType.UI: return theme.UIColor;

                case ImageType.UIVariant: return theme.UIVariantColor;
            }

            return theme.TerminalBackColor;
        }

        private Color GetTextColor(NConsoleTheme theme)
        {
            switch (m_textType)
            {
                case TextType.Placeholder: return theme.TerminalPlaceHolderColor;

                case TextType.UI: return theme.UITextColor;
            }

            return theme.TerminalTextColor;
        }
    }
}
