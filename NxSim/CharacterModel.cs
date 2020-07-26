﻿using Microsoft.Xna.Framework;
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

        public CharacterFrame(Animations animation, int frame)
        {
            var Arm = Skeleton.Arm[animation][frame];
            var Body = Skeleton.Body[animation][frame];
            var Mail = Skeleton.Mail[animation][frame];
            var MailArm = Skeleton.MailArm[animation][frame];

            armSprite = Arm.sprite;
            bodySprite = Body.sprite;
            headSprite = XmlLoader.StaticSprites["front.head"].sprite;
            hairBelowSprite = XmlLoader.StaticSprites["default.hairBelowBody"].sprite;
            hairAboveSprite = XmlLoader.StaticSprites["default.hairOverHead"].sprite;
            faceSprite = XmlLoader.StaticSprites["default.face"].sprite;
            mailSprite = Mail.sprite;
            mailArmSprite = MailArm.sprite;


            hairBelowPos = new Vector2(100, 100) - (XmlLoader.StaticSprites["default.hairBelowBody"].origin + XmlLoader.StaticSprites["default.hairBelowBody"].brow - XmlLoader.StaticSprites["front.head"].brow + XmlLoader.StaticSprites["front.head"].neck - Body.neck);
            bodyPos = new Vector2(100, 100) - Body.origin;
            armPos = new Vector2(100, 100) - (Arm.origin + Arm.navel - Body.navel);
            headPos = new Vector2(100, 100) - (XmlLoader.StaticSprites["front.head"].origin + XmlLoader.StaticSprites["front.head"].neck - Body.neck);
            hairAbovePos = new Vector2(100, 100) - (XmlLoader.StaticSprites["default.hairOverHead"].origin + XmlLoader.StaticSprites["default.hairOverHead"].brow - XmlLoader.StaticSprites["front.head"].brow + XmlLoader.StaticSprites["front.head"].neck - Body.neck);
            facePos = new Vector2(100, 100) - (XmlLoader.StaticSprites["default.face"].origin + XmlLoader.StaticSprites["default.face"].brow - XmlLoader.StaticSprites["front.head"].brow + XmlLoader.StaticSprites["front.head"].neck - Body.neck);
            mailPos = new Vector2(100, 100) - (Mail.origin + Mail.navel - Body.navel);
            mailArmPos = new Vector2(100, 100) - (MailArm.origin + MailArm.navel - Mail.navel + Mail.navel - Body.navel);
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
        private Dictionary<Animations, List<CharacterFrame>> AnimationBooks = new Dictionary<Animations, List<CharacterFrame>>();
        public Animations currentAnimation = Animations.Walk;
        TimeSpan timeIntoAnimation;

        TimeSpan Duration
        {
            get
            {
                double totalSeconds = 0;
                foreach(var delay in Skeleton.Duration[currentAnimation])
                {
                    totalSeconds += delay;
                }

                return TimeSpan.FromMilliseconds(totalSeconds);
            }
        }

        public CharacterFrame CurrentFrame
        {
            get
            {
                CharacterFrame currentFrame = null;
                TimeSpan accumulatedTime = new TimeSpan();
                for(int i = 0; i < AnimationBooks[currentAnimation].Count; i++)
                {
                    var delay = TimeSpan.FromMilliseconds(Skeleton.Duration[currentAnimation][i]);
                    if (accumulatedTime + delay >= timeIntoAnimation)
                    {
                        currentFrame = AnimationBooks[currentAnimation][i];
                        break;
                    }
                    else
                    {
                        accumulatedTime += delay;
                    }
                }
                if (currentFrame == null)
                {
                    currentFrame = AnimationBooks[currentAnimation][0];
                }
                return currentFrame;
            }
        }

        public CharacterModel()
        {
            LoadAnimation(Animations.Idle);
            LoadAnimation(Animations.Walk);
            LoadAnimation(Animations.Alert);
        }

        public void LoadAnimation(Animations a)
        {
            List<CharacterFrame> AnimationBook = new List<CharacterFrame>();
            for(int i = 0; i < Skeleton.GetAnimationLength(a); i++)
            {
                AnimationBook.Add(new CharacterFrame(a, i));
            }
            AnimationBooks.Add(a, AnimationBook);
        }

        public void Update(GameTime gameTime)
        {
            double secondsIntoAnimation = timeIntoAnimation.TotalSeconds + gameTime.ElapsedGameTime.TotalSeconds;
            double remainder = secondsIntoAnimation % Duration.TotalSeconds;
            timeIntoAnimation = TimeSpan.FromSeconds(remainder);
        }
    }
}