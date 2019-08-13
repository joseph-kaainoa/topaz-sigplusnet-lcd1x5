# SigPlusNET - CSharp LCD 1x5 Demo

This is from <http://topazsystems.com/Software/download/dotnet/sigplusnet_csharp_lcd15_demo.zip>, however this sample has the project and solution upgraded to Visual Studio 2019.

The demo did not include the image files required for this demo.  The included images are from <http://www.sigplusweb.com/SigWebLCD1x5Demo.htm>.  The images are then deployed with the binary.  Without these, the project throws an exception when you click the Start button.

While this demo does include the SigPlusNET.dll (included in the original demo), the INI file that is used to configure which signature pad is used is not.  It is recommended to install the SigPlusNET compenent from <https://www.topazsystems.com/sigplusnet.html>.
The setup wizard will step you through which hardware is going to be used so it can properly configure the INI file.
