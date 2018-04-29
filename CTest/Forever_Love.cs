using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTest
{
    public class Forever_Love
    {
        const double love = 2;
        double y, x, z;

        public void ML()
        {
            for (y = love; y > -love; y -= 0.06 * love)
            {
                for (x = -love; x < love; x += 0.03 * love)
                {
                    z = x * x + y * y - 0.6 * love;
                    Console.Write(z * z * z - x * x * y * y * y <= 0.0 ? '*' : ' ');
                }
                Console.WriteLine();
            }
        }


        #region ML
        //long Ago = 0L;
        //uint Forever = 2;
        //var YOU = new Forever_Love();
        //do
        //{
        //    YOU.ML();
        //    //Free imagination
        //    Ago++;
        //}
        //while (Ago < Forever);
        #endregion
    }
}
