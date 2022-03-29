namespace _02.LegionSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using _02.LegionSystem.Interfaces;

    public class Legion : IArmy
    {
        private SortedSet<int> enemySpeeds = new SortedSet<int>();

        private Dictionary<int, IEnemy> enemySpeedMap = new Dictionary<int, IEnemy>();

        public int Size => enemySpeeds.Count;

        public bool Contains(IEnemy enemy)
        {
           return enemySpeeds.Contains(enemy.AttackSpeed);
        }

        public void Create(IEnemy enemy)
        {
            if (!Contains(enemy))
            {
                enemySpeeds.Add(enemy.AttackSpeed);
                enemySpeedMap.Add(enemy.AttackSpeed, enemy);
            }
        }

        public IEnemy GetByAttackSpeed(int speed)
        {
            if (enemySpeedMap.ContainsKey(speed))
            {
                return enemySpeedMap[speed];
            }

            return null;
        }

        public List<IEnemy> GetFaster(int speed)
        {
            CheckNotEmpty();

            var result = new List<IEnemy>();

            foreach (var el in enemySpeeds.GetViewBetween(speed + 1, enemySpeeds.Max))
            {
                result.Add(enemySpeedMap[el]);
            }
            return result;
        }

        public IEnemy GetFastest()
        {
            CheckNotEmpty();
            return enemySpeedMap[enemySpeeds.Max()];
        }

        public IEnemy[] GetOrderedByHealth()
        {
            var res = new List<IEnemy>();
            foreach (var el in enemySpeeds.OrderBy(a => enemySpeedMap[a].Health).ToList())
            {
                res.Add(enemySpeedMap[el]);
            }
            return res.ToArray();
        }

        public List<IEnemy> GetSlower(int speed)
        {
            CheckNotEmpty();

            var result = new List<IEnemy>();

            foreach (var el in enemySpeeds.GetViewBetween(0, speed - 1))
            {
                result.Add(enemySpeedMap[el]);
            }
            return result;
        }

        public IEnemy GetSlowest()
        {
            CheckNotEmpty();
            return enemySpeedMap[enemySpeeds.Min()];
        }

        public void ShootFastest()
        {
            CheckNotEmpty();
            enemySpeeds.Remove(enemySpeeds.Max());
            enemySpeedMap.Remove(enemySpeeds.Max());
        }

        public void ShootSlowest()
        {
            CheckNotEmpty();
            enemySpeeds.Remove(enemySpeeds.Min());
            enemySpeedMap.Remove(enemySpeeds.Min());
        }
        private void CheckNotEmpty()
        {
            if (Size == 0)
            {
                throw new InvalidOperationException("Legion has no enemies!");
            }
        }
    }
}
