using SFML.Graphics;
using SFML.System;
using SFML.Window;
using TCEngine;

namespace TCGame
{
    public class LaserWeaponComponent : BaseComponent
    {
        private const float DEFAULT_FIRE_RATE = 0.3f;

        private float m_FireRate;
        private float m_TimeToShoot;

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
        }

        public void Shoot()
        {
            // TODO (3): Implement the creation of the laser bullet. Remember that the LaserWeaponComponent has a fire rate of 0.3 seconds
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
