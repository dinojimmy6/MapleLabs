using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Game1
{
    class CharacterFrame
    {
        public Vector2 armPos;
        public Vector2 bodyPos;
        public Vector2 rHandPos;
        public Vector2 lHandPos;
        public Vector2 headPos;
        public Vector2 hairBelowPos;
        public Vector2 hairAbovePos;
        public Vector2 facePos;
        public Vector2 mailPos;
        public Vector2 mailArmPos;
        public Vector2 shoesPos;

        public Vector2 armPosF;
        public Vector2 bodyPosF;
        public Vector2 rHandPosF;
        public Vector2 lHandPosF;
        public Vector2 headPosF;
        public Vector2 hairBelowPosF;
        public Vector2 hairAbovePosF;
        public Vector2 facePosF;
        public Vector2 mailPosF;
        public Vector2 mailArmPosF;
        public Vector2 shoesPosF;

        public Texture2D armSprite;
        public Texture2D bodySprite;
        public Texture2D rHandSprite;
        public Texture2D lHandSprite;
        public Texture2D headSprite;
        public Texture2D hairBelowSprite;
        public Texture2D hairAboveSprite;
        public Texture2D faceSprite;
        public Texture2D mailSprite;
        public Texture2D mailArmSprite;
        public Texture2D shoesSprite;

        public CharacterFrame(Animations animation, int frame)
        {
            var Arm = Skeleton.Sk["arm"][animation][frame];
            var Body = Skeleton.Sk["body"][animation][frame];
            var Mail = Skeleton.Sk["mail"][animation][frame];
            var MailArm = Skeleton.Sk["mailArm"][animation][frame];

            armSprite = Arm.sprite;
            bodySprite = Body.sprite;
            
            headSprite = XmlLoader.StaticSprites["front.head"].sprite;
            hairBelowSprite = XmlLoader.StaticSprites["default.hairBelowBody"].sprite;
            hairAboveSprite = XmlLoader.StaticSprites["default.hairOverHead"].sprite;
            faceSprite = XmlLoader.StaticSprites["default.face"].sprite;
            mailSprite = Mail.sprite;
            mailArmSprite = MailArm.sprite;


            hairBelowPos = new Vector2(0, 0) - (XmlLoader.StaticSprites["default.hairBelowBody"].origin + XmlLoader.StaticSprites["default.hairBelowBody"].brow - XmlLoader.StaticSprites["front.head"].brow + XmlLoader.StaticSprites["front.head"].neck - Body.neck);
            hairBelowPosF = new Vector2(0, 0) - (XmlLoader.StaticSprites["default.hairBelowBody"].origin + XmlLoader.StaticSprites["default.hairBelowBody"].brow - XmlLoader.StaticSprites["front.head"].brow + XmlLoader.StaticSprites["front.head"].neck - Body.neck);
            
            bodyPos = new Vector2(0, 0) - Body.origin;
            bodyPosF = new Vector2(0, 0) - (FlipX(Body.origin) + new Vector2(Body.sprite.Width, 0)); // width - origin

            armPos = new Vector2(0, 0) - (Arm.origin + Arm.navel - Body.navel);
            armPosF = new Vector2(0, 0) - (FlipX(Arm.origin) + new Vector2(Arm.sprite.Width, 0) + FlipX(Arm.navel) - FlipX(Body.navel));

            headPos = new Vector2(0, 0) - (XmlLoader.StaticSprites["front.head"].origin + XmlLoader.StaticSprites["front.head"].neck - Body.neck);
            headPosF = new Vector2(0, 0) - (XmlLoader.StaticSprites["front.head"].origin + FlipX(XmlLoader.StaticSprites["front.head"].neck) - FlipX(Body.neck));

            hairAbovePos = new Vector2(0, 0) - (XmlLoader.StaticSprites["default.hairOverHead"].origin + XmlLoader.StaticSprites["default.hairOverHead"].brow - XmlLoader.StaticSprites["front.head"].brow + XmlLoader.StaticSprites["front.head"].neck - Body.neck);
            hairAbovePosF = new Vector2(0, 0) - (XmlLoader.StaticSprites["default.hairOverHead"].origin + XmlLoader.StaticSprites["default.hairOverHead"].brow - XmlLoader.StaticSprites["front.head"].brow + XmlLoader.StaticSprites["front.head"].neck - Body.neck);

            facePos = new Vector2(0, 0) - (XmlLoader.StaticSprites["default.face"].origin + XmlLoader.StaticSprites["default.face"].brow - XmlLoader.StaticSprites["front.head"].brow + XmlLoader.StaticSprites["front.head"].neck - Body.neck);
            facePosF = new Vector2(0, 0) - (XmlLoader.StaticSprites["default.face"].origin + FlipX(XmlLoader.StaticSprites["default.face"].brow) - FlipX(XmlLoader.StaticSprites["front.head"].brow) + FlipX(XmlLoader.StaticSprites["front.head"].neck) - FlipX(Body.neck));

            mailPos = new Vector2(0, 0) - (Mail.origin + Mail.navel - Body.navel);
            mailPosF = new Vector2(0, 0) - (FlipX(Mail.origin) + new Vector2(Mail.sprite.Width, 0) + FlipX(Mail.navel) - FlipX(Body.navel));

            mailArmPos = new Vector2(0, 0) - (MailArm.origin + MailArm.navel - Mail.navel + Mail.navel - Body.navel);
            mailArmPosF = new Vector2(0, 0) - (FlipX(MailArm.origin) + new Vector2(MailArm.sprite.Width, 0) + FlipX(MailArm.navel) - FlipX(Mail.navel) + FlipX(Mail.navel) - FlipX(Body.navel));

            if (Skeleton.Sk["rHand"].ContainsKey(animation))
            {
                var RHand = Skeleton.Sk["rHand"][animation] == null ? null : Skeleton.Sk["rHand"][animation][frame];
                rHandSprite = RHand.sprite;
                rHandPos = new Vector2(0, 0) - (RHand.origin + RHand.navel - Body.navel);
                rHandPosF = new Vector2(0, 0) - (FlipX(RHand.origin) + new Vector2(RHand.sprite.Width, 0) + FlipX(RHand.navel) - FlipX(Body.navel));
            }
            if (Skeleton.Sk["lHand"].ContainsKey(animation))
            {
                var LHand = Skeleton.Sk["lHand"][animation] == null ? null : Skeleton.Sk["lHand"][animation][frame];
                lHandSprite = LHand.sprite;
                lHandPos = new Vector2(0, 0) - (FlipX(LHand.origin) + new Vector2(LHand.sprite.Width, 0)); //+ LHand.handMove - Body.navel);
            }
            if (Skeleton.Sk["shoes"].ContainsKey(animation))
            {
                var Shoes = Skeleton.Sk["shoes"][animation] == null ? null : Skeleton.Sk["shoes"][animation][frame];
                shoesSprite = Shoes.sprite;
                shoesPos = new Vector2(0, 0) - (Shoes.origin + Shoes.navel - Body.navel);
                shoesPosF = new Vector2(0, 0) - (FlipX(Shoes.origin) + new Vector2(Shoes.sprite.Width, 0) + FlipX(Shoes.navel) - FlipX(Body.navel));

            }

        }

        public Vector2 FlipX(Vector2 pos)
        {
            return new Vector2(-pos.X, pos.Y);
        }

        public void Draw(SpriteBatch spriteBatch, bool facingRight)
        {
            DrawPart(spriteBatch, hairBelowSprite, facingRight ? hairBelowPosF : hairBelowPos, facingRight);
            DrawPart(spriteBatch, bodySprite, facingRight ? bodyPosF : bodyPos, facingRight);
            DrawPart(spriteBatch, mailSprite, facingRight ? mailPosF : mailPos, facingRight);
            DrawPart(spriteBatch, headSprite, facingRight ? headPosF : headPos, facingRight);
            DrawPart(spriteBatch, armSprite, facingRight ? armPosF : armPos, facingRight);
            Debug.WriteLine(armPosF);
            DrawPart(spriteBatch, hairAboveSprite, facingRight ? hairAbovePosF : hairAbovePos, facingRight);
            DrawPart(spriteBatch, mailArmSprite, facingRight ? mailArmPosF : mailArmPos, facingRight);
            DrawPart(spriteBatch, faceSprite, facingRight ? facePosF : facePos, facingRight);
            if (rHandSprite != null)
            {
                DrawPart(spriteBatch, rHandSprite, facingRight ? rHandPosF : rHandPos, facingRight);
            }
            if (lHandSprite != null)
            {
                DrawPart(spriteBatch, lHandSprite, lHandPos, facingRight);
            }
            if (shoesSprite != null)
            {
                DrawPart(spriteBatch, shoesSprite, facingRight ? shoesPosF : shoesPos, facingRight);
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

        public void SetAnimation(Animations a)
        {
            this.currentAnimation = a;
        }
    }
}