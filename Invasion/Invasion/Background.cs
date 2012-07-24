using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameFramework;

namespace Invasion
{
    public class Background : SpriteObject
    {
        Game _game;

        public Background(Game game, Vector2 PosicaoInicial, Texture2D texture)
            : base(game, PosicaoInicial, texture)
        {
            _game = game;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(SpriteTexture, Position, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            PositionY += 2;
            if (PositionY >= 800)
                PositionY = -800;
            base.Update(gameTime);
        }

    }
}

