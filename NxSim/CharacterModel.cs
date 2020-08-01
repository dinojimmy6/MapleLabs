using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Game1
{
    class CharacterFrame
    {
        public Dictionary<string, ComponentFrame> Frames = new Dictionary<string, ComponentFrame>();
        public Dictionary<string, Vector2> Pos = new Dictionary<string, Vector2>();

        public CharacterFrame(Animations animation, int frame)
        {
            LoadFrameData("arm", animation, frame);
            LoadFrameData("body", animation, frame);
            LoadFrameData("mail", animation, frame);
            LoadFrameData("mailArm", animation, frame);
            LoadFrameData("rHand", animation, frame);
            LoadFrameData("lHand", animation, frame);
            LoadFrameData("shoes", animation, frame);
            LoadFrameData("rGlove", animation, frame);
            LoadFrameData("lGlove", animation, frame);

            Frames.Add("head", XmlLoader.StaticSprites["front.head"]);
            Frames.Add("hairBelow", XmlLoader.StaticSprites["default.hairBelowBody"]);
            Frames.Add("hairAbove", XmlLoader.StaticSprites["default.hairOverHead"]);
            Frames.Add("face", XmlLoader.StaticSprites["default.face"]);
         
            if(Frames.ContainsKey("hairBelow"))
            {
                Pos.Add("hairBelow", new Vector2(100, 100) - (XmlLoader.StaticSprites["default.hairBelowBody"].origin + XmlLoader.StaticSprites["default.hairBelowBody"].brow - XmlLoader.StaticSprites["front.head"].brow + XmlLoader.StaticSprites["front.head"].neck - Frames["body"].neck));
            }
            if (Frames.ContainsKey("body"))
            {
                Pos.Add("body", new Vector2(100, 100) - Frames["body"].origin);
            }
            if (Frames.ContainsKey("rGlove"))
            {
                Pos.Add("rGlove", new Vector2(100, 100) - (Frames["rGlove"].origin + Frames["rGlove"].navel - Frames["body"].navel));
            }
            if (Frames.ContainsKey("lGlove"))
            {
                Pos.Add("lGlove", new Vector2(100, 100) - (Frames["lGlove"].origin + Frames["lGlove"].navel - Frames["body"].navel));
            }
            if (Frames.ContainsKey("arm"))
            {
                Pos.Add("arm", new Vector2(100, 100) - (Frames["arm"].origin + Frames["arm"].navel - Frames["body"].navel));
            }
            if (Frames.ContainsKey("head"))
            {
                Pos.Add("head", new Vector2(100, 100) - (XmlLoader.StaticSprites["front.head"].origin + XmlLoader.StaticSprites["front.head"].neck - Frames["body"].neck));
            }
            if (Frames.ContainsKey("hairAbove"))
            {
                Pos.Add("hairAbove", new Vector2(100, 100) - (XmlLoader.StaticSprites["default.hairOverHead"].origin + XmlLoader.StaticSprites["default.hairOverHead"].brow - XmlLoader.StaticSprites["front.head"].brow + XmlLoader.StaticSprites["front.head"].neck - Frames["body"].neck));
            }
            if (Frames.ContainsKey("face"))
            {
                Pos.Add("face", new Vector2(100, 100) - (XmlLoader.StaticSprites["default.face"].origin + XmlLoader.StaticSprites["default.face"].brow - XmlLoader.StaticSprites["front.head"].brow + XmlLoader.StaticSprites["front.head"].neck - Frames["body"].neck));
            }
            if (Frames.ContainsKey("mail"))
            {
                Pos.Add("mail", new Vector2(100, 100) - (Frames["mail"].origin + Frames["mail"].navel - Frames["body"].navel));
            }
            if (Frames.ContainsKey("mailArm"))
            {
                Pos.Add("mailArm", new Vector2(100, 100) - (Frames["mailArm"].origin + Frames["mailArm"].navel - Frames["mail"].navel + Frames["mail"].navel - Frames["body"].navel));
            }
            if (Frames.ContainsKey("rHand"))
            {
                Pos.Add("rHand", new Vector2(100, 100) - (Frames["rHand"].origin + Frames["rHand"].navel - Frames["body"].navel));
            }
            if (Frames.ContainsKey("lHand"))
            {
                Pos.Add("lHand", new Vector2(100, 100) - (Frames["lHand"].origin)); //+ LHand.handMove - Body.navel);
            }
            if (Frames.ContainsKey("shoes"))
            {
                Pos.Add("shoes", new Vector2(100, 100) - (Frames["shoes"].origin + Frames["shoes"].navel - Frames["body"].navel));
            }
        }

        private void LoadFrameData(string componentName, Animations animation, int frame)
        {
            var ret = new Dictionary<string, ComponentFrame>();
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
            var componentFrame = animationSet[frame];
            Frames.Add(componentName, componentFrame);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Draw(spriteBatch, "hairBelow");
            Draw(spriteBatch, "body");
            Draw(spriteBatch, "lGlove");
            Draw(spriteBatch, "mail");
            Draw(spriteBatch, "head");
            Draw(spriteBatch, "arm");
            Draw(spriteBatch, "rGlove");
            Draw(spriteBatch, "hairAbove");
            Draw(spriteBatch, "mailArm");
            Draw(spriteBatch, "face");
            Draw(spriteBatch, "rHand");
            Draw(spriteBatch, "lHand");
            Draw(spriteBatch, "shoes");
        }

        private string getFrameString(int frame, string pre, string post)
        {
            return pre + "." + frame + "." + post;
        }

        private void Draw(SpriteBatch spriteBatch, string componentName)
        {
            if(Frames.ContainsKey(componentName) && Pos.ContainsKey(componentName))
            {
                spriteBatch.Draw(Frames[componentName].sprite, Pos[componentName], Color.White);
            }
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
            LoadAnimation(Animations.Walk2);
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