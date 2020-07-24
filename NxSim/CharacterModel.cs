using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Game1
{
    class CharacterFrame
    {
        public Vector2 armPos;
        public Vector2 bodyPos;
        public Vector2 headPos;
        public Vector2 hairBelowPos;
        public Vector2 hairAbovePos;
        public Vector2 facePos;
        public Vector2 mailPos;
        public Vector2 mailArmPos;

        public Texture2D armSprite;
        public Texture2D bodySprite;
        public Texture2D headSprite;
        public Texture2D hairBelowSprite;
        public Texture2D hairAboveSprite;
        public Texture2D faceSprite;
        public Texture2D mailSprite;
        public Texture2D mailArmSprite;

        public CharacterFrame(Dictionary<string, ComponentFrame> sprites, string animation, int frame)
        {
            string bodyFrame = getFrameString(frame, animation, "body");
            string armFrame = getFrameString(frame, animation, "arm");
            string mailFrame = getFrameString(frame, animation, "mail");
            string mailArmFrame = getFrameString(frame, animation, "mailArm");

            armSprite = XmlLoader.sprites[armFrame].sprite;
            bodySprite = XmlLoader.sprites[bodyFrame].sprite;
            headSprite = XmlLoader.sprites["front.head"].sprite;
            hairBelowSprite = XmlLoader.sprites["default.hairBelowBody"].sprite;
            hairAboveSprite = XmlLoader.sprites["default.hairOverHead"].sprite;
            faceSprite = XmlLoader.sprites["default.face"].sprite;
            mailSprite = XmlLoader.sprites[mailFrame].sprite;
            mailArmSprite = XmlLoader.sprites[mailArmFrame].sprite;


            hairBelowPos = new Vector2(100, 100) - (sprites["default.hairBelowBody"].origin + sprites["default.hairBelowBody"].brow - sprites["front.head"].brow + sprites["front.head"].neck - sprites[bodyFrame].neck);
            bodyPos = new Vector2(100, 100) - sprites[bodyFrame].origin;
            armPos = new Vector2(100, 100) - (sprites[armFrame].origin + sprites[armFrame].navel - sprites[bodyFrame].navel);
            headPos = new Vector2(100, 100) - (sprites["front.head"].origin + sprites["front.head"].neck - sprites[bodyFrame].neck);
            hairAbovePos = new Vector2(100, 100) - (sprites["default.hairOverHead"].origin + sprites["default.hairOverHead"].brow - sprites["front.head"].brow + sprites["front.head"].neck - sprites[bodyFrame].neck);
            facePos = new Vector2(100, 100) - (sprites["default.face"].origin + sprites["default.face"].brow - sprites["front.head"].brow + sprites["front.head"].neck - sprites[bodyFrame].neck);
            mailPos = new Vector2(100, 100) - (sprites[mailFrame].origin + sprites[mailFrame].navel - sprites[bodyFrame].navel);
            mailArmPos = new Vector2(100, 100) - (sprites[mailArmFrame].origin + sprites[mailArmFrame].navel - sprites[mailFrame].navel + sprites[mailFrame].navel - sprites[bodyFrame].navel);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(hairBelowSprite, hairBelowPos, Color.White);
            spriteBatch.Draw(bodySprite, bodyPos, Color.White);
            spriteBatch.Draw(mailSprite, mailPos, Color.White);
            spriteBatch.Draw(headSprite, headPos, Color.White);
            spriteBatch.Draw(armSprite, armPos, Color.White);
            spriteBatch.Draw(mailArmSprite, mailArmPos, Color.White);
            spriteBatch.Draw(hairAboveSprite, hairAbovePos, Color.White);
            spriteBatch.Draw(faceSprite, facePos, Color.White);
        }

        private string getFrameString(int frame, string pre, string post)
        {
            return pre + "." + frame + "." + post;
        }
    }

    class CharacterModel
    {
        private List<CharacterFrame> walk1 = new List<CharacterFrame>();
        TimeSpan timeIntoAnimation;

        TimeSpan Duration
        {
            get
            {
                double totalSeconds = 0;
                foreach(var frame in walk1)
                {
                    totalSeconds += .2;
                }

                return TimeSpan.FromSeconds(totalSeconds);
            }
        }

        public CharacterFrame CurrentFrame
        {
            get
            {
                CharacterFrame currentFrame = null;
                TimeSpan accumulatedTime = new TimeSpan();
                foreach (var frame in walk1)
                {
                    if (accumulatedTime + TimeSpan.FromSeconds(.2) >= timeIntoAnimation)
                    {
                        currentFrame = frame;
                        break;
                    }
                    else
                    {
                        accumulatedTime += TimeSpan.FromSeconds(.2);
                    }
                }
                if (currentFrame == null)
                {
                    currentFrame = walk1[0];
                }
                return currentFrame;
            }
        }

        public CharacterModel(Dictionary<string, ComponentFrame> sprites)
        {
            LoadAnimation(sprites, "walk1");
        }

        public void LoadAnimation(Dictionary<string, ComponentFrame> sprites, string animation)
        {
            for(int i = 0; i < 4; i++)
            {
                walk1.Add(new CharacterFrame(sprites, animation, i));
            }
        }

        public void Update(GameTime gameTime)
        {
            double secondsIntoAnimation = timeIntoAnimation.TotalSeconds + gameTime.ElapsedGameTime.TotalSeconds;
            double remainder = secondsIntoAnimation % Duration.TotalSeconds;
            timeIntoAnimation = TimeSpan.FromSeconds(remainder);
        }
    }
}
