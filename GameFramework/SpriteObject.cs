using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text;

namespace GameFramework
{
    public class SpriteObject : GameObjectBase
    {
        public Texture2D SpriteTexture { get; set; }
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public Vector2 Position
        {
            get
            {
                return new Vector2(PositionX, PositionY);
            }
            set
            {
                PositionX = value.X;
                PositionY = value.Y;
            }
        }
        public float OriginX { get; set; }
        public float OriginY { get; set; }
        public Vector2 Origin
        {
            get
            {
                return new Vector2(OriginX, OriginY);
            }
            set
            {
                OriginX = value.X;
                OriginY = value.Y;
            }
        }
        public float ScaleX { get; set; }
        public float ScaleY { get; set; }
        public Vector2 Scale
        {
            get
            {
                return new Vector2(ScaleX, ScaleY);
            }
            set
            {
                ScaleX = value.X;
                ScaleY = value.Y;
            }
        }
        public Rectangle SourceRect { get; set; }
        public Color SpriteColor { get; set; }
        public float LayerDepth { get; set; }
        public float Angle { get; set; }
        public Rectangle BoundingBox
        {
            get
            {
                Rectangle result;
                Vector2 spriteSize;

                if (SourceRect.IsEmpty)
                {
                    spriteSize = new Vector2(SpriteTexture.Width, SpriteTexture.Height);
                }
                else
                {
                    spriteSize = new Vector2(SourceRect.Width, SourceRect.Height);
                }

                result = new Rectangle((int)PositionX, (int)PositionY,
                                        (int)(spriteSize.X * ScaleX), (int)(spriteSize.Y * ScaleY));

                result.Offset((int)(-OriginX * ScaleX), (int)(-OriginY * ScaleY));
                return result;
            }
        }

        public SpriteObject(GameHost game)
            : base(game)
        {
            ScaleX = 1;
            ScaleY = 1;
            SpriteColor = Color.White;
        }

        public SpriteObject(GameHost game, Vector2 position)
            : this(game)
        {
            Position = position;
        }

        public SpriteObject(GameHost game, Vector2 position, Texture2D texture)
            : this(game, position)
        {
            SpriteTexture = texture;
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (spriteBatch != null)
            {
                if (SourceRect.IsEmpty)
                {
                    spriteBatch.Draw(SpriteTexture, Position, null, SpriteColor, Angle, Origin, Scale, SpriteEffects.None, LayerDepth);
                }
                else
                {
                    spriteBatch.Draw(SpriteTexture, Position, SourceRect, SpriteColor, Angle, Origin, Scale, SpriteEffects.None, LayerDepth);
                }
            }
        }
    }
}
