﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TrireksaTracingApps.ViewModels;
using Xamarin.Forms;

namespace TrireksaTracingApps
{
    public class Helper
    {
        private static string _server = "http://192.168.1.6/";

        public static async Task<AuthenticationToken> GetToken()
        {
            var app = ((App)App.Current);
            return await Task.FromResult(await app.GetToken());
        }


        public static Task<App> GetBaseApp()
        {
            var app = ((App)App.Current);
            return Task.FromResult(app);
        }

        public static async Task<Views.MenuView> GetMainPageAsync()
        {
            var x = await Task.FromResult(Xamarin.Forms.Application.Current.MainPage);
            return x as Views.MenuView;
        }

        public static AuthenticationToken Token { get; set; }
        public static string Server
        {
            get
            {
                return _server;
            }
            set
            {
                _server = value;
            }
        }


        internal static void ShowMessageError(string v)
        {
            MessagingCenter.Send(new MessagingCenterAlert
            {
                Title = "Error",
                Message = v,
                Cancel = "OK"
            }, "message");

        }

        internal static void ShowMessage(string v)
        {
            MessagingCenter.Send(new MessagingCenterAlert
            {
                Title = "Info",
                Message = v,
                Cancel = "OK"
            }, "message");

        }


        public static StringContent Content(object str)
        {
            var value = JsonConvert.SerializeObject(str);
            return new StringContent(value, Encoding.UTF8, "application/json");
        }
    }
     

    public class MessagingCenterAlert
    {
        /// <summary>
        /// Init this instance.
        /// </summary>
        public static void Init()
        {
            var time = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance cancel/OK text.
        /// </summary>
        /// <value><c>true</c> if this instance cancel; otherwise, <c>false</c>.</value>
        public string Cancel { get; set; }

        /// <summary>
        /// Gets or sets the OnCompleted Action.
        /// </summary>
        /// <value>The OnCompleted Action.</value>
        public Action OnCompleted { get; set; }
    }
}
