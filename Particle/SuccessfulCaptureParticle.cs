using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ChickenFarm
{
    public class SuccessfulCaptureParticle : ParticleSystem
    {
        Color[] colors = new Color[]
        {
            Color.Red,
            Color.Blue,
            Color.Green,
        };

        Color color;
        public SuccessfulCaptureParticle(Game game, int maxNum) : base(game, maxNum * 10) { }

        protected override void InitializeConstants()
        {
            textureFilename = "CaptureParticle";
            minNumParticles = 10;
            maxNumParticles = 20;
            blendState = BlendState.Additive;
            DrawOrder = AdditiveBlendDrawOrder;
        }

        protected override void InitializeParticle(ref Particle p, Vector2 where)
        {
            var velocity = RandomHelper.NextDirection() * RandomHelper.NextFloat(40, 200);
            var lifetime = RandomHelper.NextFloat(0.5f, 1.0f);
            var acceleration = -velocity / lifetime;
            var rotation = RandomHelper.NextFloat(0, MathHelper.TwoPi);
            var angularVelocity = RandomHelper.NextFloat(-MathHelper.PiOver4, MathHelper.PiOver4);
            var scale = RandomHelper.NextFloat(4, 6);

            p.Initialize(where, velocity, lifetime: lifetime, rotation: rotation, angularVelocity: angularVelocity);
        }

        protected override void UpdateParticle(ref Particle particle, float dt)
        {
            base.UpdateParticle(ref particle, dt);
            float normalizeLifetime = particle.TimeSinceStart / particle.Lifetime;
            particle.Scale = .1f + .25f * normalizeLifetime;
        }

        public void ShowSucessfulCapture(Vector2 where)
        {
            color = colors[RandomHelper.Next(colors.Length)];
            AddParticles(where);
        }
    }
}
