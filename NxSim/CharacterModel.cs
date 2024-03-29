﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace NxSim
{
    class CharacterFrame
    {
        public Dictionary<string, ComponentFrame> Frames = new Dictionary<string, ComponentFrame>();
        public Dictionary<string, Vector2> Pos = new Dictionary<string, Vector2>();

        public CharacterFrame(Animations animation, int frame)
        {
            LoadFrameData("effect", animation, frame);
            LoadFrameData("arm", animation, frame);
            LoadFrameData("body", animation, frame);
            LoadFrameData("mail", animation, frame);
            LoadFrameData("mailArm", animation, frame);
            LoadFrameData("rHand", animation, frame);
            LoadFrameData("lHand", animation, frame);
            LoadFrameData("shoes", animation, frame);
            LoadFrameData("rGlove", animation, frame);
            LoadFrameData("lGlove", animation, frame);
            LoadFrameData("weapon", animation, frame);
            LoadFrameData("head", animation, frame);
            LoadFrameData("hairOverHead", animation, frame);
            LoadFrameData("hairBelowBody", animation, frame);
            Frames.Add("face", XmlLoader.StaticSprites["default.face"]);

            if (Frames.ContainsKey("effect"))
            {
                Pos.Add("effect", new Vector2(0, 0) - (Frames["effect"].origin + Frames["effect"].navel - Frames["body"].navel));
                Pos.Add("effectF", new Vector2(0, 0) - (FlipX(Frames["effect"].origin) + new Vector2(Frames["effect"].spriteLoc.Width, 0) + FlipX(Frames["effect"].navel) - FlipX(Frames["body"].navel)));
            }
            if (Frames.ContainsKey("hairBelowBody"))
            {
                Pos.Add("hairBelowBody", new Vector2(0, 0) - (Frames["hairBelowBody"].origin + Frames["hairBelowBody"].brow - Frames["head"].brow + Frames["head"].neck - Frames["body"].neck));
                Pos.Add("hairBelowBodyF", new Vector2(0, 0) - (FlipX(Frames["hairBelowBody"].origin) + new Vector2(Frames["hairBelowBody"].spriteLoc.Width, 0) + FlipX(Frames["hairBelowBody"].brow) - FlipX(Frames["head"].brow) + FlipX(Frames["head"].neck) - FlipX(Frames["body"].neck)));
            }
            if (Frames.ContainsKey("body"))
            {
                Pos.Add("body", new Vector2(0, 0) - Frames["body"].origin);
                Pos.Add("bodyF", new Vector2(0, 0) - (FlipX(Frames["body"].origin) + new Vector2(Frames["body"].spriteLoc.Width, 0))); // width - origin
            }
            if (Frames.ContainsKey("rGlove"))
            {
                Pos.Add("rGlove", new Vector2(0, 0) - (Frames["rGlove"].origin + Frames["rGlove"].navel - Frames["body"].navel));
                Pos.Add("rGloveF", new Vector2(0, 0) - (FlipX(Frames["rGlove"].origin) + new Vector2(Frames["rGlove"].spriteLoc.Width, 0) + FlipX(Frames["rGlove"].navel) - FlipX(Frames["body"].navel)));
            }
            if (Frames.ContainsKey("lGlove"))
            {
                Pos.Add("lGlove", new Vector2(0, 0) - (Frames["lGlove"].origin + Frames["lGlove"].navel - Frames["body"].navel));
                Pos.Add("lGloveF", new Vector2(0, 0) - (FlipX(Frames["lGlove"].origin) + new Vector2(Frames["lGlove"].spriteLoc.Width, 0) + FlipX(Frames["lGlove"].navel) - FlipX(Frames["body"].navel)));
            }
            if (Frames.ContainsKey("arm"))
            {
                Pos.Add("arm", new Vector2(0, 0) - (Frames["arm"].origin + Frames["arm"].navel - Frames["body"].navel));
                Pos.Add("armF", new Vector2(0, 0) - (FlipX(Frames["arm"].origin) + new Vector2(Frames["arm"].spriteLoc.Width, 0) + FlipX(Frames["arm"].navel) - FlipX(Frames["body"].navel)));
            }
            if (Frames.ContainsKey("head"))
            {
                Pos.Add("head", new Vector2(0, 0) - (Frames["head"].origin + Frames["head"].neck - Frames["body"].neck));
                Pos.Add("headF", new Vector2(0, 0) - (FlipX(Frames["head"].origin) + new Vector2(Frames["head"].spriteLoc.Width, 0) + FlipX(Frames["head"].neck) - FlipX(Frames["body"].neck)));
            }
            if (Frames.ContainsKey("hairOverHead"))
            {
                Pos.Add("hairOverHead", new Vector2(0, 0) - (Frames["hairOverHead"].origin + Frames["hairOverHead"].brow - Frames["head"].brow + Frames["head"].neck - Frames["body"].neck));
                Pos.Add("hairOverHeadF", new Vector2(0, 0) - (FlipX(Frames["hairOverHead"].origin) + new Vector2(Frames["hairOverHead"].spriteLoc.Width, 0) + FlipX(Frames["hairOverHead"].brow) - FlipX(Frames["head"].brow) + FlipX(Frames["head"].neck) - FlipX(Frames["body"].neck)));
            }
            if (Frames.ContainsKey("face"))
            {
                Pos.Add("face", new Vector2(0, 0) - (Frames["face"].origin + Frames["face"].brow - Frames["head"].brow + Frames["head"].neck - Frames["body"].neck));
                Pos.Add("faceF", new Vector2(0, 0) - (FlipX(XmlLoader.StaticSprites["default.face"].origin) + new Vector2(XmlLoader.StaticSprites["default.face"].spriteLoc.Width, 0) + FlipX(XmlLoader.StaticSprites["default.face"].brow) - FlipX(Frames["head"].brow) + FlipX(Frames["head"].neck) - FlipX(Frames["body"].neck)));
            }
            if (Frames.ContainsKey("mail"))
            {
                Pos.Add("mail", new Vector2(0, 0) - (Frames["mail"].origin + Frames["mail"].navel - Frames["body"].navel));
                Pos.Add("mailF", new Vector2(0, 0) - (FlipX(Frames["mail"].origin) + new Vector2(Frames["mail"].spriteLoc.Width, 0) + FlipX(Frames["mail"].navel) - FlipX(Frames["body"].navel)));
            }
            if (Frames.ContainsKey("mailArm"))
            {
                Pos.Add("mailArm", new Vector2(0, 0) - (Frames["mailArm"].origin + Frames["mailArm"].navel - Frames["mail"].navel + Frames["mail"].navel - Frames["body"].navel));
                Pos.Add("mailArmF", new Vector2(0, 0) - (FlipX(Frames["mailArm"].origin) + new Vector2(Frames["mailArm"].spriteLoc.Width, 0) + FlipX(Frames["mailArm"].navel) - FlipX(Frames["mail"].navel) + FlipX(Frames["mail"].navel) - FlipX(Frames["body"].navel)));

            }
            if (Frames.ContainsKey("rHand"))
            {
                Pos.Add("rHand", new Vector2(0, 0) - (Frames["rHand"].origin + Frames["rHand"].navel - Frames["body"].navel));
                Pos.Add("rHandF", new Vector2(0, 0) - (FlipX(Frames["rHand"].origin) + new Vector2(Frames["rHand"].spriteLoc.Width, 0) + FlipX(Frames["rHand"].navel) - FlipX(Frames["body"].navel)));
            }
            if (Frames.ContainsKey("lHand"))
            {
                Pos.Add("lHand", new Vector2(0, 0) - Frames["lHand"].origin); //+ LHand.handMove - Body.navel);
                Pos.Add("lHandF", new Vector2(0, 0) - (FlipX(Frames["lHand"].origin) + new Vector2(Frames["lHand"].spriteLoc.Width, 0)));
            }
            if (Frames.ContainsKey("shoes"))
            {
                Pos.Add("shoes", new Vector2(0, 0) - (Frames["shoes"].origin + Frames["shoes"].navel - Frames["body"].navel));
                Pos.Add("shoesF", new Vector2(0, 0) - (FlipX(Frames["shoes"].origin) + new Vector2(Frames["shoes"].spriteLoc.Width, 0) + FlipX(Frames["shoes"].navel) - FlipX(Frames["body"].navel)));
            }
            if (Frames.ContainsKey("weapon"))
            {
                Pos.Add("weapon", new Vector2(0, 0) - (Frames["weapon"].origin + Frames["weapon"].navel - Frames["body"].navel));
                Pos.Add("weaponF", new Vector2(0, 0) - (FlipX(Frames["weapon"].origin) + new Vector2(Frames["weapon"].spriteLoc.Width, 0) + FlipX(Frames["weapon"].navel) - FlipX(Frames["body"].navel)));
            }
        }

        private void LoadFrameData(string componentName, Animations animation, int frame)
        {
            if(!Skeleton.Sk.ContainsKey(componentName))
            {
                return;
            }
            var component = Skeleton.Sk[componentName];
            if(!component.ContainsKey(animation))
            {
                return;
            }
            var animationSet = component[animation];
            if (animationSet.ContainsKey(frame))
            {
                var componentFrame = animationSet[frame];
                Frames.Add(componentName, componentFrame);
            }
        }

        public Vector2 FlipX(Vector2 pos)
        {
            return new Vector2(-pos.X, pos.Y);
        }

        public void Draw(SpriteBatch spriteBatch, Animations animation, Vector2 position, bool facingRight)
        {
            foreach(string layer in animation.Layer())
            {
                Draw(spriteBatch, layer, position, facingRight);
            }
        }

        public void DrawPart(SpriteBatch spriteBatch, Texture2D sprite, Vector2 pos, bool facingRight)
        {
            SpriteEffects flip = facingRight ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Rectangle destRect = new Rectangle(Convert.ToInt32(pos.X), Convert.ToInt32(pos.Y), sprite.Width, sprite.Height);
            spriteBatch.Draw(sprite, destRect, null, Color.White, 0f, new Vector2(0, 0), flip, 0f);
        }

        private string getFrameString(int frame, string pre, string post)
        {
            return pre + "." + frame + "." + post;
        }

        private void Draw(SpriteBatch spriteBatch, string componentName, Vector2 position, bool facingRight)
        {
            if(Frames.ContainsKey(componentName) && Pos.ContainsKey(componentName))
            {
                SpriteEffects flip = facingRight ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                Rectangle destRect = new Rectangle(Convert.ToInt32(Pos[facingRight ? componentName + "F" : componentName].X + position.X), Convert.ToInt32(Pos[facingRight ? componentName + "F" : componentName].Y + position.Y), (int) Frames[componentName].spriteLoc.Width, (int) Frames[componentName].spriteLoc.Height);
                Rectangle srcRect = new Rectangle((int)Frames[componentName].spriteLoc.X, (int)Frames[componentName].spriteLoc.Y, (int)Frames[componentName].spriteLoc.Width, (int)Frames[componentName].spriteLoc.Height);
                spriteBatch.Draw(Frames[componentName].Sprite, destRect, srcRect, Color.White, 0f, new Vector2(0, 0), flip, 0f);
            }
        }
    }

    class CharacterModel
    {
        private Dictionary<Animations, List<CharacterFrame>> AnimationBooks = new();
        public Animations currentAnimation = Animations.Walk;
        public TimeSpan timeIntoAnimation;

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
            LoadAnimation(Animations.Walk2);
            LoadAnimation(Animations.Alert);
            LoadAnimation(Animations.SwingO1);
            LoadAnimation(Animations.Duck);
            LoadAnimation(Animations.DuckStab);
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

        public void SetAnimation(Animations a)
        {
            this.currentAnimation = a;
        }
    }
}