using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Module_5_5_3
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {

            string tabName = "Module 5";
            application.CreateRibbonTab(tabName);
            string utilsFolderPath = @"C:\Users\BelikAE\AppData\Roaming\Autodesk\Revit\Addins\2021\Module_5_5_3\";

            var panel = application.CreateRibbonPanel(tabName, "Module_5_5");

            var button1 = new PushButtonData("Задание 1", "Задание 1",
                Path.Combine(utilsFolderPath, "Module_5_5_1.dll"),
                "Module_5_5_1.Main");

            Image img1 = Properties.Resources.RevitAPITrainingUI_32;
            ImageSource imgSource1 = Convert(img1);
            button1.LargeImage = imgSource1;
            button1.Image = imgSource1;
            panel.AddItem(button1);

            var button2 = new PushButtonData("Задание 2", "Задание 2",
                Path.Combine(utilsFolderPath, "Module_5_5_2.dll"),
                "Module_5_5_2.Main");

            Image img2 = Properties.Resources.RevitAPITrainingUI_32;
            ImageSource imgSource2 = Convert(img2);
            button2.LargeImage = imgSource2;
            button2.Image = imgSource2;
            panel.AddItem(button2);


            return Result.Succeeded;
        }


        public BitmapImage Convert(Image img)
        {
            using (var memory = new MemoryStream())
            {
                img.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                return bitmapImage;
            }
        }
    }
}
