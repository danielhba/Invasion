using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace GameFramework
{
    public abstract class GameObjectBase
    {
        public int UpdateCount { get; set; }

        GameHost gameHost;

        public GameObjectBase(GameHost game)
        {
            gameHost = game;
        }

        public virtual void Update(GameTime gameTime)
        {
            UpdateCount += 1;
        }


    }
}
