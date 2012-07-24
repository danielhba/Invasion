using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameFramework;

namespace Invasion
{
    public class Shoot2 : SpriteObject
    {

        Game _game;
        Rectangle ShipRect;

        float DesvioX;
        int SpriteX;

        public Shoot2(Game game, Vector2 PosicaoInicial, Texture2D texture, float Desvio)
            : base(game, PosicaoInicial, texture)
        {
            _game = game;
            DesvioX = Desvio;
            SpriteX = 0;
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(SpriteTexture, Position, ShipRect, Color.White);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            PositionX += DesvioX;
            PositionY -= 10;

            SpriteX++;
            if (SpriteX > 10)
                SpriteX = 0;
            ShipRect = new Rectangle(SpriteX * 20, 0, 20, 20);

            base.Update(gameTime);
        }
    }
}