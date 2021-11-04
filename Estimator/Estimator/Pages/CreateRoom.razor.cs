﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Diagnostics;

namespace Estimator.Pages
{
    public partial class CreateRoom
    {
        [Parameter] public string RoomId { get; set; } = string.Empty;
        [Parameter] public string Username { get; set; } = string.Empty;
        [Parameter] public string Type { get; set; } = "Fibonacci";


        private async void CreateNewRoom()
        {
            if (this.Username == string.Empty)
            {
                await this.JsRuntime.InvokeVoidAsync("alert", "Please enter a username!");
                return;
            }

            try
            {
                this.RoomId =
                    this.RoomManager.CreateRoom(this.ConvertType(this.Type),
                        new Data.Model.Estimator(this.Username));
                this.NavigationManager.NavigateTo($"host/{this.RoomId}/{this.Username}");
                return;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                await this.JsRuntime.InvokeVoidAsync("alert", "Something went wrong!\n Please try again.");
            }
        }

        private int ConvertType(string type)
        {
            return type == "Fibonacci" ? 1 : 2;
        }
    }
}