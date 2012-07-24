using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameFramework;

namespace Invasion
{
    public class Shoot1 : SpriteObject
    {
        
        Game _game;
        float DesvioX;

        public Shoot1(Game game, Vector2 PosicaoInicial, Texture2D texture, float Desvio): base(game, PosicaoInicial, texture)
        {
            _game = game;
            DesvioX = Desvio;
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            PositionX += DesvioX;
            PositionY -= 10;
            base.Update(gameTime);
        }
    }
}