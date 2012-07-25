using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameFramework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Invasion
{
    public class Exp : SpriteObject
    {

        Game _game;
        Rectangle ExpRect;

        int SpriteX;
        int SpriteY;

        int contador;
        public bool apagar;

        public Exp(Game game, Vector2 PosicaoInicial, Texture2D texture)
            : base(game, PosicaoInicial, texture)
        {
            _game = game;
            SpriteX = 0;
            SpriteY = 0;
            apagar = false;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(SpriteTexture, Position, ExpRect, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            contador = 0;
            SpriteX++;
            if (SpriteX > 4)
            {
                SpriteX = 0;
                SpriteY++;
                if (SpriteY > 3)
                {
                    apagar = true;
                }
            }

            ExpRect = new Rectangle(SpriteX * 70, SpriteY * 70, 70, 70);
            base.Update(gameTime);
        }
    }
}
