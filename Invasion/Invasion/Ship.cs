using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameFramework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Invasion
{
    class Ship : SpriteObject
    {
        Game _game;
        public Ship(Game game, Vector2 PosicaoInicial, Texture2D texture) : base(game, PosicaoInicial, texture)
        {
            _game = game;
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}