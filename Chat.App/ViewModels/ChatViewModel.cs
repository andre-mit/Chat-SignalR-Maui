using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.App.ViewModels;

public class ChatViewModel : BindableObject
{
    private readonly HubConnection _hub;

    private bool _isBusy;

    public bool IsBusy
    {
        get => _isBusy;
        set
        {
            _isBusy = value;
            OnPropertyChanged();
        }
    }

    private string text;

    public string Text
    {
        get => text;
        set
        {
            text = value;
            OnPropertyChanged();
        }
    }


    public ObservableCollection<Message> Messages { get; }

    public string State => _hub?.State.ToString();

    public Guid Hash { get; }

    public Command SendMessageCommand { get; set; }


    public ChatViewModel(HubConnection hub)
    {
        Messages = new ObservableCollection<Message>();

        _hub = hub;

        Hash = Guid.NewGuid();

        _hub.StartAsync().ContinueWith(ac =>
        {
            OnPropertyChanged(nameof(State));
            _hub.On<Message>("ReceiveMessage", ReceiveMessage);
        });

        SendMessageCommand = new Command(SendMessage);
    }

    public async void SendMessage()
    {
        if (_hub.State != HubConnectionState.Connected || string.IsNullOrWhiteSpace(Text))
            return;

        var message = new Message { Id = Hash, Text = Text };

        await _hub.InvokeAsync("SendMessage", message);

        Messages.Add(message);
        Text = string.Empty;
    }

    public void ReceiveMessage(Message message)
    {
        Messages.Add(message);
    }
}

public class Message
{
    public Guid Id { get; set; }
    public string Text { get; set; }
}
