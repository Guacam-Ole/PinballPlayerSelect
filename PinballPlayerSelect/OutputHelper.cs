using Microsoft.Extensions.Logging;

using System;
using System.Windows.Forms;

namespace PPS
{
    public static class OutputHelper
    {
        public static void ShowMessage(ILogger logger, string message, bool isError=false)
        {
            if (isError)
            {
                logger.LogInformation("Showed MessageBox: '{contents}'", message);
            } else
            {
                logger.LogError("Showed Error: '{contents}'", message);
            }
            MessageBox.Show(message);
        }
        public static void ShowMessage(ILogger logger, Exception ex, string message)
        {
            logger.LogInformation(ex, "Showed MessageBox: '{contents}'", message);
            MessageBox.Show(message);
        }
    }
}