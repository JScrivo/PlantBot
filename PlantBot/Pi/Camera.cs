using Microsoft.Extensions.Logging;
using MMALSharp;
using MMALSharp.Common;
using MMALSharp.Common.Utility;
using MMALSharp.Handlers;
using NLog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantBot.Pi
{
    public class Camera
    {
        public Camera()
        {
/*            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                .ClearProviders()
                .SetMinimumLevel(LogLevel.Trace)
                .AddNLog("NLog.config");
            });

            MMALLog.LoggerFactory = loggerFactory;*/
        }


        public async Task TakePicture()
        {
            // Singleton initialized lazily. Reference once in your application.
            MMALCamera cam = MMALCamera.Instance;

            using (var imgCaptureHandler = new ImageStreamCaptureHandler("/home/pi/images/", "jpg"))
            {
                await cam.TakePicture(imgCaptureHandler, MMALEncoding.JPEG, MMALEncoding.I420);
            }

            // Cleanup disposes all unmanaged resources and unloads Broadcom library. To be called when no more processing is to be done
            // on the camera.
            //cam.Cleanup();
        }
    }
}
