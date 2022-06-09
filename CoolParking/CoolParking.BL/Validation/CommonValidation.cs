using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolParking.BL.Validation
{
    public static class CommonValidation
    {
        // validation for balance insertions or instantiations
        public static bool CheckBalancePush(decimal sum)
        {
            if (sum <= 0)
                return true;

            return false;
        }
    }
}
