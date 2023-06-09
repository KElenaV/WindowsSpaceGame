﻿using System.Numerics;

namespace SpaceGame
{
    public class Transform
    {
        public Vector2 Position { get; set; }

        public Transform()
        {
            Position = new Vector2(0,0);
        }

        public void Translate(Vector2 translation)
        {
            if (!float.IsNaN(translation.X) && !float.IsNaN(translation.Y))
                Position += translation;
        }
    }
}
