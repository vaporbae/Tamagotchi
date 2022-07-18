

using System.Collections.Generic;
using TamagotchiConsoleGame.UI;
using TextGraphicsEngine;
using TextGraphicsEngine.Controls;
using TextGraphicsEngine.Misc;

namespace TamagotchiConsoleGame.Tamagotchi.Activities
{
    public partial class GameMainActivity : Activity
    {
        private static readonly Coord initialChracterPostion = new Coord(100, 47);
        private static readonly int initialCharacterFrameDuration = 100;

        private Dictionary<string, Sprite> characterStates;

        public static readonly string[] ArrayOfStates = new string[]
        {
                "basic_anim_happy",
                "basic_anim_normal",
                "cry",
                "death",
                "dissapointed",

                "after_eating",
                "during_eating",
                "full_eating",

                "glad",
                "happy",

                "ill",
                "very_ill",

                "jump",

                "mad",
                "very_mad",


                "no",

                "before_poop",
                "poop",

                "sad1",
                "sad2",
                "sad3",

                "shy",
                "side_walk",

                "sleep",

                "sorry",
                "special1",
                "special2",

                "tired",
                "yawning",
                "what",
        };

        public static readonly string[] CharactersList = new string[]
        {
            // Baby
            "Babytchi",
            "Shirobabychi", 

            // Toddler
            "Hashitamatchi",
            "Marutchi", 
            "Tongaritchi",
            "Tonmarutchi", 

            //Teen
            "Kutchitamatchi",
            "Tamatchi",
            "Nyorotchi", 
            "Takotchi", 

            // Adult
            "Bill", 
            "Ginjirotchi",
            "Hashizotchi",
            "Kuchipatchi",
            "Kusatchi", 
            "Mametchi", 
            "Masukutchi", 
            "Mimitchi", 
            "Oyajitchi", 
            "Pochitchi", 
            "Zakutchi", 
        };

        public static readonly Dictionary<string, Stage> DictOfCharacters = new Dictionary<string, Stage>()
        {
            // Baby
            {"Babytchi", Stage.Baby },
            {"Shirobabychi", Stage.Baby },

            // Toddler
            {"Hashitamatchi", Stage.Toddler },
            {"Marutchi", Stage.Toddler },
            {"Tongaritchi", Stage.Toddler },
            {"Tonmarutchi", Stage.Toddler },

            //Teen
            {"Kutchitamatchi", Stage.Teen },
            {"Tamatchi", Stage.Teen },
            {"Nyorotchi", Stage.Teen },
            {"Takotchi", Stage.Teen },

            // Adult
            {"Bill", Stage.Adult },
            {"Ginjirotchi", Stage.Adult },
            {"Hashizotchi", Stage.Adult },
            {"Kuchipatchi", Stage.Adult },
            {"Kusatchi", Stage.Adult },
            {"Mametchi", Stage.Adult },
            {"Masukutchi", Stage.Adult },
            {"Mimitchi", Stage.Adult },
            {"Oyajitchi", Stage.Adult },
            {"Pochitchi", Stage.Adult },
            {"Zakutchi", Stage.Adult },
        };



        private void initCharacterStates()
        {
            characterStates = new Dictionary<string, Sprite>()
            {
                {"basic_anim_happy",    null},
                {"basic_anim_normal",   null},
                {"cry",                 null},
                {"death",               null},
                {"dissapointed",        null},

                {"after_eating",        null},
                {"during_eating",       null},
                {"full_eating",         null},

                {"glad",                null},
                {"happy",               null},

                {"ill",                 null},
                {"very_ill",            null},

                {"jump",                null},

                {"mad",                 null},
                {"very_mad",            null},


                {"no",                  null},

                {"before_poop",         null},
                {"poop",                null},

                {"sad1",                null},
                {"sad2",                null},
                {"sad3",                null},

                {"shy",                 null},
                {"side_walk",           null},

                {"sleep",               null},

                {"sorry",               null},
                {"special1",             null},
                {"special2",             null},

                {"tired",               null},
                {"yawning",               null},
                {"what",               null},

            };



            List<string> keys = new List<string>(characterStates.Keys);

            foreach (string key in keys)
            {
                characterStates[key] = SpriteFactory.NewSpriteC(initialChracterPostion, "character_" + key, new string[] { GameResources.babytchiBasicAnimHappy0, GameResources.babytchiBasicAnimHappy1 }, null, initialCharacterFrameDuration);
                characterStates[key].IsActive = false;
                characterStates[key].AnimationEnabled = true;

            }
        }

        private void deactivateCurrentState()
        {
            List<string> keys = new List<string>(characterStates.Keys);

            foreach (string key in keys)
            {
                characterStates[key].IsActive = false;
            }
        }

        private void activateState(string state)
        {
            deactivateCurrentState();
            characterStates[state].IsActive = true;
        }


        public enum Stage
        {
            //Egg,
            Baby,
            Toddler,
            Teen,
            Adult,
        }

        private void changeCharacter(string characterName)
        {
            changeCharacter(DictOfCharacters[characterName], characterName);
        }

        private void changeCharacter(Stage stage, string characterName)
        {
            var mapping = StageToBitmaps[(int)stage][characterName];

            foreach (var item in characterStates)
            {
                var currentSequence = characterStates[item.Key].SpriteSequence;
                currentSequence.Clear();
                foreach (string str in mapping[item.Key])
                {
                    SpriteBitmap bmp = new SpriteBitmap(TGE.Instance.Resources.Get(str));
                    currentSequence.Add(bmp);
                }
            }
        }

