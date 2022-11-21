using System;
using System.IO;
using System.Windows.Forms;

namespace WaitingLong
{
    public partial class FormWaitingLong : Form
    {
        public FormWaitingLong()
        {
            InitializeComponent();
        }

        //Those are the hours and minutes for each line.
        int hours = 0;
        int mins = 0;

        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            string line1 = "";
            string line2 = "";

            int timeIntervalInMins = 0;

            string path = Application.StartupPath + @"\wait.txt";
            StreamReader streamReader = new StreamReader(path);

            bool finished = false;

            while (!finished)
            {
                line1 = streamReader.ReadLine();
                line2 = streamReader.ReadLine();
                
                if (line1 == "0" || line2 == "0")
                {
                    finished = true;
                }
                else
                {
                    int timeValue1 = DetermineTimeValue(line1);
                    TxtResult.Text += $"From {hours}:{mins} to ";

                    int timeValue2 = DetermineTimeValue(line2);
                    TxtResult.Text += $"{hours}:{mins}";

                    //We check if the first time is larger than the second.
                    //We check how many mins till midnight and then add the mins after midnight.
                    //Example: first time is 21:00 and second is 07:00,
                    //There are 180 mins till midnight and 420 mins after.
                    if (timeValue1 > timeValue2)
                    {
                        timeIntervalInMins = (1440 - timeValue1) + timeValue2;
                    }
                    else
                    {
                        timeIntervalInMins = timeValue2 - timeValue1;
                    }

                    TxtResult.Text += " is " + timeIntervalInMins + " minutes " + Environment.NewLine;
                }
            }
        }

        private int DetermineTimeValue(string time)
        {
            //As per the assigment, each line in the input file may have different lenght.
            switch (time.Length)
            {
                case 4:
                    hours = Convert.ToInt32(time.Substring(0, 2));
                    mins = Convert.ToInt32(time.Substring(2, 2));
                    break;
                case 3:
                    hours = Convert.ToInt32(time.Substring(0, 1));
                    mins = Convert.ToInt32(time.Substring(1, 2));
                    break;
                case 1:
                    hours = 0;
                    mins = Convert.ToInt32(time.Substring(0, 1));
                    break;
            }

            //We represent the clock as values between 0 and 1440, as there are 1440 mins in 24 h.
            //We return the time as value in this interval.
            return (hours * 60) + mins;

        }
    }
}
