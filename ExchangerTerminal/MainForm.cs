using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonLogic.Commons;
using CommonLogic.ExternalInterfaces;
using TradeConnection;

namespace ExchangerTerminal
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            _terminal = new TradeTerminal(new List<ITradableConnection>(){new TradableConnection()});
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(StartT);
            

        }

        private void StartT()
        {
            _terminal.GetAssets("wm.exchanger.ru", null);

            var instr = _terminal.Instruments;

            foreach (var i in instr)
            {
                foreach (var name in i.Value)
                {
                    var level2 = _terminal.GetLevel2Async(i.Key, name, 1);
                }
            }


        }


        private ITradeTerminal _terminal;
    }
}
