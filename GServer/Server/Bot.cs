using Gopet.IO;
using Gopet.Server.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Server
{
    internal class Bot : Player
    {
        public Bot() : base(new NoSession())
        {

        }

        public override bool CanAddSpendGold => base.CanAddSpendGold;

        public override void addCoin(long coin)
        {
            
        }

        public override void addGold(long gold)
        {
             
        }

        public override void addStar(int star)
        {
             
        }

        public override bool checkCoin(long value)
        {
             return false;
        }

        public override bool checkGold(long value)
        {
            return false;
        }

        public override bool checkStar(int value)
        {
            return false;
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        

        public override void loginFailed(string text)
        {
             
        }

        public override void loginOK()
        {
             
        }

        public override void mineCoin(long coin)
        {
             
        }

        public override void mineGold(long gold)
        {
             
        }

        public override void notEnoughEnergy()
        {
             
        }

        public override void notEnoughHp()
        {
             
        }

        public override void okDialog(string str)
        {
             
        }

        public override void onDisconnected()
        {
             
        }

        public override void onMessage(Message ms)
        {
            
        }

        public override void Popup(string text)
        {
            
        }

        public override void redDialog(string text)
        {
             
        }

        public override void requestChangePass(int id, string oldPass, string newPass)
        {
            
        }

        public override void showBanner(string text)
        {
            
        }


        public override void update()
        {
             
        }
    }
}
