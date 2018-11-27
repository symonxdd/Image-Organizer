using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Image_Organiser
{
    public partial class Form1 : Form
    {
        private string strSelectedPathFROM = string.Empty;
        private string strSelectedPathTO = string.Empty;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            label6.Text = "✘";
            label7.Text = "✘";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((strSelectedPathFROM != "") && (strSelectedPathTO != ""))
            {
                int intFileExistsCheck;
                string[] extensions = { ".jpg", ".png", ".bmp" }; //making array with the supported extensions
                string[] dirs = Directory.GetFiles(strSelectedPathFROM, "*.*", checkBox1.Checked? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly).Where(f => extensions.Contains(new FileInfo(f).Extension.ToLower())).ToArray();

                intFileExistsCheck = dirs.Length;
                
                //checking if files exist
                if (intFileExistsCheck >= 1)
                {
                    MessageBox.Show(intFileExistsCheck.ToString() + " images have been found.\nPress OK to Start.", "Message");
                    if (radioButton1.Checked)
                    {
                        int i = 0;
                        //BEGIN OF OPERATION
                        foreach (string dir in dirs)
                        {
                            progressBar1.Minimum = 0;
                            progressBar1.Maximum = intFileExistsCheck;
                            progressBar1.Value = i++;

                            string strDate, strYear, strMonth, strDay, strFinalDate, strOriginalFileName, strTrueFileName, strPathRenamedFiles, strPathTODate; //declaration of the used vars
                            DateTime creationTime = File.GetLastWriteTime(dir); //assign the Date Taken to the var creationTime
                            strDate = creationTime.ToString(); //convert the datetime date to a string so we can extract the fucking numbers

                            //this is for the YEAR
                            if (strDate.Substring(2, 1) == "/")
                            {
                                strYear = strDate.Substring(6, 4);
                            }
                            else
                            {
                                strYear = strDate.Substring(5, 4);
                            }

                            //this is for the MONTH
                            if (strDate.Substring(2, 1) == "/")
                            {
                                strMonth = strDate.Substring(3, 2);
                            }
                            else
                            {
                                strMonth = strDate.Substring(2, 2);
                            }

                            //this is for the DAY
                            if (strDate.Substring(2, 1) == "/")
                            {
                                strDay = strDate.Substring(0, 2);
                            }
                            else
                            {
                                strDay = strDate.Substring(0, 1);
                            }

                            //some variables are being assigned to values
                            strFinalDate = strYear + "-" + strMonth + "-" + strDay;
                            strOriginalFileName = Path.GetFileName(dir);
                            strTrueFileName = strFinalDate + " " + strOriginalFileName;
                            strPathRenamedFiles = strSelectedPathFROM + @"\" + strTrueFileName;
                            strPathTODate = strSelectedPathTO + @"\" + strFinalDate + @"\"; //this is the path with the created date (if doesn't exist)

                                if (!Directory.Exists(strSelectedPathTO + @"\" + strFinalDate))
                                {
                                    Directory.CreateDirectory(strPathTODate);
                                    File.Copy(dir, strPathTODate + strTrueFileName);
                                }
                                else
                                {
                                    File.Copy(dir, strPathTODate + strTrueFileName);
                                }
                            
                        }
                        //END OF OPERATION
                    }
                    else
                    {
                        int i = 0;
                        //BEGIN OF OPERATION
                        foreach (string dir in dirs)
                        {
                            progressBar1.Minimum = 0;
                            progressBar1.Maximum = intFileExistsCheck;
                            progressBar1.Value = i++;

                            string strDate, strYear, strMonth, strDay, strFinalDate, strOriginalFileName, strTrueFileName, strPathRenamedFiles, strPathTODate; //declaration of the vars used
                            DateTime creationTime = File.GetLastWriteTime(dir); //assign the Date Taken to the var creationTime
                            strDate = creationTime.ToString(); //convert the datetime date to a string so we can extract the fucking numbers

                            //this is for the YEAR
                            if (strDate.Substring(2, 1) == "/")
                            {
                                strYear = strDate.Substring(6, 4);
                            }
                            else
                            {
                                strYear = strDate.Substring(5, 4);
                            }

                            //this is for the MONTH
                            if (strDate.Substring(2, 1) == "/")
                            {
                                strMonth = strDate.Substring(3, 2);
                            }
                            else
                            {
                                strMonth = strDate.Substring(2, 2);
                            }

                            //this is for the DAY
                            if (strDate.Substring(2, 1) == "/")
                            {
                                strDay = strDate.Substring(0, 2);
                            }
                            else
                            {
                                strDay = strDate.Substring(0, 1);
                            }

                            strFinalDate = strYear + "-" + strMonth + "-" + strDay;
                            strOriginalFileName = Path.GetFileName(dir);
                            strTrueFileName = strFinalDate + " " + strOriginalFileName + ".jpg";
                            strPathRenamedFiles = strSelectedPathFROM + @"\" + strTrueFileName;

                            File.Move(dir, strPathRenamedFiles); //rename the images

                            strPathTODate = strSelectedPathTO + @"\" + strFinalDate + @"\"; //this is the path with the created date (if doesn't exist)

                            if (!Directory.Exists(strSelectedPathTO + @"\" + strFinalDate))
                            {
                                Directory.CreateDirectory(strPathTODate);
                                File.Move(strPathRenamedFiles, strPathTODate + strTrueFileName); //move the renamed images
                            }
                            else
                            {
                                File.Move(strPathRenamedFiles, strPathTODate + strTrueFileName); //move the renamed images
                            }
                        }
                        //END OF OPERATION
                    }
                    MessageBox.Show("All done!");
                }
                else
                {
                    MessageBox.Show("No image files found in the selected directory.", "Message");
                }
            }
            else
            {
                if ((strSelectedPathFROM == "") && (strSelectedPathTO != ""))
                {
                    MessageBox.Show("Please select the location of your pictures first.", "Error");
                }
                else if ((strSelectedPathTO == "") && (strSelectedPathFROM != ""))
                {
                    MessageBox.Show("Please select the destination folder first.", "Error");
                }
                else if ((strSelectedPathFROM == "") && (strSelectedPathTO == ""))
                {
                    MessageBox.Show("Neither of the paths are selected.", "Error");
                }

            }
        }

    private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // string strSelectedPathFROM; // SO THIS IS NOT NEEDED! OTHERWISE IT WONT WORK!!!
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK)
            {
                strSelectedPathFROM = fbd.SelectedPath;
                label6.Text = "✔";
            }
            else if (result == DialogResult.Cancel)
            {
                strSelectedPathFROM = "";
                label6.Text = "✘";
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            // string strSelectedPathTO; // SO THIS IS NOT NEEDED! OTHERWISE IT WONT WORK!!!
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK)
            {
                strSelectedPathTO = fbd.SelectedPath;
                label7.Text = "✔";
            }
            else if (result == DialogResult.Cancel)
            {
                strSelectedPathTO = "";
                label7.Text = "✘";
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (strSelectedPathFROM != "")
            {
                MessageBox.Show(strSelectedPathFROM, "Path");
            }
            else
            {
                MessageBox.Show("No path selected...", "Error");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (strSelectedPathTO != "")
            {
                MessageBox.Show(strSelectedPathTO, "Path");
            }
            else
            {
                MessageBox.Show("No path selected...", "Error");
            }
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {

        }

        private void menuItem1_Click(object sender, EventArgs e)
        {

        }

        private void menuItem2_Click_1(object sender, EventArgs e)
        {

        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuItem4_Click(object sender, EventArgs e)
        {

        }

        private void menuItem1_Click_1(object sender, EventArgs e)
        {

        }

        private void menuItem2_Click_2(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuItem4_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Image Organiser v1\nMade by Spycer0 (Symon Błażejczak) [incl. banner image]\nCopyright © 2016 Image Organiser All Rights Reserved", "About");
        }

        private void menuItem5_Click(object sender, EventArgs e)
        {

        }

        private void menuItem5_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("1. Select the source path.\n2. Select the target path.\n3. Check Include Subfolders if you want to search in the root and all the sub-directories.\n4. Choose Copy or Move whether you want to just copy the images and leave the original ones OR move them.\n5. Click Start to begin the process.\n\nExtra:\n~ You can also check if the selected paths are correct with the View buttons.", "Usage");
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void menuItem3_Click_1(object sender, EventArgs e)
        {

        }

        private void menuItem6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This program will copy/ move the chosen image files to a specified directory AND rename the images to the date when they were taken + their original name [α]. For each image it'll check in the destination path if a folder exists named with the date when the image was taken [Ω].\n\nα   File Format:     <YY-MM-DD> <original name>\nΩ  Folder Format: <YY-MM-DD>", "Purpose");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
