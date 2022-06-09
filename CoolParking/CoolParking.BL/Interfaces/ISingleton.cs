using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolParking.BL.Interfaces
{
    public interface ISingleton<T> where T : new()
    {
        private static T _instance;

        public static T GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();
                }

                return _instance;
            }
        }
    }
}
