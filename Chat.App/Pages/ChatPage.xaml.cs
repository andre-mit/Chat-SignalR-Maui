using Chat.App.ViewModels;

namespace Chat.App.Pages;

public partial class ChatPage : ContentPage
{
    private ChatViewModel _viewModel => BindingContext as ChatViewModel;
    public ChatPage(ChatViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;

        _viewModel.Messages.CollectionChanged += (sender, e) =>
        {
            var target = _viewModel.Messages.LastOrDefault();
            if (target != null)
            {
                var index = _viewModel.Messages.IndexOf(target);
                collectionMessages.ScrollTo(index, position: ScrollToPosition.End);
            }
        };
    }
}