        private static readonly Dictionary<string, Dictionary<string, string[]>>[] StageToBitmaps = new Dictionary<string, Dictionary<string, string[]>>[]
        {
            // Baby
            new Dictionary<string, Dictionary<string, string[]>>()
            {
                {"Babytchi", new Dictionary<string, string[]>()
                    {
                        {"basic_anim_happy",    new string[] { GameResources.babytchiBasicAnimHappy0, GameResources.babytchiBasicAnimHappy1, GameResources.babytchiBasicAnimHappy2, GameResources.babytchiBasicAnimHappy3, GameResources.babytchiBasicAnimHappy4, } },
                        {"basic_anim_normal",   new string[] { GameResources.babytchiBasicAnimNormal0, GameResources.babytchiBasicAnimNormal1, GameResources.babytchiBasicAnimNormal2, GameResources.babytchiBasicAnimNormal3, GameResources.babytchiBasicAnimNormal4, } },
                        {"cry",                 new string[] { GameResources.babytchiCry0, GameResources.babytchiCry1, } },
                        {"death",               new string[] { GameResources.babytchiDeath0, GameResources.babytchiDeath1, } },
                        {"dissapointed",        new string[] { GameResources.babytchiDissapointed0, GameResources.babytchiDissapointed1, } },

                        {"after_eating",        new string[] { GameResources.babytchiAfterEating0, GameResources.babytchiAfterEating1, } },
                        {"during_eating",       new string[] { GameResources.babytchiDuringEating0, GameResources.babytchiDuringEating1, } },
                        {"full_eating",         new string[] { GameResources.babytchiFull0, } },

                        {"glad",                new string[] { GameResources.babytchiGlad0, GameResources.babytchiGlad1, } },
                        {"happy",               new string[] { GameResources.babytchiHappy0, GameResources.babytchiHappy1, } },

                        {"ill",                 new string[] { GameResources.babytchiIll0, GameResources.babytchiIll1, } },
                        {"very_ill",            new string[] { GameResources.babytchiVeryIll0, GameResources.babytchiVeryIll1, } },

                        {"jump",                new string[] { GameResources.babytchiJump0, } },

                        {"mad",                 new string[] { GameResources.babytchiMad0, GameResources.babytchiMad1, } },
                        {"very_mad",            new string[] { GameResources.babytchiVeryMad0, GameResources.babytchiVeryMad1, } },

                        {"no",                  new string[] { GameResources.babytchiNo0, GameResources.babytchiNo1, } },

                        {"before_poop",         new string[] { GameResources.babytchiBeforePoop0, GameResources.babytchiBeforePoop1, } },
                        {"poop",                new string[] { GameResources.babytchiToiletPoop0, GameResources.babytchiToiletPoop1, } },

                        {"sad1",                new string[] { GameResources.babytchiV1Sad0, GameResources.babytchiV1Sad1, } },
                        {"sad2",                new string[] { GameResources.babytchiV2Sad0, GameResources.babytchiV2Sad1, } },
                        {"sad3",                new string[] { GameResources.babytchiV3Sad0, GameResources.babytchiV3Sad1, } },


                        {"shy",                 new string[] { GameResources.babytchiShy0, GameResources.babytchiShy1, } },
                        {"side_walk",           new string[] { GameResources.babytchiSideWalk0, GameResources.babytchiSideWalk1, } },

                        {"sleep",               new string[] { GameResources.babytchiSleep0, GameResources.babytchiSleep1, } },

                        {"sorry",               new string[] { GameResources.babytchiV1Sorry0, GameResources.babytchiV2Sorry0, GameResources.babytchiV2Sorry1 } },
                        {"special1",            new string[] { GameResources.babytchiV1Special0, } },
                        {"special2",            new string[] { GameResources.babytchiV2Special0, GameResources.babytchiV2Special1 } },

                        {"tired",               new string[] { GameResources.babytchiTired0, GameResources.babytchiTired1, } },
                        {"yawning",             new string[] { GameResources.babytchiYawning0, GameResources.babytchiYawning1, GameResources.babytchiYawning2, } },
                        {"what",                new string[] { GameResources.babytchiWhat0, } },
                    }
                },

                {"Shirobabychi", new Dictionary<string, string[]>()
                    {
                        {"basic_anim_happy",    new string[] { GameResources.shirobabytchiBasicAnimHappy0, GameResources.shirobabytchiBasicAnimHappy1, GameResources.shirobabytchiBasicAnimHappy2, GameResources.shirobabytchiBasicAnimHappy3, GameResources.shirobabytchiBasicAnimHappy4, } },
                        {"basic_anim_normal",   new string[] { GameResources.shirobabytchiBasicAnimNormal0, GameResources.shirobabytchiBasicAnimNormal1, GameResources.shirobabytchiBasicAnimNormal2, GameResources.shirobabytchiBasicAnimNormal3, GameResources.shirobabytchiBasicAnimNormal4, } },
                        {"cry",                 new string[] { GameResources.shirobabytchiCry0, GameResources.shirobabytchiCry1, } },
                        {"death",               new string[] { GameResources.shirobabytchiDeath0, GameResources.shirobabytchiDeath1, } },
                        {"dissapointed",        new string[] { GameResources.shirobabytchiDissapointed0, GameResources.shirobabytchiDissapointed1, } },

                        {"after_eating",        new string[] { GameResources.shirobabytchiAfterEating0, GameResources.shirobabytchiAfterEating1, } },
                        {"during_eating",       new string[] { GameResources.shirobabytchiDuringEating0, GameResources.shirobabytchiDuringEating1, } },
                        {"full_eating",         new string[] { GameResources.shirobabytchiFull0, } },

                        {"glad",                new string[] { GameResources.shirobabytchiGlad0, GameResources.shirobabytchiGlad1, } },
                        {"happy",               new string[] { GameResources.shirobabytchiHappy0, GameResources.shirobabytchiHappy1, } },

                        {"ill",                 new string[] { GameResources.shirobabytchiIll0, GameResources.shirobabytchiIll1, } },
                        {"very_ill",            new string[] { GameResources.shirobabytchiVeryIll0, GameResources.shirobabytchiVeryIll1, } },

                        {"jump",                new string[] { GameResources.shirobabytchiJump0, } },

                        {"mad",                 new string[] { GameResources.shirobabytchiMad0, GameResources.shirobabytchiMad1, } },
                        {"very_mad",            new string[] { GameResources.shirobabytchiVeryMad0, GameResources.shirobabytchiVeryMad1, } },

                        {"no",                  new string[] { GameResources.shirobabytchiNo0, GameResources.shirobabytchiNo1, } },

                        {"before_poop",         new string[] { GameResources.shirobabytchiBeforePoop0, GameResources.shirobabytchiBeforePoop1, } },
                        {"poop",                new string[] { GameResources.shirobabytchiToiletPoop0, GameResources.shirobabytchiToiletPoop1, } },

                        {"sad1",                new string[] { GameResources.shirobabytchiV1Sad0, GameResources.shirobabytchiV1Sad1, } },
                        {"sad2",                new string[] { GameResources.shirobabytchiV2Sad0, GameResources.shirobabytchiV2Sad1, } },
                        {"sad3",                new string[] { GameResources.shirobabytchiV3Sad0, GameResources.shirobabytchiV3Sad1, } },


                        {"shy",                 new string[] { GameResources.shirobabytchiShy0, GameResources.shirobabytchiShy1, } },
                        {"side_walk",           new string[] { GameResources.shirobabytchiSideWalk0, GameResources.shirobabytchiSideWalk1, } },

                        {"sleep",               new string[] { GameResources.shirobabytchiSleep0, GameResources.shirobabytchiSleep1, } },

                        {"sorry",               new string[] { GameResources.shirobabytchiV1Sorry0, GameResources.shirobabytchiV2Sorry0, GameResources.shirobabytchiV2Sorry1 } },
                        {"special1",             new string[] { GameResources.shirobabytchiV1Special0, } },
                        {"special2",             new string[] { GameResources.shirobabytchiV2Special0, GameResources.shirobabytchiV2Special1 } },

                        {"tired",               new string[] { GameResources.shirobabytchiTired0, GameResources.shirobabytchiTired1, } },
                        {"yawning",             new string[] { GameResources.shirobabytchiYawning0, GameResources.shirobabytchiYawning1, GameResources.shirobabytchiYawning2, } },
                        {"what",                new string[] { GameResources.shirobabytchiWhat0, } },
                    }
                },

            },

            // Toddler
            new Dictionary<string, Dictionary<string, string[]>>()
            {
                 {"Hashitamatchi", new Dictionary<string, string[]>()
                    {
                        {"basic_anim_happy",    new string[] { GameResources.hashitamatchiBasicAnimHappy0, GameResources.hashitamatchiBasicAnimHappy1, GameResources.hashitamatchiBasicAnimHappy2, GameResources.hashitamatchiBasicAnimHappy3, GameResources.hashitamatchiBasicAnimHappy4, } },
                        {"basic_anim_normal",   new string[] { GameResources.hashitamatchiBasicAnimNormal0, GameResources.hashitamatchiBasicAnimNormal1, GameResources.hashitamatchiBasicAnimNormal2, GameResources.hashitamatchiBasicAnimNormal3, GameResources.hashitamatchiBasicAnimNormal4, } },
                        {"cry",                 new string[] { GameResources.hashitamatchiCry0, GameResources.hashitamatchiCry1, } },
                        {"death",               new string[] { GameResources.hashitamatchiDeath0, GameResources.hashitamatchiDeath1, } },
                        {"dissapointed",        new string[] { GameResources.hashitamatchiDissapointed0, GameResources.hashitamatchiDissapointed1, } },

                        {"after_eating",        new string[] { GameResources.hashitamatchiAfterEating0, GameResources.hashitamatchiAfterEating1, } },
                        {"during_eating",       new string[] { GameResources.hashitamatchiDuringEating0, GameResources.hashitamatchiDuringEating1, } },
                        {"full_eating",         new string[] { GameResources.hashitamatchiFull0, } },

                        {"glad",                new string[] { GameResources.hashitamatchiGlad0, GameResources.hashitamatchiGlad1, } },
                        {"happy",               new string[] { GameResources.hashitamatchiHappy0, GameResources.hashitamatchiHappy1, } },

                        {"ill",                 new string[] { GameResources.hashitamatchiIll0, GameResources.hashitamatchiIll1, } },
                        {"very_ill",            new string[] { GameResources.hashitamatchiVeryIll0, GameResources.hashitamatchiVeryIll1, } },

                        {"jump",                new string[] { GameResources.hashitamatchiJump0, } },

                        {"mad",                 new string[] { GameResources.hashitamatchiMad0, GameResources.hashitamatchiMad1, } },
                        {"very_mad",            new string[] { GameResources.hashitamatchiVeryMad0, GameResources.hashitamatchiVeryMad1, } },

                        {"no",                  new string[] { GameResources.hashitamatchiNo0, GameResources.hashitamatchiNo1, } },

                        {"before_poop",         new string[] { GameResources.hashitamatchiBeforePoop0, GameResources.hashitamatchiBeforePoop1, } },
                        {"poop",                new string[] { GameResources.hashitamatchiToiletPoop0, GameResources.hashitamatchiToiletPoop1, } },

                        {"sad1",                new string[] { GameResources.hashitamatchiV1Sad0, GameResources.hashitamatchiV1Sad1, } },
                        {"sad2",                new string[] { GameResources.hashitamatchiV2Sad0, GameResources.hashitamatchiV2Sad1, } },
                        {"sad3",                new string[] { GameResources.hashitamatchiV3Sad0, GameResources.hashitamatchiV3Sad1, } },


                        {"shy",                 new string[] { GameResources.hashitamatchiShy0, GameResources.hashitamatchiShy1, } },
                        {"side_walk",           new string[] { GameResources.hashitamatchiSideWalk0, GameResources.hashitamatchiSideWalk1, } },

                        {"sleep",               new string[] { GameResources.hashitamatchiSleep0, GameResources.hashitamatchiSleep1, } },

                        {"sorry",               new string[] { GameResources.hashitamatchiV1Sorry0, GameResources.hashitamatchiV2Sorry0, GameResources.hashitamatchiV2Sorry1 } },
                        {"special1",            new string[] { GameResources.hashitamatchiV1Special0, } },
                        {"special2",            new string[] { GameResources.hashitamatchiV2Special0, GameResources.hashitamatchiV2Special1 } },

                        {"tired",               new string[] { GameResources.hashitamatchiTired0, GameResources.hashitamatchiTired1, } },
                        {"yawning",             new string[] { GameResources.hashitamatchiYawning0, GameResources.hashitamatchiYawning1, GameResources.hashitamatchiYawning2, } },
                        {"what",                new string[] { GameResources.hashitamatchiWhat0, } },
                    }
                },

                {"Marutchi", new Dictionary<string, string[]>()
                    {
                        {"basic_anim_happy",    new string[] { GameResources.marutchiBasicAnimHappy0, GameResources.marutchiBasicAnimHappy1, GameResources.marutchiBasicAnimHappy2, GameResources.marutchiBasicAnimHappy3, GameResources.marutchiBasicAnimHappy4, } },
                        {"basic_anim_normal",   new string[] { GameResources.marutchiBasicAnimNormal0, GameResources.marutchiBasicAnimNormal1, GameResources.marutchiBasicAnimNormal2, GameResources.marutchiBasicAnimNormal3, GameResources.marutchiBasicAnimNormal4, } },
                        {"cry",                 new string[] { GameResources.marutchiCry0, GameResources.marutchiCry1, } },
                        {"death",               new string[] { GameResources.marutchiDeath0, GameResources.marutchiDeath1, } },
                        {"dissapointed",        new string[] { GameResources.marutchiDissapointed0, GameResources.marutchiDissapointed1, } },

                        {"after_eating",        new string[] { GameResources.marutchiAfterEating0, GameResources.marutchiAfterEating1, } },
                        {"during_eating",       new string[] { GameResources.marutchiDuringEating0, GameResources.marutchiDuringEating1, } },
                        {"full_eating",         new string[] { GameResources.marutchiFull0, } },

                        {"glad",                new string[] { GameResources.marutchiGlad0, GameResources.marutchiGlad1, } },
                        {"happy",               new string[] { GameResources.marutchiHappy0, GameResources.marutchiHappy1, } },

                        {"ill",                 new string[] { GameResources.marutchiIll0, GameResources.marutchiIll1, } },
                        {"very_ill",            new string[] { GameResources.marutchiVeryIll0, GameResources.marutchiVeryIll1, } },

                        {"jump",                new string[] { GameResources.marutchiJump0, } },

                        {"mad",                 new string[] { GameResources.marutchiMad0, GameResources.marutchiMad1, } },
                        {"very_mad",            new string[] { GameResources.marutchiVeryMad0, GameResources.marutchiVeryMad1, } },

                        {"no",                  new string[] { GameResources.marutchiNo0, GameResources.marutchiNo1, } },

                        {"before_poop",         new string[] { GameResources.marutchiBeforePoop0, GameResources.marutchiBeforePoop1, } },
                        {"poop",                new string[] { GameResources.marutchiToiletPoop0, GameResources.marutchiToiletPoop1, } },

                        {"sad1",                new string[] { GameResources.marutchiV1Sad0, GameResources.marutchiV1Sad1, } },
                        {"sad2",                new string[] { GameResources.marutchiV2Sad0, GameResources.marutchiV2Sad1, } },
                        {"sad3",                new string[] { GameResources.marutchiV3Sad0, GameResources.marutchiV3Sad1, } },


                        {"shy",                 new string[] { GameResources.marutchiShy0, GameResources.marutchiShy1, } },
                        {"side_walk",           new string[] { GameResources.marutchiSideWalk0, GameResources.marutchiSideWalk1, } },

                        {"sleep",               new string[] { GameResources.marutchiSleep0, GameResources.marutchiSleep1, } },

                        {"sorry",               new string[] { GameResources.marutchiV1Sorry0, GameResources.marutchiV2Sorry0, GameResources.marutchiV2Sorry1 } },
                        {"special1",            new string[] { GameResources.marutchiV1Special0, } },
                        {"special2",            new string[] { GameResources.marutchiV2Special0, GameResources.marutchiV2Special1 } },

                        {"tired",               new string[] { GameResources.marutchiTired0, GameResources.marutchiTired1, } },
                        {"yawning",             new string[] { GameResources.marutchiYawning0, GameResources.marutchiYawning1, GameResources.marutchiYawning2, } },
                        {"what",                new string[] { GameResources.marutchiWhat0, } },
                    }
                },

                {"Tongaritchi", new Dictionary<string, string[]>()
                    {
                        {"basic_anim_happy",    new string[] { GameResources.tongaritchiBasicAnimHappy0, GameResources.tongaritchiBasicAnimHappy1, GameResources.tongaritchiBasicAnimHappy2, GameResources.tongaritchiBasicAnimHappy3, GameResources.tongaritchiBasicAnimHappy4, } },
                        {"basic_anim_normal",   new string[] { GameResources.tongaritchiBasicAnimNormal0, GameResources.tongaritchiBasicAnimNormal1, GameResources.tongaritchiBasicAnimNormal2, GameResources.tongaritchiBasicAnimNormal3, GameResources.tongaritchiBasicAnimNormal4, } },
                        {"cry",                 new string[] { GameResources.tongaritchiCry0, GameResources.tongaritchiCry1, } },
                        {"death",               new string[] { GameResources.tongaritchiDeath0, GameResources.tongaritchiDeath1, } },
                        {"dissapointed",        new string[] { GameResources.tongaritchiDissapointed0, GameResources.tongaritchiDissapointed1, } },

                        {"after_eating",        new string[] { GameResources.tongaritchiAfterEating0, GameResources.tongaritchiAfterEating1, } },
                        {"during_eating",       new string[] { GameResources.tongaritchiDuringEating0, GameResources.tongaritchiDuringEating1, } },
                        {"full_eating",         new string[] { GameResources.tongaritchiFull0, } },

                        {"glad",                new string[] { GameResources.tongaritchiGlad0, GameResources.tongaritchiGlad1, } },
                        {"happy",               new string[] { GameResources.tongaritchiHappy0, GameResources.tongaritchiHappy1, } },

                        {"ill",                 new string[] { GameResources.tongaritchiIll0, GameResources.tongaritchiIll1, } },
                        {"very_ill",            new string[] { GameResources.tongaritchiVeryIll0, GameResources.tongaritchiVeryIll1, } },

                        {"jump",                new string[] { GameResources.tongaritchiJump0, } },

                        {"mad",                 new string[] { GameResources.tongaritchiMad0, GameResources.tongaritchiMad1, } },
                        {"very_mad",            new string[] { GameResources.tongaritchiVeryMad0, GameResources.tongaritchiVeryMad1, } },

                        {"no",                  new string[] { GameResources.tongaritchiNo0, GameResources.tongaritchiNo1, } },

                        {"before_poop",         new string[] { GameResources.tongaritchiBeforePoop0, GameResources.tongaritchiBeforePoop1, } },
                        {"poop",                new string[] { GameResources.tongaritchiToiletPoop0, GameResources.tongaritchiToiletPoop1, } },

                        {"sad1",                new string[] { GameResources.tongaritchiV1Sad0, GameResources.tongaritchiV1Sad1, } },
                        {"sad2",                new string[] { GameResources.tongaritchiV2Sad0, GameResources.tongaritchiV2Sad1, } },
                        {"sad3",                new string[] { GameResources.tongaritchiV3Sad0, GameResources.tongaritchiV3Sad1, } },


                        {"shy",                 new string[] { GameResources.tongaritchiShy0, GameResources.tongaritchiShy1, } },
                        {"side_walk",           new string[] { GameResources.tongaritchiSideWalk0, GameResources.tongaritchiSideWalk1, } },

                        {"sleep",               new string[] { GameResources.tongaritchiSleep0, GameResources.tongaritchiSleep1, } },

                        {"sorry",               new string[] { GameResources.tongaritchiV1Sorry0, GameResources.tongaritchiV2Sorry0, GameResources.tongaritchiV2Sorry1 } },
                        {"special1",            new string[] { GameResources.tongaritchiV1Special0, } },
                        {"special2",            new string[] { GameResources.tongaritchiV2Special0, GameResources.tongaritchiV2Special1 } },

                        {"tired",               new string[] { GameResources.tongaritchiTired0, GameResources.tongaritchiTired1, } },
                        {"yawning",             new string[] { GameResources.tongaritchiYawning0, GameResources.tongaritchiYawning1, GameResources.tongaritchiYawning2, } },
                        {"what",                new string[] { GameResources.tongaritchiWhat0, } },
                    }
                },

                {"Tonmarutchi", new Dictionary<string, string[]>()
                    {
                        {"basic_anim_happy",    new string[] { GameResources.tonmarutchiBasicAnimHappy0, GameResources.tonmarutchiBasicAnimHappy1, GameResources.tonmarutchiBasicAnimHappy2, GameResources.tonmarutchiBasicAnimHappy3, GameResources.tonmarutchiBasicAnimHappy4, } },
                        {"basic_anim_normal",   new string[] { GameResources.tonmarutchiBasicAnimNormal0, GameResources.tonmarutchiBasicAnimNormal1, GameResources.tonmarutchiBasicAnimNormal2, GameResources.tonmarutchiBasicAnimNormal3, GameResources.tonmarutchiBasicAnimNormal4, } },
                        {"cry",                 new string[] { GameResources.tonmarutchiCry0, GameResources.tonmarutchiCry1, } },
                        {"death",               new string[] { GameResources.tonmarutchiDeath0, GameResources.tonmarutchiDeath1, } },
                        {"dissapointed",        new string[] { GameResources.tonmarutchiDissapointed0, GameResources.tonmarutchiDissapointed1, } },

                        {"after_eating",        new string[] { GameResources.tonmarutchiAfterEating0, GameResources.tonmarutchiAfterEating1, } },
                        {"during_eating",       new string[] { GameResources.tonmarutchiDuringEating0, GameResources.tonmarutchiDuringEating1, } },
                        {"full_eating",         new string[] { GameResources.tonmarutchiFull0, } },

                        {"glad",                new string[] { GameResources.tonmarutchiGlad0, GameResources.tonmarutchiGlad1, } },
                        {"happy",               new string[] { GameResources.tonmarutchiHappy0, GameResources.tonmarutchiHappy1, } },

                        {"ill",                 new string[] { GameResources.tonmarutchiIll0, GameResources.tonmarutchiIll1, } },
                        {"very_ill",            new string[] { GameResources.tonmarutchiVeryIll0, GameResources.tonmarutchiVeryIll1, } },

                        {"jump",                new string[] { GameResources.tonmarutchiJump0, } },

                        {"mad",                 new string[] { GameResources.tonmarutchiMad0, GameResources.tonmarutchiMad1, } },
                        {"very_mad",            new string[] { GameResources.tonmarutchiVeryMad0, GameResources.tonmarutchiVeryMad1, } },

                        {"no",                  new string[] { GameResources.tonmarutchiNo0, GameResources.tonmarutchiNo1, } },

                        {"before_poop",         new string[] { GameResources.tonmarutchiBeforePoop0, GameResources.tonmarutchiBeforePoop1, } },
                        {"poop",                new string[] { GameResources.tonmarutchiToiletPoop0, GameResources.tonmarutchiToiletPoop1, } },

                        {"sad1",                new string[] { GameResources.tonmarutchiV1Sad0, GameResources.tonmarutchiV1Sad1, } },
                        {"sad2",                new string[] { GameResources.tonmarutchiV2Sad0, GameResources.tonmarutchiV2Sad1, } },
                        {"sad3",                new string[] { GameResources.tonmarutchiV3Sad0, GameResources.tonmarutchiV3Sad1, } },


                        {"shy",                 new string[] { GameResources.tonmarutchiShy0, GameResources.tonmarutchiShy1, } },
                        {"side_walk",           new string[] { GameResources.tonmarutchiSideWalk0, GameResources.tonmarutchiSideWalk1, } },

                        {"sleep",               new string[] { GameResources.tonmarutchiSleep0, GameResources.tonmarutchiSleep1, } },

                        {"sorry",               new string[] { GameResources.tonmarutchiV1Sorry0, GameResources.tonmarutchiV2Sorry0, GameResources.tonmarutchiV2Sorry1 } },
                        {"special1",            new string[] { GameResources.tonmarutchiV1Special0, } },
                        {"special2",            new string[] { GameResources.tonmarutchiV2Special0, GameResources.tonmarutchiV2Special1 } },

                        {"tired",               new string[] { GameResources.tonmarutchiTired0, GameResources.tonmarutchiTired1, } },
                        {"yawning",             new string[] { GameResources.tonmarutchiYawning0, GameResources.tonmarutchiYawning1, GameResources.tonmarutchiYawning2, } },
                        {"what",                new string[] { GameResources.tonmarutchiWhat0, } },
                    }
                },
            },

            // Teen
            new Dictionary<string, Dictionary<string, string[]>>()
            {
                 {"Kutchitamatchi", new Dictionary<string, string[]>()
                    {
                        {"basic_anim_happy",    new string[] { GameResources.kutchitamatchiBasicAnimHappy0, GameResources.kutchitamatchiBasicAnimHappy1, GameResources.kutchitamatchiBasicAnimHappy2, GameResources.kutchitamatchiBasicAnimHappy3, GameResources.kutchitamatchiBasicAnimHappy4, } },
                        {"basic_anim_normal",   new string[] { GameResources.kutchitamatchiBasicAnimNormal0, GameResources.kutchitamatchiBasicAnimNormal1, GameResources.kutchitamatchiBasicAnimNormal2, GameResources.kutchitamatchiBasicAnimNormal3, GameResources.kutchitamatchiBasicAnimNormal4, } },
                        {"cry",                 new string[] { GameResources.kutchitamatchiCry0, GameResources.kutchitamatchiCry1, } },
                        {"death",               new string[] { GameResources.kutchitamatchiDeath0, GameResources.kutchitamatchiDeath1, } },
                        {"dissapointed",        new string[] { GameResources.kutchitamatchiDissapointed0, GameResources.kutchitamatchiDissapointed1, } },

                        {"after_eating",        new string[] { GameResources.kutchitamatchiAfterEating0, GameResources.kutchitamatchiAfterEating1, } },
                        {"during_eating",       new string[] { GameResources.kutchitamatchiDuringEating0, GameResources.kutchitamatchiDuringEating1, } },
                        {"full_eating",         new string[] { GameResources.kutchitamatchiFull0, } },

                        {"glad",                new string[] { GameResources.kutchitamatchiGlad0, GameResources.kutchitamatchiGlad1, } },
                        {"happy",               new string[] { GameResources.kutchitamatchiHappy0, GameResources.kutchitamatchiHappy1, } },

                        {"ill",                 new string[] { GameResources.kutchitamatchiIll0, GameResources.kutchitamatchiIll1, } },
                        {"very_ill",            new string[] { GameResources.kutchitamatchiVeryIll0, GameResources.kutchitamatchiVeryIll1, } },

                        {"jump",                new string[] { GameResources.kutchitamatchiJump0, } },

                        {"mad",                 new string[] { GameResources.kutchitamatchiMad0, GameResources.kutchitamatchiMad1, } },
                        {"very_mad",            new string[] { GameResources.kutchitamatchiVeryMad0, GameResources.kutchitamatchiVeryMad1, } },

                        {"no",                  new string[] { GameResources.kutchitamatchiNo0, GameResources.kutchitamatchiNo1, } },

                        {"before_poop",         new string[] { GameResources.kutchitamatchiBeforePoop0, GameResources.kutchitamatchiBeforePoop1, } },
                        {"poop",                new string[] { GameResources.kutchitamatchiToiletPoop0, GameResources.kutchitamatchiToiletPoop1, } },

                        {"sad1",                new string[] { GameResources.kutchitamatchiV1Sad0, GameResources.kutchitamatchiV1Sad1, } },
                        {"sad2",                new string[] { GameResources.kutchitamatchiV2Sad0, GameResources.kutchitamatchiV2Sad1, } },
                        {"sad3",                new string[] { GameResources.kutchitamatchiV3Sad0, GameResources.kutchitamatchiV3Sad1, } },


                        {"shy",                 new string[] { GameResources.kutchitamatchiShy0, GameResources.kutchitamatchiShy1, } },
                        {"side_walk",           new string[] { GameResources.kutchitamatchiSideWalk0, GameResources.kutchitamatchiSideWalk1, } },

                        {"sleep",               new string[] { GameResources.kutchitamatchiSleep0, GameResources.kutchitamatchiSleep1, } },

                        {"sorry",               new string[] { GameResources.kutchitamatchiV1Sorry0, GameResources.kutchitamatchiV2Sorry0, GameResources.kutchitamatchiV2Sorry1 } },
                        {"special1",            new string[] { GameResources.kutchitamatchiV1Special0, } },
                        {"special2",            new string[] { GameResources.kutchitamatchiV2Special0, GameResources.kutchitamatchiV2Special1 } },

                        {"tired",               new string[] { GameResources.kutchitamatchiTired0, GameResources.kutchitamatchiTired1, } },
                        {"yawning",             new string[] { GameResources.kutchitamatchiYawning0, GameResources.kutchitamatchiYawning1, GameResources.kutchitamatchiYawning2, } },
                        {"what",                new string[] { GameResources.kutchitamatchiWhat0, } },
                    }
                },

                {"Tamatchi", new Dictionary<string, string[]>()
                    {
                        {"basic_anim_happy",    new string[] { GameResources.tamatchiBasicAnimHappy0, GameResources.tamatchiBasicAnimHappy1, GameResources.tamatchiBasicAnimHappy2, GameResources.tamatchiBasicAnimHappy3, GameResources.tamatchiBasicAnimHappy4, } },
                        {"basic_anim_normal",   new string[] { GameResources.tamatchiBasicAnimNormal0, GameResources.tamatchiBasicAnimNormal1, GameResources.tamatchiBasicAnimNormal2, GameResources.tamatchiBasicAnimNormal3, GameResources.tamatchiBasicAnimNormal4, } },
                        {"cry",                 new string[] { GameResources.tamatchiCry0, GameResources.tamatchiCry1, } },
                        {"death",               new string[] { GameResources.tamatchiDeath0, GameResources.tamatchiDeath1, } },
                        {"dissapointed",        new string[] { GameResources.tamatchiDissapointed0, GameResources.tamatchiDissapointed1, } },

                        {"after_eating",        new string[] { GameResources.tamatchiAfterEating0, GameResources.tamatchiAfterEating1, } },
                        {"during_eating",       new string[] { GameResources.tamatchiDuringEating0, GameResources.tamatchiDuringEating1, } },
                        {"full_eating",         new string[] { GameResources.tamatchiFull0, } },

                        {"glad",                new string[] { GameResources.tamatchiGlad0, GameResources.tamatchiGlad1, } },
                        {"happy",               new string[] { GameResources.tamatchiHappy0, GameResources.tamatchiHappy1, } },

                        {"ill",                 new string[] { GameResources.tamatchiIll0, GameResources.tamatchiIll1, } },
                        {"very_ill",            new string[] { GameResources.tamatchiVeryIll0, GameResources.tamatchiVeryIll1, } },

                        {"jump",                new string[] { GameResources.tamatchiJump0, } },

                        {"mad",                 new string[] { GameResources.tamatchiMad0, GameResources.tamatchiMad1, } },
                        {"very_mad",            new string[] { GameResources.tamatchiVeryMad0, GameResources.tamatchiVeryMad1, } },

                        {"no",                  new string[] { GameResources.tamatchiNo0, GameResources.tamatchiNo1, } },

                        {"before_poop",         new string[] { GameResources.tamatchiBeforePoop0, GameResources.tamatchiBeforePoop1, } },
                        {"poop",                new string[] { GameResources.tamatchiToiletPoop0, GameResources.tamatchiToiletPoop1, } },

                        {"sad1",                new string[] { GameResources.tamatchiV1Sad0, GameResources.tamatchiV1Sad1, } },
                        {"sad2",                new string[] { GameResources.tamatchiV2Sad0, GameResources.tamatchiV2Sad1, } },
                        {"sad3",                new string[] { GameResources.tamatchiV3Sad0, GameResources.tamatchiV3Sad1, } },


                        {"shy",                 new string[] { GameResources.tamatchiShy0, GameResources.tamatchiShy1, } },
                        {"side_walk",           new string[] { GameResources.tamatchiSideWalk0, GameResources.tamatchiSideWalk1, } },

                        {"sleep",               new string[] { GameResources.tamatchiSleep0, GameResources.tamatchiSleep1, } },

                        {"sorry",               new string[] { GameResources.tamatchiV1Sorry0, GameResources.tamatchiV2Sorry0, GameResources.tamatchiV2Sorry1 } },
                        {"special1",            new string[] { GameResources.tamatchiV1Special0, } },
                        {"special2",            new string[] { GameResources.tamatchiV2Special0, GameResources.tamatchiV2Special1 } },

                        {"tired",               new string[] { GameResources.tamatchiTired0, GameResources.tamatchiTired1, } },
                        {"yawning",             new string[] { GameResources.tamatchiYawning0, GameResources.tamatchiYawning1, GameResources.tamatchiYawning2, } },
                        {"what",                new string[] { GameResources.tamatchiWhat0, } },
                    }
                },

                {"Nyorotchi", new Dictionary<string, string[]>()
                    {
                        {"basic_anim_happy",    new string[] { GameResources.nyorotchiBasicAnimHappy0, GameResources.nyorotchiBasicAnimHappy1, GameResources.nyorotchiBasicAnimHappy2, GameResources.nyorotchiBasicAnimHappy3, GameResources.nyorotchiBasicAnimHappy4, } },
                        {"basic_anim_normal",   new string[] { GameResources.nyorotchiBasicAnimNormal0, GameResources.nyorotchiBasicAnimNormal1, GameResources.nyorotchiBasicAnimNormal2, GameResources.nyorotchiBasicAnimNormal3, GameResources.nyorotchiBasicAnimNormal4, } },
                        {"cry",                 new string[] { GameResources.nyorotchiCry0, GameResources.nyorotchiCry1, } },
                        {"death",               new string[] { GameResources.nyorotchiDeath0, GameResources.nyorotchiDeath1, } },
                        {"dissapointed",        new string[] { GameResources.nyorotchiDissapointed0, GameResources.nyorotchiDissapointed1, } },

                        {"after_eating",        new string[] { GameResources.nyorotchiAfterEating0, GameResources.nyorotchiAfterEating1, } },
                        {"during_eating",       new string[] { GameResources.nyorotchiDuringEating0, GameResources.nyorotchiDuringEating1, } },
                        {"full_eating",         new string[] { GameResources.nyorotchiFull0, } },

                        {"glad",                new string[] { GameResources.nyorotchiGlad0, GameResources.nyorotchiGlad1, } },
                        {"happy",               new string[] { GameResources.nyorotchiHappy0, GameResources.nyorotchiHappy1, } },

                        {"ill",                 new string[] { GameResources.nyorotchiIll0, GameResources.nyorotchiIll1, } },
                        {"very_ill",            new string[] { GameResources.nyorotchiVeryIll0, GameResources.nyorotchiVeryIll1, } },

                        {"jump",                new string[] { GameResources.nyorotchiJump0, } },

                        {"mad",                 new string[] { GameResources.nyorotchiMad0, GameResources.nyorotchiMad1, } },
                        {"very_mad",            new string[] { GameResources.nyorotchiVeryMad0, GameResources.nyorotchiVeryMad1, } },

                        {"no",                  new string[] { GameResources.nyorotchiNo0, GameResources.nyorotchiNo1, } },

                        {"before_poop",         new string[] { GameResources.nyorotchiBeforePoop0, GameResources.nyorotchiBeforePoop1, } },
                        {"poop",                new string[] { GameResources.nyorotchiToiletPoop0, GameResources.nyorotchiToiletPoop1, } },

                        {"sad1",                new string[] { GameResources.nyorotchiV1Sad0, GameResources.nyorotchiV1Sad1, } },
                        {"sad2",                new string[] { GameResources.nyorotchiV2Sad0, GameResources.nyorotchiV2Sad1, } },
                        {"sad3",                new string[] { GameResources.nyorotchiV3Sad0, GameResources.nyorotchiV3Sad1, } },


                        {"shy",                 new string[] { GameResources.nyorotchiShy0, GameResources.nyorotchiShy1, } },
                        {"side_walk",           new string[] { GameResources.nyorotchiSideWalk0, GameResources.nyorotchiSideWalk1, } },

                        {"sleep",               new string[] { GameResources.nyorotchiSleep0, GameResources.nyorotchiSleep1, } },

                        {"sorry",               new string[] { GameResources.nyorotchiV1Sorry0, GameResources.nyorotchiV2Sorry0, GameResources.nyorotchiV2Sorry1 } },
                        {"special1",            new string[] { GameResources.nyorotchiV1Special0, } },
                        {"special2",            new string[] { GameResources.nyorotchiV2Special0, GameResources.nyorotchiV2Special1 } },

                        {"tired",               new string[] { GameResources.nyorotchiTired0, GameResources.nyorotchiTired1, } },
                        {"yawning",             new string[] { GameResources.nyorotchiYawning0, GameResources.nyorotchiYawning1, GameResources.nyorotchiYawning2, } },
                        {"what",                new string[] { GameResources.nyorotchiWhat0, } },
                    }
                },

                {"Takotchi", new Dictionary<string, string[]>()
                    {
                        {"basic_anim_happy",    new string[] { GameResources.takotchiBasicAnimHappy0, GameResources.takotchiBasicAnimHappy1, GameResources.takotchiBasicAnimHappy2, GameResources.takotchiBasicAnimHappy3, GameResources.takotchiBasicAnimHappy4, } },
                        {"basic_anim_normal",   new string[] { GameResources.takotchiBasicAnimNormal0, GameResources.takotchiBasicAnimNormal1, GameResources.takotchiBasicAnimNormal2, GameResources.takotchiBasicAnimNormal3, GameResources.takotchiBasicAnimNormal4, } },
                        {"cry",                 new string[] { GameResources.takotchiCry0, GameResources.takotchiCry1, } },
                        {"death",               new string[] { GameResources.takotchiDeath0, GameResources.takotchiDeath1, } },
                        {"dissapointed",        new string[] { GameResources.takotchiDissapointed0, GameResources.takotchiDissapointed1, } },

                        {"after_eating",        new string[] { GameResources.takotchiAfterEating0, GameResources.takotchiAfterEating1, } },
                        {"during_eating",       new string[] { GameResources.takotchiDuringEating0, GameResources.takotchiDuringEating1, } },
                        {"full_eating",         new string[] { GameResources.takotchiFull0, } },

                        {"glad",                new string[] { GameResources.takotchiGlad0, GameResources.takotchiGlad1, } },
                        {"happy",               new string[] { GameResources.takotchiHappy0, GameResources.takotchiHappy1, } },

                        {"ill",                 new string[] { GameResources.takotchiIll0, GameResources.takotchiIll1, } },
                        {"very_ill",            new string[] { GameResources.takotchiVeryIll0, GameResources.takotchiVeryIll1, } },

                        {"jump",                new string[] { GameResources.takotchiJump0, } },

                        {"mad",                 new string[] { GameResources.takotchiMad0, GameResources.takotchiMad1, } },
                        {"very_mad",            new string[] { GameResources.takotchiVeryMad0, GameResources.takotchiVeryMad1, } },

                        {"no",                  new string[] { GameResources.takotchiNo0, GameResources.takotchiNo1, } },

                        {"before_poop",         new string[] { GameResources.takotchiBeforePoop0, GameResources.takotchiBeforePoop1, } },
                        {"poop",                new string[] { GameResources.takotchiToiletPoop0, GameResources.takotchiToiletPoop1, } },

                        {"sad1",                new string[] { GameResources.takotchiV1Sad0, GameResources.takotchiV1Sad1, } },
                        {"sad2",                new string[] { GameResources.takotchiV2Sad0, GameResources.takotchiV2Sad1, } },
                        {"sad3",                new string[] { GameResources.takotchiV3Sad0, GameResources.takotchiV3Sad1, } },


                        {"shy",                 new string[] { GameResources.takotchiShy0, GameResources.takotchiShy1, } },
                        {"side_walk",           new string[] { GameResources.takotchiSideWalk0, GameResources.takotchiSideWalk1, } },

                        {"sleep",               new string[] { GameResources.takotchiSleep0, GameResources.takotchiSleep1, } },

                        {"sorry",               new string[] { GameResources.takotchiV1Sorry0, GameResources.takotchiV2Sorry0, GameResources.takotchiV2Sorry1 } },
                        {"special1",            new string[] { GameResources.takotchiV1Special0, } },
                        {"special2",            new string[] { GameResources.takotchiV2Special0, GameResources.takotchiV2Special1 } },

                        {"tired",               new string[] { GameResources.takotchiTired0, GameResources.takotchiTired1, } },
                        {"yawning",             new string[] { GameResources.takotchiYawning0, GameResources.takotchiYawning1, GameResources.takotchiYawning2, } },
                        {"what",                new string[] { GameResources.takotchiWhat0, } },
                    }
                },
            },

            // Adult
            new Dictionary<string, Dictionary<string, string[]>>()
            {
                {"Bill", new Dictionary<string, string[]>()
                    {
                        {"basic_anim_happy",    new string[] { GameResources.billBasicAnimHappy0, GameResources.billBasicAnimHappy1, GameResources.billBasicAnimHappy2, GameResources.billBasicAnimHappy3, GameResources.billBasicAnimHappy4, } },
                        {"basic_anim_normal",   new string[] { GameResources.billBasicAnimNormal0, GameResources.billBasicAnimNormal1, GameResources.billBasicAnimNormal2, GameResources.billBasicAnimNormal3, GameResources.billBasicAnimNormal4, } },
                        {"cry",                 new string[] { GameResources.billCry0, GameResources.billCry1, } },
                        {"death",               new string[] { GameResources.billDeath0, GameResources.billDeath1, } },
                        {"dissapointed",        new string[] { GameResources.billDissapointed0, GameResources.billDissapointed1, } },

                        {"after_eating",        new string[] { GameResources.billAfterEating0, GameResources.billAfterEating1, } },
                        {"during_eating",       new string[] { GameResources.billDuringEating0, GameResources.billDuringEating1, } },
                        {"full_eating",         new string[] { GameResources.billFull0, } },

                        {"glad",                new string[] { GameResources.billGlad0, GameResources.billGlad1, } },
                        {"happy",               new string[] { GameResources.billHappy0, GameResources.billHappy1, } },

                        {"ill",                 new string[] { GameResources.billIll0, GameResources.billIll1, } },
                        {"very_ill",            new string[] { GameResources.billVeryIll0, GameResources.billVeryIll1, } },

                        {"jump",                new string[] { GameResources.billJump0, } },

                        {"mad",                 new string[] { GameResources.billMad0, GameResources.billMad1, } },
                        {"very_mad",            new string[] { GameResources.billVeryMad0, GameResources.billVeryMad1, } },

                        {"no",                  new string[] { GameResources.billNo0, GameResources.billNo1, } },

                        {"before_poop",         new string[] { GameResources.billBeforePoop0, GameResources.billBeforePoop1, } },
                        {"poop",                new string[] { GameResources.billToiletPoop0, GameResources.billToiletPoop1, } },

                        {"sad1",                new string[] { GameResources.billV1Sad0, GameResources.billV1Sad1, } },
                        {"sad2",                new string[] { GameResources.billV2Sad0, GameResources.billV2Sad1, } },
                        {"sad3",                new string[] { GameResources.billV3Sad0, GameResources.billV3Sad1, } },


                        {"shy",                 new string[] { GameResources.billShy0, GameResources.billShy1, } },
                        {"side_walk",           new string[] { GameResources.billSideWalk0, GameResources.billSideWalk1, } },

                        {"sleep",               new string[] { GameResources.billSleep0, GameResources.billSleep1, } },

                        {"sorry",               new string[] { GameResources.billV1Sorry0, GameResources.billV2Sorry0, GameResources.billV2Sorry1 } },
                        {"special1",            new string[] { GameResources.billV1Special0, } },
                        {"special2",            new string[] { GameResources.billV2Special0, GameResources.billV2Special1 } },

                        {"tired",               new string[] { GameResources.billTired0, GameResources.billTired1, } },
                        {"yawning",             new string[] { GameResources.billYawning0, GameResources.billYawning1, GameResources.billYawning2, } },
                        {"what",                new string[] { GameResources.billWhat0, } },
                    }
                },

                {"Ginjirotchi", new Dictionary<string, string[]>()
                    {
                        {"basic_anim_happy",    new string[] { GameResources.ginjirotchiBasicAnimHappy0, GameResources.ginjirotchiBasicAnimHappy1, GameResources.ginjirotchiBasicAnimHappy2, GameResources.ginjirotchiBasicAnimHappy3, GameResources.ginjirotchiBasicAnimHappy4, } },
                        {"basic_anim_normal",   new string[] { GameResources.ginjirotchiBasicAnimNormal0, GameResources.ginjirotchiBasicAnimNormal1, GameResources.ginjirotchiBasicAnimNormal2, GameResources.ginjirotchiBasicAnimNormal3, GameResources.ginjirotchiBasicAnimNormal4, } },
                        {"cry",                 new string[] { GameResources.ginjirotchiCry0, GameResources.ginjirotchiCry1, } },
                        {"death",               new string[] { GameResources.ginjirotchiDeath0, GameResources.ginjirotchiDeath1, } },
                        {"dissapointed",        new string[] { GameResources.ginjirotchiDissapointed0, GameResources.ginjirotchiDissapointed1, } },

                        {"after_eating",        new string[] { GameResources.ginjirotchiAfterEating0, GameResources.ginjirotchiAfterEating1, } },
                        {"during_eating",       new string[] { GameResources.ginjirotchiDuringEating0, GameResources.ginjirotchiDuringEating1, } },
                        {"full_eating",         new string[] { GameResources.ginjirotchiFull0, } },

                        {"glad",                new string[] { GameResources.ginjirotchiGlad0, GameResources.ginjirotchiGlad1, } },
                        {"happy",               new string[] { GameResources.ginjirotchiHappy0, GameResources.ginjirotchiHappy1, } },

                        {"ill",                 new string[] { GameResources.ginjirotchiIll0, GameResources.ginjirotchiIll1, } },
                        {"very_ill",            new string[] { GameResources.ginjirotchiVeryIll0, GameResources.ginjirotchiVeryIll1, } },

                        {"jump",                new string[] { GameResources.ginjirotchiJump0, } },

                        {"mad",                 new string[] { GameResources.ginjirotchiMad0, GameResources.ginjirotchiMad1, } },
                        {"very_mad",            new string[] { GameResources.ginjirotchiVeryMad0, GameResources.ginjirotchiVeryMad1, } },

                        {"no",                  new string[] { GameResources.ginjirotchiNo0, GameResources.ginjirotchiNo1, } },

                        {"before_poop",         new string[] { GameResources.ginjirotchiBeforePoop0, GameResources.ginjirotchiBeforePoop1, } },
                        {"poop",                new string[] { GameResources.ginjirotchiToiletPoop0, GameResources.ginjirotchiToiletPoop1, } },

                        {"sad1",                new string[] { GameResources.ginjirotchiV1Sad0, GameResources.ginjirotchiV1Sad1, } },
                        {"sad2",                new string[] { GameResources.ginjirotchiV2Sad0, GameResources.ginjirotchiV2Sad1, } },
                        {"sad3",                new string[] { GameResources.ginjirotchiV3Sad0, GameResources.ginjirotchiV3Sad1, } },


                        {"shy",                 new string[] { GameResources.ginjirotchiShy0, GameResources.ginjirotchiShy1, } },
                        {"side_walk",           new string[] { GameResources.ginjirotchiSideWalk0, GameResources.ginjirotchiSideWalk1, } },

                        {"sleep",               new string[] { GameResources.ginjirotchiSleep0, GameResources.ginjirotchiSleep1, } },

                        {"sorry",               new string[] { GameResources.ginjirotchiV1Sorry0, GameResources.ginjirotchiV2Sorry0, GameResources.ginjirotchiV2Sorry1 } },
                        {"special1",            new string[] { GameResources.ginjirotchiV1Special0, } },
                        {"special2",            new string[] { GameResources.ginjirotchiV2Special0, GameResources.ginjirotchiV2Special1 } },

                        {"tired",               new string[] { GameResources.ginjirotchiTired0, GameResources.ginjirotchiTired1, } },
                        {"yawning",             new string[] { GameResources.ginjirotchiYawning0, GameResources.ginjirotchiYawning1, GameResources.ginjirotchiYawning2, } },
                        {"what",                new string[] { GameResources.ginjirotchiWhat0, } },
                    }
                },

                {"Hashizotchi", new Dictionary<string, string[]>()
                    {
                        {"basic_anim_happy",    new string[] { GameResources.hashizotchiBasicAnimHappy0, GameResources.hashizotchiBasicAnimHappy1, GameResources.hashizotchiBasicAnimHappy2, GameResources.hashizotchiBasicAnimHappy3, GameResources.hashizotchiBasicAnimHappy4, } },
                        {"basic_anim_normal",   new string[] { GameResources.hashizotchiBasicAnimNormal0, GameResources.hashizotchiBasicAnimNormal1, GameResources.hashizotchiBasicAnimNormal2, GameResources.hashizotchiBasicAnimNormal3, GameResources.hashizotchiBasicAnimNormal4, } },
                        {"cry",                 new string[] { GameResources.hashizotchiCry0, GameResources.hashizotchiCry1, } },
                        {"death",               new string[] { GameResources.hashizotchiDeath0, GameResources.hashizotchiDeath1, } },
                        {"dissapointed",        new string[] { GameResources.hashizotchiDissapointed0, GameResources.hashizotchiDissapointed1, } },

                        {"after_eating",        new string[] { GameResources.hashizotchiAfterEating0, GameResources.hashizotchiAfterEating1, } },
                        {"during_eating",       new string[] { GameResources.hashizotchiDuringEating0, GameResources.hashizotchiDuringEating1, } },
                        {"full_eating",         new string[] { GameResources.hashizotchiFull0, } },

                        {"glad",                new string[] { GameResources.hashizotchiGlad0, GameResources.hashizotchiGlad1, } },
                        {"happy",               new string[] { GameResources.hashizotchiHappy0, GameResources.hashizotchiHappy1, } },

                        {"ill",                 new string[] { GameResources.hashizotchiIll0, GameResources.hashizotchiIll1, } },
                        {"very_ill",            new string[] { GameResources.hashizotchiVeryIll0, GameResources.hashizotchiVeryIll1, } },

                        {"jump",                new string[] { GameResources.hashizotchiJump0, } },

                        {"mad",                 new string[] { GameResources.hashizotchiMad0, GameResources.hashizotchiMad1, } },
                        {"very_mad",            new string[] { GameResources.hashizotchiVeryMad0, GameResources.hashizotchiVeryMad1, } },

                        {"no",                  new string[] { GameResources.hashizotchiNo0, GameResources.hashizotchiNo1, } },

                        {"before_poop",         new string[] { GameResources.hashizotchiBeforePoop0, GameResources.hashizotchiBeforePoop1, } },
                        {"poop",                new string[] { GameResources.hashizotchiToiletPoop0, GameResources.hashizotchiToiletPoop1, } },

                        {"sad1",                new string[] { GameResources.hashizotchiV1Sad0, GameResources.hashizotchiV1Sad1, } },
                        {"sad2",                new string[] { GameResources.hashizotchiV2Sad0, GameResources.hashizotchiV2Sad1, } },
                        {"sad3",                new string[] { GameResources.hashizotchiV3Sad0, GameResources.hashizotchiV3Sad1, } },


                        {"shy",                 new string[] { GameResources.hashizotchiShy0, GameResources.hashizotchiShy1, } },
                        {"side_walk",           new string[] { GameResources.hashizotchiSideWalk0, GameResources.hashizotchiSideWalk1, } },

                        {"sleep",               new string[] { GameResources.hashizotchiSleep0, GameResources.hashizotchiSleep1, } },

                        {"sorry",               new string[] { GameResources.hashizotchiV1Sorry0, GameResources.hashizotchiV2Sorry0, GameResources.hashizotchiV2Sorry1 } },
                        {"special1",            new string[] { GameResources.hashizotchiV1Special0, } },
                        {"special2",            new string[] { GameResources.hashizotchiV2Special0, GameResources.hashizotchiV2Special1 } },

                        {"tired",               new string[] { GameResources.hashizotchiTired0, GameResources.hashizotchiTired1, } },
                        {"yawning",             new string[] { GameResources.hashizotchiYawning0, GameResources.hashizotchiYawning1, GameResources.hashizotchiYawning2, } },
                        {"what",                new string[] { GameResources.hashizotchiWhat0, } },
                    }
                },

                {"Kuchipatchi", new Dictionary<string, string[]>()
                    {
                        {"basic_anim_happy",    new string[] { GameResources.kuchipatchiBasicAnimHappy0, GameResources.kuchipatchiBasicAnimHappy1, GameResources.kuchipatchiBasicAnimHappy2, GameResources.kuchipatchiBasicAnimHappy3, GameResources.kuchipatchiBasicAnimHappy4, } },
                        {"basic_anim_normal",   new string[] { GameResources.kuchipatchiBasicAnimNormal0, GameResources.kuchipatchiBasicAnimNormal1, GameResources.kuchipatchiBasicAnimNormal2, GameResources.kuchipatchiBasicAnimNormal3, GameResources.kuchipatchiBasicAnimNormal4, } },
                        {"cry",                 new string[] { GameResources.kuchipatchiCry0, GameResources.kuchipatchiCry1, } },
                        {"death",               new string[] { GameResources.kuchipatchiDeath0, GameResources.kuchipatchiDeath1, } },
                        {"dissapointed",        new string[] { GameResources.kuchipatchiDissapointed0, GameResources.kuchipatchiDissapointed1, } },

                        {"after_eating",        new string[] { GameResources.kuchipatchiAfterEating0, GameResources.kuchipatchiAfterEating1, } },
                        {"during_eating",       new string[] { GameResources.kuchipatchiDuringEating0, GameResources.kuchipatchiDuringEating1, } },
                        {"full_eating",         new string[] { GameResources.kuchipatchiFull0, } },

                        {"glad",                new string[] { GameResources.kuchipatchiGlad0, GameResources.kuchipatchiGlad1, } },
                        {"happy",               new string[] { GameResources.kuchipatchiHappy0, GameResources.kuchipatchiHappy1, } },

                        {"ill",                 new string[] { GameResources.kuchipatchiIll0, GameResources.kuchipatchiIll1, } },
                        {"very_ill",            new string[] { GameResources.kuchipatchiVeryIll0, GameResources.kuchipatchiVeryIll1, } },

                        {"jump",                new string[] { GameResources.kuchipatchiJump0, } },

                        {"mad",                 new string[] { GameResources.kuchipatchiMad0, GameResources.kuchipatchiMad1, } },
                        {"very_mad",            new string[] { GameResources.kuchipatchiVeryMad0, GameResources.kuchipatchiVeryMad1, } },

                        {"no",                  new string[] { GameResources.kuchipatchiNo0, GameResources.kuchipatchiNo1, } },

                        {"before_poop",         new string[] { GameResources.kuchipatchiBeforePoop0, GameResources.kuchipatchiBeforePoop1, } },
                        {"poop",                new string[] { GameResources.kuchipatchiToiletPoop0, GameResources.kuchipatchiToiletPoop1, } },

                        {"sad1",                new string[] { GameResources.kuchipatchiV1Sad0, GameResources.kuchipatchiV1Sad1, } },
                        {"sad2",                new string[] { GameResources.kuchipatchiV2Sad0, GameResources.kuchipatchiV2Sad1, } },
                        {"sad3",                new string[] { GameResources.kuchipatchiV3Sad0, GameResources.kuchipatchiV3Sad1, } },


                        {"shy",                 new string[] { GameResources.kuchipatchiShy0, GameResources.kuchipatchiShy1, } },
                        {"side_walk",           new string[] { GameResources.kuchipatchiSideWalk0, GameResources.kuchipatchiSideWalk1, } },

                        {"sleep",               new string[] { GameResources.kuchipatchiSleep0, GameResources.kuchipatchiSleep1, } },

                        {"sorry",               new string[] { GameResources.kuchipatchiV1Sorry0, GameResources.kuchipatchiV2Sorry0, GameResources.kuchipatchiV2Sorry1 } },
                        {"special1",            new string[] { GameResources.kuchipatchiV1Special0, } },
                        {"special2",            new string[] { GameResources.kuchipatchiV2Special0, GameResources.kuchipatchiV2Special1 } },

                        {"tired",               new string[] { GameResources.kuchipatchiTired0, GameResources.kuchipatchiTired1, } },
                        {"yawning",             new string[] { GameResources.kuchipatchiYawning0, GameResources.kuchipatchiYawning1, GameResources.kuchipatchiYawning2, } },
                        {"what",                new string[] { GameResources.kuchipatchiWhat0, } },
                    }
                },

                {"Kusatchi", new Dictionary<string, string[]>()
                    {
                        {"basic_anim_happy",    new string[] { GameResources.kusatchiBasicAnimHappy0, GameResources.kusatchiBasicAnimHappy1, GameResources.kusatchiBasicAnimHappy2, GameResources.kusatchiBasicAnimHappy3, GameResources.kusatchiBasicAnimHappy4, } },
                        {"basic_anim_normal",   new string[] { GameResources.kusatchiBasicAnimNormal0, GameResources.kusatchiBasicAnimNormal1, GameResources.kusatchiBasicAnimNormal2, GameResources.kusatchiBasicAnimNormal3, GameResources.kusatchiBasicAnimNormal4, } },
                        {"cry",                 new string[] { GameResources.kusatchiCry0, GameResources.kusatchiCry1, } },
                        {"death",               new string[] { GameResources.kusatchiDeath0, GameResources.kusatchiDeath1, } },
                        {"dissapointed",        new string[] { GameResources.kusatchiDissapointed0, GameResources.kusatchiDissapointed1, } },

                        {"after_eating",        new string[] { GameResources.kusatchiAfterEating0, GameResources.kusatchiAfterEating1, } },
                        {"during_eating",       new string[] { GameResources.kusatchiDuringEating0, GameResources.kusatchiDuringEating1, } },
                        {"full_eating",         new string[] { GameResources.kusatchiFull0, } },

                        {"glad",                new string[] { GameResources.kusatchiGlad0, GameResources.kusatchiGlad1, } },
                        {"happy",               new string[] { GameResources.kusatchiHappy0, GameResources.kusatchiHappy1, } },

                        {"ill",                 new string[] { GameResources.kusatchiIll0, GameResources.kusatchiIll1, } },
                        {"very_ill",            new string[] { GameResources.kusatchiVeryIll0, GameResources.kusatchiVeryIll1, } },

                        {"jump",                new string[] { GameResources.kusatchiJump0, } },

                        {"mad",                 new string[] { GameResources.kusatchiMad0, GameResources.kusatchiMad1, } },
                        {"very_mad",            new string[] { GameResources.kusatchiVeryMad0, GameResources.kusatchiVeryMad1, } },

                        {"no",                  new string[] { GameResources.kusatchiNo0, GameResources.kusatchiNo1, } },

                        {"before_poop",         new string[] { GameResources.kusatchiBeforePoop0, GameResources.kusatchiBeforePoop1, } },
                        {"poop",                new string[] { GameResources.kusatchiToiletPoop0, GameResources.kusatchiToiletPoop1, } },

                        {"sad1",                new string[] { GameResources.kusatchiV1Sad0, GameResources.kusatchiV1Sad1, } },
                        {"sad2",                new string[] { GameResources.kusatchiV2Sad0, GameResources.kusatchiV2Sad1, } },
                        {"sad3",                new string[] { GameResources.kusatchiV3Sad0, GameResources.kusatchiV3Sad1, } },


                        {"shy",                 new string[] { GameResources.kusatchiShy0, GameResources.kusatchiShy1, } },
                        {"side_walk",           new string[] { GameResources.kusatchiSideWalk0, GameResources.kusatchiSideWalk1, } },

                        {"sleep",               new string[] { GameResources.kusatchiSleep0, GameResources.kusatchiSleep1, } },

                        {"sorry",               new string[] { GameResources.kusatchiV1Sorry0, GameResources.kusatchiV2Sorry0, GameResources.kusatchiV2Sorry1 } },
                        {"special1",            new string[] { GameResources.kusatchiV1Special0, } },
                        {"special2",            new string[] { GameResources.kusatchiV2Special0, GameResources.kusatchiV2Special1 } },

                        {"tired",               new string[] { GameResources.kusatchiTired0, GameResources.kusatchiTired1, } },
                        {"yawning",             new string[] { GameResources.kusatchiYawning0, GameResources.kusatchiYawning1, GameResources.kusatchiYawning2, } },
                        {"what",                new string[] { GameResources.kusatchiWhat0, } },
                    }
                },

                {"Mametchi", new Dictionary<string, string[]>()
                    {
                        {"basic_anim_happy",    new string[] { GameResources.mametchiBasicAnimHappy0, GameResources.mametchiBasicAnimHappy1, GameResources.mametchiBasicAnimHappy2, GameResources.mametchiBasicAnimHappy3, GameResources.mametchiBasicAnimHappy4, } },
                        {"basic_anim_normal",   new string[] { GameResources.mametchiBasicAnimNormal0, GameResources.mametchiBasicAnimNormal1, GameResources.mametchiBasicAnimNormal2, GameResources.mametchiBasicAnimNormal3, GameResources.mametchiBasicAnimNormal4, } },
                        {"cry",                 new string[] { GameResources.mametchiCry0, GameResources.mametchiCry1, } },
                        {"death",               new string[] { GameResources.mametchiDeath0, GameResources.mametchiDeath1, } },
                        {"dissapointed",        new string[] { GameResources.mametchiDissapointed0, GameResources.mametchiDissapointed1, } },

                        {"after_eating",        new string[] { GameResources.mametchiAfterEating0, GameResources.mametchiAfterEating1, } },
                        {"during_eating",       new string[] { GameResources.mametchiDuringEating0, GameResources.mametchiDuringEating1, } },
                        {"full_eating",         new string[] { GameResources.mametchiFull0, } },

                        {"glad",                new string[] { GameResources.mametchiGlad0, GameResources.mametchiGlad1, } },
                        {"happy",               new string[] { GameResources.mametchiHappy0, GameResources.mametchiHappy1, } },

                        {"ill",                 new string[] { GameResources.mametchiIll0, GameResources.mametchiIll1, } },
                        {"very_ill",            new string[] { GameResources.mametchiVeryIll0, GameResources.mametchiVeryIll1, } },

                        {"jump",                new string[] { GameResources.mametchiJump0, } },

                        {"mad",                 new string[] { GameResources.mametchiMad0, GameResources.mametchiMad1, } },
                        {"very_mad",            new string[] { GameResources.mametchiVeryMad0, GameResources.mametchiVeryMad1, } },

                        {"no",                  new string[] { GameResources.mametchiNo0, GameResources.mametchiNo1, } },

                        {"before_poop",         new string[] { GameResources.mametchiBeforePoop0, GameResources.mametchiBeforePoop1, } },
                        {"poop",                new string[] { GameResources.mametchiToiletPoop0, GameResources.mametchiToiletPoop1, } },

                        {"sad1",                new string[] { GameResources.mametchiV1Sad0, GameResources.mametchiV1Sad1, } },
                        {"sad2",                new string[] { GameResources.mametchiV2Sad0, GameResources.mametchiV2Sad1, } },
                        {"sad3",                new string[] { GameResources.mametchiV3Sad0, GameResources.mametchiV3Sad1, } },


                        {"shy",                 new string[] { GameResources.mametchiShy0, GameResources.mametchiShy1, } },
                        {"side_walk",           new string[] { GameResources.mametchiSideWalk0, GameResources.mametchiSideWalk1, } },

                        {"sleep",               new string[] { GameResources.mametchiSleep0, GameResources.mametchiSleep1, } },

                        {"sorry",               new string[] { GameResources.mametchiV1Sorry0, GameResources.mametchiV2Sorry0, GameResources.mametchiV2Sorry1 } },
                        {"special1",            new string[] { GameResources.mametchiV1Special0, } },
                        {"special2",            new string[] { GameResources.mametchiV2Special0, GameResources.mametchiV2Special1 } },

                        {"tired",               new string[] { GameResources.mametchiTired0, GameResources.mametchiTired1, } },
                        {"yawning",             new string[] { GameResources.mametchiYawning0, GameResources.mametchiYawning1, GameResources.mametchiYawning2, } },
                        {"what",                new string[] { GameResources.mametchiWhat0, } },
                    }
                },

                {"Masukutchi", new Dictionary<string, string[]>()
                    {
                        {"basic_anim_happy",    new string[] { GameResources.masukutchiBasicAnimHappy0, GameResources.masukutchiBasicAnimHappy1, GameResources.masukutchiBasicAnimHappy2, GameResources.masukutchiBasicAnimHappy3, GameResources.masukutchiBasicAnimHappy4, } },
                        {"basic_anim_normal",   new string[] { GameResources.masukutchiBasicAnimNormal0, GameResources.masukutchiBasicAnimNormal1, GameResources.masukutchiBasicAnimNormal2, GameResources.masukutchiBasicAnimNormal3, GameResources.masukutchiBasicAnimNormal4, } },
                        {"cry",                 new string[] { GameResources.masukutchiCry0, GameResources.masukutchiCry1, } },
                        {"death",               new string[] { GameResources.masukutchiDeath0, GameResources.masukutchiDeath1, } },
                        {"dissapointed",        new string[] { GameResources.masukutchiDissapointed0, GameResources.masukutchiDissapointed1, } },

                        {"after_eating",        new string[] { GameResources.masukutchiAfterEating0, GameResources.masukutchiAfterEating1, } },
                        {"during_eating",       new string[] { GameResources.masukutchiDuringEating0, GameResources.masukutchiDuringEating1, } },
                        {"full_eating",         new string[] { GameResources.masukutchiFull0, } },

                        {"glad",                new string[] { GameResources.masukutchiGlad0, GameResources.masukutchiGlad1, } },
                        {"happy",               new string[] { GameResources.masukutchiHappy0, GameResources.masukutchiHappy1, } },

                        {"ill",                 new string[] { GameResources.masukutchiIll0, GameResources.masukutchiIll1, } },
                        {"very_ill",            new string[] { GameResources.masukutchiVeryIll0, GameResources.masukutchiVeryIll1, } },

                        {"jump",                new string[] { GameResources.masukutchiJump0, } },

                        {"mad",                 new string[] { GameResources.masukutchiMad0, GameResources.masukutchiMad1, } },
                        {"very_mad",            new string[] { GameResources.masukutchiVeryMad0, GameResources.masukutchiVeryMad1, } },

                        {"no",                  new string[] { GameResources.masukutchiNo0, GameResources.masukutchiNo1, } },

                        {"before_poop",         new string[] { GameResources.masukutchiBeforePoop0, GameResources.masukutchiBeforePoop1, } },
                        {"poop",                new string[] { GameResources.masukutchiToiletPoop0, GameResources.masukutchiToiletPoop1, } },

                        {"sad1",                new string[] { GameResources.masukutchiV1Sad0, GameResources.masukutchiV1Sad1, } },
                        {"sad2",                new string[] { GameResources.masukutchiV2Sad0, GameResources.masukutchiV2Sad1, } },
                        {"sad3",                new string[] { GameResources.masukutchiV3Sad0, GameResources.masukutchiV3Sad1, } },


                        {"shy",                 new string[] { GameResources.masukutchiShy0, GameResources.masukutchiShy1, } },
                        {"side_walk",           new string[] { GameResources.masukutchiSideWalk0, GameResources.masukutchiSideWalk1, } },

                        {"sleep",               new string[] { GameResources.masukutchiSleep0, GameResources.masukutchiSleep1, } },

                        {"sorry",               new string[] { GameResources.masukutchiV1Sorry0, GameResources.masukutchiV2Sorry0, GameResources.masukutchiV2Sorry1 } },
                        {"special1",            new string[] { GameResources.masukutchiV1Special0, } },
                        {"special2",            new string[] { GameResources.masukutchiV2Special0, GameResources.masukutchiV2Special1 } },

                        {"tired",               new string[] { GameResources.masukutchiTired0, GameResources.masukutchiTired1, } },
                        {"yawning",             new string[] { GameResources.masukutchiYawning0, GameResources.masukutchiYawning1, GameResources.masukutchiYawning2, } },
                        {"what",                new string[] { GameResources.masukutchiWhat0, } },
                    }
                },

                {"Mimitchi", new Dictionary<string, string[]>()
                    {
                        {"basic_anim_happy",    new string[] { GameResources.mimitchiBasicAnimHappy0, GameResources.mimitchiBasicAnimHappy1, GameResources.mimitchiBasicAnimHappy2, GameResources.mimitchiBasicAnimHappy3, GameResources.mimitchiBasicAnimHappy4, } },
                        {"basic_anim_normal",   new string[] { GameResources.mimitchiBasicAnimNormal0, GameResources.mimitchiBasicAnimNormal1, GameResources.mimitchiBasicAnimNormal2, GameResources.mimitchiBasicAnimNormal3, GameResources.mimitchiBasicAnimNormal4, } },
                        {"cry",                 new string[] { GameResources.mimitchiCry0, GameResources.mimitchiCry1, } },
                        {"death",               new string[] { GameResources.mimitchiDeath0, GameResources.mimitchiDeath1, } },
                        {"dissapointed",        new string[] { GameResources.mimitchiDissapointed0, GameResources.mimitchiDissapointed1, } },

                        {"after_eating",        new string[] { GameResources.mimitchiAfterEating0, GameResources.mimitchiAfterEating1, } },
                        {"during_eating",       new string[] { GameResources.mimitchiDuringEating0, GameResources.mimitchiDuringEating1, } },
                        {"full_eating",         new string[] { GameResources.mimitchiFull0, } },

                        {"glad",                new string[] { GameResources.mimitchiGlad0, GameResources.mimitchiGlad1, } },
                        {"happy",               new string[] { GameResources.mimitchiHappy0, GameResources.mimitchiHappy1, } },

                        {"ill",                 new string[] { GameResources.mimitchiIll0, GameResources.mimitchiIll1, } },
                        {"very_ill",            new string[] { GameResources.mimitchiVeryIll0, GameResources.mimitchiVeryIll1, } },

                        {"jump",                new string[] { GameResources.mimitchiJump0, } },

                        {"mad",                 new string[] { GameResources.mimitchiMad0, GameResources.mimitchiMad1, } },
                        {"very_mad",            new string[] { GameResources.mimitchiVeryMad0, GameResources.mimitchiVeryMad1, } },

                        {"no",                  new string[] { GameResources.mimitchiNo0, GameResources.mimitchiNo1, } },

                        {"before_poop",         new string[] { GameResources.mimitchiBeforePoop0, GameResources.mimitchiBeforePoop1, } },
                        {"poop",                new string[] { GameResources.mimitchiToiletPoop0, GameResources.mimitchiToiletPoop1, } },

                        {"sad1",                new string[] { GameResources.mimitchiV1Sad0, GameResources.mimitchiV1Sad1, } },
                        {"sad2",                new string[] { GameResources.mimitchiV2Sad0, GameResources.mimitchiV2Sad1, } },
                        {"sad3",                new string[] { GameResources.mimitchiV3Sad0, GameResources.mimitchiV3Sad1, } },


                        {"shy",                 new string[] { GameResources.mimitchiShy0, GameResources.mimitchiShy1, } },
                        {"side_walk",           new string[] { GameResources.mimitchiSideWalk0, GameResources.mimitchiSideWalk1, } },

                        {"sleep",               new string[] { GameResources.mimitchiSleep0, GameResources.mimitchiSleep1, } },

                        {"sorry",               new string[] { GameResources.mimitchiV1Sorry0, GameResources.mimitchiV2Sorry0, GameResources.mimitchiV2Sorry1 } },
                        {"special1",            new string[] { GameResources.mimitchiV1Special0, } },
                        {"special2",            new string[] { GameResources.mimitchiV2Special0, GameResources.mimitchiV2Special1 } },

                        {"tired",               new string[] { GameResources.mimitchiTired0, GameResources.mimitchiTired1, } },
                        {"yawning",             new string[] { GameResources.mimitchiYawning0, GameResources.mimitchiYawning1, GameResources.mimitchiYawning2, } },
                        {"what",                new string[] { GameResources.mimitchiWhat0, } },
                    }
                },

                {"Oyajitchi", new Dictionary<string, string[]>()
                    {
                        {"basic_anim_happy",    new string[] { GameResources.oyajitchiBasicAnimHappy0, GameResources.oyajitchiBasicAnimHappy1, GameResources.oyajitchiBasicAnimHappy2, GameResources.oyajitchiBasicAnimHappy3, GameResources.oyajitchiBasicAnimHappy4, } },
                        {"basic_anim_normal",   new string[] { GameResources.oyajitchiBasicAnimNormal0, GameResources.oyajitchiBasicAnimNormal1, GameResources.oyajitchiBasicAnimNormal2, GameResources.oyajitchiBasicAnimNormal3, GameResources.oyajitchiBasicAnimNormal4, } },
                        {"cry",                 new string[] { GameResources.oyajitchiCry0, GameResources.oyajitchiCry1, } },
                        {"death",               new string[] { GameResources.oyajitchiDeath0, GameResources.oyajitchiDeath1, } },
                        {"dissapointed",        new string[] { GameResources.oyajitchiDissapointed0, GameResources.oyajitchiDissapointed1, } },

                        {"after_eating",        new string[] { GameResources.oyajitchiAfterEating0, GameResources.oyajitchiAfterEating1, } },
                        {"during_eating",       new string[] { GameResources.oyajitchiDuringEating0, GameResources.oyajitchiDuringEating1, } },
                        {"full_eating",         new string[] { GameResources.oyajitchiFull0, } },

                        {"glad",                new string[] { GameResources.oyajitchiGlad0, GameResources.oyajitchiGlad1, } },
                        {"happy",               new string[] { GameResources.oyajitchiHappy0, GameResources.oyajitchiHappy1, } },

                        {"ill",                 new string[] { GameResources.oyajitchiIll0, GameResources.oyajitchiIll1, } },
                        {"very_ill",            new string[] { GameResources.oyajitchiVeryIll0, GameResources.oyajitchiVeryIll1, } },

                        {"jump",                new string[] { GameResources.oyajitchiJump0, } },

                        {"mad",                 new string[] { GameResources.oyajitchiMad0, GameResources.oyajitchiMad1, } },
                        {"very_mad",            new string[] { GameResources.oyajitchiVeryMad0, GameResources.oyajitchiVeryMad1, } },

                        {"no",                  new string[] { GameResources.oyajitchiNo0, GameResources.oyajitchiNo1, } },

                        {"before_poop",         new string[] { GameResources.oyajitchiBeforePoop0, GameResources.oyajitchiBeforePoop1, } },
                        {"poop",                new string[] { GameResources.oyajitchiToiletPoop0, GameResources.oyajitchiToiletPoop1, } },

                        {"sad1",                new string[] { GameResources.oyajitchiV1Sad0, GameResources.oyajitchiV1Sad1, } },
                        {"sad2",                new string[] { GameResources.oyajitchiV2Sad0, GameResources.oyajitchiV2Sad1, } },
                        {"sad3",                new string[] { GameResources.oyajitchiV3Sad0, GameResources.oyajitchiV3Sad1, } },


                        {"shy",                 new string[] { GameResources.oyajitchiShy0, GameResources.oyajitchiShy1, } },
                        {"side_walk",           new string[] { GameResources.oyajitchiSideWalk0, GameResources.oyajitchiSideWalk1, } },

                        {"sleep",               new string[] { GameResources.oyajitchiSleep0, GameResources.oyajitchiSleep1, } },

                        {"sorry",               new string[] { GameResources.oyajitchiV1Sorry0, GameResources.oyajitchiV2Sorry0, GameResources.oyajitchiV2Sorry1 } },
                        {"special1",            new string[] { GameResources.oyajitchiV1Special0, } },
                        {"special2",            new string[] { GameResources.oyajitchiV2Special0, GameResources.oyajitchiV2Special1 } },

                        {"tired",               new string[] { GameResources.oyajitchiTired0, GameResources.oyajitchiTired1, } },
                        {"yawning",             new string[] { GameResources.oyajitchiYawning0, GameResources.oyajitchiYawning1, GameResources.oyajitchiYawning2, } },
                        {"what",                new string[] { GameResources.oyajitchiWhat0, } },
                    }
                },

                {"Pochitchi", new Dictionary<string, string[]>()
                    {
                        {"basic_anim_happy",    new string[] { GameResources.pochitchiBasicAnimHappy0, GameResources.pochitchiBasicAnimHappy1, GameResources.pochitchiBasicAnimHappy2, GameResources.pochitchiBasicAnimHappy3, GameResources.pochitchiBasicAnimHappy4, } },
                        {"basic_anim_normal",   new string[] { GameResources.pochitchiBasicAnimNormal0, GameResources.pochitchiBasicAnimNormal1, GameResources.pochitchiBasicAnimNormal2, GameResources.pochitchiBasicAnimNormal3, GameResources.pochitchiBasicAnimNormal4, } },
                        {"cry",                 new string[] { GameResources.pochitchiCry0, GameResources.pochitchiCry1, } },
                        {"death",               new string[] { GameResources.pochitchiDeath0, GameResources.pochitchiDeath1, } },
                        {"dissapointed",        new string[] { GameResources.pochitchiDissapointed0, GameResources.pochitchiDissapointed1, } },

                        {"after_eating",        new string[] { GameResources.pochitchiAfterEating0, GameResources.pochitchiAfterEating1, } },
                        {"during_eating",       new string[] { GameResources.pochitchiDuringEating0, GameResources.pochitchiDuringEating1, } },
                        {"full_eating",         new string[] { GameResources.pochitchiFull0, } },

                        {"glad",                new string[] { GameResources.pochitchiGlad0, GameResources.pochitchiGlad1, } },
                        {"happy",               new string[] { GameResources.pochitchiHappy0, GameResources.pochitchiHappy1, } },

                        {"ill",                 new string[] { GameResources.pochitchiIll0, GameResources.pochitchiIll1, } },
                        {"very_ill",            new string[] { GameResources.pochitchiVeryIll0, GameResources.pochitchiVeryIll1, } },

                        {"jump",                new string[] { GameResources.pochitchiJump0, } },

                        {"mad",                 new string[] { GameResources.pochitchiMad0, GameResources.pochitchiMad1, } },
                        {"very_mad",            new string[] { GameResources.pochitchiVeryMad0, GameResources.pochitchiVeryMad1, } },

                        {"no",                  new string[] { GameResources.pochitchiNo0, GameResources.pochitchiNo1, } },

                        {"before_poop",         new string[] { GameResources.pochitchiBeforePoop0, GameResources.pochitchiBeforePoop1, } },
                        {"poop",                new string[] { GameResources.pochitchiToiletPoop0, GameResources.pochitchiToiletPoop1, } },

                        {"sad1",                new string[] { GameResources.pochitchiV1Sad0, GameResources.pochitchiV1Sad1, } },
                        {"sad2",                new string[] { GameResources.pochitchiV2Sad0, GameResources.pochitchiV2Sad1, } },
                        {"sad3",                new string[] { GameResources.pochitchiV3Sad0, GameResources.pochitchiV3Sad1, } },


                        {"shy",                 new string[] { GameResources.pochitchiShy0, GameResources.pochitchiShy1, } },
                        {"side_walk",           new string[] { GameResources.pochitchiSideWalk0, GameResources.pochitchiSideWalk1, } },

                        {"sleep",               new string[] { GameResources.pochitchiSleep0, GameResources.pochitchiSleep1, } },

                        {"sorry",               new string[] { GameResources.pochitchiV1Sorry0, GameResources.pochitchiV2Sorry0, GameResources.pochitchiV2Sorry1 } },
                        {"special1",            new string[] { GameResources.pochitchiV1Special0, } },
                        {"special2",            new string[] { GameResources.pochitchiV2Special0, GameResources.pochitchiV2Special1 } },

                        {"tired",               new string[] { GameResources.pochitchiTired0, GameResources.pochitchiTired1, } },
                        {"yawning",             new string[] { GameResources.pochitchiYawning0, GameResources.pochitchiYawning1, GameResources.pochitchiYawning2, } },
                        {"what",                new string[] { GameResources.pochitchiWhat0, } },
                    }
                },

                {"Zakutchi", new Dictionary<string, string[]>()
                    {
                        {"basic_anim_happy",    new string[] { GameResources.zakutchiBasicAnimHappy0, GameResources.zakutchiBasicAnimHappy1, GameResources.zakutchiBasicAnimHappy2, GameResources.zakutchiBasicAnimHappy3, GameResources.zakutchiBasicAnimHappy4, } },
                        {"basic_anim_normal",   new string[] { GameResources.zakutchiBasicAnimNormal0, GameResources.zakutchiBasicAnimNormal1, GameResources.zakutchiBasicAnimNormal2, GameResources.zakutchiBasicAnimNormal3, GameResources.zakutchiBasicAnimNormal4, } },
                        {"cry",                 new string[] { GameResources.zakutchiCry0, GameResources.zakutchiCry1, } },
                        {"death",               new string[] { GameResources.zakutchiDeath0, GameResources.zakutchiDeath1, } },
                        {"dissapointed",        new string[] { GameResources.zakutchiDissapointed0, GameResources.zakutchiDissapointed1, } },

                        {"after_eating",        new string[] { GameResources.zakutchiAfterEating0, GameResources.zakutchiAfterEating1, } },
                        {"during_eating",       new string[] { GameResources.zakutchiDuringEating0, GameResources.zakutchiDuringEating1, } },
                        {"full_eating",         new string[] { GameResources.zakutchiFull0, } },

                        {"glad",                new string[] { GameResources.zakutchiGlad0, GameResources.zakutchiGlad1, } },
                        {"happy",               new string[] { GameResources.zakutchiHappy0, GameResources.zakutchiHappy1, } },

                        {"ill",                 new string[] { GameResources.zakutchiIll0, GameResources.zakutchiIll1, } },
                        {"very_ill",            new string[] { GameResources.zakutchiVeryIll0, GameResources.zakutchiVeryIll1, } },

                        {"jump",                new string[] { GameResources.zakutchiJump0, } },

                        {"mad",                 new string[] { GameResources.zakutchiMad0, GameResources.zakutchiMad1, } },
                        {"very_mad",            new string[] { GameResources.zakutchiVeryMad0, GameResources.zakutchiVeryMad1, } },

                        {"no",                  new string[] { GameResources.zakutchiNo0, GameResources.zakutchiNo1, } },

                        {"before_poop",         new string[] { GameResources.zakutchiBeforePoop0, GameResources.zakutchiBeforePoop1, } },
                        {"poop",                new string[] { GameResources.zakutchiToiletPoop0, GameResources.zakutchiToiletPoop1, } },

                        {"sad1",                new string[] { GameResources.zakutchiV1Sad0, GameResources.zakutchiV1Sad1, } },
                        {"sad2",                new string[] { GameResources.zakutchiV2Sad0, GameResources.zakutchiV2Sad1, } },
                        {"sad3",                new string[] { GameResources.zakutchiV3Sad0, GameResources.zakutchiV3Sad1, } },


                        {"shy",                 new string[] { GameResources.zakutchiShy0, GameResources.zakutchiShy1, } },
                        {"side_walk",           new string[] { GameResources.zakutchiSideWalk0, GameResources.zakutchiSideWalk1, } },

                        {"sleep",               new string[] { GameResources.zakutchiSleep0, GameResources.zakutchiSleep1, } },

                        {"sorry",               new string[] { GameResources.zakutchiV1Sorry0, GameResources.zakutchiV2Sorry0, GameResources.zakutchiV2Sorry1 } },
                        {"special1",            new string[] { GameResources.zakutchiV1Special0, } },
                        {"special2",            new string[] { GameResources.zakutchiV2Special0, GameResources.zakutchiV2Special1 } },

                        {"tired",               new string[] { GameResources.zakutchiTired0, GameResources.zakutchiTired1, } },
                        {"yawning",             new string[] { GameResources.zakutchiYawning0, GameResources.zakutchiYawning1, GameResources.zakutchiYawning2, } },
                        {"what",                new string[] { GameResources.zakutchiWhat0, } },
                    }
                },
            },
        };

    }
}
