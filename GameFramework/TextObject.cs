using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFramework
{
    public class TextObject : SpriteObject
    {
        public enum TextAlignment { Manual, Near, Center, Far }

        SpriteFont _font;
        String _text;
        TextAlignment _horizontalAlignment = TextAlignment.Manual;
        TextAlignment _verticalAlignment = TextAlignment.Manual;

        public SpriteFont Font
        {
            get
            {
                return _font;
            }
            set
            {
                if (_font != value)
                {
                    _font = value;
                    CalculateAlignmentOrigin();
                }
            }
        }
        public String Text
        {
            get
            {
                return _text;
            }
            set
            {
                if (value != _text)
                {
                    _text = value;
                    CalculateAlignmentOrigin();
                }
            }
        }
        public TextAlignment HorizontalAlignment
        {
            get
            {
                return _horizontalAlignment;
            }
            set
            {
                if (_horizontalAlignment != value)
                {
                    _horizontalAlignment = value;
                    CalculateAlignmentOrigin();
                }
            }
        }
        public TextAlignment VerticalAlignment
        {
            get
            {
                return _verticalAlignment;
            }
            set
            {
                if (_verticalAlignment != value)
                {
                    _verticalAlignment = value;
                    CalculateAlignmentOrigin();
                }
            }
        }


        public TextObject(GameHost game)
            : base(game)
        {
            ScaleX = 1;
            ScaleY = 1;
            SpriteColor = Color.White;
        }

        public TextObject(GameHost game, SpriteFont font)
            : this(game)
        {
            Font = font;
        }

        public TextObject(GameHost game, SpriteFont font, Vector2 position)
            : this(game, font)
        {
            Position = position;
        }

        public TextObject(GameHost game, SpriteFont font, Vector2 position, String text)
            : this(game, font, position)
        {
            Text = text;
        }

        public TextObject(GameHost game, SpriteFont font, Vector2 position, String text, TextAlignment horizontalAlignment, TextAlignment verticalAlignment)
            : this(game, font, position, text)
        {
            HorizontalAlignment = horizontalAlignment;
            VerticalAlignment = verticalAlignment;
        }


        private void CalculateAlignmentOrigin()
        {
            if (HorizontalAlignment == TextAlignment.Manual && VerticalAlignment == TextAlignment.Manual)
                return;

            if (Font == null || Text == null || Text.Length == 0)
                return;

            Vector2 size = Font.MeasureString(Text);

            switch (HorizontalAlignment)
            {
                case TextAlignment.Near: OriginX = 0; break;
                case TextAlignment.Center: OriginX = size.X / 2; break;
                case TextAlignment.Far: OriginX = size.X; break;
            }
            switch (HorizontalAlignment)
            {
                case TextAlignment.Near: OriginY = 0; break;
                case TextAlignment.Center: OriginY = size.Y / 2; break;
                case TextAlignment.Far: OriginY = size.Y; break;
            }


        }

        public void Draw(GameTime time, SpriteBatch spriteBatch)
        {
            if (Font != null && Text != null && Text.Length > 0)
                spriteBatch.DrawString(Font, Text, Position, SpriteColor, Angle, Origin, Scale, SpriteEffects.None, LayerDepth);
        }

        public Rectangle BoundingBox
        {
            get
            {
                Rectangle result;
                Vector2 size;

                size = Font.MeasureString(Text);
                result = new Rectangle((int)PositionX, (int)PositionY,
                    (int)(size.X * ScaleX), (int)(size.Y * ScaleY));

                result.Offset((int)(-OriginX * ScaleX), (int)(-OriginY * ScaleY));

                return result;
            }
        }
    }
}
