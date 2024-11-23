using EasyTrader.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTrader.Core.Models
{
    public class Portfolio
    {
        public Portfolio(ICollection<ExchangeConfiguration> exchangeConfigurations)
        {
            ExchangeConfigurations = exchangeConfigurations; 
        }


        public ICollection<ExchangeConfiguration> ExchangeConfigurations { get; set; }

        public List<string> ExchangeNames
        {
            get
            {
                return ExchangeConfigurations.Select(x => x.ExchangeName).ToList();
            }
        }

        public ExchangeConfiguration? GetExchangeConfiguration(string exchangeName)
        {
            return ExchangeConfigurations.Where(ec => ec.ExchangeName == exchangeName).FirstOrDefault();
        }
    }
}
