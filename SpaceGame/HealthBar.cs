using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
    internal class HealthBar
    {
        Vector2 pos;
        const int Width = 50, Height = 20;
        const float criticalHealth = 0.2f;
        int fullHealth, currentHealth;
        float percent;

        public HealthBar(int fullHealth)
        {
            this.fullHealth = fullHealth;
        }

        public void UpdatePos(Vector2 pos)
        {
            this.pos = new Vector2(pos.X - (Width/2), pos.Y + Height + 5);
        }

        public void ChangeCurrentHealth(int currentHealth)
        {
            this.currentHealth = currentHealth;
            percent = currentHealth / fullHealth;
        }

        public void DrawHealthBar()
        {
            GlobalConstants.SpriteBatch.Draw(GlobalConstants.HealthBar, new Rectangle((int) pos.X, (int) pos.Y, Width, Height), Color.Gray);

            if(currentHealth <= fullHealth * criticalHealth)
            {
                GlobalConstants.SpriteBatch.Draw(GlobalConstants.HealthBar, new Rectangle((int)pos.X, (int)pos.Y, (int)(Width * percent), Height), Color.Red);
            }

            else
            {
                GlobalConstants.SpriteBatch.Draw(GlobalConstants.HealthBar, new Rectangle((int)pos.X, (int)pos.Y, (int)(Width * percent), Height), Color.Green);
            }
        }
    }
}
