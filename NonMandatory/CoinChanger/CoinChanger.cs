using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinChangerApplication
{
    public class CoinChanger
    {
        private List<decimal> coinTypes;

        public List<decimal> CoinTypes
        {
            get {
                coinTypes.Sort();
                coinTypes.Reverse();
                return coinTypes;
                }
            set { coinTypes = value; }
        }

        public CoinChanger(List<decimal> coinTypes)
        {
            this.coinTypes = coinTypes;
        }

        public Dictionary<decimal, int> MakeChange(decimal amount)
        {
            var result = new Dictionary<decimal, int>();
            do
            {
                foreach (var cointype in CoinTypes)
                {
                    result[cointype] = 0;
                    do
                    {
                        result[cointype] = result[cointype] + 1;
                        amount -= cointype;
                        if (amount == 0)
                            break;
                    } while (amount >= cointype);
                    
                }
            } while (amount > 0);

            return result;
        }
    }
}
