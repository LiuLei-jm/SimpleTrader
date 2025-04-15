using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;

namespace SimpleTrader.WPF.ViewModels
{
    public class StockIndexViewModel
    {
        private readonly IStockIndexService _stockIndexService;

        public StockIndexViewModel(IStockIndexService stockIndexService)
        {
            _stockIndexService = stockIndexService;
        }

        public StockIndex DJI { get; set; }
        public StockIndex IXIC { get; set; }
        public StockIndex GSPC { get; set; }

        public static StockIndexViewModel LoadMajorIndexViewModel(
            IStockIndexService stockIndexService
        )
        {
            var stockIndexViewModel = new StockIndexViewModel(stockIndexService);
            //stockIndexViewModel.LoadStockIndexes();
            return stockIndexViewModel;
        }

        private void LoadStockIndexes()
        {
            _stockIndexService
                .GetStockIndex("DJI")
                .ContinueWith(task =>
                {
                    if (task.Exception == null)
                    {
                        DJI = task.Result;
                    }
                });
            _stockIndexService
                .GetStockIndex("IXIC")
                .ContinueWith(task =>
                {
                    if (task.Exception == null)
                    {
                        IXIC = task.Result;
                    }
                });
            _stockIndexService
                .GetStockIndex("GSPC")
                .ContinueWith(task =>
                {
                    if (task.Exception == null)
                    {
                        GSPC = task.Result;
                    }
                });
        }
    }
}
