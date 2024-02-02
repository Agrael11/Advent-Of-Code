namespace advent_of_code.Year2015.Day22
{
    internal class Gamestate
    {
        private readonly short PlayerHP;
        private ushort PlayerMana;
        private ushort PlayerArmor;
        
        private short BossHP;
        private readonly ushort BossDamage;

        private readonly List<(string, int)> MagicEffects;
        private readonly string EffectUsed;
        private readonly ushort Round;
        private readonly bool HardMode;

        public Gamestate(short playerHP, ushort playerMana, short bossHP, ushort bossDamage, bool hard)
        {
            HardMode = hard;
            PlayerHP = playerHP;
            PlayerMana = playerMana;
            PlayerArmor = 0;
            BossHP = bossHP;
            BossDamage = bossDamage;
            Round = 0;
            EffectUsed = "";
            MagicEffects = new List<(string, int)>();
        }

        public Gamestate(Gamestate state)
        {
            HardMode = state.HardMode;
            PlayerHP = state.PlayerHP;
            PlayerMana = state.PlayerMana;
            PlayerArmor = 0;
            BossHP = state.BossHP;
            BossDamage = state.BossDamage;
            EffectUsed = "<" + state.EffectUsed;
            Round = (ushort)(state.Round+1);
            MagicEffects = new List<(string, int)>(state.MagicEffects);

            DoEffects();
            short bossAttack = 0;
            if (BossHP > 0)
            {
                bossAttack = (short)Math.Max(BossDamage - PlayerArmor, 1);
            }
            PlayerHP -= bossAttack;
        }
        public Gamestate(Gamestate state, Magic effect)
        {
            HardMode = state.HardMode;
            PlayerHP = state.PlayerHP;
            PlayerMana = (ushort)(state.PlayerMana - effect.ManaCost);
            PlayerArmor = 0;
            BossHP = state.BossHP;
            BossDamage = state.BossDamage;
            Round = (ushort)(state.Round + 1);
            MagicEffects = new List<(string, int)>(state.MagicEffects);

            EffectUsed = effect.ItemName;

            if (HardMode) PlayerHP--;
            if (PlayerHP <= 0) return;

            DoEffects();
            if (effect.Instant)
            {
                BossHP -= effect.Damage;
                PlayerHP += effect.HPRecharge;
            }
            else
            {
                MagicEffects.Add((effect.ItemName, effect.Lasts));
            }
        }

        public bool? Wins()
        {
            if (PlayerHP <= 0) return false;
            if (BossHP<=0) return true;
            return null;
        }

        public bool CanDoMagic(string effect)
        {
            if (PlayerMana < MagicAvailable.MagicStore[effect].ManaCost) return false;
            if (MagicEffects.Any(e => e.Item1 == effect))
            {
                return MagicEffects.Where(e => e.Item1 == effect).First().Item2 == 1;
            }
            return true;
        }

        public void DoEffects()
        {
            for (var magicIndex = MagicEffects.Count-1; magicIndex >= 0; magicIndex--)
            {
                (var effectName, var length) = MagicEffects[magicIndex];
                length--;
                var effect = MagicAvailable.MagicStore[effectName];
                PlayerArmor += effect.Armor;
                BossHP -= effect.Damage;
                PlayerMana += effect.ManaRecharge;
                MagicEffects[magicIndex] = (effect.ItemName, length);
                if (length == 0)
                {
                    MagicEffects.RemoveAt(magicIndex);
                    PlayerArmor -= effect.Armor;
                }
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is Gamestate gamestate &&
                   PlayerHP == gamestate.PlayerHP &&
                   PlayerMana == gamestate.PlayerMana &&
                   PlayerArmor == gamestate.PlayerArmor &&
                   BossHP == gamestate.BossHP &&
                   BossDamage == gamestate.BossDamage &&
                   EqualityComparer<List<(string,int)>>.Default.Equals(MagicEffects, gamestate.MagicEffects);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PlayerHP, PlayerMana, PlayerArmor, BossHP, BossDamage, MagicEffects);
        }
    }
}
