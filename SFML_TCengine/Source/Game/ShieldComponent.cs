using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Diagnostics;
using TCEngine;

namespace TCGame
{
    public class ShieldComponent : BaseComponent
    {

        private const float LOADING_TIME = 0.2f;
        private const float LOADED_TIME = 5.0f;
        private const float UNLOADING_TIME = 0.2f;

        private const float MIN_SCALE = 0.1f;
        private const float MAX_SCALE = 1.0f;

        private Actor m_ShieldActor;
        TransformComponent m_ShieldTransform;
        private Vector2f m_CurrentScale;

        private enum EState
        {
            Inactive,
            Loading,
            Loaded,
            Unloading
        }

        private EState m_CurrentState;
        private float m_CurrentStateTime = 0.0f;

        public ShieldComponent()
        {
        }

        public override void OnActorCreated()
        {
            base.OnActorCreated();
            m_CurrentState = EState.Inactive;
        }

        public void ActivateShield()
        {
            if (m_CurrentState == EState.Inactive)
            {
                ChangeState(EState.Loading);
            }
        }

        public bool IsActive()
        {
            return m_CurrentState != EState.Inactive;
        }



        public override void Update(float _dt)
        {
            base.Update(_dt);

            // TODO (1): You need to implement the code that update the different states of the ShieldComponent in the Update
            switch (m_CurrentState)
	        {
                   case EState.Loading:
                    UpdatingLoading();
                        break;
                   case EState.Loaded:
                        break;
                   case EState.Unloading:
                        break;
                   default:
                        break;
	        }
        }

        void UpdatingLoading()
        {
            //From MIN_SCALE to MAX_SCALE
            m_ShieldTransform.Transform.Scale = m_CurrentScale;

            // aqui tengo que hacer algo con el time que me tiene confuso como se hacia



            // Exit when MAX_SCALE has been reached
            if (true)
            {

            }
        }



        private void CreateShieldActor()
        {
            m_ShieldActor = new Actor("Shield Actor");

            m_ShieldActor.AddComponent<AnimatedSpriteComponent>("Textures/Shield", 3u, 2u);
            TransformComponent m_ShieldTransform = m_ShieldActor.AddComponent<TransformComponent>();
            m_ShieldActor.AddComponent<ParentActorComponent>(Owner, new Vector2f(0.0f, 20.0f));

            TecnoCampusEngine.Get.Scene.CreateActor(m_ShieldActor);
        }

        private void ChangeState(EState _newState)
        {
            OnLeaveState(m_CurrentState);
            OnEnterState(_newState);

            m_CurrentState = _newState;
        }

        private void OnEnterState(EState _nextState)
        {
            switch (_nextState)
            {
                case EState.Loading:
                    CreateShieldActor();
                    m_CurrentScale = new Vector2f(MIN_SCALE,MIN_SCALE);
                    break;
                default:
                    break;
            }
        }
        private void OnLeaveState(EState _previousState)
        {
            switch (_previousState)
            {
                case EState.Unloading:
                    m_ShieldActor.Destroy();
                    m_ShieldActor = null;
                    break;
                default:
                    break;
            }
        }

        public override EComponentUpdateCategory GetUpdateCategory()
        {
            return EComponentUpdateCategory.Update;
        }

        public override object Clone()
        {
            ShieldComponent clonedComponent = new ShieldComponent();
            return clonedComponent;
        }
    }
}
