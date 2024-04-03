using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.dialog
{
    public class AnimationMenu
    {
        public string Title { get; set; }

        public AnimationMenu(string title)
        {
            Title = title;
        }

        public List<AnimationMenuCommand> Commands { get; } = new List<AnimationMenuCommand>();
        public List<AnimationMenuElement> Elements { get; } = new List<AnimationMenuElement>();

        public void AddLabel(sbyte type, string text, FontStyle fontStyle = FontStyle.BIG_FONT, bool canSelect = false)
        {
            Elements.Add(new Label() { Type = type, Text = text, Style = fontStyle, CanSelect = canSelect });
        }

        public void AddImage(sbyte type, string frameImgPath, int numFrame = 2, bool canSelect = false)
        {
            Elements.Add(new Animation() { ImagePath = frameImgPath, NumFrame = numFrame, Type = type, CanSelect = canSelect });
        }

        public class AnimationMenuElement
        {
            public sbyte Type { get; set; }
            public bool CanSelect { get; set; } = false;
        }

        public class Animation : AnimationMenuElement
        {
            public string ImagePath { get; set; }
            public int NumFrame { get; set; } = 0;
        }

        public class Label : AnimationMenuElement
        {
            public string Text { get; set; }
            public FontStyle Style { get; set; }
        }


        public static AnimationMenuCommand RightExitCMD => new AnimationMenuCommand() { Name = "Đóng", Id = -1, Type = AnimationMenuCommand.LEFT, IsCloseScreen = true, IsRelpyServer = false };

        public class AnimationMenuCommand
        {
            public const sbyte LEFT = 2;
            public const sbyte CENTER = 1;
            public const sbyte RIGHT = 0;

            public string Name { get; set; } = string.Empty;
            public int Id { get; set; }
            public sbyte Type { get; set; }
            public bool IsCloseScreen { get; set; } = true;

            public bool IsRelpyServer { get; set; } = false;

            public bool ApplyForElement
            {
                get
                {
                    return this.Id == -1;
                }
            }
        }
    }
}
