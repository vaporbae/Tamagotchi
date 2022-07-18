using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TextGraphicsEngine.Misc
{
    public sealed class Font
    {
        private static readonly String fontDirectory = "font\\";

        private static readonly Lazy<Font> lazy = new Lazy<Font>(() => new Font());

        public static Font Instance { get { return lazy.Value; } }


        private static Dictionary<char, string> fileMappings = new Dictionary<char, string>();
        private static Dictionary<char, Bitmap> characterBitmaps = new Dictionary<char, Bitmap>();


        private Font()
        {
            foreach (char c in Enumerable.Range('a', 'z' - 'a' + 1))
                fileMappings.Add(c, c.ToString() + ".png");

            foreach (char c in Enumerable.Range('0', '9' - '0' + 1))
                fileMappings.Add(c, c.ToString() + ".png");


            fileMappings.Add('\'', "apostrophe.png");
            fileMappings.Add('*', "asterix.png");
            fileMappings.Add('\\', "backslash.png");
            fileMappings.Add(':', "colon.png");
            fileMappings.Add(',', "comma.png");
            fileMappings.Add('.', "dot.png");
            fileMappings.Add('_', "downscore.png");
            fileMappings.Add('=', "equal.png");
            fileMappings.Add('!', "exclamation_mark.png");
            fileMappings.Add('#', "hash.png");
            fileMappings.Add('<', "left_bracket.png");
            fileMappings.Add('(', "left_round_bracket.png");
            fileMappings.Add('[', "left_square_bracket.png");
            fileMappings.Add('-', "minus.png");
            fileMappings.Add('+', "plus.png");
            fileMappings.Add('?', "question_mark.png");
            fileMappings.Add('>', "right_bracket.png");
            fileMappings.Add(')', "right_round_bracket.png");
            fileMappings.Add(']', "right_square_bracket.png");
            fileMappings.Add(';', "semicolon.png");
            fileMappings.Add('/', "slash.png");
            fileMappings.Add(' ', "space.png");
            fileMappings.Add((char)0, "null.png");


            foreach (var ch in fileMappings)
            {
                String path = AppDomain.CurrentDomain.BaseDirectory + fontDirectory + ch.Value;
                Bitmap bmp = new Bitmap(path, false);
                characterBitmaps.Add(ch.Key, bmp);

            }
        }

        // for now if there is no required character, special null character is returned
        public Bitmap GetBitmap(char ch)
        {
            char fontCh = Char.ToLower(ch);
            if (characterBitmaps.ContainsKey(fontCh))
                return characterBitmaps[fontCh];
            else
                return characterBitmaps[(char)0];
        }

    }
}
