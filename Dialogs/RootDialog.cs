using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AzureSearchBot.Dialogs;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;

namespace Bot_Application2.Dialogs
{
    [LuisModel("51c26cf0-29f8-45ac-ae80-f74a8aa91f75", "6f52ffc881a4402d9208579e6aa2fe16")]
    [Serializable]
    public class RootDialog : LuisDialog<object>
    {
        [LuisIntent("")]
        [LuisIntent("None")]
        private async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("None");
        }

        [LuisIntent("Greeting")]
        public async Task Search(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            await context.PostAsync($"Hi, How can i help you today");
            context.Wait(this.MessageReceived);
        }
        [LuisIntent("SearchRooms")]
        private async Task SearchRooms(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var Activity = await activity;
            await context.PostAsync("Search");
            context.Call(new RoomSearchDialog(), FallbackDialog);
        }

        private async Task FallbackDialog(IDialogContext context, IAwaitable<object> result)
        {
            context.PostAsync("Select your room");
        }

        [LuisIntent("ShowRoom")]
        private async Task ShowRoom(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var Activity = await activity;
            await context.PostAsync("Show");
            context.Call(new ShowRoomDialog(), FallbackDialog);
            /* var act = await activity as Activity;
             act.TextFormat = "markdown";
                 //activity.Text = "Please greet me!!";
                 Activity reply = act.CreateReply(act.Text);
                 // card code here
                 reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                 reply.Attachments = new List<Attachment>();

                 CardAction _Link2 = new CardAction("OpenUrl", "Goooogle", "null", "https://gmail.com");
                 CardAction _Link = new CardAction("openUrl","Google",null,"https://Google.com");
                 CardImage _GoogleLogo = new CardImage("https://www.google.co.in/url?sa=i&rct=j&q=&esrc=s&source=images&cd=&cad=rja&uact=8&ved=2ahUKEwjNhOmWmezbAhWNfSsKHeRgDfsQjRx6BAgBEAU&url=http%3A%2F%2Fcode.google.com%2F&psig=AOvVaw2ZUzvGDsRE-xeeTRfFcM7E&ust=1529926147635999");

                 HeroCard googlecard = new HeroCard();

                 googlecard.Buttons = new List<CardAction>();
                 googlecard.Images = new List<CardImage>();

                 googlecard.Buttons.Add(_Link);
                 googlecard.Images.Add(_GoogleLogo);
                 googlecard.Title= "Google";

                 googlecard.Buttons.Add(_Link2);
                 reply.Attachments.Add(googlecard.ToAttachment());
             await context.PostAsync(reply);*/

        }
    }
}