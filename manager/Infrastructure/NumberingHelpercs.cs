using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class NumberingHelpercs
    {
        private static readonly Object Locker = new Object();

        /*public static string GeneratePublicId<T>(string type) where T : class
        {
            lock (Locker)
            {
                string newPublicId;
                do
                {
                    var currentNumber = ManagerFramework.Current.Content.GetNextNumber(type);
                    newPublicId = string.Format("{0}{1:D" + currentNumber.MaxLength + "}", currentNumber.Prefix, currentNumber.Number);
                } while (ManagerFramework.Current.Content.GetItemByFieldName<T>("PublicId", newPublicId) != null);
                return newPublicId;
            }
        }*/

        public enum NumberingType
        {
            User,
            Player,
            Team
        }
    }
}
