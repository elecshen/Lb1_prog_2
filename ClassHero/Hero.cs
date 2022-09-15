using System;

namespace ClassHero
{
    public class Hero
    {
        private readonly string name;
        private int health;
        private int score;
        private int bonuses;
        private int level;

        static readonly Random rand = new();

        public Hero(string name, int health, int score, int bonuses, int level)
        {
            this.name = name;
            this.health = health;
            this.score = score;
            this.bonuses = bonuses;
            this.level = level;
        }

        /// <summary>
        /// Пополнение жизней на заданное количество
        /// </summary>
        /// <param name="health">число добавляемых жизней</param>
        public void HealHero(int health)
        {
            this.health += health;
            if (this.health < 0)
                this.health = 0;
        }

        /// <summary>
        /// Переводит бонусы в счет
        /// </summary>
        /// <param name="bonuses">число бонусов, которое будет переведено в счет</param>
        /// <returns></returns>
        public bool TopUpScore(int bonuses)
        {
            if (this.bonuses >= bonuses)
            {
                this.bonuses -= bonuses;
                this.score += bonuses;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Увеличивает уровень на 1
        /// </summary>
        public void IncreaseLevel()
        {
            this.level += 1;
        }

        /// <summary>
        /// Проводит "бой" с другим героем. Шанс зависит от уровня героев. Проигравший теряет 1 жизнь и 50 баллов.
        /// </summary>
        /// <param name="opponent">Герой с которым будет проведён бой</param>
        /// <returns>В случае победы героя инициировавшего бой возвращает true, иначе false</returns>
        public bool FightWith(Hero opponent)
        {
            double chanceToWin = (double)level / (double)(level + opponent.level);
            if(rand.NextDouble() > chanceToWin)
            {
                health -= 1;
                score -= 50;
                return false;
            }
            else
            {
                opponent.health -= 1;
                opponent.score -= 50;
                return true;
            }
        }

        public string GetName()
        {
            return name;
        }

        public int GetHealth()
        {
            return health;
        }

        public int GetScore()
        {
            return score;
        }

        public int GetBonuses()
        {
            return bonuses;
        }

        public int GetLevel()
        {
            return level;
        }
    }
}
