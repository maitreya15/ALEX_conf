using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using AzureSearchBot.Services;
using AzureSearchBot.Model;
using System.Diagnostics;
using System.Collections.Generic;

namespace AzureSearchBot.Dialogs
{
    [Serializable]
    public class RoomSearchDialog : IDialog<object>
    {
        private readonly AzureSearchService searchService = new AzureSearchService();
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Which room you want?:");
            context.Wait(MessageRecievedAsync);
        }

        public virtual async Task MessageRecievedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            try
            {
                SearchResult searchResult = await searchService.SearchByRoom(message.Text);
                if (searchResult.value.Length != 0)
                {
                    HeroCard RoomCard = new HeroCard();

                    RoomCard.Title = searchResult.value[0].Id;
                    //RoomCard.Subtitle = searchResult.value[0].Type;

                    Activity reply = ((Activity)message).CreateReply();

                    reply.Text = "Rooms that matches the Search Criteria ";
                    reply.Attachments = new List<Attachment>();
                    reply.Attachments.Add(RoomCard.ToAttachment());

                    await context.PostAsync(reply);
                }
                else
                {
                    await context.PostAsync("No such room found");
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine($"Error when searching for ROOM: {e.Message}");
            }
            context.Done<object>(null);
        }
    }
}