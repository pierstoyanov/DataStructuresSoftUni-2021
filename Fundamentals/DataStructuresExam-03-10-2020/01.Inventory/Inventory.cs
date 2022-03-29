namespace _01.Inventory
{
    using _01.Inventory.Interfaces;
    using _01.Inventory.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Inventory : IHolder
    {
        private List<int> weaponIdList = new List<int>();

        private Dictionary<int, IWeapon> weapons = new Dictionary<int, IWeapon>();

        private Dictionary<Category, List<int>> wCategorieLists = new Dictionary<Category, List<int>>();

        public int Capacity => this.weaponIdList.Count;

        public void Add(IWeapon weapon)
        {
            this.weapons.Add(weapon.Id, weapon);
            this.weaponIdList.Add(weapon.Id);
            if (!wCategorieLists.ContainsKey(weapon.Category))
            {
                wCategorieLists.Add(weapon.Category, new List<int>());
            }
            this.wCategorieLists[weapon.Category].Add(weapon.Id);
        }

        public void Clear()
        {
            this.weaponIdList = new List<int>();

            this.weapons = new Dictionary<int, IWeapon>();

            this.wCategorieLists = new Dictionary<Category, List<int>>();
        }

        public bool Contains(IWeapon weapon)
        {
            return this.CheckWeaponById(weapon.Id);
        }

        public void EmptyArsenal(Category category)
        {
            foreach (int Id in wCategorieLists[category])
            {
                this.weapons[Id].Ammunition = 0;
            }
        }

        public bool Fire(IWeapon weapon, int ammunition)
        {
            this.CheckWeaponExists(weapon);

            if (this.weapons[weapon.Id].Ammunition >= ammunition)
            {
                this.weapons[weapon.Id].Ammunition -= ammunition;

                return true;
            }
            return false;
        }

        public IWeapon GetById(int Id)
        {
            if (!CheckWeaponById(Id))
            {
                return null;
            }
            return this.weapons[Id];
        }

        public IEnumerator GetEnumerator()
        {
            foreach (int id in weaponIdList)
            {
                yield return this.weapons[id];
            }
        }

        public int Refill(IWeapon weapon, int ammunition)
        {
            this.CheckWeaponExists(weapon);

            var temp = this.weapons[weapon.Id];
            var newAmmo = temp.Ammunition + ammunition;
            if (!(newAmmo > temp.MaxCapacity))
            {
                this.weapons[weapon.Id].Ammunition += ammunition;

                return this.weapons[weapon.Id].Ammunition;
            }
            return this.weapons[weapon.Id].Ammunition;
        }

        public IWeapon RemoveById(int Id)
        {
            CheckWeaponById(Id);
            var temp = weapons[Id];

            weapons.Remove(Id);
            weaponIdList.Remove(Id);
            wCategorieLists[temp.Category].Remove(Id);

            return temp;
        }

        public int RemoveHeavy()
        {
            var heavy = this.wCategorieLists[Category.Heavy];
            var count = heavy.Count;
            while (heavy.Count > 0)
            {
                this.RemoveById(heavy[0]);
            }

            this.wCategorieLists[Category.Heavy] = new List<int>();

            return count;
        }

        public List<IWeapon> RetrieveAll()
        {
            var result = new List<IWeapon>();
            if (this.weapons.Count == 0)
            {
                return result;
            }

            for (int i = 0; i < this.weaponIdList.Count; i++)
            {
                result.Add(this.weapons[weaponIdList[i]]);
            }
            result.TrimExcess();
            return result;
        }

        public List<IWeapon> RetriveInRange(Category lower, Category upper)
        {
            var result = new List<IWeapon>();
            
            foreach (Category cat in Enum.GetValues(typeof(Category)))
            {
                if ((int)cat >= (int)lower && (int)cat <= (int)upper)
                {
                    foreach (var weapon in this.wCategorieLists[cat])
                    {
                        result.Add(this.weapons[weapon]);
                    }
                }
            }
            return result;
        }

        public void Swap(IWeapon firstWeapon, IWeapon secondWeapon)
        {
            CheckWeaponExists(firstWeapon);
            CheckWeaponExists(secondWeapon);

            if (firstWeapon.Category == secondWeapon.Category)
            {
                int firstIndex = weaponIdList.IndexOf(firstWeapon.Id);
                int secondIndex = weaponIdList.IndexOf(secondWeapon.Id);

                var temp = weaponIdList[secondIndex];

                weaponIdList[secondIndex] = weaponIdList[firstIndex];
                weaponIdList[firstIndex] = temp;
            }
        }

        private bool CheckWeaponById(int Id)
        {
            return weapons.ContainsKey(Id);
        }

        private void CheckWeaponExists(IWeapon wp)
        {
            if (!CheckWeaponById(wp.Id))
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

    }
}
