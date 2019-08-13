using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace sigplusnet_csharp_lcd15_demo
{
    public partial class Form1 : Form
    {
        Bitmap sign, ok, clear, please;
        int lcdX, lcdY, screen;
        uint lcdSize;
        string data, data2;

        public Form1()
        {
            InitializeComponent();
        }

        private void cmdStart_Click(object sender, EventArgs e)
        {
            //The following code will write BMP images out to the LCD 1X5 screen

            sign = new System.Drawing.Bitmap(Application.StartupPath + "\\images\\Sign.bmp");
            ok = new System.Drawing.Bitmap(Application.StartupPath + "\\images\\OK.bmp");
            clear = new System.Drawing.Bitmap(Application.StartupPath + "\\images\\CLEAR.bmp");
            please = new System.Drawing.Bitmap(Application.StartupPath + "\\images\\please.bmp");

            sigPlusNET1.SetTabletState(1); //Turns tablet on to collect signature
            sigPlusNET1.LCDRefresh(0, 0, 0, 240, 64);
            sigPlusNET1.SetTranslateBitmapEnable(false);

            //Images sent to the background
            sigPlusNET1.LCDSendGraphic(1, 2, 0, 20, sign);
            sigPlusNET1.LCDSendGraphic(1, 2, 207, 4, ok);
            sigPlusNET1.LCDSendGraphic(1, 2, 15, 4, clear);

            //Get LCD size in pixels.
            lcdSize = sigPlusNET1.LCDGetLCDSize();
            lcdX = (int)(lcdSize & 0xFFFF);
            lcdY = (int)((lcdSize >> 16) & 0xFFFF);
 
            //lcdX = 240;
            //lcdY = 64;

            //Demo text
            Font f = new System.Drawing.Font("Arial", 9.0F, System.Drawing.FontStyle.Regular);
            data = "These are sample terms and conditions. Please press Continue.";
            string[] words = data.Split(new char[] { ' ' });
            string writeData = "", tempData = "";

            int xSize, ySize, i, yPos = 0;

            for (i = 0; i < words.Length; i++)
            {
                tempData += words[i];

                xSize = sigPlusNET1.LCDStringWidth(f, tempData);

                if (xSize < lcdX)
                {
                    writeData = tempData;
                    tempData += " ";

                    xSize = sigPlusNET1.LCDStringWidth(f, tempData);

                    if (xSize < lcdX)
                    {
                        writeData = tempData;
                    }
                }
                else
                {
                    ySize = sigPlusNET1.LCDStringHeight(f, tempData);

                    sigPlusNET1.LCDWriteString(0, 2, 0, yPos, f, writeData);

                    tempData = "";
                    writeData = "";
                    yPos += (short)ySize;
                    i--;
                }
            }

            if (writeData != "")
            {
                sigPlusNET1.LCDWriteString(0, 2, 0, yPos, f, writeData);
            }

            //Hotspot text
            sigPlusNET1.LCDWriteString(0, 2, 15, 45, f, "Continue");
            sigPlusNET1.LCDWriteString(0, 2, 200, 45, f, "Exit");

            //Create the hot spots for the Continue and Exit buttons
            sigPlusNET1.KeyPadAddHotSpot(0, 1, 12, 40, 40, 15); //For Continue button
            sigPlusNET1.KeyPadAddHotSpot(1, 1, 195, 40, 20, 15); //For Exit button

            sigPlusNET1.ClearTablet();

            sigPlusNET1.LCDSetWindow(0, 0, 1, 1);
            sigPlusNET1.SetSigWindow(1, 0, 0, 1, 1); //Sets the area where ink is permitted in the SigPlus object
            sigPlusNET1.SetLCDCaptureMode(2);   //Sets mode so ink will not disappear after a few seconds
           
            screen = 1;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //reset hardware
            sigPlusNET1.LCDRefresh(0, 0, 0, 240, 64);
            sigPlusNET1.LCDSetWindow(0, 0, 240, 64);
            sigPlusNET1.SetSigWindow(1, 0, 0, 240, 64);
            sigPlusNET1.KeyPadClearHotSpotList();

            Bitmap blank = new System.Drawing.Bitmap(240, 64);
            sigPlusNET1.LCDSendGraphic(1, 0, 0, 0, blank);

            sigPlusNET1.SetLCDCaptureMode(1);
            sigPlusNET1.SetTabletState(0);
        }

        private void sigPlusNET1_PenDown(object sender, EventArgs e)
        {

        }

        private void sigPlusNET1_PenUp(object sender, EventArgs e)
        {
            string strSig;

            if (sigPlusNET1.KeyPadQueryHotSpot(0) > 0)//If the Continue hotspot is tapped, then...
            {
                if (screen == 1)
                {
                    sigPlusNET1.ClearSigWindow(1);
                    sigPlusNET1.LCDRefresh(1, 16, 45, 50, 15); //Refresh LCD at 'Continue' to indicate to user that this option has been sucessfully chosen
                    sigPlusNET1.ClearTablet();
                    sigPlusNET1.LCDRefresh(0, 0, 0, 240, 64);

                    //Demo text
                    Font f = new System.Drawing.Font("Arial", 9.0F, System.Drawing.FontStyle.Regular);
                    data2 = "We'll bind the signature to all the displayed text. Please press Continue.";
                    string[] words = data2.Split(new char[] { ' ' });
                    string writeData = "", tempData = "";

                    int xSize, ySize, i, yPos = 0;

                    for (i = 0; i < words.Length; i++)
                    {
                        tempData += words[i];

                        xSize = sigPlusNET1.LCDStringWidth(f, tempData);

                        if (xSize < lcdX)
                        {
                            writeData = tempData;
                            tempData += " ";

                            xSize = sigPlusNET1.LCDStringWidth(f, tempData);

                            if (xSize < lcdX)
                            {
                                writeData = tempData;
                            }
                        }
                        else
                        {
                            ySize = sigPlusNET1.LCDStringHeight(f, tempData);

                            sigPlusNET1.LCDWriteString(0, 2, 0, yPos, f, writeData);

                            tempData = "";
                            writeData = "";
                            yPos += (short)ySize;
                            i--;
                        }
                    }

                    if (writeData != "")
                    {
                        sigPlusNET1.LCDWriteString(0, 2, 0, yPos, f, writeData);
                    }

                    //Hotspot text
                    sigPlusNET1.LCDWriteString(0, 2, 15, 45, f, "Continue");
                    sigPlusNET1.LCDWriteString(0, 2, 200, 45, f, "Back");

                    screen = 2;
                }
                else if (screen == 2)
                {
                    sigPlusNET1.ClearSigWindow(1);
                    sigPlusNET1.LCDRefresh(1, 16, 45, 50, 15); //Refresh LCD at 'Continue' to indicate to user that this option has been sucessfully chosen
                    sigPlusNET1.LCDRefresh(2, 0, 0, 240, 64); //Brings the background image already loaded into foreground
                    sigPlusNET1.ClearTablet();
                    sigPlusNET1.KeyPadClearHotSpotList();
                    sigPlusNET1.KeyPadAddHotSpot(2, 1, 10, 5, 53, 17); //For CLEAR button
                    sigPlusNET1.KeyPadAddHotSpot(3, 1, 197, 5, 19, 17); //For OK button
                    sigPlusNET1.LCDSetWindow(2, 22, 236, 40);
                    sigPlusNET1.SetSigWindow(1, 0, 22, 240, 40); //Sets the area where ink is permitted in the SigPlus object
                }

                sigPlusNET1.SetLCDCaptureMode(2);
            }
            else if (sigPlusNET1.KeyPadQueryHotSpot(1) > 0) //If the Exit hotspot is tapped, then...
            {
                if (screen == 1)
                {
                    sigPlusNET1.ClearSigWindow(1);
                    sigPlusNET1.LCDRefresh(1, 200, 45, 20, 15); //Refresh (invert) LCD at 'EXIT' to indicate to user that this option has been sucessfully chosen
                    sigPlusNET1.SetLCDCaptureMode(1);
                    sigPlusNET1.LCDRefresh(0, 0, 0, 240, 64);

                    //reset hardware
                    sigPlusNET1.SetTabletState(0);
                    Application.Exit();
                }
                else if (screen == 2)
                {
                    sigPlusNET1.ClearSigWindow(1);
                    sigPlusNET1.LCDRefresh(1, 200, 45, 25, 15); //Refresh LCD at 'Back' to indicate to user that this option has been sucessfully chosen
                    sigPlusNET1.ClearTablet();
                    sigPlusNET1.LCDRefresh(0, 0, 0, 240, 64);

                    //Demo text
                    Font f = new System.Drawing.Font("Arial", 9.0F, System.Drawing.FontStyle.Regular);
                    data = "These are sample terms and conditions. Please press Continue.";
                    string[] words = data.Split(new char[] { ' ' });
                    string writeData = "", tempData = "";

                    int xSize, ySize, i, yPos = 0;

                    for (i = 0; i < words.Length; i++)
                    {
                        tempData += words[i];

                        xSize = sigPlusNET1.LCDStringWidth(f, tempData);

                        if (xSize < lcdX)
                        {
                            writeData = tempData;
                            tempData += " ";

                            xSize = sigPlusNET1.LCDStringWidth(f, tempData);

                            if (xSize < lcdX)
                            {
                                writeData = tempData;
                            }
                        }
                        else
                        {
                            ySize = sigPlusNET1.LCDStringHeight(f, tempData);

                            sigPlusNET1.LCDWriteString(0, 2, 0, yPos, f, writeData);

                            tempData = "";
                            writeData = "";
                            yPos += (short)ySize;
                            i--;
                        }
                    }

                    if (writeData != "")
                    {
                        sigPlusNET1.LCDWriteString(0, 2, 0, yPos, f, writeData);
                    }

                    //Hotspot text
                    sigPlusNET1.LCDWriteString(0, 2, 15, 45, f, "Continue");
                    sigPlusNET1.LCDWriteString(0, 2, 200, 45, f, "Exit");

                    screen = 1;
                }

                sigPlusNET1.SetLCDCaptureMode(2);
            }
            else if (sigPlusNET1.KeyPadQueryHotSpot(2) > 0) //If the CLEAR hotspot is tapped, then...
            {
                sigPlusNET1.ClearSigWindow(1);
                sigPlusNET1.LCDRefresh(1, 10, 0, 53, 17); //Refresh LCD at 'CLEAR' to indicate to user that this option has been sucessfully chosen
                sigPlusNET1.LCDRefresh(2, 0, 0, 240, 64); //Brings the background image already loaded into foreground
                sigPlusNET1.ClearTablet();
            }
            else if (sigPlusNET1.KeyPadQueryHotSpot(3) > 0) //If the OK hotspot is tapped, then...
            {
                sigPlusNET1.ClearSigWindow(1);

                /*the following code is used to cryptographically bind the
                  signature to some specific data, passed in
                  using the AutoKeyData property
                  the signature will not be decrypted without this data*/
                sigPlusNET1.SetSigCompressionMode(1);
                sigPlusNET1.AutoKeyStart();
                sigPlusNET1.SetAutoKeyData(data);
                sigPlusNET1.SetAutoKeyData(data2);
                sigPlusNET1.AutoKeyFinish();
                sigPlusNET1.SetEncryptionMode(2);

                /*********************Two ways to save the signature*
                *************************************'
                Method 1--storing as an ASCII string value*/
                strSig = sigPlusNET1.GetSigString();
                /*the strSig String variable now holds the signature as a long ASCII string.
                this can be stored as desired, in a database, etc.

                Method 2--storing as a SIG file on the hard drive
                sigPlusNET1.ExportSigFile "C:\SigFile1.sig"
                The commented-out function above will export the signature to the SIG file
                specified (in this case C:\SigFile1.sig, saving the signature as a file on your hardrive
                *****************************************************************************************/

                sigPlusNET1.LCDRefresh(1, 210, 3, 14, 14); //Refresh LCD at 'OK' to indicate to user that this option has been sucessfully chosen

                if (sigPlusNET1.NumberOfTabletPoints() > 0)
                {
                    sigPlusNET1.LCDRefresh(0, 0, 0, 240, 64);
                    Font f = new System.Drawing.Font("Arial", 9.0F, System.Drawing.FontStyle.Regular);
                    sigPlusNET1.LCDWriteString(0, 2, 35, 25, f, "Signature capture complete.");
                    System.Threading.Thread.Sleep(2000);
                    Application.Exit();
                }
                else
                {
                    sigPlusNET1.LCDRefresh(0, 0, 0, 240, 64);
                    sigPlusNET1.LCDSendGraphic(0, 2, 4, 20, please);
                    System.Threading.Thread.Sleep(750);
                    sigPlusNET1.ClearTablet();
                    sigPlusNET1.LCDRefresh(2, 0, 0, 240, 64);
                    sigPlusNET1.SetLCDCaptureMode(2);   //Sets mode so ink will not disappear after a few seconds
                }
            }

            sigPlusNET1.ClearSigWindow(1);
        }
    }
}
