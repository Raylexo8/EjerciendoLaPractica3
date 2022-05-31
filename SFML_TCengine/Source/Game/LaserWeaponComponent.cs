using SFML.Graphics;
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

        private float m_FireRate;
        private float m_TimeToShoot;
        private string m_BulletTextureName;

        public string BulletTextureName
        {
            get => m_BulletTextureName;
            set => m_BulletTextureName = value;
        }
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

                Actor laserActor = new Actor("LaserActor");
                SpriteComponent spriteComponent = laserActor.AddComponent<SpriteComponent>(m_BulletTextureName);
                spriteComponent.m_RenderLayer = RenderComponent.ERenderLayer.Middle;

                TransformComponent lasertransformComponent = laserActor.AddComponent<TransformComponent>();
                lasertransformComponent.Transform.Position = transformComponent.Transform.Position;
                lasertransformComponent.Transform.Rotation = transformComponent.Transform.Rotation;

                laserActor.AddComponent<ForwardMovementComponent>(Owner.GetComponent<ShipControllerComponent>().Forward, 700.0f);
                laserActor.AddComponent<AsteroidDestructorComponent>();
                laserActor.AddComponent<ExplosionComponent>();
                laserActor.AddComponent<OutOfWindowDestructionComponent>();

                TecnoCampusEngine.Get.Scene.CreateActor(laserActor);
                m_TimeToShoot = m_FireRate;
            }

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
