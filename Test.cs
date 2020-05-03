using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bst
{
    class Test
    {
        public void OTest_rand()
        {
            var rand = new Random();

            var BT = new BT<int>();

            DateTime start = DateTime.Now;
            for (int ctr = 0; ctr <= 500000; ctr++)
                BT.Add(rand.Next());
            DateTime end = DateTime.Now;
            string change = Convert.ToString(end - start);
            Console.WriteLine(change);
        }
        public void OTest_bad()
        {
            var rand = new Random();
            int element = rand.Next();

            var BT = new BT<int>();

            DateTime start = DateTime.Now;
            for (int ctr = 0; ctr <= 500000; ctr++)
                BT.Add(element + rand.Next());
            DateTime end = DateTime.Now;
            string change = Convert.ToString(end - start);
            Console.WriteLine(change);
            //BT.PrintBT();
        }
    }
}
