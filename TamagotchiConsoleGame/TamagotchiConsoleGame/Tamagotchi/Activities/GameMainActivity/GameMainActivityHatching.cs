using System.Drawing;
using TextGraphicsEngine;
using TextGraphicsEngine.Misc;

namespace TamagotchiConsoleGame.Tamagotchi.Activities
{
    public partial class GameMainActivity
    {
        private void eggToDisplay()
        {
            Bitmap bmp1 = null, bmp2 = null;
            switch (character.TypeName)
            {
                case "Boy 1":
                    bmp1 = TGE.Instance.Resources.Get(GameResources.boyV1WaitingToHatch0);
                    bmp2 = TGE.Instance.Resources.Get(GameResources.boyV1WaitingToHatch1);
                    break;
                case "Boy 2":
                    bmp1 = TGE.Instance.Resources.Get(GameResources.boyV2WaitingToHatch0);
                    bmp2 = TGE.Instance.Resources.Get(GameResources.boyV2WaitingToHatch1);
                    break;
                case "Boy 3":
                    bmp1 = TGE.Instance.Resources.Get(GameResources.boyV3WaitingToHatch0);
                    bmp2 = TGE.Instance.Resources.Get(GameResources.boyV3WaitingToHatch1);
                    break;
                case "Boy 4":
                    bmp1 = TGE.Instance.Resources.Get(GameResources.boyV3WaitingToHatch0);
                    bmp2 = TGE.Instance.Resources.Get(GameResources.boyV3WaitingToHatch1);
                    break;
                case "Girl 1":
                    bmp1 = TGE.Instance.Resources.Get(GameResources.girlV1WaitingToHatch0);
                    bmp2 = TGE.Instance.Resources.Get(GameResources.girlV1WaitingToHatch1);
                    break;
                case "Girl 2":
                    bmp1 = TGE.Instance.Resources.Get(GameResources.girlV2WaitingToHatch0);
                    bmp2 = TGE.Instance.Resources.Get(GameResources.girlV2WaitingToHatch1);
                    break;
                case "Girl 3":
                    bmp1 = TGE.Instance.Resources.Get(GameResources.girlV3WaitingToHatch0);
                    bmp2 = TGE.Instance.Resources.Get(GameResources.girlV3WaitingToHatch1);
                    break;
                case "Girl 4":
                    bmp1 = TGE.Instance.Resources.Get(GameResources.girlV4WaitingToHatch0);
                    bmp2 = TGE.Instance.Resources.Get(GameResources.girlV4WaitingToHatch1);
                    break;
            }

            characterHatching.SpriteSequence.Clear();
            characterHatching.SpriteSequence.Add(new SpriteBitmap(bmp1));
            characterHatching.SpriteSequence.Add(new SpriteBitmap(bmp2));

            characterHatching.IsActive = true;

        }

        public void hatch()
        {
            Bitmap bmp1 = null, bmp2 = null;

            switch (character.TypeName)
            {
                case "Boy 1":
                    bmp1 = TGE.Instance.Resources.Get(GameResources.boyV1Hatching0);
                    bmp2 = bmp2 = TGE.Instance.Resources.Get(GameResources.boyV1Hatching1);
                    break;
                case "Boy 2":
                    bmp1 = TGE.Instance.Resources.Get(GameResources.boyV2Hatching0);
                    bmp2 = TGE.Instance.Resources.Get(GameResources.boyV2Hatching1);
                    break;
                case "Boy 3":
                    bmp1 = TGE.Instance.Resources.Get(GameResources.boyV3Hatching0);
                    bmp2 = TGE.Instance.Resources.Get(GameResources.boyV3Hatching1);
                    break;
                case "Boy 4":
                    bmp1 = TGE.Instance.Resources.Get(GameResources.boyV4Hatching0);
                    bmp2 = TGE.Instance.Resources.Get(GameResources.boyV4Hatching1);
                    break;
                case "Girl 1":
                    bmp1 = TGE.Instance.Resources.Get(GameResources.girlV1Hatching0);
                    bmp2 = TGE.Instance.Resources.Get(GameResources.girlV1Hatching1);
                    break;
                case "Girl 2":
                    bmp1 = TGE.Instance.Resources.Get(GameResources.girlV2Hatching0);
                    bmp2 = TGE.Instance.Resources.Get(GameResources.girlV2Hatching1);
                    break;
                case "Girl 3":
                    bmp1 = TGE.Instance.Resources.Get(GameResources.girlV3Hatching0);
                    bmp2 = TGE.Instance.Resources.Get(GameResources.girlV3Hatching1);
                    break;
                case "Girl 4":
                    bmp1 = TGE.Instance.Resources.Get(GameResources.girlV4Hatching0);
                    bmp2 = TGE.Instance.Resources.Get(GameResources.girlV4Hatching1);
                    break;
            }
            characterHatching.SpriteSequence.Clear();
            characterHatching.SpriteSequence.Add(new SpriteBitmap(bmp1));
            characterHatching.SpriteSequence.Add(new SpriteBitmap(bmp2));
            characterHatching.SpriteSequence.Add(new SpriteBitmap(bmp2));

            characterHatching.OnAnimationEnd = becomeBaby;
            characterHatching.AnimationFrameDuration = 1000;
            characterHatching.AnimationRepeatCount = 1;

            characterHatching.IsActive = true;
        }

        private void becomeBaby()
        {
            character.Stage = "Baby";
            characterHatching.IsActive = false;


            loadExtraMenuButtons();
            characterStates["basic_anim_normal"].IsActive = true;
            changeCharacter(Stage.Baby, "Babytchi");

            save();
        }

    }
}
