using SFML.Graphics;
using System;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;
using System.Diagnostics;
using TCEngine;

namespace TCGame
{
    public class LaserWeaponComponent : BaseComponent
    {
        private const float DEFAULT_FIRE_RATE = 0.3f;
        private static Vector2f UP_VECTOR = new Vector2f(0.0f, -1.0f);

        float m_FireRate;
        float m_TimeToShoot;
        Vector2f m_MousePosition;
        Vector2f m_Forward;

        public float FireRate
        {
            get => m_FireRate;
            set
            {
                m_FireRate = value;
                m_TimeToShoot = m_FireRate;
            }
        }

        public LaserWeaponComponent()
        {
            m_FireRate = DEFAULT_FIRE_RATE;
            m_TimeToShoot = 0.0f;
        }

        public LaserWeaponComponent(float _fireRate)
        {
            m_FireRate = _fireRate;
            m_TimeToShoot = 0.0f;
        }

        public override void OnActorCreated()
        {
            base.OnActorCreated();
            TecnoCampusEngine.Get.Window.MouseMoved += HandleMouseMoved;
            TransformComponent laserTransformComponent = Owner.GetComponent<TransformComponent>();
        }

        public override void Update(float _dt)
        {
            base.Update(_dt);
            // TODO (3): Remember to update the m_TimeToShoot member
            m_TimeToShoot -= _dt;
        }

        public void Shoot()
        {
            // TODO (3): Implement the creation of the laser bullet. Remember that the LaserWeaponComponent has a fire rate of 0.3 seconds
            if (m_TimeToShoot <= 0.0f)
            {
                TransformComponent transformComponent = Owner.GetComponent<TransformComponent>();
                Debug.Assert(transformComponent != null);

                //Create Actor 
                Actor laserActor = new Actor("LaserActor");

                //Create Sprite
                SpriteComponent spriteComponent = laserActor.AddComponent<SpriteComponent>("Textures/Bullet");
                spriteComponent.m_RenderLayer = RenderComponent.ERenderLayer.Middle;

                //Set Position and Forward info
                TransformComponent laserTransformComponent = laserActor.AddComponent<TransformComponent>();
                laserTransformComponent.Transform.Position = transformComponent.Transform.Position;
                Vector2f objectToMouseOffset = m_MousePosition - laserTransformComponent.Transform.Position;
                m_Forward = objectToMouseOffset.Normal();

                //Set Rotation
                float angle = MathUtil.AngleWithSign(m_Forward, UP_VECTOR);
                laserTransformComponent.Transform.Rotation = angle;
                
                //Laser Weapon Components
                laserActor.AddComponent<ForwardMovementComponent>(m_Forward, 700.0f);
                laserActor.AddComponent<AsteroidDestructorComponent>();
                laserActor.AddComponent<ExplosionComponent>();
                laserActor.AddComponent<OutOfWindowDestructionComponent>();

                TecnoCampusEngine.Get.Scene.CreateActor(laserActor);
                m_TimeToShoot = m_FireRate;
            }
        }

        private void HandleMouseMoved(object _sender, MouseMoveEventArgs _moveEventArgs)
        {
            m_MousePosition.X = _moveEventArgs.X;
            m_MousePosition.Y = _moveEventArgs.Y;
        }

        public override EComponentUpdateCategory GetUpdateCategory()
        {
            return EComponentUpdateCategory.PostUpdate;
        }

        public override object Clone()
        {
            LaserWeaponComponent clonedComponent = new LaserWeaponComponent(m_FireRate);
            return clonedComponent;
        }
    }
}
