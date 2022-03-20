using System;
using System.Globalization;
using AppKit;
using Foundation;

namespace Craps
{
    public partial class ViewController : NSViewController

    {
        double bet = 0.00d;
        double balance = 100.00d;
        int point = 0;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Display opening balance.
            lblBalance.StringValue = string.Format("{0:C2}", balance);
        }

        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.
            }
        }

        partial void btnRoll(Foundation.NSObject sender)
        {
            // Ensure that txtEnterBet contains a valid entry (i.e., a whole number) and
            // that there are sufficient funds to cover the bet.
            //bool result = int.TryParse(txtEnterBet.StringValue, out bet);
            bool result = double.TryParse(txtEnterBet.StringValue, out bet);
            var bal = double.Parse(lblBalance.StringValue.Substring(1));
            if (result && bet < bal) // Valid bet.
                calculate_score();
            else if (bet > bal)
                lblResult.StringValue = "Insufficient funds.";
            else
                lblResult.StringValue = "Place bet, must be a number with no dollar sign."; // Invalid bet, not a number.
        } // end btnRoll

        private void calculate_score()
        {
            var random = new Random();
            var die1 = random.Next(6);
            var die2 = random.Next(6);
            // Add 1 for each die since the rolls range from 0 to 5.
            die1 += 1;
            die2 += 1;
            var sum = die1 + die2; 
            // Display die images.
            imgDie1.Image = NSImage.ImageNamed($"Die{die1}.png");
            imgDie2.Image = NSImage.ImageNamed($"Die{die2}.png");

            // 2, 3 or 12 on first roll loses
            if (sum == 2 || sum == 3 || sum == 12)
                if (point == 0)
                {
                    lblResult.StringValue = $"You rolled {sum} on first roll. You lose. Bet again.";
                    youLose();
                }
                else
                    lblResult.StringValue = $"You rolled {sum}, point is {point}. Roll again.";
            // These rolls will set point if first roll and must check if point was made on
            // subsequent rolls.
            else if ((sum > 3 && sum < 7) || (sum > 7 && sum < 11))
                if (point == sum)
                {
                    lblResult.StringValue = $"You rolled {sum}. Point is {point}. You win. Bet again.";
                    youWin();
                }
                else if (point == 0)
                {
                    point = sum;
                    lblResult.StringValue = $"Point is {point}. Roll again.";
                }
                else
                    lblResult.StringValue = $"You rolled {sum}, point is {point}. Roll again.";
            // 7 wins if it's the first roll, loses otherwise
            else if (sum == 7)
                if (point > 0)
                {
                    // Rolled 7 after first roll - lose.
                    lblResult.StringValue = $"You rolled {sum}, point is {point}. You lose. Bet again.";
                    youLose();
                }
                else
                {
                    // Rolled 7 on first roll (i.e., point = 0) - win.
                    lblResult.StringValue = $"You rolled {sum} on the first roll. You win. Bet again,";
                    youWin();
                }
            // 11 wins if it's the first roll, roll again on subsequent rolls - no need to check the
            // point because thenpoint will never be set to 11.
            else
                if (point == 0)
                {
                    lblResult.StringValue = $"You rolled {sum} on the first roll. You win. Bet again,";
                    youWin();
                }
                else
                    lblResult.StringValue = $"You rolled {sum}, point is {point}. Roll again.";
            } // end calculate_score

        private void youWin()
        {
            // Update balance.
            balance += bet;
            // Update balance display.
            lblBalance.StringValue = string.Format("{0:C2}", balance);
            // Reset bet.
            txtEnterBet.StringValue = "";
            // Reset point.
            point = 0;
        } // end youWin

        private void youLose()
        {
            // Update balance.
            balance -= bet;
            // Update balance display.
            lblBalance.StringValue = string.Format("{0:C2}", balance);
            // Reset bet.
            txtEnterBet.StringValue = "";
            // Reset point.
            point = 0;
        } // end youLose

    } // end ViewController

} // end Craps