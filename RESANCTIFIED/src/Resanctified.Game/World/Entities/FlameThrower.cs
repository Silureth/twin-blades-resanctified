using Resanctified.Engine.World;

namespace Resanctified.Game.World.Entities
{
    public class FlameThrower : Gun
    {
        public FlameThrower()
        {
            this.m_gunType = GunType.FlameThrower;
            this.IsAutofireAvailable = true;
            this.SetLevel(1);
        }

        public override void SetLevel(int level)
        {
            this.m_level = level;
            this.m_damage = 6 + (level - 1) * 2;
            this.m_manaConsumption = (int)(2.0 + (level - 1) * 0.800000011920929);
        }

        public override bool Fire()
        {
            PlayableCharacter hero = ((GameWorld)WorldManager.GetInstance().GetCurrentWorld()).GetHero();
            if (this.m_cooldown > 0.0)
                return false;
            this.m_cooldown = 0.1f;
            if (hero.ManaPoints - this.m_manaConsumption < 0)
            {
                hero.GetSound(PlayableCharacter.SoundFxId.SoundFx_Weapon_Empty).Play();
                this.OutOfMana = true;
                return false;
            }
            this.OutOfMana = false;
            hero.DoGunRaycast = true;
            hero.ManaPoints -= this.m_manaConsumption;
            if (!hero.GetGunFireView().IsVisible())
            {
                hero.GetGunFireView().SetAnimation2D(hero.GetAnimationStateMachine().GunFXAnimationArray[1]);
                hero.GetGunFireView().GetAnimation2D().SetTime(0.0f);
                hero.GetGunFireView().SetVisible(true);
                hero.GetSound(PlayableCharacter.SoundFxId.SoundFx_FlameThrower_Start).Play();
                hero.GetSound(PlayableCharacter.SoundFxId.SoundFx_FlameThrower).Play();
            }
            return true;
        }
    }
}
