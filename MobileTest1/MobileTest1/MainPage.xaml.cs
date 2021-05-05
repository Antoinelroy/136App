using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileTest1
{
    public partial class MainPage : ContentPage
    {
        //Booleans for selected dices
        bool isSelected1 = false;
        bool isSelected2 = false;
        bool isSelected3 = false;

        //Boolean to flag triples or straights
        bool flag = false;

        //random numbers for each dice
        int randomNumber1;
        int randomNumber2;
        int randomNumber3;

        //fixed numbers for best hand comparison
        int bestHandNum1 = 0;
        int bestHandNum2 = 0; 
        int bestHandNum3 = 0;

        //Selecting images/dices functions
        private void Image1_clicked(object sender, EventArgs e)
        {
            isSelected1 = !isSelected1;
            img1.Scale = isSelected1 ? 5 : 4;
        }
        private void Image2_clicked(object sender, EventArgs e)
        {
            isSelected2 = !isSelected2;
            img2.Scale = isSelected2 ? 5 : 4;
        }
        private void Image3_clicked(object sender, EventArgs e)
        {
            isSelected3 = !isSelected3;
            img3.Scale = isSelected3 ? 5 : 4;
        }

        void resetSelected()
        {
            if (isSelected1 == true)
            {
                isSelected1 = false;
                img1.Scale = 4;
            }
            if (isSelected2 == true)
            {
                isSelected2 = false;
                img2.Scale = 4;
            }
            if (isSelected3 == true)
            {
                isSelected3 = false;
                img3.Scale = 4;
            }
        }

        public class RollsLeft : INotifyPropertyChanged
        {
            private int _rolls = 2;
            public int Rolls
            {
                get => _rolls;
                set
                {
                    if (_rolls == value)
                    {
                        return;
                    }
                    else
                    {
                        _rolls = value;
                        OnPropertyChanged(nameof(_rolls));
                    }
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            void OnPropertyChanged(string name)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        void Handle_Clicked(object sender, EventArgs e)
        {
            var random = new Random(); //instance of random class for random numbers 1 to 3

            nextPlayer.IsVisible = false; //hide the next player text

            rollsLeft.Rolls = rollsLeft.Rolls - 1; // could be rollsLeft.Rolls--?
            rollslefttext.Text = Convert.ToString(rollsLeft.Rolls); // display number of rolls left

            //next player when rolls = 0
            if (rollsLeft.Rolls == 0)
            {
                nextPlayer.IsVisible = true;
                rollsLeft.Rolls = 2; //put rolls back to 2
                rollslefttext.Text = Convert.ToString(rollsLeft.Rolls);
                //resetSelected(); //glitchy function
            }

            //Only roll dices that are not selected
            if (isSelected1 == false)
            {
                randomNumber1 = random.Next(1, 7);
                img1.Source = "https://136gameimages.s3.us-east-2.amazonaws.com/images/dice" + randomNumber1 + ".png";
            }

            if (isSelected2 == false)
            {
                randomNumber2 = random.Next(1, 7);
                img2.Source = "https://136gameimages.s3.us-east-2.amazonaws.com/images/dice" + randomNumber2 + ".png";
            }

            if (isSelected3 == false)
            {
                randomNumber3 = random.Next(1, 7);
                img3.Source = "https://136gameimages.s3.us-east-2.amazonaws.com/images/dice" + randomNumber3 + ".png";
            }

            //Checking for 136
            if ((randomNumber1 == 1 && randomNumber2 == 3 && randomNumber3 == 6) ||
                (randomNumber1 == 1 && randomNumber2 == 6 && randomNumber3 == 3) ||
                (randomNumber1 == 3 && randomNumber2 == 1 && randomNumber3 == 6) ||
                (randomNumber1 == 3 && randomNumber2 == 6 && randomNumber3 == 1) ||
                (randomNumber1 == 6 && randomNumber2 == 3 && randomNumber3 == 1) ||
                (randomNumber1 == 6 && randomNumber2 == 1 && randomNumber3 == 3))
            {
                onethreesix.IsVisible = true; //Make the !!!!136!!!! text visible
                nextPlayer.IsVisible = true;
                rollsLeft.Rolls = 2;
                rollslefttext.Text = Convert.ToString(rollsLeft.Rolls);
                resetBestHand();
                flag = false;
            }
            else
            {
                onethreesix.IsVisible = false;
            }

            //Best hand if/else if statements. Thats a lot of lines of code... wondering if I could do it differently
            //As of now 1 1 6 is better than 2 2 4.. have to fix this.

            //pair of 1:
            if ((randomNumber1 == 1 && randomNumber2 == 1 && randomNumber3 != 1) ||
                (randomNumber2 == 1 && randomNumber3 == 1 && randomNumber1 != 1) ||
                (randomNumber1 == 1 && randomNumber3 == 1 && randomNumber2 != 1))
            {
                if (randomNumber1 != 1 && flag == false && randomNumber2 >= bestHandNum2)
                {
                    bestHandNum1 = 1;
                    bestHandNum2 = 1;
                    if (randomNumber1 >= bestHandNum3)
                    {
                        bestHandNum3 = randomNumber1;
                    }
                } 
                else if (randomNumber2 != 1 && flag == false && randomNumber1 >= bestHandNum2)
                {
                    bestHandNum1 = 1;
                    bestHandNum2 = 1;
                    if (randomNumber2 >= bestHandNum3)
                    {
                        bestHandNum3 = randomNumber2;
                    }
                }
                else if (randomNumber3 != 1 && flag == false && randomNumber2 >= bestHandNum2)
                {
                    bestHandNum1 = 1;
                    bestHandNum2 = 1;
                    if (randomNumber3 >= bestHandNum3)
                    {
                        bestHandNum3 = randomNumber3;
                    }
                }
            }
            //pair of 2
            else if ((randomNumber1 == 2 && randomNumber2 == 2 && randomNumber3 != 2) ||
                (randomNumber2 == 2 && randomNumber3 == 2 && randomNumber1 != 2) ||
                (randomNumber1 == 2 && randomNumber3 == 2 && randomNumber2 != 2))
            {
                if (randomNumber1 != 2 && flag == false && randomNumber2 >= bestHandNum2)
                {
                    bestHandNum1 = 2;
                    bestHandNum2 = 2;
                    if(randomNumber1 >= bestHandNum3)
                    {
                        bestHandNum3 = randomNumber1;
                    }
                }
                else if (randomNumber2 != 2 && flag == false && randomNumber1 >= bestHandNum2)
                {
                    bestHandNum1 = 2;
                    bestHandNum2 = 2;
                    if (randomNumber2 >= bestHandNum3)
                    {
                        bestHandNum3 = randomNumber2;
                    }
                    
                }
                else if (randomNumber3 != 2 && flag == false && randomNumber2 >= bestHandNum2)
                {
                    bestHandNum1 = 2;
                    bestHandNum2 = 2;
                    if (randomNumber3 >= bestHandNum3)
                    {
                        bestHandNum3 = randomNumber3;
                    }
                    
                }
            }
            //pair of 3
            else if ((randomNumber1 == 3 && randomNumber2 == 3 && randomNumber3 != 3) ||
               (randomNumber2 == 3 && randomNumber3 == 3 && randomNumber1 != 3) ||
               (randomNumber1 == 3 && randomNumber3 == 3 && randomNumber2 != 3))
            {
                if (randomNumber1 != 3 && flag == false && randomNumber2 >= bestHandNum2)
                {
                    bestHandNum1 = 3;
                    bestHandNum2 = 3;
                    if (randomNumber1 >= bestHandNum3)
                    {
                        bestHandNum3 = randomNumber1;
                    }
                }
                else if (randomNumber2 != 3 && flag == false && randomNumber1 >= bestHandNum2)
                {
                    bestHandNum1 = 3;
                    bestHandNum2 = 3;
                    if (randomNumber2 >= bestHandNum3)
                    {
                        bestHandNum3 = randomNumber2;
                    }
                }
                else if (randomNumber3 != 3 && flag == false && randomNumber2 >= bestHandNum2)
                {
                    bestHandNum1 = 3;
                    bestHandNum2 = 3;
                    if (randomNumber3 >= bestHandNum3)
                    {
                        bestHandNum3 = randomNumber3;
                    }
                }
            }
            //pair of 4
            else if ((randomNumber1 == 4 && randomNumber2 == 4 && randomNumber3 != 4) ||
               (randomNumber2 == 4 && randomNumber3 == 4 && randomNumber1 != 4) ||
               (randomNumber1 == 4 && randomNumber3 == 4 && randomNumber2 != 4))
            {
                if (randomNumber1 != 4 && flag == false && randomNumber2 >= bestHandNum2)
                {
                    bestHandNum1 = 4;
                    bestHandNum2 = 4;
                    if (randomNumber1 >= bestHandNum3)
                    {
                        bestHandNum3 = randomNumber1;
                    }
                }
                else if (randomNumber2 != 4 && flag == false && randomNumber1 >= bestHandNum2)
                {
                    bestHandNum1 = 4;
                    bestHandNum2 = 4;
                    if (randomNumber2 >= bestHandNum3)
                    {
                        bestHandNum3 = randomNumber2;
                    }
                }
                else if (randomNumber3 != 4 && flag == false && randomNumber2 >= bestHandNum2)
                {
                    bestHandNum1 = 4;
                    bestHandNum2 = 4;
                    if (randomNumber3 >= bestHandNum3)
                    {
                        bestHandNum3 = randomNumber3;
                    }
                }
            }
            //pair of 5
            else if ((randomNumber1 == 5 && randomNumber2 == 5 && randomNumber3 != 5) ||
               (randomNumber2 == 5 && randomNumber3 == 5 && randomNumber1 != 5) ||
               (randomNumber1 == 5 && randomNumber3 == 5 && randomNumber2 != 5))
            {
                if (randomNumber1 != 5 && flag == false && randomNumber2 >= bestHandNum2)
                {
                    bestHandNum1 = 5;
                    bestHandNum2 = 5;
                    if (randomNumber1 >= bestHandNum3)
                    {
                        bestHandNum3 = randomNumber1;
                    }
                }
                else if (randomNumber2 != 5 && flag == false && randomNumber1 >= bestHandNum2)
                {
                    bestHandNum1 = 5;
                    bestHandNum2 = 5;
                    if (randomNumber2 >= bestHandNum3)
                    {
                        bestHandNum3 = randomNumber2;
                    }
                }
                else if (randomNumber3 != 5 && flag == false && randomNumber2 >= bestHandNum2)
                {
                    bestHandNum1 = 5;
                    bestHandNum2 = 5;
                    if (randomNumber3 >= bestHandNum3)
                    {
                        bestHandNum3 = randomNumber3;
                    }
                }
            }
            //pair of 6
            else if ((randomNumber1 == 6 && randomNumber2 == 6 && randomNumber3 != 6) ||
               (randomNumber2 == 6 && randomNumber3 == 6 && randomNumber1 != 6) ||
               (randomNumber1 == 6 && randomNumber3 == 6 && randomNumber2 != 6))
            {
                if (randomNumber1 != 6 && flag == false && randomNumber2 >= bestHandNum2)
                {
                    bestHandNum1 = 6;
                    bestHandNum2 = 6;
                    if (randomNumber1 >= bestHandNum3)
                    {
                        bestHandNum3 = randomNumber1;
                    }
                }
                else if (randomNumber2 != 6 && flag == false && randomNumber1 >= bestHandNum2)
                {
                    bestHandNum1 = 6;
                    bestHandNum2 = 6;
                    if (randomNumber2 >= bestHandNum3)
                    {
                        bestHandNum3 = randomNumber2;
                    }
                }
                else if (randomNumber3 != 6 && flag == false && randomNumber2 >= bestHandNum2)
                {
                    bestHandNum1 = 6;
                    bestHandNum2 = 6;
                    if (randomNumber3 >= bestHandNum3)
                    {
                        bestHandNum3 = randomNumber3;
                    }
                }
            }
            //triple 1
            else if (randomNumber1 == 1 && randomNumber2 == 1 && randomNumber3 == 1)
            {
                bestHandNum1 = 1;
                bestHandNum2 = 1;
                bestHandNum3 = 1;
                flag = true;           
            }
            //triple 2
            else if (randomNumber1 == 2 && randomNumber2 == 2 && randomNumber3 == 2)
            {                
                bestHandNum1 = 2;
                bestHandNum2 = 2;
                bestHandNum3 = 2;
                flag = true;            
            }
            //1 2 3
            else if ((randomNumber1 == 1 && randomNumber2 == 2 && randomNumber3 == 3) ||
                (randomNumber1 == 1 && randomNumber2 == 3 && randomNumber3 == 2) ||
                (randomNumber1 == 2 && randomNumber2 == 1 && randomNumber3 == 3) ||
                (randomNumber1 == 2 && randomNumber2 == 3 && randomNumber3 == 1) ||
                (randomNumber1 == 3 && randomNumber2 == 1 && randomNumber3 == 2) ||
                (randomNumber1 == 3 && randomNumber2 == 2 && randomNumber3 == 1))
            {
                bestHandNum1 = 1;
                bestHandNum2 = 2;
                bestHandNum3 = 3;
                flag = true;
            }
            //triple 3
            else if (randomNumber1 == 3 && randomNumber2 == 3 && randomNumber3 == 3)
            {
                bestHandNum1 = 3;
                bestHandNum2 = 3;
                bestHandNum3 = 3;
                flag = true;
            }
            //2 3 4
            else if ((randomNumber1 == 2 && randomNumber2 == 3 && randomNumber3 == 4) ||
                (randomNumber1 == 2 && randomNumber2 == 4 && randomNumber3 == 3) ||
                (randomNumber1 == 3 && randomNumber2 == 2 && randomNumber3 == 4) ||
                (randomNumber1 == 3 && randomNumber2 == 4 && randomNumber3 == 2) ||
                (randomNumber1 == 4 && randomNumber2 == 2 && randomNumber3 == 3) ||
                (randomNumber1 == 4 && randomNumber2 == 3 && randomNumber3 == 2))
            {
                bestHandNum1 = 2;
                bestHandNum2 = 3;
                bestHandNum3 = 4;
                flag = true;
            }
            //triple 4
            else if (randomNumber1 == 4 && randomNumber2 == 4 && randomNumber3 == 4)
            {
                bestHandNum1 = 4;
                bestHandNum2 = 4;
                bestHandNum3 = 4;
                flag = true;
            }
            //3 4 5
            else if ((randomNumber1 == 3 && randomNumber2 == 4 && randomNumber3 == 5) ||
                (randomNumber1 == 3 && randomNumber2 == 5 && randomNumber3 == 4) ||
                (randomNumber1 == 4 && randomNumber2 == 3 && randomNumber3 == 5) ||
                (randomNumber1 == 4 && randomNumber2 == 5 && randomNumber3 == 3) ||
                (randomNumber1 == 5 && randomNumber2 == 3 && randomNumber3 == 4) ||
                (randomNumber1 == 5 && randomNumber2 == 4 && randomNumber3 == 3))
            {
                bestHandNum1 = 3;
                bestHandNum2 = 4;
                bestHandNum3 = 5;
                flag = true;
            }
            //triple 5
            else if (randomNumber1 == 5 && randomNumber2 == 5 && randomNumber3 == 5)
            {
                if(bestHandNum1 < 6 && bestHandNum2 < 6 && bestHandNum3 < 6)
                {
                    bestHandNum1 = 5;
                    bestHandNum2 = 5;
                    bestHandNum3 = 5;
                    flag = true;
                }                
            }
            //4 5 6
            else if ((randomNumber1 == 4 && randomNumber2 == 5 && randomNumber3 == 6) ||
                (randomNumber1 == 4 && randomNumber2 == 6 && randomNumber3 == 5) ||
                (randomNumber1 == 5 && randomNumber2 == 4 && randomNumber3 == 6) ||
                (randomNumber1 == 5 && randomNumber2 == 6 && randomNumber3 == 4) ||
                (randomNumber1 == 6 && randomNumber2 == 4 && randomNumber3 == 5) ||
                (randomNumber1 == 6 && randomNumber2 == 5 && randomNumber3 == 4))
            {
                bestHandNum1 = 4;
                bestHandNum2 = 5;
                bestHandNum3 = 6;
                flag = true;
            }
            //triple 6
            else if (randomNumber1 == 6 && randomNumber2 == 6 && randomNumber3 == 6)
            {
                bestHandNum1 = 6;
                bestHandNum2 = 6;
                bestHandNum3 = 6;
                flag = true;
                onethreesix.Text = "THE DEVIL";
                onethreesix.IsVisible = true;
            }


            bestHand1.Source = "https://136gameimages.s3.us-east-2.amazonaws.com/images/dice" + bestHandNum1 + ".png";
            bestHand2.Source = "https://136gameimages.s3.us-east-2.amazonaws.com/images/dice" + bestHandNum2 + ".png";
            bestHand3.Source = "https://136gameimages.s3.us-east-2.amazonaws.com/images/dice" + bestHandNum3 + ".png";
        }

        RollsLeft rollsLeft = new RollsLeft();
        public void defineRolls()
        {
            rollslefttext.Text = Convert.ToString(rollsLeft.Rolls);
        }

        void resetBestHand()
        {
            bestHandNum1 = 0;
            bestHandNum2 = 0;
            bestHandNum3 = 0;
        }
        

        public MainPage()
        {
            InitializeComponent();
            defineRolls();
        }             
    }
}
